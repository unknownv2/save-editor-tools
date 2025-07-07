namespace Horizon.PackageEditors.NPlus
{
    partial class NPlus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboEpisode = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblEpisode = new DevComponents.DotNetBar.LabelX();
            this.panelFlags = new DevComponents.DotNetBar.PanelEx();
            this.ckMultiplayerUnlocked = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckSoloUnlocked = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckMultiplayer = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckSolo = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.fMultiplayer = new System.Windows.Forms.NumericUpDown();
            this.fSolo = new System.Windows.Forms.NumericUpDown();
            this.lblMultiplayer = new DevComponents.DotNetBar.LabelX();
            this.lblSolo = new DevComponents.DotNetBar.LabelX();
            this.cmdComplete = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelFlags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fMultiplayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fSolo)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(380, 141);
            this.rbPackageEditor.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.rbPackageEditor.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.rbPackageEditor.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.rbPackageEditor.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.rbPackageEditor.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.rbPackageEditor.SystemText.QatDialogAddButton = "&Add >>";
            this.rbPackageEditor.SystemText.QatDialogCancelButton = "Cancel";
            this.rbPackageEditor.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.rbPackageEditor.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.rbPackageEditor.SystemText.QatDialogOkButton = "OK";
            this.rbPackageEditor.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.rbPackageEditor.SystemText.QatDialogRemoveButton = "&Remove";
            this.rbPackageEditor.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.rbPackageEditor.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.rbPackageEditor.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.cmdComplete);
            this.panelMain.Controls.Add(this.panelFlags);
            this.panelMain.Controls.Add(this.lblEpisode);
            this.panelMain.Controls.Add(this.comboEpisode);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(380, 86);
            // 
            // 
            // 
            this.panelMain.Style.Class = "";
            this.panelMain.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelMain.StyleMouseDown.Class = "";
            this.panelMain.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelMain.StyleMouseOver.Class = "";
            this.panelMain.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.panelMain.Visible = true;
            // 
            // tabMain
            // 
            this.tabMain.Text = "Episodes";
            // 
            // comboEpisode
            // 
            this.comboEpisode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboEpisode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboEpisode.FocusCuesEnabled = false;
            this.comboEpisode.FormattingEnabled = true;
            this.comboEpisode.ItemHeight = 14;
            this.comboEpisode.Location = new System.Drawing.Point(3, 31);
            this.comboEpisode.Name = "comboEpisode";
            this.comboEpisode.Size = new System.Drawing.Size(72, 20);
            this.comboEpisode.Sorted = true;
            this.comboEpisode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboEpisode.TabIndex = 0;
            this.comboEpisode.SelectedIndexChanged += new System.EventHandler(this.comboEpisode_SelectedIndexChanged);
            // 
            // lblEpisode
            // 
            this.lblEpisode.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblEpisode.BackgroundStyle.Class = "";
            this.lblEpisode.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblEpisode.Location = new System.Drawing.Point(6, 6);
            this.lblEpisode.Name = "lblEpisode";
            this.lblEpisode.Size = new System.Drawing.Size(52, 20);
            this.lblEpisode.TabIndex = 1;
            this.lblEpisode.Text = "Episode:";
            // 
            // panelFlags
            // 
            this.panelFlags.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelFlags.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelFlags.Controls.Add(this.ckMultiplayerUnlocked);
            this.panelFlags.Controls.Add(this.ckSoloUnlocked);
            this.panelFlags.Controls.Add(this.ckMultiplayer);
            this.panelFlags.Controls.Add(this.ckSolo);
            this.panelFlags.Controls.Add(this.fMultiplayer);
            this.panelFlags.Controls.Add(this.fSolo);
            this.panelFlags.Controls.Add(this.lblMultiplayer);
            this.panelFlags.Controls.Add(this.lblSolo);
            this.panelFlags.Location = new System.Drawing.Point(78, 3);
            this.panelFlags.Name = "panelFlags";
            this.panelFlags.Size = new System.Drawing.Size(298, 48);
            this.panelFlags.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelFlags.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelFlags.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelFlags.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelFlags.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelFlags.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelFlags.Style.GradientAngle = 90;
            this.panelFlags.TabIndex = 2;
            // 
            // ckMultiplayerUnlocked
            // 
            // 
            // 
            // 
            this.ckMultiplayerUnlocked.BackgroundStyle.Class = "";
            this.ckMultiplayerUnlocked.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckMultiplayerUnlocked.FocusCuesEnabled = false;
            this.ckMultiplayerUnlocked.Location = new System.Drawing.Point(67, 25);
            this.ckMultiplayerUnlocked.Name = "ckMultiplayerUnlocked";
            this.ckMultiplayerUnlocked.Size = new System.Drawing.Size(70, 20);
            this.ckMultiplayerUnlocked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckMultiplayerUnlocked.TabIndex = 7;
            this.ckMultiplayerUnlocked.Text = "Unlocked";
            this.ckMultiplayerUnlocked.CheckedChanged += new System.EventHandler(this.ckMultiplayerUnlocked_CheckedChanged);
            // 
            // ckSoloUnlocked
            // 
            // 
            // 
            // 
            this.ckSoloUnlocked.BackgroundStyle.Class = "";
            this.ckSoloUnlocked.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckSoloUnlocked.FocusCuesEnabled = false;
            this.ckSoloUnlocked.Location = new System.Drawing.Point(67, 3);
            this.ckSoloUnlocked.Name = "ckSoloUnlocked";
            this.ckSoloUnlocked.Size = new System.Drawing.Size(70, 20);
            this.ckSoloUnlocked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckSoloUnlocked.TabIndex = 6;
            this.ckSoloUnlocked.Text = "Unlocked";
            this.ckSoloUnlocked.CheckedChanged += new System.EventHandler(this.ckSoloUnlocked_CheckedChanged);
            // 
            // ckMultiplayer
            // 
            // 
            // 
            // 
            this.ckMultiplayer.BackgroundStyle.Class = "";
            this.ckMultiplayer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckMultiplayer.FocusCuesEnabled = false;
            this.ckMultiplayer.Location = new System.Drawing.Point(143, 25);
            this.ckMultiplayer.Name = "ckMultiplayer";
            this.ckMultiplayer.Size = new System.Drawing.Size(78, 20);
            this.ckMultiplayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckMultiplayer.TabIndex = 5;
            this.ckMultiplayer.Text = "Completed";
            this.ckMultiplayer.CheckedChanged += new System.EventHandler(this.ckMultiplayer_CheckedChanged);
            // 
            // ckSolo
            // 
            // 
            // 
            // 
            this.ckSolo.BackgroundStyle.Class = "";
            this.ckSolo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckSolo.FocusCuesEnabled = false;
            this.ckSolo.Location = new System.Drawing.Point(143, 3);
            this.ckSolo.Name = "ckSolo";
            this.ckSolo.Size = new System.Drawing.Size(78, 20);
            this.ckSolo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckSolo.TabIndex = 4;
            this.ckSolo.Text = "Completed";
            this.ckSolo.CheckedChanged += new System.EventHandler(this.ckSolo_CheckedChanged);
            // 
            // fMultiplayer
            // 
            this.fMultiplayer.DecimalPlaces = 2;
            this.fMultiplayer.Location = new System.Drawing.Point(227, 25);
            this.fMultiplayer.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.fMultiplayer.Name = "fMultiplayer";
            this.fMultiplayer.Size = new System.Drawing.Size(68, 20);
            this.fMultiplayer.TabIndex = 3;
            this.fMultiplayer.ValueChanged += new System.EventHandler(this.fMultiplayer_ValueChanged);
            // 
            // fSolo
            // 
            this.fSolo.DecimalPlaces = 2;
            this.fSolo.Location = new System.Drawing.Point(227, 3);
            this.fSolo.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.fSolo.Name = "fSolo";
            this.fSolo.Size = new System.Drawing.Size(68, 20);
            this.fSolo.TabIndex = 2;
            this.fSolo.ValueChanged += new System.EventHandler(this.fSolo_ValueChanged);
            // 
            // lblMultiplayer
            // 
            // 
            // 
            // 
            this.lblMultiplayer.BackgroundStyle.Class = "";
            this.lblMultiplayer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMultiplayer.Location = new System.Drawing.Point(3, 24);
            this.lblMultiplayer.Name = "lblMultiplayer";
            this.lblMultiplayer.Size = new System.Drawing.Size(62, 20);
            this.lblMultiplayer.TabIndex = 1;
            this.lblMultiplayer.Text = "Multiplayer:";
            // 
            // lblSolo
            // 
            // 
            // 
            // 
            this.lblSolo.BackgroundStyle.Class = "";
            this.lblSolo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSolo.Location = new System.Drawing.Point(3, 3);
            this.lblSolo.Name = "lblSolo";
            this.lblSolo.Size = new System.Drawing.Size(62, 17);
            this.lblSolo.TabIndex = 0;
            this.lblSolo.Text = "Solo:";
            // 
            // cmdComplete
            // 
            this.cmdComplete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdComplete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdComplete.FocusCuesEnabled = false;
            this.cmdComplete.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdComplete.Location = new System.Drawing.Point(3, 54);
            this.cmdComplete.Name = "cmdComplete";
            this.cmdComplete.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdComplete.Size = new System.Drawing.Size(373, 28);
            this.cmdComplete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdComplete.TabIndex = 3;
            this.cmdComplete.Text = "Complete All Episodes";
            this.cmdComplete.Click += new System.EventHandler(this.cmdComplete_Click);
            // 
            // NPlus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 144);
            this.Name = "NPlus";
            this.Text = "N+ Progress Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelFlags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fMultiplayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fSolo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblEpisode;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboEpisode;
        private DevComponents.DotNetBar.PanelEx panelFlags;
        private DevComponents.DotNetBar.LabelX lblSolo;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckMultiplayer;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckSolo;
        private System.Windows.Forms.NumericUpDown fMultiplayer;
        private System.Windows.Forms.NumericUpDown fSolo;
        private DevComponents.DotNetBar.LabelX lblMultiplayer;
        private DevComponents.DotNetBar.ButtonX cmdComplete;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckMultiplayerUnlocked;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckSoloUnlocked;
    }
}
