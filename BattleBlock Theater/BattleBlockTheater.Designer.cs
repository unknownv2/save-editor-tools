namespace Horizon.PackageEditors.BattleBlock_Theater
{
    partial class BattleBlockTheater
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
            this.btnMaxGems = new DevComponents.DotNetBar.ButtonX();
            this.intGems = new DevComponents.Editors.IntegerInput();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.intYarn = new DevComponents.Editors.IntegerInput();
            this.btnMaxYarn = new DevComponents.DotNetBar.ButtonX();
            this.btnExtractProfileData = new DevComponents.DotNetBar.ButtonItem();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intGems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intYarn)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnExtractProfileData});
            this.rbPackageEditor.Size = new System.Drawing.Size(208, 117);
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
            this.panelMain.Controls.Add(this.btnMaxYarn);
            this.panelMain.Controls.Add(this.btnMaxGems);
            this.panelMain.Controls.Add(this.intYarn);
            this.panelMain.Controls.Add(this.intGems);
            this.panelMain.Controls.Add(this.labelX1);
            this.panelMain.Controls.Add(this.labelX8);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(208, 62);
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
            this.tabMain.Text = "Profile";
            // 
            // btnMaxGems
            // 
            this.btnMaxGems.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxGems.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxGems.FocusCuesEnabled = false;
            this.btnMaxGems.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxGems.Location = new System.Drawing.Point(143, 7);
            this.btnMaxGems.Name = "btnMaxGems";
            this.btnMaxGems.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxGems.Size = new System.Drawing.Size(56, 20);
            this.btnMaxGems.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxGems.TabIndex = 10;
            this.btnMaxGems.Text = "Max";
            this.btnMaxGems.Click += new System.EventHandler(this.BtnClickMaxGems);
            // 
            // intGems
            // 
            // 
            // 
            // 
            this.intGems.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intGems.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intGems.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intGems.Location = new System.Drawing.Point(49, 7);
            this.intGems.MinValue = 0;
            this.intGems.Name = "intGems";
            this.intGems.ShowUpDown = true;
            this.intGems.Size = new System.Drawing.Size(88, 20);
            this.intGems.TabIndex = 8;
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(7, 7);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(36, 20);
            this.labelX8.TabIndex = 9;
            this.labelX8.Text = "Gems:";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(7, 33);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(36, 20);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "Yarn:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // intYarn
            // 
            // 
            // 
            // 
            this.intYarn.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intYarn.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intYarn.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intYarn.Location = new System.Drawing.Point(49, 33);
            this.intYarn.MinValue = 0;
            this.intYarn.Name = "intYarn";
            this.intYarn.ShowUpDown = true;
            this.intYarn.Size = new System.Drawing.Size(88, 20);
            this.intYarn.TabIndex = 8;
            // 
            // btnMaxYarn
            // 
            this.btnMaxYarn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxYarn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxYarn.FocusCuesEnabled = false;
            this.btnMaxYarn.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxYarn.Location = new System.Drawing.Point(143, 33);
            this.btnMaxYarn.Name = "btnMaxYarn";
            this.btnMaxYarn.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxYarn.Size = new System.Drawing.Size(56, 20);
            this.btnMaxYarn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxYarn.TabIndex = 10;
            this.btnMaxYarn.Text = "Max";
            this.btnMaxYarn.Click += new System.EventHandler(this.BtnClickMaxYarn);
            // 
            // btnExtractProfileData
            // 
            this.btnExtractProfileData.Name = "btnExtractProfileData";
            this.btnExtractProfileData.Text = "Extract";
            this.btnExtractProfileData.Visible = false;
            this.btnExtractProfileData.Click += new System.EventHandler(this.BtnClickExtractProfileData);
            // 
            // BattleBlockTheater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 120);
            this.Name = "BattleBlockTheater";
            this.Text = "BattleBlock Theater";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intGems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intYarn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnMaxYarn;
        private DevComponents.DotNetBar.ButtonX btnMaxGems;
        private DevComponents.Editors.IntegerInput intYarn;
        private DevComponents.Editors.IntegerInput intGems;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.ButtonItem btnExtractProfileData;

    }
}
