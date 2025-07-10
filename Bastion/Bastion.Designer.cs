namespace Horizon.PackageEditors.Bastion
{
    partial class Bastion
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
            this.intMoney = new DevComponents.Editors.IntegerInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.intXP = new DevComponents.Editors.IntegerInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.intLevel = new DevComponents.Editors.IntegerInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmdMaxAll = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.listWeapons = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.lblSlot = new DevComponents.DotNetBar.LabelX();
            this.cmdSaveValue = new DevComponents.DotNetBar.ButtonX();
            this.comboWeapon = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panelSet = new DevComponents.DotNetBar.PanelEx();
            this.cmdUnlockUpgrades = new DevComponents.DotNetBar.ButtonX();
            this.cmdUnlockMaps = new DevComponents.DotNetBar.ButtonX();
            this.cmdUnlockWeapons = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intMoney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intXP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intLevel)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.panelSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(469, 245);
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
            this.panelMain.Controls.Add(this.cmdUnlockUpgrades);
            this.panelMain.Controls.Add(this.cmdUnlockMaps);
            this.panelMain.Controls.Add(this.cmdUnlockWeapons);
            this.panelMain.Controls.Add(this.panelSet);
            this.panelMain.Controls.Add(this.listWeapons);
            this.panelMain.Controls.Add(this.groupPanel1);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(469, 190);
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
            // intMoney
            // 
            // 
            // 
            // 
            this.intMoney.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intMoney.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intMoney.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intMoney.Location = new System.Drawing.Point(71, 54);
            this.intMoney.MinValue = 0;
            this.intMoney.Name = "intMoney";
            this.intMoney.ShowUpDown = true;
            this.intMoney.Size = new System.Drawing.Size(92, 20);
            this.intMoney.TabIndex = 5;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(5, 54);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(60, 20);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "Money:";
            // 
            // intXP
            // 
            // 
            // 
            // 
            this.intXP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intXP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intXP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intXP.Location = new System.Drawing.Point(71, 28);
            this.intXP.MinValue = 0;
            this.intXP.Name = "intXP";
            this.intXP.ShowUpDown = true;
            this.intXP.Size = new System.Drawing.Size(92, 20);
            this.intXP.TabIndex = 3;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(5, 28);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(60, 20);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "Experience:";
            // 
            // intLevel
            // 
            // 
            // 
            // 
            this.intLevel.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intLevel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intLevel.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intLevel.Location = new System.Drawing.Point(71, 2);
            this.intLevel.MaxValue = 10;
            this.intLevel.MinValue = 0;
            this.intLevel.Name = "intLevel";
            this.intLevel.ShowUpDown = true;
            this.intLevel.Size = new System.Drawing.Size(92, 20);
            this.intLevel.TabIndex = 1;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(5, 2);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 20);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "Level:";
            // 
            // cmdMaxAll
            // 
            this.cmdMaxAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxAll.FocusCuesEnabled = false;
            this.cmdMaxAll.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMaxAll.Location = new System.Drawing.Point(5, 80);
            this.cmdMaxAll.Name = "cmdMaxAll";
            this.cmdMaxAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxAll.Size = new System.Drawing.Size(158, 50);
            this.cmdMaxAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxAll.TabIndex = 6;
            this.cmdMaxAll.Text = "Max All";
            this.cmdMaxAll.Click += new System.EventHandler(this.buttonX1_Click_1);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.Color.Transparent;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.cmdMaxAll);
            this.groupPanel1.Controls.Add(this.intXP);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.intLevel);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.intMoney);
            this.groupPanel1.Location = new System.Drawing.Point(6, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(170, 152);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 7;
            this.groupPanel1.Text = "Stats";
            // 
            // listWeapons
            // 
            this.listWeapons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listWeapons.Location = new System.Drawing.Point(182, 3);
            this.listWeapons.MultiSelect = false;
            this.listWeapons.Name = "listWeapons";
            this.listWeapons.Size = new System.Drawing.Size(281, 133);
            this.listWeapons.TabIndex = 13;
            this.listWeapons.UseCompatibleStateImageBehavior = false;
            this.listWeapons.View = System.Windows.Forms.View.Details;
            this.listWeapons.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged_1);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Weapon";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Slot";
            this.columnHeader2.Width = 95;
            // 
            // lblSlot
            // 
            // 
            // 
            // 
            this.lblSlot.BackgroundStyle.Class = "";
            this.lblSlot.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSlot.Location = new System.Drawing.Point(145, 0);
            this.lblSlot.Name = "lblSlot";
            this.lblSlot.Size = new System.Drawing.Size(78, 20);
            this.lblSlot.TabIndex = 12;
            this.lblSlot.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // cmdSaveValue
            // 
            this.cmdSaveValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSaveValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSaveValue.FocusCuesEnabled = false;
            this.cmdSaveValue.Location = new System.Drawing.Point(229, 0);
            this.cmdSaveValue.Name = "cmdSaveValue";
            this.cmdSaveValue.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSaveValue.Size = new System.Drawing.Size(52, 20);
            this.cmdSaveValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSaveValue.TabIndex = 11;
            this.cmdSaveValue.Text = "Save";
            this.cmdSaveValue.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // comboWeapon
            // 
            this.comboWeapon.DisplayMember = "Text";
            this.comboWeapon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWeapon.FormattingEnabled = true;
            this.comboWeapon.ItemHeight = 14;
            this.comboWeapon.Location = new System.Drawing.Point(0, 0);
            this.comboWeapon.Name = "comboWeapon";
            this.comboWeapon.Size = new System.Drawing.Size(139, 20);
            this.comboWeapon.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboWeapon.TabIndex = 10;
            this.comboWeapon.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // panelSet
            // 
            this.panelSet.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelSet.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelSet.Controls.Add(this.comboWeapon);
            this.panelSet.Controls.Add(this.cmdSaveValue);
            this.panelSet.Controls.Add(this.lblSlot);
            this.panelSet.Enabled = false;
            this.panelSet.Location = new System.Drawing.Point(182, 135);
            this.panelSet.Name = "panelSet";
            this.panelSet.Size = new System.Drawing.Size(281, 20);
            this.panelSet.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelSet.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelSet.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelSet.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelSet.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelSet.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelSet.Style.GradientAngle = 90;
            this.panelSet.TabIndex = 14;
            // 
            // cmdUnlockUpgrades
            // 
            this.cmdUnlockUpgrades.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlockUpgrades.AutoCheckOnClick = true;
            this.cmdUnlockUpgrades.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlockUpgrades.FocusCuesEnabled = false;
            this.cmdUnlockUpgrades.Location = new System.Drawing.Point(327, 161);
            this.cmdUnlockUpgrades.Name = "cmdUnlockUpgrades";
            this.cmdUnlockUpgrades.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlockUpgrades.Size = new System.Drawing.Size(136, 23);
            this.cmdUnlockUpgrades.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlockUpgrades.TabIndex = 18;
            this.cmdUnlockUpgrades.Text = "Unlock All Upgrades";
            // 
            // cmdUnlockMaps
            // 
            this.cmdUnlockMaps.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlockMaps.AutoCheckOnClick = true;
            this.cmdUnlockMaps.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlockMaps.FocusCuesEnabled = false;
            this.cmdUnlockMaps.Location = new System.Drawing.Point(185, 161);
            this.cmdUnlockMaps.Name = "cmdUnlockMaps";
            this.cmdUnlockMaps.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlockMaps.Size = new System.Drawing.Size(143, 23);
            this.cmdUnlockMaps.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlockMaps.TabIndex = 17;
            this.cmdUnlockMaps.Text = "Unlock All Maps";
            // 
            // cmdUnlockWeapons
            // 
            this.cmdUnlockWeapons.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlockWeapons.AutoCheckOnClick = true;
            this.cmdUnlockWeapons.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlockWeapons.FocusCuesEnabled = false;
            this.cmdUnlockWeapons.Location = new System.Drawing.Point(6, 161);
            this.cmdUnlockWeapons.Name = "cmdUnlockWeapons";
            this.cmdUnlockWeapons.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlockWeapons.Size = new System.Drawing.Size(180, 23);
            this.cmdUnlockWeapons.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlockWeapons.TabIndex = 16;
            this.cmdUnlockWeapons.Text = "Unlock All Weapons";
            this.cmdUnlockWeapons.Click += new System.EventHandler(this.cmdUnlockWeapons_Click);
            // 
            // Bastion
            // 
            this.ClientSize = new System.Drawing.Size(479, 248);
            this.Name = "Bastion";
            this.Text = "Bastion Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intMoney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intXP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intLevel)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.panelSet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.IntegerInput intXP;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.IntegerInput intLevel;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IntegerInput intMoney;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX cmdMaxAll;
        private System.Windows.Forms.ListView listWeapons;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private DevComponents.DotNetBar.LabelX lblSlot;
        private DevComponents.DotNetBar.ButtonX cmdSaveValue;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboWeapon;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX cmdUnlockUpgrades;
        private DevComponents.DotNetBar.ButtonX cmdUnlockMaps;
        private DevComponents.DotNetBar.ButtonX cmdUnlockWeapons;
        private DevComponents.DotNetBar.PanelEx panelSet;
    }
}