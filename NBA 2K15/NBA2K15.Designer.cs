namespace Horizon.PackageEditors.NBA_2K15
{
    partial class NBA2K15
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
            this.btnMaxSP = new DevComponents.DotNetBar.ButtonX();
            this.intSkillPoints = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnMaxFans = new DevComponents.DotNetBar.ButtonX();
            this.intFans = new DevComponents.Editors.IntegerInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intSkillPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intFans)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(276, 114);
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
            this.panelMain.Controls.Add(this.btnMaxFans);
            this.panelMain.Controls.Add(this.intFans);
            this.panelMain.Controls.Add(this.labelX2);
            this.panelMain.Controls.Add(this.btnMaxSP);
            this.panelMain.Controls.Add(this.intSkillPoints);
            this.panelMain.Controls.Add(this.labelX1);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(276, 59);
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
            // 
            // tabMain
            // 
            this.tabMain.Text = "Career Editor";
            // 
            // btnMaxSP
            // 
            this.btnMaxSP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxSP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxSP.FocusCuesEnabled = false;
            this.btnMaxSP.Image = global::Horizon.Properties.Resources.Plus;
            this.btnMaxSP.Location = new System.Drawing.Point(180, 7);
            this.btnMaxSP.Name = "btnMaxSP";
            this.btnMaxSP.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxSP.Size = new System.Drawing.Size(89, 20);
            this.btnMaxSP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxSP.TabIndex = 15;
            this.btnMaxSP.Text = "Max SP";
            this.btnMaxSP.Click += new System.EventHandler(this.BtnClickMaxSP);
            // 
            // intSkillPoints
            // 
            // 
            // 
            // 
            this.intSkillPoints.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSkillPoints.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSkillPoints.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSkillPoints.Location = new System.Drawing.Point(78, 7);
            this.intSkillPoints.MinValue = 0;
            this.intSkillPoints.Name = "intSkillPoints";
            this.intSkillPoints.ShowUpDown = true;
            this.intSkillPoints.Size = new System.Drawing.Size(92, 20);
            this.intSkillPoints.TabIndex = 14;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(58, 23);
            this.labelX1.TabIndex = 13;
            this.labelX1.Text = "Skill Points:";
            // 
            // btnMaxFans
            // 
            this.btnMaxFans.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxFans.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxFans.FocusCuesEnabled = false;
            this.btnMaxFans.Image = global::Horizon.Properties.Resources.Plus;
            this.btnMaxFans.Location = new System.Drawing.Point(180, 34);
            this.btnMaxFans.Name = "btnMaxFans";
            this.btnMaxFans.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxFans.Size = new System.Drawing.Size(89, 20);
            this.btnMaxFans.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxFans.TabIndex = 18;
            this.btnMaxFans.Text = "Max Fans";
            this.btnMaxFans.Click += new System.EventHandler(this.BtnClickMaxFans);
            // 
            // intFans
            // 
            // 
            // 
            // 
            this.intFans.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intFans.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intFans.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intFans.Location = new System.Drawing.Point(78, 34);
            this.intFans.MinValue = 0;
            this.intFans.Name = "intFans";
            this.intFans.ShowUpDown = true;
            this.intFans.Size = new System.Drawing.Size(92, 20);
            this.intFans.TabIndex = 17;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 31);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(58, 23);
            this.labelX2.TabIndex = 16;
            this.labelX2.Text = "Fans:";
            // 
            // NBA2K14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 117);
            this.Name = "NBA2K15";
            this.Text = "NBA 2K15";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intSkillPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intFans)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnMaxSP;
        private DevComponents.Editors.IntegerInput intSkillPoints;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnMaxFans;
        private DevComponents.Editors.IntegerInput intFans;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}
