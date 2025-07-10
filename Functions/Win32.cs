using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.IO;

namespace Horizon.Functions
{
    public class Win32
    {
        public const int SECURITY_MAX_SID_SIZE = 68;
        public const int SDDL_REVISION_1 = 1;
        public const uint INVALID_HANDLE_VALUE = 0xffffffff;
        public const int PAGE_READWRITE = 0x04;
        public const int FILE_MAP_WRITE = 0X02;

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateBoundaryDescriptor
        (
        [In] string Name,
        [In] int Flags
        );

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool CreateWellKnownSid
        (
        [In] WellKnownSidType WellKnownSidType,
        [In] [Optional] IntPtr DomainSid,
        [In] IntPtr pSid,
        [In][Out]ref int cbSid
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AddSIDToBoundaryDescriptor
        (
        [In][Out] ref IntPtr BoundaryDescriptor,
        [In] IntPtr RequiredSid
        );

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor
        (
        [In] string StringSecurityDescriptor,
        [In] int StringSDRevision,
        [Out] out IntPtr SecurityDescriptor,
        [Out] IntPtr SecurityDescriptorSize
        );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree([In] IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreatePrivateNamespace(
        [In][Optional] ref SECURITY_ATTRIBUTES lpPrivateNamespaceAttributes,
        [In] IntPtr lpBoundaryDescriptor,
        [In] string lpAliasPrefix
        );

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateFileMapping(
        [In] uint hFile,
        [In][Optional] ref SECURITY_ATTRIBUTES lpAttributes,
        [In] int flProtect,
        [In] int dwMaximumSizeHigh,
        [In] int dwMaximumSizeLow,
        [In][Optional] string lpName
        );

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(
        [In] IntPtr hFileMappingObject,
        [In] int dwDesiredAccess,
        [In] int dwFileOffsetHigh,
        [In] int dwFileOffsetLow,
        [In] int dwNumberOfBytesToMap
        );

        [DllImport("msvcrt.dll", EntryPoint = "memcpy", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr MemCopy(IntPtr dest, IntPtr src, uint count);

        [DllImport("msvcrt.dll", EntryPoint = "memset", CallingConvention = CallingConvention.Cdecl, SetLastError = false)]
        public static extern IntPtr MemSet(IntPtr dest, int value, uint count);

        public static IntPtr CreateMemoryMappedFile(string FileName)
        {
            bool bResult = false;
            IntPtr hBoundary = IntPtr.Zero;
            IntPtr pSid = IntPtr.Zero;
            int cbSid = Win32.SECURITY_MAX_SID_SIZE;
            IntPtr hNamespace = IntPtr.Zero;
            Win32.SECURITY_ATTRIBUTES securityAttributes = new Win32.SECURITY_ATTRIBUTES();
            IntPtr hFile = IntPtr.Zero;
            IntPtr pView = IntPtr.Zero;
            IntPtr pData = IntPtr.Zero;

            try
            {
                // Create boundary 
                hBoundary = Win32.CreateBoundaryDescriptor(
                "AlejacmaBoundaryDescriptor",
                0
                );
                if (hBoundary == IntPtr.Zero) { throw new Exception("CreateBoundaryDescriptor", new Win32Exception(Marshal.GetLastWin32Error())); }

                pSid = Marshal.AllocHGlobal(cbSid);
                bResult = Win32.CreateWellKnownSid(
                WellKnownSidType.BuiltinAdministratorsSid,
                IntPtr.Zero,
                pSid,
                ref cbSid
                );
                if (!bResult) { throw new Exception("CreateWellKnownSid", new Win32Exception(Marshal.GetLastWin32Error())); }

                bResult = Win32.AddSIDToBoundaryDescriptor(
                ref hBoundary,
                pSid
                );
                if (!bResult) { throw new Exception("AddSIDToBoundaryDescriptor", new Win32Exception(Marshal.GetLastWin32Error())); }

                // Create namespace and give access to some groups of users:
                // - Remote Desktop Users (needed for remote users to access the mapped file)
                // - Administrators (needed to create the mapped file within this program) 
                // - Interactive users (needed for local users to access mapped file)
                bResult = Win32.ConvertStringSecurityDescriptorToSecurityDescriptor(
                "D:(A;;GA;;;RD)(A;;GA;;;S-1-5-32-544)(A;;GA;;;S-1-5-4)",
                Win32.SDDL_REVISION_1,
                out securityAttributes.lpSecurityDescriptor,
                IntPtr.Zero
                );
                if (!bResult) { throw new Exception("ConvertStringSecurityDescriptorToSecurityDescriptor", new Win32Exception(Marshal.GetLastWin32Error())); }

                string NameSpace = FileName.Split("\\")[0];
                securityAttributes.nLength = Marshal.SizeOf(securityAttributes);
                securityAttributes.bInheritHandle = false;
                hNamespace = Win32.CreatePrivateNamespace(
                ref securityAttributes,
                hBoundary,
                NameSpace
                );
                if (hNamespace == IntPtr.Zero) { throw new Exception("CreatePrivateNamespace", new Win32Exception(Marshal.GetLastWin32Error())); }

                // Create file mapping, and give access to all users with access to the namespace
                hFile = Win32.CreateFileMapping(
                Win32.INVALID_HANDLE_VALUE,
                ref securityAttributes,
                Win32.PAGE_READWRITE,
                0,
                20971520,
                FileName
                );
                if (hFile == IntPtr.Zero) { throw new Exception("CreateFileMapping", new Win32Exception(Marshal.GetLastWin32Error())); }

                /*
                // Map file and write something to it
                pView = Win32.MapViewOfFile(
                hFile,
                Win32.FILE_MAP_WRITE,
                0,
                0,
                11
                );
                if (pView == IntPtr.Zero) { throw new Exception("MapViewOfFile", new Win32Exception(Marshal.GetLastWin32Error())); }

                pData = Marshal.StringToHGlobalAnsi("Hello World");
                Win32.MemCopy(pView, pData, 11);
                */
            }
            catch (Exception ex)
            {
                // Any error?
                MessageBox.Show(ex.Message + " failed with '" + ex.InnerException.Message + "' error");
            }
            finally
            {
                // Clean up memory
                if (pSid != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(pSid);
                }

                if (securityAttributes.lpSecurityDescriptor != IntPtr.Zero)
                {
                    Win32.LocalFree(securityAttributes.lpSecurityDescriptor);
                }
            }
            return hFile;
        }
    }
}