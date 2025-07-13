namespace Horizon.PackageEditors.Halo_4
{
    partial class Halo4Profile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Halo4Profile));
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.intExperience = new DevComponents.Editors.IntegerInput();
            this.btnAddExp = new DevComponents.DotNetBar.ButtonX();
            this.advUnlockablesTree = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.cmbSupportUpgrade = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbTacticalPackage = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbArmorAbility = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbGrenade = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbSecondaryWeapon = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbLoadout = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbPrimaryWeapon = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.btnUnlockAll = new DevComponents.DotNetBar.ButtonX();
            this.btnAddSP = new DevComponents.DotNetBar.ButtonX();
            this.intSP = new DevComponents.Editors.IntegerInput();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.cmbCampaignLevel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnLevelLegendary = new DevComponents.DotNetBar.ButtonX();
            this.btnLevelNormal = new DevComponents.DotNetBar.ButtonX();
            this.btnLevelHeroic = new DevComponents.DotNetBar.ButtonX();
            this.btnCoOp = new DevComponents.DotNetBar.ButtonX();
            this.btnSolo = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnLevelEasy = new DevComponents.DotNetBar.ButtonX();
            this.btnCmpgnUnlocked = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnExtract = new DevComponents.DotNetBar.ButtonItem();
            this.btnInject = new DevComponents.DotNetBar.ButtonItem();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intExperience)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advUnlockablesTree)).BeginInit();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intSP)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.groupPanel1.SuspendLayout();
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
            this.cmdSave,
            this.btnExtract,
            this.btnInject});
            this.rbPackageEditor.Size = new System.Drawing.Size(557, 509);
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
            this.panelMain.Controls.Add(this.groupPanel1);
            this.panelMain.Controls.Add(this.panelEx3);
            this.panelMain.Controls.Add(this.btnUnlockAll);
            this.panelMain.Controls.Add(this.advUnlockablesTree);
            this.panelMain.Controls.Add(this.labelX12);
            this.panelMain.Controls.Add(this.labelX1);
            this.panelMain.Controls.Add(this.panelEx2);
            this.panelMain.Controls.Add(this.intSP);
            this.panelMain.Controls.Add(this.panelEx1);
            this.panelMain.Controls.Add(this.btnAddSP);
            this.panelMain.Controls.Add(this.intExperience);
            this.panelMain.Controls.Add(this.btnAddExp);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(557, 454);
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
            this.tabMain.Text = "Profile and Campaign";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 5);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(252, 58);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "<b>Notice:</b> You can bring a total of 250,000 XP online the first time you conn" +
                "ect to Xbox LIVE in the game. You can add 10,000 XP daily if you plan to connect" +
                " to Xbox LIVE.";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 7);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Size = new System.Drawing.Size(45, 20);
            this.labelX1.TabIndex = 9;
            this.labelX1.Text = "Add XP:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Location = new System.Drawing.Point(6, 59);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(261, 68);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 12;
            // 
            // intExperience
            // 
            // 
            // 
            // 
            this.intExperience.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intExperience.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intExperience.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intExperience.Location = new System.Drawing.Point(57, 7);
            this.intExperience.MinValue = 0;
            this.intExperience.Name = "intExperience";
            this.intExperience.ShowUpDown = true;
            this.intExperience.Size = new System.Drawing.Size(109, 20);
            this.intExperience.TabIndex = 10;
            // 
            // btnAddExp
            // 
            this.btnAddExp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddExp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddExp.FocusCuesEnabled = false;
            this.btnAddExp.Image = global::Horizon.Properties.Resources.Plus;
            this.btnAddExp.Location = new System.Drawing.Point(172, 7);
            this.btnAddExp.Name = "btnAddExp";
            this.btnAddExp.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnAddExp.Size = new System.Drawing.Size(95, 20);
            this.btnAddExp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddExp.TabIndex = 11;
            this.btnAddExp.Text = "Add XP";
            this.btnAddExp.Click += new System.EventHandler(this.BtnClickAddXp);
            // 
            // advUnlockablesTree
            // 
            this.advUnlockablesTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advUnlockablesTree.AllowDrop = true;
            this.advUnlockablesTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advUnlockablesTree.BackgroundStyle.Class = "TreeBorderKey";
            this.advUnlockablesTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advUnlockablesTree.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.advUnlockablesTree.Location = new System.Drawing.Point(6, 131);
            this.advUnlockablesTree.Name = "advUnlockablesTree";
            this.advUnlockablesTree.NodesConnector = this.nodeConnector1;
            this.advUnlockablesTree.NodeStyle = this.elementStyle1;
            this.advUnlockablesTree.PathSeparator = ";";
            this.advUnlockablesTree.Size = new System.Drawing.Size(261, 163);
            this.advUnlockablesTree.Styles.Add(this.elementStyle1);
            this.advUnlockablesTree.TabIndex = 14;
            this.advUnlockablesTree.Text = "advTree1";
            this.advUnlockablesTree.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.AdvUnlockablesAfterCheck);
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
            // cmbSupportUpgrade
            // 
            this.cmbSupportUpgrade.DisplayMember = "Text";
            this.cmbSupportUpgrade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSupportUpgrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupportUpgrade.FormattingEnabled = true;
            this.cmbSupportUpgrade.ItemHeight = 14;
            this.cmbSupportUpgrade.Location = new System.Drawing.Point(119, 168);
            this.cmbSupportUpgrade.Name = "cmbSupportUpgrade";
            this.cmbSupportUpgrade.Size = new System.Drawing.Size(146, 20);
            this.cmbSupportUpgrade.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSupportUpgrade.TabIndex = 2;
            // 
            // cmbTacticalPackage
            // 
            this.cmbTacticalPackage.DisplayMember = "Text";
            this.cmbTacticalPackage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTacticalPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTacticalPackage.FormattingEnabled = true;
            this.cmbTacticalPackage.ItemHeight = 14;
            this.cmbTacticalPackage.Location = new System.Drawing.Point(119, 142);
            this.cmbTacticalPackage.Name = "cmbTacticalPackage";
            this.cmbTacticalPackage.Size = new System.Drawing.Size(146, 20);
            this.cmbTacticalPackage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTacticalPackage.TabIndex = 2;
            // 
            // cmbArmorAbility
            // 
            this.cmbArmorAbility.DisplayMember = "Text";
            this.cmbArmorAbility.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbArmorAbility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArmorAbility.FormattingEnabled = true;
            this.cmbArmorAbility.ItemHeight = 14;
            this.cmbArmorAbility.Location = new System.Drawing.Point(119, 116);
            this.cmbArmorAbility.Name = "cmbArmorAbility";
            this.cmbArmorAbility.Size = new System.Drawing.Size(146, 20);
            this.cmbArmorAbility.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbArmorAbility.TabIndex = 2;
            // 
            // cmbGrenade
            // 
            this.cmbGrenade.DisplayMember = "Text";
            this.cmbGrenade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbGrenade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrenade.FormattingEnabled = true;
            this.cmbGrenade.ItemHeight = 14;
            this.cmbGrenade.Location = new System.Drawing.Point(119, 90);
            this.cmbGrenade.Name = "cmbGrenade";
            this.cmbGrenade.Size = new System.Drawing.Size(146, 20);
            this.cmbGrenade.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbGrenade.TabIndex = 2;
            // 
            // cmbSecondaryWeapon
            // 
            this.cmbSecondaryWeapon.DisplayMember = "Text";
            this.cmbSecondaryWeapon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSecondaryWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecondaryWeapon.FormattingEnabled = true;
            this.cmbSecondaryWeapon.ItemHeight = 14;
            this.cmbSecondaryWeapon.Location = new System.Drawing.Point(119, 64);
            this.cmbSecondaryWeapon.Name = "cmbSecondaryWeapon";
            this.cmbSecondaryWeapon.Size = new System.Drawing.Size(146, 20);
            this.cmbSecondaryWeapon.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSecondaryWeapon.TabIndex = 2;
            // 
            // cmbLoadout
            // 
            this.cmbLoadout.DisplayMember = "Text";
            this.cmbLoadout.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbLoadout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLoadout.FormattingEnabled = true;
            this.cmbLoadout.ItemHeight = 14;
            this.cmbLoadout.Location = new System.Drawing.Point(119, 12);
            this.cmbLoadout.Name = "cmbLoadout";
            this.cmbLoadout.Size = new System.Drawing.Size(62, 20);
            this.cmbLoadout.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLoadout.TabIndex = 2;
            this.cmbLoadout.SelectedIndexChanged += new System.EventHandler(this.CmbLoadoutIndexChanged);
            // 
            // cmbPrimaryWeapon
            // 
            this.cmbPrimaryWeapon.DisplayMember = "Text";
            this.cmbPrimaryWeapon.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPrimaryWeapon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrimaryWeapon.FormattingEnabled = true;
            this.cmbPrimaryWeapon.ItemHeight = 14;
            this.cmbPrimaryWeapon.Location = new System.Drawing.Point(119, 38);
            this.cmbPrimaryWeapon.Name = "cmbPrimaryWeapon";
            this.cmbPrimaryWeapon.Size = new System.Drawing.Size(146, 20);
            this.cmbPrimaryWeapon.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPrimaryWeapon.TabIndex = 2;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(4, 168);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(109, 23);
            this.labelX10.TabIndex = 1;
            this.labelX10.Text = "Support Upgrade:";
            this.labelX10.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(6, 90);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(107, 23);
            this.labelX7.TabIndex = 1;
            this.labelX7.Text = "Grenade:";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(6, 142);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(107, 23);
            this.labelX9.TabIndex = 1;
            this.labelX9.Text = "Tactical Package:";
            this.labelX9.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(6, 64);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(107, 23);
            this.labelX6.TabIndex = 1;
            this.labelX6.Text = "Secondary Weapon:";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(6, 116);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(107, 23);
            this.labelX8.TabIndex = 1;
            this.labelX8.Text = "Armor Ability:";
            this.labelX8.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(6, 38);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(107, 23);
            this.labelX5.TabIndex = 1;
            this.labelX5.Text = "Primary Weapon:";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(6, 12);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(107, 23);
            this.labelX4.TabIndex = 1;
            this.labelX4.Text = "Loadout:";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labelX11);
            this.panelEx2.Location = new System.Drawing.Point(6, 298);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(261, 151);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 12;
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.Class = "";
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.labelX11.Location = new System.Drawing.Point(6, 4);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(252, 144);
            this.labelX11.TabIndex = 0;
            this.labelX11.Text = resources.GetString("labelX11.Text");
            // 
            // btnUnlockAll
            // 
            this.btnUnlockAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAll.FocusCuesEnabled = false;
            this.btnUnlockAll.Image = global::Horizon.Properties.Resources.Star;
            this.btnUnlockAll.Location = new System.Drawing.Point(273, 207);
            this.btnUnlockAll.Name = "btnUnlockAll";
            this.btnUnlockAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAll.Size = new System.Drawing.Size(275, 33);
            this.btnUnlockAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAll.TabIndex = 16;
            this.btnUnlockAll.Text = "Unlock All";
            this.btnUnlockAll.Click += new System.EventHandler(this.BtnClickUnlockAll);
            // 
            // btnAddSP
            // 
            this.btnAddSP.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddSP.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddSP.FocusCuesEnabled = false;
            this.btnAddSP.Image = global::Horizon.Properties.Resources.Plus;
            this.btnAddSP.Location = new System.Drawing.Point(172, 33);
            this.btnAddSP.Name = "btnAddSP";
            this.btnAddSP.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnAddSP.Size = new System.Drawing.Size(95, 20);
            this.btnAddSP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddSP.TabIndex = 11;
            this.btnAddSP.Text = "Add SP";
            this.btnAddSP.Click += new System.EventHandler(this.BtnClickAddSp);
            // 
            // intSP
            // 
            // 
            // 
            // 
            this.intSP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSP.Location = new System.Drawing.Point(57, 33);
            this.intSP.MinValue = 0;
            this.intSP.Name = "intSP";
            this.intSP.ShowUpDown = true;
            this.intSP.Size = new System.Drawing.Size(109, 20);
            this.intSP.TabIndex = 10;
            // 
            // labelX12
            // 
            this.labelX12.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.Class = "";
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(6, 33);
            this.labelX12.Name = "labelX12";
            this.labelX12.SingleLineColor = System.Drawing.SystemColors.ControlText;
            this.labelX12.Size = new System.Drawing.Size(45, 20);
            this.labelX12.TabIndex = 9;
            this.labelX12.Text = "Add SP:";
            this.labelX12.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.cmbLoadout);
            this.panelEx3.Controls.Add(this.labelX4);
            this.panelEx3.Controls.Add(this.labelX5);
            this.panelEx3.Controls.Add(this.cmbSupportUpgrade);
            this.panelEx3.Controls.Add(this.labelX8);
            this.panelEx3.Controls.Add(this.cmbTacticalPackage);
            this.panelEx3.Controls.Add(this.labelX6);
            this.panelEx3.Controls.Add(this.cmbArmorAbility);
            this.panelEx3.Controls.Add(this.labelX9);
            this.panelEx3.Controls.Add(this.cmbGrenade);
            this.panelEx3.Controls.Add(this.labelX7);
            this.panelEx3.Controls.Add(this.cmbSecondaryWeapon);
            this.panelEx3.Controls.Add(this.labelX10);
            this.panelEx3.Controls.Add(this.cmbPrimaryWeapon);
            this.panelEx3.Location = new System.Drawing.Point(273, 7);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(275, 201);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 17;
            // 
            // cmbCampaignLevel
            // 
            this.cmbCampaignLevel.DisplayMember = "Text";
            this.cmbCampaignLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbCampaignLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCampaignLevel.FormattingEnabled = true;
            this.cmbCampaignLevel.ItemHeight = 14;
            this.cmbCampaignLevel.Location = new System.Drawing.Point(102, 6);
            this.cmbCampaignLevel.Name = "cmbCampaignLevel";
            this.cmbCampaignLevel.Size = new System.Drawing.Size(129, 20);
            this.cmbCampaignLevel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbCampaignLevel.TabIndex = 27;
            this.cmbCampaignLevel.SelectedIndexChanged += new System.EventHandler(this.CmbCampaignLevelIndexChanged);
            // 
            // btnLevelLegendary
            // 
            this.btnLevelLegendary.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLevelLegendary.AutoCheckOnClick = true;
            this.btnLevelLegendary.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLevelLegendary.FocusCuesEnabled = false;
            this.btnLevelLegendary.Location = new System.Drawing.Point(140, 102);
            this.btnLevelLegendary.Name = "btnLevelLegendary";
            this.btnLevelLegendary.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnLevelLegendary.Size = new System.Drawing.Size(120, 35);
            this.btnLevelLegendary.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLevelLegendary.TabIndex = 24;
            this.btnLevelLegendary.Text = "Legendary";
            // 
            // btnLevelNormal
            // 
            this.btnLevelNormal.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLevelNormal.AutoCheckOnClick = true;
            this.btnLevelNormal.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLevelNormal.FocusCuesEnabled = false;
            this.btnLevelNormal.Location = new System.Drawing.Point(140, 61);
            this.btnLevelNormal.Name = "btnLevelNormal";
            this.btnLevelNormal.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnLevelNormal.Size = new System.Drawing.Size(120, 35);
            this.btnLevelNormal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLevelNormal.TabIndex = 25;
            this.btnLevelNormal.Text = "Normal";
            // 
            // btnLevelHeroic
            // 
            this.btnLevelHeroic.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLevelHeroic.AutoCheckOnClick = true;
            this.btnLevelHeroic.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLevelHeroic.FocusCuesEnabled = false;
            this.btnLevelHeroic.Location = new System.Drawing.Point(13, 102);
            this.btnLevelHeroic.Name = "btnLevelHeroic";
            this.btnLevelHeroic.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnLevelHeroic.Size = new System.Drawing.Size(120, 35);
            this.btnLevelHeroic.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLevelHeroic.TabIndex = 26;
            this.btnLevelHeroic.Text = "Heroic";
            // 
            // btnCoOp
            // 
            this.btnCoOp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCoOp.AutoCheckOnClick = true;
            this.btnCoOp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCoOp.FocusCuesEnabled = false;
            this.btnCoOp.Location = new System.Drawing.Point(140, 32);
            this.btnCoOp.Name = "btnCoOp";
            this.btnCoOp.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnCoOp.Size = new System.Drawing.Size(120, 23);
            this.btnCoOp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCoOp.TabIndex = 23;
            this.btnCoOp.Text = "Co-Op";
            this.btnCoOp.Click += new System.EventHandler(this.BtnClickCoOpSelected);
            // 
            // btnSolo
            // 
            this.btnSolo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSolo.AutoCheckOnClick = true;
            this.btnSolo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSolo.FocusCuesEnabled = false;
            this.btnSolo.Location = new System.Drawing.Point(13, 32);
            this.btnSolo.Name = "btnSolo";
            this.btnSolo.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnSolo.Size = new System.Drawing.Size(120, 23);
            this.btnSolo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSolo.TabIndex = 22;
            this.btnSolo.Text = "Solo";
            this.btnSolo.Click += new System.EventHandler(this.BtnClickSoloSelected);
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(41, 6);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(55, 20);
            this.labelX3.TabIndex = 20;
            this.labelX3.Text = "Level:";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // btnLevelEasy
            // 
            this.btnLevelEasy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLevelEasy.AutoCheckOnClick = true;
            this.btnLevelEasy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLevelEasy.FocusCuesEnabled = false;
            this.btnLevelEasy.Location = new System.Drawing.Point(13, 61);
            this.btnLevelEasy.Name = "btnLevelEasy";
            this.btnLevelEasy.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnLevelEasy.Size = new System.Drawing.Size(120, 35);
            this.btnLevelEasy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLevelEasy.TabIndex = 21;
            this.btnLevelEasy.Text = "Easy";
            // 
            // btnCmpgnUnlocked
            // 
            this.btnCmpgnUnlocked.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCmpgnUnlocked.AutoCheckOnClick = true;
            this.btnCmpgnUnlocked.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCmpgnUnlocked.FocusCuesEnabled = false;
            this.btnCmpgnUnlocked.Image = global::Horizon.Properties.Resources.Medal_Blue;
            this.btnCmpgnUnlocked.Location = new System.Drawing.Point(13, 143);
            this.btnCmpgnUnlocked.Name = "btnCmpgnUnlocked";
            this.btnCmpgnUnlocked.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnCmpgnUnlocked.Size = new System.Drawing.Size(247, 34);
            this.btnCmpgnUnlocked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCmpgnUnlocked.TabIndex = 28;
            this.btnCmpgnUnlocked.Text = "Unlocked";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnCoOp);
            this.groupPanel1.Controls.Add(this.cmbCampaignLevel);
            this.groupPanel1.Controls.Add(this.btnSolo);
            this.groupPanel1.Controls.Add(this.btnLevelLegendary);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.btnLevelNormal);
            this.groupPanel1.Controls.Add(this.btnCmpgnUnlocked);
            this.groupPanel1.Controls.Add(this.btnLevelHeroic);
            this.groupPanel1.Controls.Add(this.btnLevelEasy);
            this.groupPanel1.Location = new System.Drawing.Point(273, 244);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(275, 205);
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
            this.groupPanel1.TabIndex = 29;
            this.groupPanel1.Text = "Campaign";
            // 
            // btnExtract
            // 
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Text = "Extract";
            this.btnExtract.Visible = false;
            this.btnExtract.Click += new System.EventHandler(this.BtnClickExtractProfileData);
            // 
            // btnInject
            // 
            this.btnInject.Name = "btnInject";
            this.btnInject.Text = "Inject";
            this.btnInject.Visible = false;
            this.btnInject.Click += new System.EventHandler(this.BtnClickInjectProfileData);
            // 
            // Halo4Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 512);
            this.Name = "Halo4Profile";
            this.Text = "Halo 4 Profile Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intExperience)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advUnlockablesTree)).EndInit();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intSP)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IntegerInput intExperience;
        private DevComponents.DotNetBar.ButtonX btnAddExp;
        private DevComponents.AdvTree.AdvTree advUnlockablesTree;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSupportUpgrade;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTacticalPackage;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbArmorAbility;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbGrenade;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSecondaryWeapon;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbLoadout;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPrimaryWeapon;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.ButtonX btnUnlockAll;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.Editors.IntegerInput intSP;
        private DevComponents.DotNetBar.ButtonX btnAddSP;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbCampaignLevel;
        private DevComponents.DotNetBar.ButtonX btnLevelLegendary;
        private DevComponents.DotNetBar.ButtonX btnLevelNormal;
        private DevComponents.DotNetBar.ButtonX btnLevelHeroic;
        private DevComponents.DotNetBar.ButtonX btnCoOp;
        private DevComponents.DotNetBar.ButtonX btnSolo;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX btnLevelEasy;
        private DevComponents.DotNetBar.ButtonX btnCmpgnUnlocked;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonItem btnExtract;
        private DevComponents.DotNetBar.ButtonItem btnInject;
    }
}
