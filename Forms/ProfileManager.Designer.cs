namespace Horizon
{
    partial class ProfileManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileManager));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.listProfiles = new DevComponents.AdvTree.AdvTree();
            this.colTreeEntry = new DevComponents.AdvTree.ColumnHeader();
            this.colTreeFavorite = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.panelAddProfile = new DevComponents.DotNetBar.PanelEx();
            this.cmdClearCache = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listProfiles)).BeginInit();
            this.panelAddProfile.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.listProfiles);
            this.panelEx1.Controls.Add(this.panelAddProfile);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(251, 232);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            this.panelEx1.Text = "panelEx1";
            // 
            // listProfiles
            // 
            this.listProfiles.AllowDrop = true;
            this.listProfiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listProfiles.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listProfiles.BackgroundStyle.Class = "TreeBorderKey";
            this.listProfiles.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listProfiles.Columns.Add(this.colTreeEntry);
            this.listProfiles.Columns.Add(this.colTreeFavorite);
            this.listProfiles.DragDropEnabled = false;
            this.listProfiles.DragDropNodeCopyEnabled = false;
            this.listProfiles.ExpandButtonType = DevComponents.AdvTree.eExpandButtonType.Triangle;
            this.listProfiles.ExpandWidth = 14;
            this.listProfiles.GridRowLines = true;
            this.listProfiles.HotTracking = true;
            this.listProfiles.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listProfiles.Location = new System.Drawing.Point(0, 0);
            this.listProfiles.MultiNodeDragDropAllowed = false;
            this.listProfiles.Name = "listProfiles";
            this.listProfiles.NodesConnector = this.nodeConnector1;
            this.listProfiles.NodeStyle = this.elementStyle1;
            this.listProfiles.PathSeparator = ";";
            this.listProfiles.Size = new System.Drawing.Size(251, 207);
            this.listProfiles.Styles.Add(this.elementStyle1);
            this.listProfiles.TabIndex = 4;
            this.listProfiles.TileSize = new System.Drawing.Size(225, 64);
            this.listProfiles.View = DevComponents.AdvTree.eView.Tile;
            // 
            // colTreeEntry
            // 
            this.colTreeEntry.MinimumWidth = 60;
            this.colTreeEntry.Name = "colTreeEntry";
            this.colTreeEntry.Text = "Entry";
            this.colTreeEntry.Width.AutoSize = true;
            this.colTreeEntry.Width.AutoSizeMinHeader = true;
            // 
            // colTreeFavorite
            // 
            this.colTreeFavorite.MinimumWidth = 60;
            this.colTreeFavorite.Name = "colTreeFavorite";
            this.colTreeFavorite.Text = "Favorite";
            this.colTreeFavorite.Width.AutoSize = true;
            this.colTreeFavorite.Width.AutoSizeMinHeader = true;
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
            // panelAddProfile
            // 
            this.panelAddProfile.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelAddProfile.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelAddProfile.Controls.Add(this.cmdClearCache);
            this.panelAddProfile.Location = new System.Drawing.Point(0, 206);
            this.panelAddProfile.Name = "panelAddProfile";
            this.panelAddProfile.Size = new System.Drawing.Size(251, 26);
            this.panelAddProfile.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelAddProfile.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelAddProfile.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelAddProfile.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelAddProfile.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelAddProfile.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelAddProfile.Style.GradientAngle = 90;
            this.panelAddProfile.TabIndex = 1;
            // 
            // cmdClearCache
            // 
            this.cmdClearCache.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdClearCache.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdClearCache.FocusCuesEnabled = false;
            this.cmdClearCache.Location = new System.Drawing.Point(3, 3);
            this.cmdClearCache.Name = "cmdClearCache";
            this.cmdClearCache.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdClearCache.Size = new System.Drawing.Size(245, 20);
            this.cmdClearCache.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdClearCache.TabIndex = 3;
            this.cmdClearCache.Text = "Clear Profile Cache";
            this.cmdClearCache.Click += new System.EventHandler(this.cmdClearCache_Click);
            // 
            // ProfileManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 232);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileManager";
            this.Text = "Profile Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileManager_FormClosing);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listProfiles)).EndInit();
            this.panelAddProfile.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelAddProfile;
        internal DevComponents.AdvTree.ColumnHeader colTreeEntry;
        internal DevComponents.AdvTree.ColumnHeader colTreeFavorite;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.AdvTree listProfiles;
        private DevComponents.DotNetBar.ButtonX cmdClearCache;
    }
}