namespace Horizon.Forms
{
    partial class GamercardViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GamercardViewer));
            this.rbGamercardViewer = new DevComponents.DotNetBar.RibbonControl();
            this.rpSearch = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdSearch = new DevComponents.DotNetBar.ButtonX();
            this.txtGamertag = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmdGamertag = new DevComponents.DotNetBar.Office2007StartButton();
            this.tabSearch = new DevComponents.DotNetBar.RibbonTabItem();
            this.pbAvatar = new System.Windows.Forms.PictureBox();
            this.pbGamerpic = new System.Windows.Forms.PictureBox();
            this.pbAvatarSmall = new System.Windows.Forms.PictureBox();
            this.gpAvatar = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panelGamerpics = new DevComponents.DotNetBar.PanelEx();
            this.cmdExtractAllImages = new DevComponents.DotNetBar.ButtonX();
            this.wbGamercard = new System.Windows.Forms.WebBrowser();
            this.rbGamercardViewer.SuspendLayout();
            this.rpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGamerpic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarSmall)).BeginInit();
            this.gpAvatar.SuspendLayout();
            this.panelGamerpics.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbGamercardViewer
            // 
            this.rbGamercardViewer.AutoExpand = false;
            // 
            // 
            // 
            this.rbGamercardViewer.BackgroundStyle.Class = "";
            this.rbGamercardViewer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbGamercardViewer.CanCustomize = false;
            this.rbGamercardViewer.CaptionVisible = true;
            this.rbGamercardViewer.Controls.Add(this.rpSearch);
            this.rbGamercardViewer.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbGamercardViewer.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdGamertag,
            this.tabSearch});
            this.rbGamercardViewer.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.rbGamercardViewer.Location = new System.Drawing.Point(5, 1);
            this.rbGamercardViewer.Name = "rbGamercardViewer";
            this.rbGamercardViewer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.rbGamercardViewer.Size = new System.Drawing.Size(375, 83);
            this.rbGamercardViewer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rbGamercardViewer.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.rbGamercardViewer.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.rbGamercardViewer.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.rbGamercardViewer.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.rbGamercardViewer.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.rbGamercardViewer.SystemText.QatDialogAddButton = "&Add >>";
            this.rbGamercardViewer.SystemText.QatDialogCancelButton = "Cancel";
            this.rbGamercardViewer.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.rbGamercardViewer.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.rbGamercardViewer.SystemText.QatDialogOkButton = "OK";
            this.rbGamercardViewer.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.rbGamercardViewer.SystemText.QatDialogRemoveButton = "&Remove";
            this.rbGamercardViewer.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.rbGamercardViewer.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.rbGamercardViewer.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.rbGamercardViewer.TabGroupHeight = 14;
            this.rbGamercardViewer.TabIndex = 0;
            // 
            // rpSearch
            // 
            this.rpSearch.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rpSearch.Controls.Add(this.cmdSearch);
            this.rpSearch.Controls.Add(this.txtGamertag);
            this.rpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpSearch.Location = new System.Drawing.Point(0, 53);
            this.rpSearch.Name = "rpSearch";
            this.rpSearch.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.rpSearch.Size = new System.Drawing.Size(375, 28);
            // 
            // 
            // 
            this.rpSearch.Style.Class = "";
            this.rpSearch.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.rpSearch.StyleMouseDown.Class = "";
            this.rpSearch.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.rpSearch.StyleMouseOver.Class = "";
            this.rpSearch.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rpSearch.TabIndex = 1;
            // 
            // cmdSearch
            // 
            this.cmdSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSearch.FocusCuesEnabled = false;
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = global::Horizon.Properties.Resources.Magnifier;
            this.cmdSearch.Location = new System.Drawing.Point(212, 3);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSearch.Size = new System.Drawing.Size(158, 20);
            this.cmdSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtGamertag
            // 
            // 
            // 
            // 
            this.txtGamertag.Border.Class = "TextBoxBorder";
            this.txtGamertag.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGamertag.Location = new System.Drawing.Point(6, 3);
            this.txtGamertag.Name = "txtGamertag";
            this.txtGamertag.Size = new System.Drawing.Size(200, 20);
            this.txtGamertag.TabIndex = 0;
            this.txtGamertag.WatermarkText = "Gamertag...";
            this.txtGamertag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGamertag_KeyPress);
            // 
            // cmdGamertag
            // 
            this.cmdGamertag.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.cmdGamertag.CanCustomize = false;
            this.cmdGamertag.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.cmdGamertag.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdGamertag.Image = ((System.Drawing.Image)(resources.GetObject("cmdGamertag.Image")));
            this.cmdGamertag.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdGamertag.ImagePaddingHorizontal = 0;
            this.cmdGamertag.ImagePaddingVertical = 0;
            this.cmdGamertag.Name = "cmdGamertag";
            this.cmdGamertag.ShowSubItems = false;
            this.cmdGamertag.Text = "Cheater912";
            this.cmdGamertag.Click += new System.EventHandler(this.cmdGamertag_Click);
            // 
            // tabSearch
            // 
            this.tabSearch.Checked = true;
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Panel = this.rpSearch;
            this.tabSearch.Text = "Seach for a Gamertag";
            // 
            // pbAvatar
            // 
            this.pbAvatar.BackColor = System.Drawing.Color.Transparent;
            this.pbAvatar.ErrorImage = global::Horizon.Properties.Resources.Refresh;
            this.pbAvatar.ImageLocation = "http://avatar.xboxlive.com/avatar/Cheater912/avatar-body.png";
            this.pbAvatar.InitialImage = global::Horizon.Properties.Resources.Refresh;
            this.pbAvatar.Location = new System.Drawing.Point(3, -7);
            this.pbAvatar.Name = "pbAvatar";
            this.pbAvatar.Size = new System.Drawing.Size(150, 296);
            this.pbAvatar.TabIndex = 1;
            this.pbAvatar.TabStop = false;
            // 
            // pbGamerpic
            // 
            this.pbGamerpic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGamerpic.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pbGamerpic.ErrorImage")));
            this.pbGamerpic.ImageLocation = "http://image.xboxlive.com/global/t.4b4b07d1/tile/0/2810d";
            this.pbGamerpic.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbGamerpic.InitialImage")));
            this.pbGamerpic.Location = new System.Drawing.Point(25, 3);
            this.pbGamerpic.Name = "pbGamerpic";
            this.pbGamerpic.Size = new System.Drawing.Size(64, 64);
            this.pbGamerpic.TabIndex = 3;
            this.pbGamerpic.TabStop = false;
            // 
            // pbAvatarSmall
            // 
            this.pbAvatarSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAvatarSmall.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pbAvatarSmall.ErrorImage")));
            this.pbAvatarSmall.ImageLocation = "http://avatar.xboxlive.com/avatar/ProfessorOak/avatarpic-l.png";
            this.pbAvatarSmall.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbAvatarSmall.InitialImage")));
            this.pbAvatarSmall.Location = new System.Drawing.Point(111, 4);
            this.pbAvatarSmall.Name = "pbAvatarSmall";
            this.pbAvatarSmall.Size = new System.Drawing.Size(64, 64);
            this.pbAvatarSmall.TabIndex = 4;
            this.pbAvatarSmall.TabStop = false;
            // 
            // gpAvatar
            // 
            this.gpAvatar.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpAvatar.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpAvatar.Controls.Add(this.pbAvatar);
            this.gpAvatar.Location = new System.Drawing.Point(217, 90);
            this.gpAvatar.Name = "gpAvatar";
            this.gpAvatar.Size = new System.Drawing.Size(158, 294);
            // 
            // 
            // 
            this.gpAvatar.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpAvatar.Style.BackColorGradientAngle = 90;
            this.gpAvatar.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpAvatar.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAvatar.Style.BorderBottomWidth = 1;
            this.gpAvatar.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpAvatar.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAvatar.Style.BorderLeftWidth = 1;
            this.gpAvatar.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAvatar.Style.BorderRightWidth = 1;
            this.gpAvatar.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAvatar.Style.BorderTopWidth = 1;
            this.gpAvatar.Style.Class = "";
            this.gpAvatar.Style.CornerDiameter = 4;
            this.gpAvatar.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpAvatar.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpAvatar.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpAvatar.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpAvatar.StyleMouseDown.Class = "";
            this.gpAvatar.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpAvatar.StyleMouseOver.Class = "";
            this.gpAvatar.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpAvatar.TabIndex = 6;
            // 
            // panelGamerpics
            // 
            this.panelGamerpics.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelGamerpics.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelGamerpics.Controls.Add(this.pbAvatarSmall);
            this.panelGamerpics.Controls.Add(this.pbGamerpic);
            this.panelGamerpics.Location = new System.Drawing.Point(11, 231);
            this.panelGamerpics.Name = "panelGamerpics";
            this.panelGamerpics.Size = new System.Drawing.Size(200, 71);
            this.panelGamerpics.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelGamerpics.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.panelGamerpics.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.panelGamerpics.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelGamerpics.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelGamerpics.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelGamerpics.Style.GradientAngle = 90;
            this.panelGamerpics.TabIndex = 7;
            // 
            // cmdExtractAllImages
            // 
            this.cmdExtractAllImages.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExtractAllImages.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExtractAllImages.FocusCuesEnabled = false;
            this.cmdExtractAllImages.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdExtractAllImages.Location = new System.Drawing.Point(11, 308);
            this.cmdExtractAllImages.Name = "cmdExtractAllImages";
            this.cmdExtractAllImages.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdExtractAllImages.Size = new System.Drawing.Size(203, 76);
            this.cmdExtractAllImages.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExtractAllImages.TabIndex = 11;
            this.cmdExtractAllImages.Text = "Save All Images";
            this.cmdExtractAllImages.Click += new System.EventHandler(this.cmdExtractAllImages_Click);
            // 
            // wbGamercard
            // 
            this.wbGamercard.AllowNavigation = false;
            this.wbGamercard.AllowWebBrowserDrop = false;
            this.wbGamercard.IsWebBrowserContextMenuEnabled = false;
            this.wbGamercard.Location = new System.Drawing.Point(11, 90);
            this.wbGamercard.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbGamercard.Name = "wbGamercard";
            this.wbGamercard.ScriptErrorsSuppressed = true;
            this.wbGamercard.ScrollBarsEnabled = false;
            this.wbGamercard.Size = new System.Drawing.Size(200, 135);
            this.wbGamercard.TabIndex = 12;
            this.wbGamercard.Url = new System.Uri("http://gamercard.xbox.com/en-US/Cheater912.card", System.UriKind.Absolute);
            this.wbGamercard.WebBrowserShortcutsEnabled = false;
            // 
            // GamercardViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.BottomLeftCornerSize = 0;
            this.BottomRightCornerSize = 0;
            this.ClientSize = new System.Drawing.Size(385, 394);
            this.Controls.Add(this.wbGamercard);
            this.Controls.Add(this.cmdExtractAllImages);
            this.Controls.Add(this.panelGamerpics);
            this.Controls.Add(this.gpAvatar);
            this.Controls.Add(this.rbGamercardViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(385, 394);
            this.MinimumSize = new System.Drawing.Size(385, 394);
            this.Name = "GamercardViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gamercard Viewer";
            this.rbGamercardViewer.ResumeLayout(false);
            this.rbGamercardViewer.PerformLayout();
            this.rpSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGamerpic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatarSmall)).EndInit();
            this.gpAvatar.ResumeLayout(false);
            this.panelGamerpics.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl rbGamercardViewer;
        private DevComponents.DotNetBar.RibbonPanel rpSearch;
        private DevComponents.DotNetBar.Office2007StartButton cmdGamertag;
        private DevComponents.DotNetBar.RibbonTabItem tabSearch;
        private DevComponents.DotNetBar.ButtonX cmdSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGamertag;
        private System.Windows.Forms.PictureBox pbAvatar;
        private System.Windows.Forms.PictureBox pbGamerpic;
        private System.Windows.Forms.PictureBox pbAvatarSmall;
        private DevComponents.DotNetBar.Controls.GroupPanel gpAvatar;
        private DevComponents.DotNetBar.PanelEx panelGamerpics;
        private DevComponents.DotNetBar.ButtonX cmdExtractAllImages;
        private System.Windows.Forms.WebBrowser wbGamercard;

    }
}