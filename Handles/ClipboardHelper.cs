using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Horizon
{
    internal static class ClipboardHelper
    {
        [DllImport("User32.dll")]
        internal static extern int SetClipboardViewer(int hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        internal static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        internal static bool Freeze = false;
        internal static void parseClipboardText(string currentClip)
        {
            if (Freeze)
                return;
            string[] baseParts = currentClip.Trim().ToLower().Split('.');
            if (baseParts.Length == 2 && baseParts[0] == "horizon")
            {
                string[] dataParts = baseParts[1].Split(':');
                if (dataParts.Length == 2)
                {
#if PNET
                    /*switch (dataParts[0])
                    {
                        
                    }*/
                    Freeze = true;
                    Clipboard.Clear();
                    Freeze = false;
#endif
                }
            }
        }
    }
}
