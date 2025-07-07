using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Horizon.PackageEditors.Avatar_Color_Editor
{
    public partial class AvatarColorEditor : EditorControl
    {
        public AvatarColorEditor()
        {
            InitializeComponent();
            TitleID = "FFFE07D1";
        }

        public override bool Entry()
        {
            if (readGPD() && loadTitleSetting(XboxDataBaseFile.XProfileIds.XPROFILE_GAMERCARD_AVATAR_INFO_1, System.IO.EndianType.BigEndian))
            {
                IO.Stream.Position = 0xFC;
                cpSkin.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpHair.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpLip.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpEye.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpEyeBrow.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpEyeShadow.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpFaceHair.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpFacePaint.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                cpFacePaint2.SelectedColor = Color.FromArgb(IO.In.ReadInt32());
                return true;
            }
            Functions.UI.messageBox("No avatar colors found in the selected profile.", "No Avatar Colors", MessageBoxIcon.Error);
            return false;
        }

        public override void Save()
        {
            IO.Stream.Position = 0xFC;
            IO.Out.Write(cpSkin.SelectedColor.ToArgb());
            IO.Out.Write(cpHair.SelectedColor.ToArgb());
            IO.Out.Write(cpLip.SelectedColor.ToArgb());
            IO.Out.Write(cpEye.SelectedColor.ToArgb());
            IO.Out.Write(cpEyeBrow.SelectedColor.ToArgb());
            IO.Out.Write(cpEyeShadow.SelectedColor.ToArgb());
            IO.Out.Write(cpFaceHair.SelectedColor.ToArgb());
            IO.Out.Write(cpFacePaint.SelectedColor.ToArgb());
            IO.Out.Write(cpFacePaint2.SelectedColor.ToArgb());
            writeTitleSetting(XboxDataBaseFile.XProfileIds.XPROFILE_GAMERCARD_AVATAR_INFO_1, IO.ToArray());
        }
    }
}
