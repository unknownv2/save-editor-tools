namespace Horizon.PackageEditors.Borderlands
{
    partial class Borderlands
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.intPlayerLevel = new DevComponents.Editors.IntegerInput();
            this.intPlayerSkillPoints = new DevComponents.Editors.IntegerInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.intPlayerXPPoints = new DevComponents.Editors.IntegerInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.intPlayerMoney = new DevComponents.Editors.IntegerInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.lblKeyName = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.intSettingValue = new DevComponents.Editors.IntegerInput();
            this.cmdSetValue = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.listValues = new DevComponents.AdvTree.AdvTree();
            this.colSetting = new DevComponents.AdvTree.ColumnHeader();
            this.colValue = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.cmdPlayerMoneyMax = new DevComponents.DotNetBar.ButtonX();
            this.cmdPlayerSkillPointsMax = new DevComponents.DotNetBar.ButtonX();
            this.cmdPlayerXPPointsMax = new DevComponents.DotNetBar.ButtonX();
            this.cmdMaxPlayerLevel = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerSkillPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerXPPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSettingValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(431, 387);
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
            this.panelMain.Controls.Add(this.panelEx2);
            this.panelMain.Controls.Add(this.panelEx1);
            this.panelMain.Controls.Add(this.listValues);
            this.panelMain.Location = new System.Drawing.Point(0, 56);
            this.panelMain.Size = new System.Drawing.Size(431, 329);
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
            this.tabMain.Text = "Stats and Specific Values";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(54, 20);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Level:";
            // 
            // intPlayerLevel
            // 
            // 
            // 
            // 
            this.intPlayerLevel.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intPlayerLevel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intPlayerLevel.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intPlayerLevel.Location = new System.Drawing.Point(66, 6);
            this.intPlayerLevel.MaxValue = 500;
            this.intPlayerLevel.MinValue = 0;
            this.intPlayerLevel.Name = "intPlayerLevel";
            this.intPlayerLevel.ShowUpDown = true;
            this.intPlayerLevel.Size = new System.Drawing.Size(71, 20);
            this.intPlayerLevel.TabIndex = 22;
            // 
            // intPlayerSkillPoints
            // 
            // 
            // 
            // 
            this.intPlayerSkillPoints.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intPlayerSkillPoints.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intPlayerSkillPoints.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intPlayerSkillPoints.Location = new System.Drawing.Point(276, 6);
            this.intPlayerSkillPoints.MaxValue = 500;
            this.intPlayerSkillPoints.MinValue = 0;
            this.intPlayerSkillPoints.Name = "intPlayerSkillPoints";
            this.intPlayerSkillPoints.ShowUpDown = true;
            this.intPlayerSkillPoints.Size = new System.Drawing.Size(79, 20);
            this.intPlayerSkillPoints.TabIndex = 24;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(204, 6);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(66, 20);
            this.labelX2.TabIndex = 23;
            this.labelX2.Text = "Skill Points:";
            // 
            // intPlayerXPPoints
            // 
            // 
            // 
            // 
            this.intPlayerXPPoints.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intPlayerXPPoints.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intPlayerXPPoints.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intPlayerXPPoints.Location = new System.Drawing.Point(66, 32);
            this.intPlayerXPPoints.MaxValue = 500;
            this.intPlayerXPPoints.MinValue = 0;
            this.intPlayerXPPoints.Name = "intPlayerXPPoints";
            this.intPlayerXPPoints.ShowUpDown = true;
            this.intPlayerXPPoints.Size = new System.Drawing.Size(71, 20);
            this.intPlayerXPPoints.TabIndex = 26;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(6, 32);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(54, 20);
            this.labelX3.TabIndex = 25;
            this.labelX3.Text = "XP Points:";
            // 
            // intPlayerMoney
            // 
            // 
            // 
            // 
            this.intPlayerMoney.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intPlayerMoney.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intPlayerMoney.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intPlayerMoney.Location = new System.Drawing.Point(276, 32);
            this.intPlayerMoney.MaxValue = 500;
            this.intPlayerMoney.MinValue = 0;
            this.intPlayerMoney.Name = "intPlayerMoney";
            this.intPlayerMoney.ShowUpDown = true;
            this.intPlayerMoney.Size = new System.Drawing.Size(79, 20);
            this.intPlayerMoney.TabIndex = 28;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(204, 32);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(66, 20);
            this.labelX4.TabIndex = 27;
            this.labelX4.Text = "Money:";
            // 
            // lblKeyName
            // 
            this.lblKeyName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblKeyName.BackgroundStyle.Class = "";
            this.lblKeyName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblKeyName.Location = new System.Drawing.Point(98, 3);
            this.lblKeyName.Name = "lblKeyName";
            this.lblKeyName.Size = new System.Drawing.Size(315, 23);
            this.lblKeyName.TabIndex = 34;
            this.lblKeyName.Text = "<None selected>";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(6, 3);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(86, 23);
            this.labelX6.TabIndex = 33;
            this.labelX6.Text = "Full Key Name:";
            // 
            // intSettingValue
            // 
            // 
            // 
            // 
            this.intSettingValue.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSettingValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSettingValue.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSettingValue.Location = new System.Drawing.Point(98, 32);
            this.intSettingValue.MaxValue = 500;
            this.intSettingValue.MinValue = 0;
            this.intSettingValue.Name = "intSettingValue";
            this.intSettingValue.ShowUpDown = true;
            this.intSettingValue.Size = new System.Drawing.Size(251, 20);
            this.intSettingValue.TabIndex = 32;
            // 
            // cmdSetValue
            // 
            this.cmdSetValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSetValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSetValue.FocusCuesEnabled = false;
            this.cmdSetValue.Location = new System.Drawing.Point(355, 32);
            this.cmdSetValue.Name = "cmdSetValue";
            this.cmdSetValue.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSetValue.Size = new System.Drawing.Size(58, 20);
            this.cmdSetValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSetValue.TabIndex = 31;
            this.cmdSetValue.Text = "Set";
            this.cmdSetValue.Click += new System.EventHandler(this.cmdSetValue_Click);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(6, 32);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 20);
            this.labelX5.TabIndex = 30;
            this.labelX5.Text = "Edit Value:";
            // 
            // listValues
            // 
            this.listValues.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listValues.AllowDrop = true;
            this.listValues.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listValues.BackgroundStyle.Class = "TreeBorderKey";
            this.listValues.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listValues.CellEdit = true;
            this.listValues.Columns.Add(this.colSetting);
            this.listValues.Columns.Add(this.colValue);
            this.listValues.DragDropEnabled = false;
            this.listValues.DragDropNodeCopyEnabled = false;
            this.listValues.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listValues.Location = new System.Drawing.Point(6, 67);
            this.listValues.MultiNodeDragDropAllowed = false;
            this.listValues.Name = "listValues";
            this.listValues.NodesConnector = this.nodeConnector1;
            this.listValues.NodeStyle = this.elementStyle1;
            this.listValues.PathSeparator = ";";
            this.listValues.Size = new System.Drawing.Size(419, 195);
            this.listValues.Styles.Add(this.elementStyle1);
            this.listValues.TabIndex = 29;
            this.listValues.SelectedIndexChanged += new System.EventHandler(this.listValues_SelectedIndexChanged);
            // 
            // colSetting
            // 
            this.colSetting.DoubleClickAutoSize = false;
            this.colSetting.Editable = false;
            this.colSetting.Name = "colSetting";
            this.colSetting.Text = "Setting";
            this.colSetting.Width.Absolute = 150;
            // 
            // colValue
            // 
            this.colValue.DoubleClickAutoSize = false;
            this.colValue.MaxInputLength = 50;
            this.colValue.Name = "colValue";
            this.colValue.Text = "Value";
            this.colValue.Width.Absolute = 175;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.lblKeyName);
            this.panelEx1.Controls.Add(this.labelX6);
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.cmdSetValue);
            this.panelEx1.Controls.Add(this.intSettingValue);
            this.panelEx1.Location = new System.Drawing.Point(6, 268);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(419, 58);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 35;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.cmdPlayerMoneyMax);
            this.panelEx2.Controls.Add(this.cmdPlayerSkillPointsMax);
            this.panelEx2.Controls.Add(this.cmdPlayerXPPointsMax);
            this.panelEx2.Controls.Add(this.cmdMaxPlayerLevel);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Controls.Add(this.intPlayerLevel);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.intPlayerMoney);
            this.panelEx2.Controls.Add(this.intPlayerSkillPoints);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.labelX3);
            this.panelEx2.Controls.Add(this.intPlayerXPPoints);
            this.panelEx2.Location = new System.Drawing.Point(6, 3);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(419, 58);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 36;
            // 
            // cmdPlayerMoneyMax
            // 
            this.cmdPlayerMoneyMax.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPlayerMoneyMax.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPlayerMoneyMax.FocusCuesEnabled = false;
            this.cmdPlayerMoneyMax.Location = new System.Drawing.Point(361, 32);
            this.cmdPlayerMoneyMax.Name = "cmdPlayerMoneyMax";
            this.cmdPlayerMoneyMax.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdPlayerMoneyMax.Size = new System.Drawing.Size(52, 20);
            this.cmdPlayerMoneyMax.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdPlayerMoneyMax.TabIndex = 32;
            this.cmdPlayerMoneyMax.Text = "Max";
            this.cmdPlayerMoneyMax.Click += new System.EventHandler(this.cmdPlayerMoneyMax_Click);
            // 
            // cmdPlayerSkillPointsMax
            // 
            this.cmdPlayerSkillPointsMax.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPlayerSkillPointsMax.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPlayerSkillPointsMax.FocusCuesEnabled = false;
            this.cmdPlayerSkillPointsMax.Location = new System.Drawing.Point(361, 6);
            this.cmdPlayerSkillPointsMax.Name = "cmdPlayerSkillPointsMax";
            this.cmdPlayerSkillPointsMax.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdPlayerSkillPointsMax.Size = new System.Drawing.Size(52, 20);
            this.cmdPlayerSkillPointsMax.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdPlayerSkillPointsMax.TabIndex = 31;
            this.cmdPlayerSkillPointsMax.Text = "Max";
            this.cmdPlayerSkillPointsMax.Click += new System.EventHandler(this.cmdPlayerSkillPointsMax_Click);
            // 
            // cmdPlayerXPPointsMax
            // 
            this.cmdPlayerXPPointsMax.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdPlayerXPPointsMax.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdPlayerXPPointsMax.FocusCuesEnabled = false;
            this.cmdPlayerXPPointsMax.Location = new System.Drawing.Point(143, 32);
            this.cmdPlayerXPPointsMax.Name = "cmdPlayerXPPointsMax";
            this.cmdPlayerXPPointsMax.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdPlayerXPPointsMax.Size = new System.Drawing.Size(52, 20);
            this.cmdPlayerXPPointsMax.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdPlayerXPPointsMax.TabIndex = 30;
            this.cmdPlayerXPPointsMax.Text = "Max";
            this.cmdPlayerXPPointsMax.Click += new System.EventHandler(this.cmdPlayerXPPointsMax_Click);
            // 
            // cmdMaxPlayerLevel
            // 
            this.cmdMaxPlayerLevel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxPlayerLevel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxPlayerLevel.FocusCuesEnabled = false;
            this.cmdMaxPlayerLevel.Location = new System.Drawing.Point(143, 6);
            this.cmdMaxPlayerLevel.Name = "cmdMaxPlayerLevel";
            this.cmdMaxPlayerLevel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxPlayerLevel.Size = new System.Drawing.Size(52, 20);
            this.cmdMaxPlayerLevel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxPlayerLevel.TabIndex = 29;
            this.cmdMaxPlayerLevel.Text = "Max";
            this.cmdMaxPlayerLevel.Click += new System.EventHandler(this.cmdMaxPlayerLevel_Click);
            // 
            // Borderlands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 390);
            this.Name = "Borderlands";
            this.Text = "Borderlands Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerSkillPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerXPPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intPlayerMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intSettingValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.IntegerInput intPlayerMoney;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.Editors.IntegerInput intPlayerXPPoints;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.IntegerInput intPlayerSkillPoints;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IntegerInput intPlayerLevel;
        private DevComponents.DotNetBar.LabelX lblKeyName;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.Editors.IntegerInput intSettingValue;
        private DevComponents.DotNetBar.ButtonX cmdSetValue;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.AdvTree.AdvTree listValues;
        private DevComponents.AdvTree.ColumnHeader colSetting;
        private DevComponents.AdvTree.ColumnHeader colValue;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX cmdPlayerMoneyMax;
        private DevComponents.DotNetBar.ButtonX cmdPlayerSkillPointsMax;
        private DevComponents.DotNetBar.ButtonX cmdPlayerXPPointsMax;
        private DevComponents.DotNetBar.ButtonX cmdMaxPlayerLevel;
    }
}