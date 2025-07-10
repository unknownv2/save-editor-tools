using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Horizon.Forms;

namespace Horizon.Functions
{
    public static class UI
    {
        public static DialogResult messageBox(string message, string title, MessageBoxIcon icon)
        {
            return messageBox(Main.mainForm, message, title, icon);
        }

        public static DialogResult messageBox(IWin32Window owner, string message, string title, MessageBoxIcon icon)
        {
            if (!Program.shuttingDown)
            {
                if (Main.mainForm == null || Main.mainForm.InvokeRequired)
                {
                    DialogResult dRes = DialogResult.Cancel;
                    if (Main.mainForm == null)
                        dRes = MessageBoxEx.Show(null, message, title, MessageBoxButtons.OK, icon);
                    else
                        Main.mainForm.Invoke((MethodInvoker)delegate
                        {
                            dRes = MessageBoxEx.Show(owner, message, title, MessageBoxButtons.OK, icon);
                        });
                    return dRes;
                }
                else
                {
                    if (Program.doneLoading || Program.glassEnabled)
                        return MessageBoxEx.Show(owner, message, title, MessageBoxButtons.OK, icon);
                    else
                        return MessageBox.Show(owner, message, title, MessageBoxButtons.OK, icon);
                }
            }
            return DialogResult.Cancel;
        }

        public static DialogResult messageBox(string message)
        {
            return messageBox(Main.mainForm, message);
        }

        public static DialogResult messageBox(IWin32Window owner, string message)
        {
            if (!Program.shuttingDown)
            {
                if (Main.mainForm == null || Main.mainForm.InvokeRequired)
                {
                    DialogResult dRes = DialogResult.Cancel;
                    if (Main.mainForm == null)
                        dRes = MessageBoxEx.Show(null, message, String.Empty);
                    else
                        Main.mainForm.Invoke((MethodInvoker)delegate
                        {
                            dRes = MessageBoxEx.Show(owner, message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                    return dRes;
                }
                else
                {
                    if (Program.doneLoading || Program.glassEnabled)
                        return MessageBoxEx.Show(owner, message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        return MessageBox.Show(owner, message, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return DialogResult.Cancel;
        }

        public static DialogResult messageBox(string message, string title, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            return messageBox(Main.mainForm, message, title, icon, buttons);
        }

        public static DialogResult messageBox(IWin32Window owner, string message, string title, MessageBoxIcon icon, MessageBoxButtons buttons)
        {
            if (!Program.shuttingDown)
            {
                if (Main.mainForm == null || Main.mainForm.InvokeRequired)
                {
                    DialogResult dRes = DialogResult.Cancel;
                    if (Main.mainForm == null)
                        dRes = MessageBoxEx.Show(null, message, title, buttons, icon);
                    else
                        Main.mainForm.Invoke((MethodInvoker)delegate
                        {
                            dRes = MessageBoxEx.Show(owner, message, title, buttons, icon);
                        });
                    return dRes;
                }
                else
                {
                    if (Program.doneLoading || Program.glassEnabled)
                        return MessageBoxEx.Show(owner, message, title, buttons, icon);
                    else
                        return MessageBox.Show(owner, message, title, buttons, icon);
                }
            }
            return DialogResult.Cancel;
        }

        public static DialogResult messageBox(string message, string title, MessageBoxIcon icon, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            return messageBox(Main.mainForm, message, title, icon, buttons, defaultButton);
        }

        public static DialogResult messageBox(IWin32Window owner, string message, string title, MessageBoxIcon icon, MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            if (!Program.shuttingDown)
            {
                if (Main.mainForm == null || Main.mainForm.InvokeRequired)
                {
                    DialogResult dRes = DialogResult.Cancel;
                    if (Main.mainForm == null)
                        dRes = MessageBoxEx.Show(null, message, title, buttons, icon, defaultButton);
                    else
                        Main.mainForm.Invoke((MethodInvoker)delegate
                        {
                            dRes = MessageBoxEx.Show(owner, message, title, buttons, icon, defaultButton);
                        });
                    return dRes;
                }
                else
                {
                    if (Program.doneLoading || Program.glassEnabled)
                        return MessageBoxEx.Show(owner, message, title, buttons, icon, defaultButton);
                    else
                        return MessageBox.Show(owner, message, title, buttons, icon, defaultButton);
                }
            }
            return DialogResult.Cancel;
        }

        public static DialogResult errorBox(string message)
        {
            return messageBox(message, "Error", MessageBoxIcon.Error);
        }

        public static DialogResult errorBox(IWin32Window owner, string message)
        {
            return messageBox(owner, message, "Error", MessageBoxIcon.Error);
        }

        public static void throwError(string message)
        {
            throw new Exception(message);
        }
    }
}
