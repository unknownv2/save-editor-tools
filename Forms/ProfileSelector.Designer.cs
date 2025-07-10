namespace Horizon.Forms
{
    partial class ProfileSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileSelector));
            this.listProfiles = new DevComponents.AdvTree.AdvTree();
            this.colTreeProfile = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.cmdCancel = new DevComponents.DotNetBar.ButtonX();
            this.cmdSelectProfile = new DevComponents.DotNetBar.ButtonX();
            this.node2 = new DevComponents.AdvTree.Node();
            this.cmdSkip = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.listProfiles)).BeginInit();
            this.SuspendLayout();
            // 
            // listProfiles
            // 
            this.listProfiles.AllowDrop = true;
            this.listProfiles.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listProfiles.BackgroundStyle.Class = "TreeBorderKey";
            this.listProfiles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listProfiles.Columns.Add(this.colTreeProfile);
            this.listProfiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.listProfiles.DragDropNodeCopyEnabled = false;
            this.listProfiles.ExpandButtonType = DevComponents.AdvTree.eExpandButtonType.Triangle;
            this.listProfiles.ExpandWidth = 3;
            this.listProfiles.GridColumnLines = false;
            this.listProfiles.GridRowLines = true;
            this.listProfiles.HotTracking = true;
            this.listProfiles.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listProfiles.Location = new System.Drawing.Point(0, 0);
            this.listProfiles.Name = "listProfiles";
            this.listProfiles.NodesConnector = this.nodeConnector1;
            this.listProfiles.NodeStyle = this.elementStyle1;
            this.listProfiles.PathSeparator = ";";
            this.listProfiles.Size = new System.Drawing.Size(270, 237);
            this.listProfiles.Styles.Add(this.elementStyle1);
            this.listProfiles.TabIndex = 4;
            this.listProfiles.NodeDoubleClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.listProfiles_NodeDoubleClick);
            // 
            // colTreeProfile
            // 
            this.colTreeProfile.Name = "colTreeProfile";
            this.colTreeProfile.Text = "Profile";
            this.colTreeProfile.Width.Absolute = 240;
            this.colTreeProfile.Width.AutoSizeMinHeader = true;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlDark;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // cmdCancel
            // 
            this.cmdCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FocusCuesEnabled = false;
            this.cmdCancel.Location = new System.Drawing.Point(0, 236);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdCancel.Size = new System.Drawing.Size(60, 31);
            this.cmdCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSelectProfile
            // 
            this.cmdSelectProfile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSelectProfile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSelectProfile.FocusCuesEnabled = false;
            this.cmdSelectProfile.Image = global::Horizon.Properties.Resources.GreenDot;
            this.cmdSelectProfile.Location = new System.Drawing.Point(59, 236);
            this.cmdSelectProfile.Name = "cmdSelectProfile";
            this.cmdSelectProfile.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSelectProfile.Size = new System.Drawing.Size(112, 31);
            this.cmdSelectProfile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSelectProfile.TabIndex = 6;
            this.cmdSelectProfile.Text = "Use this Profile";
            this.cmdSelectProfile.Click += new System.EventHandler(this.cmdSelectProfile_Click);
            // 
            // node2
            // 
            this.node2.Image = global::Horizon.Properties.Resources.Unearned;
            this.node2.ImageAlignment = DevComponents.AdvTree.eCellPartAlignment.NearCenter;
            this.node2.Name = "node2";
            this.node2.Text = "Cheater912<br></br>\r\n<font color=\"#7D7974\">E0000382171711114</font>";
            // 
            // cmdSkip
            // 
            this.cmdSkip.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSkip.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSkip.FocusCuesEnabled = false;
            this.cmdSkip.Image = global::Horizon.Properties.Resources.GrayDot;
            this.cmdSkip.Location = new System.Drawing.Point(170, 236);
            this.cmdSkip.Name = "cmdSkip";
            this.cmdSkip.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSkip.Size = new System.Drawing.Size(100, 31);
            this.cmdSkip.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSkip.TabIndex = 7;
            this.cmdSkip.Text = "Skip this Step";
            this.cmdSkip.Click += new System.EventHandler(this.cmdSkip_Click);
            // 
            // ProfileSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BottomLeftCornerSize = 0;
            this.BottomRightCornerSize = 0;
            this.ClientSize = new System.Drawing.Size(270, 267);
            this.Controls.Add(this.cmdSkip);
            this.Controls.Add(this.cmdSelectProfile);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.listProfiles);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select a Profile for ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectProfile_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.listProfiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal DevComponents.AdvTree.AdvTree listProfiles;
        private DevComponents.AdvTree.ColumnHeader colTreeProfile;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.ButtonX cmdCancel;
        private DevComponents.DotNetBar.ButtonX cmdSelectProfile;
        private DevComponents.AdvTree.Node node2;
        private DevComponents.DotNetBar.ButtonX cmdSkip;

    }
}