using System;
using System.Runtime.InteropServices;

namespace Horizon
{
    internal enum TaskbarProgressState
    {
        NoProgress = 0,
        Indeterminate = 1,
        Normal = 2,
        Error = 4,
        Paused = 8
    }

    [ComImportAttribute()]
    [GuidAttribute("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
    [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ITaskbarList3
    {
        [PreserveSig]
        void HrInit();
        [PreserveSig]
        void AddTab(IntPtr hwnd);
        [PreserveSig]
        void DeleteTab(IntPtr hwnd);
        [PreserveSig]
        void ActivateTab(IntPtr hwnd);
        [PreserveSig]
        void SetActiveAlt(IntPtr hwnd);
        [PreserveSig]
        void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);
        void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
        void SetProgressState(IntPtr hwnd, TaskbarProgressState tbpFlags);
    }

    [GuidAttribute("56FDF344-FD6D-11d0-958A-006097C9A090")]
    [ClassInterfaceAttribute(ClassInterfaceType.None)]
    [ComImportAttribute()]
    internal class CTaskbarList { }

    internal static class TaskbarManager
    {
        private static ITaskbarList3 taskbarList;
        static TaskbarManager()
        {
            if (Program.isWindows7)
            {
                taskbarList = (ITaskbarList3)new CTaskbarList();
                taskbarList.HrInit();
            }
        }

        private static IntPtr windowHandle;
        internal static void setWindowHandle(IntPtr handle)
        {
            windowHandle = handle;
        }

        internal static void setProgressState(TaskbarProgressState state)
        {
            if (Program.isWindows7)
                taskbarList.SetProgressState(windowHandle, state);
        }

        internal static void setProgressValue(int value, int max)
        {
            if (Program.isWindows7)
                taskbarList.SetProgressValue(windowHandle, (ulong)value, (ulong)max);
        }
    }
}
