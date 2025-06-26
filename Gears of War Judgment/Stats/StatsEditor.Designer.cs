namespace Horizon.PackageEditors.Gears_of_War_Judgment.Stats
{
    partial class StatsEditor
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
            this.cmdExportStats = new DevComponents.DotNetBar.ButtonItem();
            this.cbPrestige = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cbRank = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblRank = new DevComponents.DotNetBar.LabelX();
            this.lblXp = new DevComponents.DotNetBar.LabelX();
            this.intXP = new DevComponents.Editors.IntegerInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.intDoubleXP = new DevComponents.Editors.IntegerInput();
            this.panelMainValues = new DevComponents.DotNetBar.PanelEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.intRibbons = new DevComponents.Editors.IntegerInput();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.intDoubleXPTotal = new DevComponents.Editors.IntegerInput();
            this.ckInfiniteMode = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckGears3 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckGears2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckGears1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdMarkViewed = new DevComponents.DotNetBar.ButtonX();
            this.listAwards = new DevComponents.AdvTree.AdvTree();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.listMain = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.cbModeMain = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblMain = new DevComponents.DotNetBar.LabelX();
            this.ribbonTabItem2 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
            this.intRibbonCount = new DevComponents.Editors.IntegerInput();
            this.cmdMaxRibbons = new DevComponents.DotNetBar.ButtonX();
            this.listRibbons = new DevComponents.AdvTree.AdvTree();
            this.columnHeader5 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader6 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intXP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intDoubleXP)).BeginInit();
            this.panelMainValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intRibbons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intDoubleXPTotal)).BeginInit();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listAwards)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMain)).BeginInit();
            this.ribbonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intRibbonCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listRibbons)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Controls.Add(this.ribbonPanel1);
            this.rbPackageEditor.Controls.Add(this.ribbonPanel2);
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItem1,
            this.ribbonTabItem2,
            this.cmdExportStats});
            this.rbPackageEditor.Size = new System.Drawing.Size(591, 424);
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
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel2, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel1, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.listMain);
            this.panelMain.Controls.Add(this.cbModeMain);
            this.panelMain.Controls.Add(this.lblMain);
            this.panelMain.Controls.Add(this.panelMainValues);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(591, 369);
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
            this.tabMain.Text = "Statistics";
            // 
            // cmdExportStats
            // 
            this.cmdExportStats.Name = "cmdExportStats";
            this.cmdExportStats.Text = "Export Stats";
            this.cmdExportStats.Visible = false;
            this.cmdExportStats.Click += new System.EventHandler(this.cmdExportStats_Click);
            // 
            // cbPrestige
            // 
            this.cbPrestige.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbPrestige.DisplayMember = "Text";
            this.cbPrestige.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPrestige.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrestige.FocusCuesEnabled = false;
            this.cbPrestige.FormattingEnabled = true;
            this.cbPrestige.ItemHeight = 14;
            this.cbPrestige.Location = new System.Drawing.Point(61, 15);
            this.cbPrestige.Name = "cbPrestige";
            this.cbPrestige.Size = new System.Drawing.Size(92, 20);
            this.cbPrestige.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbPrestige.TabIndex = 62;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(19, 15);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(36, 20);
            this.labelX1.TabIndex = 61;
            this.labelX1.Text = "Re-Up:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbRank
            // 
            this.cbRank.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cbRank.DisplayMember = "Text";
            this.cbRank.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbRank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRank.FocusCuesEnabled = false;
            this.cbRank.FormattingEnabled = true;
            this.cbRank.ItemHeight = 14;
            this.cbRank.Location = new System.Drawing.Point(61, 41);
            this.cbRank.Name = "cbRank";
            this.cbRank.Size = new System.Drawing.Size(92, 20);
            this.cbRank.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbRank.TabIndex = 5;
            // 
            // lblRank
            // 
            this.lblRank.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblRank.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblRank.BackgroundStyle.Class = "";
            this.lblRank.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRank.Location = new System.Drawing.Point(13, 41);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(42, 20);
            this.lblRank.TabIndex = 1;
            this.lblRank.Text = "Level:";
            this.lblRank.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblXp
            // 
            this.lblXp.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblXp.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblXp.BackgroundStyle.Class = "";
            this.lblXp.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblXp.Location = new System.Drawing.Point(13, 67);
            this.lblXp.Name = "lblXp";
            this.lblXp.Size = new System.Drawing.Size(42, 20);
            this.lblXp.TabIndex = 3;
            this.lblXp.Text = "XP:";
            this.lblXp.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // intXP
            // 
            this.intXP.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.intXP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intXP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intXP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intXP.Location = new System.Drawing.Point(61, 67);
            this.intXP.MaxValue = 99999999;
            this.intXP.MinValue = 0;
            this.intXP.Name = "intXP";
            this.intXP.ShowUpDown = true;
            this.intXP.Size = new System.Drawing.Size(92, 20);
            this.intXP.TabIndex = 4;
            // 
            // labelX2
            // 
            this.labelX2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(13, 209);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(142, 20);
            this.labelX2.TabIndex = 61;
            this.labelX2.Text = "Double XP Tickets:";
            // 
            // intDoubleXP
            // 
            this.intDoubleXP.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.intDoubleXP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intDoubleXP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intDoubleXP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intDoubleXP.Location = new System.Drawing.Point(13, 235);
            this.intDoubleXP.MaxValue = 999999999;
            this.intDoubleXP.MinValue = 0;
            this.intDoubleXP.Name = "intDoubleXP";
            this.intDoubleXP.ShowUpDown = true;
            this.intDoubleXP.Size = new System.Drawing.Size(107, 20);
            this.intDoubleXP.TabIndex = 62;
            // 
            // panelMainValues
            // 
            this.panelMainValues.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelMainValues.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelMainValues.Controls.Add(this.labelX1);
            this.panelMainValues.Controls.Add(this.cbPrestige);
            this.panelMainValues.Controls.Add(this.labelX3);
            this.panelMainValues.Controls.Add(this.intRibbons);
            this.panelMainValues.Controls.Add(this.lblRank);
            this.panelMainValues.Controls.Add(this.cbRank);
            this.panelMainValues.Controls.Add(this.labelX4);
            this.panelMainValues.Controls.Add(this.intDoubleXPTotal);
            this.panelMainValues.Controls.Add(this.lblXp);
            this.panelMainValues.Controls.Add(this.ckInfiniteMode);
            this.panelMainValues.Controls.Add(this.intXP);
            this.panelMainValues.Controls.Add(this.ckGears3);
            this.panelMainValues.Controls.Add(this.labelX2);
            this.panelMainValues.Controls.Add(this.intDoubleXP);
            this.panelMainValues.Controls.Add(this.ckGears2);
            this.panelMainValues.Controls.Add(this.ckGears1);
            this.panelMainValues.Location = new System.Drawing.Point(0, -1);
            this.panelMainValues.Name = "panelMainValues";
            this.panelMainValues.Size = new System.Drawing.Size(164, 371);
            this.panelMainValues.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelMainValues.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelMainValues.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelMainValues.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelMainValues.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelMainValues.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelMainValues.Style.GradientAngle = 90;
            this.panelMainValues.TabIndex = 33;
            // 
            // labelX3
            // 
            this.labelX3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 313);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(140, 20);
            this.labelX3.TabIndex = 72;
            this.labelX3.Text = "Total Ribbons:";
            // 
            // intRibbons
            // 
            this.intRibbons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.intRibbons.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intRibbons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intRibbons.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intRibbons.Location = new System.Drawing.Point(13, 339);
            this.intRibbons.MaxValue = 999999999;
            this.intRibbons.MinValue = 0;
            this.intRibbons.Name = "intRibbons";
            this.intRibbons.ShowUpDown = true;
            this.intRibbons.Size = new System.Drawing.Size(107, 20);
            this.intRibbons.TabIndex = 73;
            // 
            // labelX4
            // 
            this.labelX4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(13, 261);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(140, 20);
            this.labelX4.TabIndex = 70;
            this.labelX4.Text = "Double XP Tickets (+Used):";
            // 
            // intDoubleXPTotal
            // 
            this.intDoubleXPTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.intDoubleXPTotal.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intDoubleXPTotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intDoubleXPTotal.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intDoubleXPTotal.Location = new System.Drawing.Point(13, 287);
            this.intDoubleXPTotal.MaxValue = 999999999;
            this.intDoubleXPTotal.MinValue = 0;
            this.intDoubleXPTotal.Name = "intDoubleXPTotal";
            this.intDoubleXPTotal.ShowUpDown = true;
            this.intDoubleXPTotal.Size = new System.Drawing.Size(107, 20);
            this.intDoubleXPTotal.TabIndex = 71;
            // 
            // ckInfiniteMode
            // 
            this.ckInfiniteMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.ckInfiniteMode.BackgroundStyle.Class = "";
            this.ckInfiniteMode.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckInfiniteMode.FocusCuesEnabled = false;
            this.ckInfiniteMode.Location = new System.Drawing.Point(9, 180);
            this.ckInfiniteMode.Name = "ckInfiniteMode";
            this.ckInfiniteMode.Size = new System.Drawing.Size(144, 23);
            this.ckInfiniteMode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckInfiniteMode.TabIndex = 69;
            this.ckInfiniteMode.Text = "Aftermath Unlocked";
            // 
            // ckGears3
            // 
            this.ckGears3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.ckGears3.BackgroundStyle.Class = "";
            this.ckGears3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckGears3.FocusCuesEnabled = false;
            this.ckGears3.Location = new System.Drawing.Point(9, 151);
            this.ckGears3.Name = "ckGears3";
            this.ckGears3.Size = new System.Drawing.Size(146, 23);
            this.ckGears3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckGears3.TabIndex = 68;
            this.ckGears3.Text = "Beat Gears 3 Campaign";
            // 
            // ckGears2
            // 
            this.ckGears2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.ckGears2.BackgroundStyle.Class = "";
            this.ckGears2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckGears2.FocusCuesEnabled = false;
            this.ckGears2.Location = new System.Drawing.Point(9, 122);
            this.ckGears2.Name = "ckGears2";
            this.ckGears2.Size = new System.Drawing.Size(146, 23);
            this.ckGears2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckGears2.TabIndex = 67;
            this.ckGears2.Text = "Beat Gears 2 Campaign";
            // 
            // ckGears1
            // 
            this.ckGears1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.ckGears1.BackgroundStyle.Class = "";
            this.ckGears1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckGears1.FocusCuesEnabled = false;
            this.ckGears1.Location = new System.Drawing.Point(9, 93);
            this.ckGears1.Name = "ckGears1";
            this.ckGears1.Size = new System.Drawing.Size(144, 23);
            this.ckGears1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckGears1.TabIndex = 66;
            this.ckGears1.Text = "Beat Gears 1 Campaign";
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "Unlockables";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.cmdMarkViewed);
            this.ribbonPanel1.Controls.Add(this.listAwards);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(591, 369);
            // 
            // 
            // 
            this.ribbonPanel1.Style.Class = "";
            this.ribbonPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseDown.Class = "";
            this.ribbonPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseOver.Class = "";
            this.ribbonPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel1.TabIndex = 2;
            this.ribbonPanel1.Visible = false;
            // 
            // cmdMarkViewed
            // 
            this.cmdMarkViewed.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMarkViewed.AutoCheckOnClick = true;
            this.cmdMarkViewed.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMarkViewed.FocusCuesEnabled = false;
            this.cmdMarkViewed.Location = new System.Drawing.Point(0, 345);
            this.cmdMarkViewed.Name = "cmdMarkViewed";
            this.cmdMarkViewed.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMarkViewed.Size = new System.Drawing.Size(591, 25);
            this.cmdMarkViewed.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMarkViewed.TabIndex = 39;
            this.cmdMarkViewed.Text = "Mark All as Viewed";
            // 
            // listAwards
            // 
            this.listAwards.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listAwards.AllowDrop = true;
            this.listAwards.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listAwards.BackgroundStyle.Class = "TreeBorderKey";
            this.listAwards.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listAwards.Columns.Add(this.columnHeader3);
            this.listAwards.DoubleClickTogglesNode = false;
            this.listAwards.DragDropEnabled = false;
            this.listAwards.DragDropNodeCopyEnabled = false;
            this.listAwards.HotTracking = true;
            this.listAwards.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listAwards.Location = new System.Drawing.Point(0, -1);
            this.listAwards.MultiNodeDragCountVisible = false;
            this.listAwards.MultiNodeDragDropAllowed = false;
            this.listAwards.Name = "listAwards";
            this.listAwards.NodesConnector = this.nodeConnector1;
            this.listAwards.NodeStyle = this.elementStyle1;
            this.listAwards.PathSeparator = ";";
            this.listAwards.Size = new System.Drawing.Size(591, 347);
            this.listAwards.Styles.Add(this.elementStyle1);
            this.listAwards.TabIndex = 38;
            // 
            // columnHeader3
            // 
            this.columnHeader3.MinimumWidth = 230;
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Awards";
            this.columnHeader3.Width.Absolute = 450;
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
            // listMain
            // 
            this.listMain.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listMain.AllowDrop = true;
            this.listMain.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listMain.BackgroundStyle.Class = "TreeBorderKey";
            this.listMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listMain.Columns.Add(this.columnHeader1);
            this.listMain.Columns.Add(this.columnHeader2);
            this.listMain.DragDropEnabled = false;
            this.listMain.DragDropNodeCopyEnabled = false;
            this.listMain.HotTracking = true;
            this.listMain.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listMain.Location = new System.Drawing.Point(163, 46);
            this.listMain.MultiNodeDragCountVisible = false;
            this.listMain.MultiNodeDragDropAllowed = false;
            this.listMain.Name = "listMain";
            this.listMain.NodesConnector = this.nodeConnector2;
            this.listMain.NodeStyle = this.elementStyle2;
            this.listMain.PathSeparator = ";";
            this.listMain.Size = new System.Drawing.Size(428, 324);
            this.listMain.Styles.Add(this.elementStyle2);
            this.listMain.TabIndex = 37;
            // 
            // columnHeader1
            // 
            this.columnHeader1.MinimumWidth = 230;
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Stat Name";
            this.columnHeader1.Width.Absolute = 230;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Stat Value";
            this.columnHeader2.Width.Absolute = 90;
            // 
            // nodeConnector2
            // 
            this.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle2
            // 
            this.elementStyle2.Class = "";
            this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // cbModeMain
            // 
            this.cbModeMain.DisplayMember = "Text";
            this.cbModeMain.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbModeMain.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModeMain.FocusCuesEnabled = false;
            this.cbModeMain.FormattingEnabled = true;
            this.cbModeMain.ItemHeight = 14;
            this.cbModeMain.Location = new System.Drawing.Point(170, 20);
            this.cbModeMain.Name = "cbModeMain";
            this.cbModeMain.Size = new System.Drawing.Size(117, 20);
            this.cbModeMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbModeMain.TabIndex = 38;
            // 
            // lblMain
            // 
            this.lblMain.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMain.BackgroundStyle.Class = "";
            this.lblMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMain.Location = new System.Drawing.Point(282, 9);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(297, 28);
            this.lblMain.TabIndex = 39;
            this.lblMain.Text = "Campaign Mode Stats";
            this.lblMain.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // ribbonTabItem2
            // 
            this.ribbonTabItem2.Name = "ribbonTabItem2";
            this.ribbonTabItem2.Panel = this.ribbonPanel2;
            this.ribbonTabItem2.Text = "Ribbons";
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel2.Controls.Add(this.intRibbonCount);
            this.ribbonPanel2.Controls.Add(this.cmdMaxRibbons);
            this.ribbonPanel2.Controls.Add(this.listRibbons);
            this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel2.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel2.Size = new System.Drawing.Size(591, 369);
            // 
            // 
            // 
            this.ribbonPanel2.Style.Class = "";
            this.ribbonPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseDown.Class = "";
            this.ribbonPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseOver.Class = "";
            this.ribbonPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel2.TabIndex = 3;
            this.ribbonPanel2.Visible = false;
            // 
            // intRibbonCount
            // 
            this.intRibbonCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            // 
            // 
            // 
            this.intRibbonCount.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intRibbonCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intRibbonCount.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intRibbonCount.Location = new System.Drawing.Point(2, 347);
            this.intRibbonCount.MaxValue = 999999999;
            this.intRibbonCount.MinValue = 0;
            this.intRibbonCount.Name = "intRibbonCount";
            this.intRibbonCount.ShowUpDown = true;
            this.intRibbonCount.Size = new System.Drawing.Size(107, 20);
            this.intRibbonCount.TabIndex = 63;
            this.intRibbonCount.Value = 1000;
            // 
            // cmdMaxRibbons
            // 
            this.cmdMaxRibbons.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxRibbons.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxRibbons.FocusCuesEnabled = false;
            this.cmdMaxRibbons.Location = new System.Drawing.Point(110, 345);
            this.cmdMaxRibbons.Name = "cmdMaxRibbons";
            this.cmdMaxRibbons.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxRibbons.Size = new System.Drawing.Size(481, 25);
            this.cmdMaxRibbons.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxRibbons.TabIndex = 40;
            this.cmdMaxRibbons.Text = "Set All Values";
            this.cmdMaxRibbons.Click += new System.EventHandler(this.cmdMaxRibbons_Click);
            // 
            // listRibbons
            // 
            this.listRibbons.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listRibbons.AllowDrop = true;
            this.listRibbons.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listRibbons.BackgroundStyle.Class = "TreeBorderKey";
            this.listRibbons.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listRibbons.Columns.Add(this.columnHeader5);
            this.listRibbons.Columns.Add(this.columnHeader6);
            this.listRibbons.DoubleClickTogglesNode = false;
            this.listRibbons.DragDropEnabled = false;
            this.listRibbons.DragDropNodeCopyEnabled = false;
            this.listRibbons.ExpandButtonSize = new System.Drawing.Size(1, 15);
            this.listRibbons.ExpandWidth = 5;
            this.listRibbons.HotTracking = true;
            this.listRibbons.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listRibbons.Location = new System.Drawing.Point(0, -1);
            this.listRibbons.MultiNodeDragCountVisible = false;
            this.listRibbons.MultiNodeDragDropAllowed = false;
            this.listRibbons.Name = "listRibbons";
            this.listRibbons.NodeHorizontalSpacing = 5;
            this.listRibbons.NodesConnector = this.nodeConnector3;
            this.listRibbons.NodeSpacing = 5;
            this.listRibbons.NodeStyle = this.elementStyle3;
            this.listRibbons.PathSeparator = ";";
            this.listRibbons.Size = new System.Drawing.Size(591, 347);
            this.listRibbons.Styles.Add(this.elementStyle3);
            this.listRibbons.TabIndex = 39;
            // 
            // columnHeader5
            // 
            this.columnHeader5.MinimumWidth = 230;
            this.columnHeader5.Name = "columnHeader5";
            this.columnHeader5.Text = "Ribbons";
            this.columnHeader5.Width.Absolute = 420;
            // 
            // columnHeader6
            // 
            this.columnHeader6.EditorType = DevComponents.AdvTree.eCellEditorType.NumericInteger;
            this.columnHeader6.Name = "columnHeader6";
            this.columnHeader6.Text = "Count";
            this.columnHeader6.Width.Absolute = 110;
            // 
            // nodeConnector3
            // 
            this.nodeConnector3.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle3
            // 
            this.elementStyle3.Class = "";
            this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // StatsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 427);
            this.Name = "StatsEditor";
            this.Text = "Gears of War: Judgment Stats Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intXP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intDoubleXP)).EndInit();
            this.panelMainValues.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intRibbons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intDoubleXPTotal)).EndInit();
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listAwards)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listMain)).EndInit();
            this.ribbonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intRibbonCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listRibbons)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem cmdExportStats;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbPrestige;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbRank;
        private DevComponents.DotNetBar.LabelX lblRank;
        private DevComponents.DotNetBar.LabelX lblXp;
        private DevComponents.Editors.IntegerInput intXP;
        private DevComponents.Editors.IntegerInput intDoubleXP;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.PanelEx panelMainValues;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.Editors.IntegerInput intDoubleXPTotal;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckInfiniteMode;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckGears3;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckGears2;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckGears1;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.AdvTree.AdvTree listMain;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbModeMain;
        private DevComponents.DotNetBar.LabelX lblMain;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.IntegerInput intRibbons;
        private DevComponents.AdvTree.AdvTree listAwards;
        private DevComponents.AdvTree.ColumnHeader columnHeader3;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel2;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem2;
        private DevComponents.AdvTree.AdvTree listRibbons;
        private DevComponents.AdvTree.ColumnHeader columnHeader5;
        private DevComponents.AdvTree.ColumnHeader columnHeader6;
        private DevComponents.AdvTree.NodeConnector nodeConnector3;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.Editors.IntegerInput intRibbonCount;
        private DevComponents.DotNetBar.ButtonX cmdMaxRibbons;
        private DevComponents.DotNetBar.ButtonX cmdMarkViewed;
    }
}
