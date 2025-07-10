namespace Horizon.PackageEditors.Injustice_Gods_Among_Us
{
    partial class InjusticeGAU
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
            this.btnMaxExp = new DevComponents.DotNetBar.ButtonX();
            this.intExperience = new DevComponents.Editors.IntegerInput();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.intAccessCards = new DevComponents.Editors.IntegerInput();
            this.btnMaxAccessCards = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.intArmoryKeys = new DevComponents.Editors.IntegerInput();
            this.btnMaxArmoryKeys = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnMaxTotalStars = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.intTotalStars = new DevComponents.Editors.IntegerInput();
            this.btnUnlockAll = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intExperience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAccessCards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intArmoryKeys)).BeginInit();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intTotalStars)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(262, 206);
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
            this.panelMain.Controls.Add(this.btnUnlockAll);
            this.panelMain.Controls.Add(this.panelEx1);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(262, 151);
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
            this.tabMain.Text = "Player";
            // 
            // btnMaxExp
            // 
            this.btnMaxExp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxExp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxExp.FocusCuesEnabled = false;
            this.btnMaxExp.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxExp.Location = new System.Drawing.Point(186, 8);
            this.btnMaxExp.Name = "btnMaxExp";
            this.btnMaxExp.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxExp.Size = new System.Drawing.Size(56, 21);
            this.btnMaxExp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxExp.TabIndex = 10;
            this.btnMaxExp.Text = "Max";
            this.btnMaxExp.Click += new System.EventHandler(this.BtnClickMaxValue);
            // 
            // intExperience
            // 
            // 
            // 
            // 
            this.intExperience.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intExperience.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intExperience.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intExperience.Location = new System.Drawing.Point(92, 9);
            this.intExperience.MinValue = 0;
            this.intExperience.Name = "intExperience";
            this.intExperience.ShowUpDown = true;
            this.intExperience.Size = new System.Drawing.Size(88, 20);
            this.intExperience.TabIndex = 8;
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(6, 8);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(80, 20);
            this.labelX8.TabIndex = 9;
            this.labelX8.Text = "Experience:";
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
            this.labelX1.Location = new System.Drawing.Point(6, 34);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(80, 20);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "Access Cards:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // intAccessCards
            // 
            // 
            // 
            // 
            this.intAccessCards.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intAccessCards.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intAccessCards.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intAccessCards.Location = new System.Drawing.Point(92, 35);
            this.intAccessCards.MinValue = 0;
            this.intAccessCards.Name = "intAccessCards";
            this.intAccessCards.ShowUpDown = true;
            this.intAccessCards.Size = new System.Drawing.Size(88, 20);
            this.intAccessCards.TabIndex = 8;
            // 
            // btnMaxAccessCards
            // 
            this.btnMaxAccessCards.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxAccessCards.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxAccessCards.FocusCuesEnabled = false;
            this.btnMaxAccessCards.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxAccessCards.Location = new System.Drawing.Point(186, 34);
            this.btnMaxAccessCards.Name = "btnMaxAccessCards";
            this.btnMaxAccessCards.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxAccessCards.Size = new System.Drawing.Size(56, 21);
            this.btnMaxAccessCards.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxAccessCards.TabIndex = 10;
            this.btnMaxAccessCards.Text = "Max";
            this.btnMaxAccessCards.Click += new System.EventHandler(this.BtnClickMaxValue);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 60);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(80, 20);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "Armory Keys:";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // intArmoryKeys
            // 
            // 
            // 
            // 
            this.intArmoryKeys.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intArmoryKeys.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intArmoryKeys.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intArmoryKeys.Location = new System.Drawing.Point(92, 61);
            this.intArmoryKeys.MinValue = 0;
            this.intArmoryKeys.Name = "intArmoryKeys";
            this.intArmoryKeys.ShowUpDown = true;
            this.intArmoryKeys.Size = new System.Drawing.Size(88, 20);
            this.intArmoryKeys.TabIndex = 8;
            // 
            // btnMaxArmoryKeys
            // 
            this.btnMaxArmoryKeys.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxArmoryKeys.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxArmoryKeys.FocusCuesEnabled = false;
            this.btnMaxArmoryKeys.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxArmoryKeys.Location = new System.Drawing.Point(186, 60);
            this.btnMaxArmoryKeys.Name = "btnMaxArmoryKeys";
            this.btnMaxArmoryKeys.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxArmoryKeys.Size = new System.Drawing.Size(56, 21);
            this.btnMaxArmoryKeys.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxArmoryKeys.TabIndex = 10;
            this.btnMaxArmoryKeys.Text = "Max";
            this.btnMaxArmoryKeys.Click += new System.EventHandler(this.BtnClickMaxValue);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnMaxTotalStars);
            this.panelEx1.Controls.Add(this.btnMaxArmoryKeys);
            this.panelEx1.Controls.Add(this.labelX8);
            this.panelEx1.Controls.Add(this.btnMaxAccessCards);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.btnMaxExp);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.intTotalStars);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.intArmoryKeys);
            this.panelEx1.Controls.Add(this.intExperience);
            this.panelEx1.Controls.Add(this.intAccessCards);
            this.panelEx1.Location = new System.Drawing.Point(6, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(250, 116);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 11;
            // 
            // btnMaxTotalStars
            // 
            this.btnMaxTotalStars.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxTotalStars.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxTotalStars.FocusCuesEnabled = false;
            this.btnMaxTotalStars.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxTotalStars.Location = new System.Drawing.Point(186, 86);
            this.btnMaxTotalStars.Name = "btnMaxTotalStars";
            this.btnMaxTotalStars.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxTotalStars.Size = new System.Drawing.Size(56, 21);
            this.btnMaxTotalStars.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxTotalStars.TabIndex = 10;
            this.btnMaxTotalStars.Text = "Max";
            this.btnMaxTotalStars.Click += new System.EventHandler(this.BtnClickMaxValue);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(6, 86);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(80, 20);
            this.labelX3.TabIndex = 9;
            this.labelX3.Text = "Total Stars:";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // intTotalStars
            // 
            // 
            // 
            // 
            this.intTotalStars.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intTotalStars.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intTotalStars.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intTotalStars.Location = new System.Drawing.Point(92, 87);
            this.intTotalStars.MaxValue = 350;
            this.intTotalStars.MinValue = 0;
            this.intTotalStars.Name = "intTotalStars";
            this.intTotalStars.ShowUpDown = true;
            this.intTotalStars.Size = new System.Drawing.Size(88, 20);
            this.intTotalStars.TabIndex = 8;
            // 
            // btnUnlockAll
            // 
            this.btnUnlockAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAll.FocusCuesEnabled = false;
            this.btnUnlockAll.Image = global::Horizon.Properties.Resources.Star;
            this.btnUnlockAll.Location = new System.Drawing.Point(6, 116);
            this.btnUnlockAll.Name = "btnUnlockAll";
            this.btnUnlockAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAll.Size = new System.Drawing.Size(250, 28);
            this.btnUnlockAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAll.TabIndex = 18;
            this.btnUnlockAll.Text = "Unlock All";
            this.btnUnlockAll.Click += new System.EventHandler(this.BtnClickUnlockAll);
            // 
            // InjusticeGAU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 209);
            this.Name = "InjusticeGAU";
            this.Text = "Injustice: Gods Among Us";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intExperience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAccessCards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intArmoryKeys)).EndInit();
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intTotalStars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnMaxArmoryKeys;
        private DevComponents.DotNetBar.ButtonX btnMaxAccessCards;
        private DevComponents.DotNetBar.ButtonX btnMaxExp;
        private DevComponents.Editors.IntegerInput intArmoryKeys;
        private DevComponents.Editors.IntegerInput intAccessCards;
        private DevComponents.Editors.IntegerInput intExperience;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnMaxTotalStars;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.IntegerInput intTotalStars;
        private DevComponents.DotNetBar.ButtonX btnUnlockAll;
    }
}
