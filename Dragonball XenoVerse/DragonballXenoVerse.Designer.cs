using DevComponents.DotNetBar;

namespace Horizon.PackageEditors.Dragonball_XenoVerse
{
    partial class DragonballXenoVerse
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
            this.gpEquipment = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.bUnlockAllHands = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllLowerBody = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllZSoul = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllFeet = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllAccessory = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllUpperBody = new DevComponents.DotNetBar.ButtonX();
            this.gpBattle = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.bUnlockAllImportantItems = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllMixingItems = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllCapsules = new DevComponents.DotNetBar.ButtonX();
            this.bUnlockAllSkillSets = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbPlayerIndex = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnMaxAllStats = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtPlayerName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.listPlayerStats = new DevComponents.AdvTree.AdvTree();
            this.columnHeader7 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader8 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbTransplantChIndex = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnTransplantCh = new DevComponents.DotNetBar.ButtonX();
            this.chkUnlockChSlots = new DevComponents.DotNetBar.ButtonX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnMaxZeni = new DevComponents.DotNetBar.ButtonX();
            this.intZeni = new DevComponents.Editors.IntegerInput();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.gpEquipment.SuspendLayout();
            this.gpBattle.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listPlayerStats)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intZeni)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(680, 409);
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
            this.panelMain.Controls.Add(this.groupPanel3);
            this.panelMain.Controls.Add(this.groupPanel2);
            this.panelMain.Controls.Add(this.groupPanel1);
            this.panelMain.Controls.Add(this.gpBattle);
            this.panelMain.Controls.Add(this.gpEquipment);
            this.panelMain.Controls.Add(this.bUnlockAllSkillSets);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(680, 354);
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
            this.tabMain.Text = "Player";
            // 
            // gpEquipment
            // 
            this.gpEquipment.BackColor = System.Drawing.Color.Transparent;
            this.gpEquipment.CanvasColor = System.Drawing.Color.Transparent;
            this.gpEquipment.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpEquipment.Controls.Add(this.bUnlockAllHands);
            this.gpEquipment.Controls.Add(this.bUnlockAllLowerBody);
            this.gpEquipment.Controls.Add(this.bUnlockAllZSoul);
            this.gpEquipment.Controls.Add(this.bUnlockAllFeet);
            this.gpEquipment.Controls.Add(this.bUnlockAllAccessory);
            this.gpEquipment.Controls.Add(this.bUnlockAllUpperBody);
            this.gpEquipment.Location = new System.Drawing.Point(490, 3);
            this.gpEquipment.Name = "gpEquipment";
            this.gpEquipment.Size = new System.Drawing.Size(179, 200);
            // 
            // 
            // 
            this.gpEquipment.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpEquipment.Style.BackColorGradientAngle = 90;
            this.gpEquipment.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpEquipment.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpEquipment.Style.BorderBottomWidth = 1;
            this.gpEquipment.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpEquipment.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpEquipment.Style.BorderLeftWidth = 1;
            this.gpEquipment.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpEquipment.Style.BorderRightWidth = 1;
            this.gpEquipment.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpEquipment.Style.BorderTopWidth = 1;
            this.gpEquipment.Style.Class = "";
            this.gpEquipment.Style.CornerDiameter = 4;
            this.gpEquipment.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpEquipment.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpEquipment.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpEquipment.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpEquipment.StyleMouseDown.Class = "";
            this.gpEquipment.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpEquipment.StyleMouseOver.Class = "";
            this.gpEquipment.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpEquipment.TabIndex = 0;
            this.gpEquipment.Text = "Unlock Equipment";
            // 
            // bUnlockAllHands
            // 
            this.bUnlockAllHands.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllHands.AutoCheckOnClick = true;
            this.bUnlockAllHands.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllHands.FocusCuesEnabled = false;
            this.bUnlockAllHands.Location = new System.Drawing.Point(-1, 63);
            this.bUnlockAllHands.Name = "bUnlockAllHands";
            this.bUnlockAllHands.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllHands.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllHands.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllHands.TabIndex = 24;
            this.bUnlockAllHands.Text = "Hands";
            // 
            // bUnlockAllLowerBody
            // 
            this.bUnlockAllLowerBody.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllLowerBody.AutoCheckOnClick = true;
            this.bUnlockAllLowerBody.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllLowerBody.FocusCuesEnabled = false;
            this.bUnlockAllLowerBody.Location = new System.Drawing.Point(-1, 33);
            this.bUnlockAllLowerBody.Name = "bUnlockAllLowerBody";
            this.bUnlockAllLowerBody.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllLowerBody.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllLowerBody.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllLowerBody.TabIndex = 24;
            this.bUnlockAllLowerBody.Text = "Lower Body";
            // 
            // bUnlockAllZSoul
            // 
            this.bUnlockAllZSoul.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllZSoul.AutoCheckOnClick = true;
            this.bUnlockAllZSoul.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllZSoul.FocusCuesEnabled = false;
            this.bUnlockAllZSoul.Location = new System.Drawing.Point(-1, 153);
            this.bUnlockAllZSoul.Name = "bUnlockAllZSoul";
            this.bUnlockAllZSoul.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllZSoul.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllZSoul.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllZSoul.TabIndex = 24;
            this.bUnlockAllZSoul.Text = "Z-Soul";
            // 
            // bUnlockAllFeet
            // 
            this.bUnlockAllFeet.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllFeet.AutoCheckOnClick = true;
            this.bUnlockAllFeet.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllFeet.FocusCuesEnabled = false;
            this.bUnlockAllFeet.Location = new System.Drawing.Point(-1, 93);
            this.bUnlockAllFeet.Name = "bUnlockAllFeet";
            this.bUnlockAllFeet.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllFeet.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllFeet.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllFeet.TabIndex = 24;
            this.bUnlockAllFeet.Text = "Feet";
            // 
            // bUnlockAllAccessory
            // 
            this.bUnlockAllAccessory.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllAccessory.AutoCheckOnClick = true;
            this.bUnlockAllAccessory.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllAccessory.FocusCuesEnabled = false;
            this.bUnlockAllAccessory.Location = new System.Drawing.Point(-1, 123);
            this.bUnlockAllAccessory.Name = "bUnlockAllAccessory";
            this.bUnlockAllAccessory.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllAccessory.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllAccessory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllAccessory.TabIndex = 24;
            this.bUnlockAllAccessory.Text = "Accessory";
            // 
            // bUnlockAllUpperBody
            // 
            this.bUnlockAllUpperBody.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllUpperBody.AutoCheckOnClick = true;
            this.bUnlockAllUpperBody.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllUpperBody.FocusCuesEnabled = false;
            this.bUnlockAllUpperBody.Location = new System.Drawing.Point(-1, 3);
            this.bUnlockAllUpperBody.Name = "bUnlockAllUpperBody";
            this.bUnlockAllUpperBody.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllUpperBody.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllUpperBody.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllUpperBody.TabIndex = 24;
            this.bUnlockAllUpperBody.Text = "Upper Body";
            // 
            // gpBattle
            // 
            this.gpBattle.BackColor = System.Drawing.Color.Transparent;
            this.gpBattle.CanvasColor = System.Drawing.Color.Transparent;
            this.gpBattle.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpBattle.Controls.Add(this.bUnlockAllImportantItems);
            this.gpBattle.Controls.Add(this.bUnlockAllMixingItems);
            this.gpBattle.Controls.Add(this.bUnlockAllCapsules);
            this.gpBattle.Location = new System.Drawing.Point(490, 209);
            this.gpBattle.Name = "gpBattle";
            this.gpBattle.Size = new System.Drawing.Size(179, 109);
            // 
            // 
            // 
            this.gpBattle.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpBattle.Style.BackColorGradientAngle = 90;
            this.gpBattle.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpBattle.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpBattle.Style.BorderBottomWidth = 1;
            this.gpBattle.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpBattle.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpBattle.Style.BorderLeftWidth = 1;
            this.gpBattle.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpBattle.Style.BorderRightWidth = 1;
            this.gpBattle.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpBattle.Style.BorderTopWidth = 1;
            this.gpBattle.Style.Class = "";
            this.gpBattle.Style.CornerDiameter = 4;
            this.gpBattle.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpBattle.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpBattle.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpBattle.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpBattle.StyleMouseDown.Class = "";
            this.gpBattle.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpBattle.StyleMouseOver.Class = "";
            this.gpBattle.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpBattle.TabIndex = 25;
            this.gpBattle.Text = "Unlock Battle Items";
            // 
            // bUnlockAllImportantItems
            // 
            this.bUnlockAllImportantItems.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllImportantItems.AutoCheckOnClick = true;
            this.bUnlockAllImportantItems.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllImportantItems.FocusCuesEnabled = false;
            this.bUnlockAllImportantItems.Location = new System.Drawing.Point(-1, 62);
            this.bUnlockAllImportantItems.Name = "bUnlockAllImportantItems";
            this.bUnlockAllImportantItems.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllImportantItems.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllImportantItems.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllImportantItems.TabIndex = 28;
            this.bUnlockAllImportantItems.Text = "Important Items";
            // 
            // bUnlockAllMixingItems
            // 
            this.bUnlockAllMixingItems.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllMixingItems.AutoCheckOnClick = true;
            this.bUnlockAllMixingItems.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllMixingItems.FocusCuesEnabled = false;
            this.bUnlockAllMixingItems.Location = new System.Drawing.Point(-1, 33);
            this.bUnlockAllMixingItems.Name = "bUnlockAllMixingItems";
            this.bUnlockAllMixingItems.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllMixingItems.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllMixingItems.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllMixingItems.TabIndex = 27;
            this.bUnlockAllMixingItems.Text = "Mixing Items";
            // 
            // bUnlockAllCapsules
            // 
            this.bUnlockAllCapsules.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllCapsules.AutoCheckOnClick = true;
            this.bUnlockAllCapsules.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllCapsules.FocusCuesEnabled = false;
            this.bUnlockAllCapsules.Location = new System.Drawing.Point(-1, 3);
            this.bUnlockAllCapsules.Name = "bUnlockAllCapsules";
            this.bUnlockAllCapsules.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllCapsules.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllCapsules.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllCapsules.TabIndex = 26;
            this.bUnlockAllCapsules.Text = "Capsules";
            // 
            // bUnlockAllSkillSets
            // 
            this.bUnlockAllSkillSets.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUnlockAllSkillSets.AutoCheckOnClick = true;
            this.bUnlockAllSkillSets.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUnlockAllSkillSets.FocusCuesEnabled = false;
            this.bUnlockAllSkillSets.Location = new System.Drawing.Point(490, 315);
            this.bUnlockAllSkillSets.Name = "bUnlockAllSkillSets";
            this.bUnlockAllSkillSets.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.bUnlockAllSkillSets.Size = new System.Drawing.Size(179, 31);
            this.bUnlockAllSkillSets.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUnlockAllSkillSets.TabIndex = 25;
            this.bUnlockAllSkillSets.Text = "Skill Set";
            // 
            // groupPanel3
            // 
            this.groupPanel3.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.cmbPlayerIndex);
            this.groupPanel3.Controls.Add(this.btnMaxAllStats);
            this.groupPanel3.Controls.Add(this.labelX1);
            this.groupPanel3.Controls.Add(this.txtPlayerName);
            this.groupPanel3.Controls.Add(this.listPlayerStats);
            this.groupPanel3.Location = new System.Drawing.Point(175, 3);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(312, 343);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.Class = "";
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 26;
            this.groupPanel3.Text = "Character Stats and Attributes";
            // 
            // cmbPlayerIndex
            // 
            this.cmbPlayerIndex.DisplayMember = "Text";
            this.cmbPlayerIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPlayerIndex.FormattingEnabled = true;
            this.cmbPlayerIndex.ItemHeight = 14;
            this.cmbPlayerIndex.Location = new System.Drawing.Point(7, 9);
            this.cmbPlayerIndex.Name = "cmbPlayerIndex";
            this.cmbPlayerIndex.Size = new System.Drawing.Size(297, 20);
            this.cmbPlayerIndex.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPlayerIndex.TabIndex = 137;
            this.cmbPlayerIndex.SelectedIndexChanged += new System.EventHandler(this.CmbCharacterIndexChanged);
            // 
            // btnMaxAllStats
            // 
            this.btnMaxAllStats.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxAllStats.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxAllStats.FocusCuesEnabled = false;
            this.btnMaxAllStats.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxAllStats.Location = new System.Drawing.Point(203, 296);
            this.btnMaxAllStats.Name = "btnMaxAllStats";
            this.btnMaxAllStats.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxAllStats.Size = new System.Drawing.Size(101, 23);
            this.btnMaxAllStats.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxAllStats.TabIndex = 136;
            this.btnMaxAllStats.Text = "Max All";
            this.btnMaxAllStats.Click += new System.EventHandler(this.BtnClick_MaxAllStats);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(5, 296);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(37, 23);
            this.labelX1.TabIndex = 135;
            this.labelX1.Text = "Name:";
            // 
            // txtPlayerName
            // 
            // 
            // 
            // 
            this.txtPlayerName.Border.Class = "TextBoxBorder";
            this.txtPlayerName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPlayerName.Location = new System.Drawing.Point(48, 297);
            this.txtPlayerName.MaxLength = 32;
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(149, 20);
            this.txtPlayerName.TabIndex = 134;
            this.txtPlayerName.TextChanged += new System.EventHandler(this.TxtChanged_CharacterName);
            // 
            // listPlayerStats
            // 
            this.listPlayerStats.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listPlayerStats.AllowDrop = true;
            this.listPlayerStats.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listPlayerStats.BackgroundStyle.Class = "TreeBorderKey";
            this.listPlayerStats.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listPlayerStats.Columns.Add(this.columnHeader7);
            this.listPlayerStats.Columns.Add(this.columnHeader8);
            this.listPlayerStats.DoubleClickTogglesNode = false;
            this.listPlayerStats.DragDropEnabled = false;
            this.listPlayerStats.DragDropNodeCopyEnabled = false;
            this.listPlayerStats.ExpandButtonSize = new System.Drawing.Size(1, 15);
            this.listPlayerStats.ExpandWidth = 5;
            this.listPlayerStats.HotTracking = true;
            this.listPlayerStats.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listPlayerStats.Location = new System.Drawing.Point(5, 36);
            this.listPlayerStats.MultiNodeDragCountVisible = false;
            this.listPlayerStats.MultiNodeDragDropAllowed = false;
            this.listPlayerStats.Name = "listPlayerStats";
            this.listPlayerStats.NodeHorizontalSpacing = 5;
            this.listPlayerStats.NodesConnector = this.nodeConnector2;
            this.listPlayerStats.NodeSpacing = 5;
            this.listPlayerStats.NodeStyle = this.elementStyle2;
            this.listPlayerStats.PathSeparator = ";";
            this.listPlayerStats.Size = new System.Drawing.Size(299, 254);
            this.listPlayerStats.Styles.Add(this.elementStyle2);
            this.listPlayerStats.TabIndex = 133;
            // 
            // columnHeader7
            // 
            this.columnHeader7.MinimumWidth = 100;
            this.columnHeader7.Name = "columnHeader7";
            this.columnHeader7.Text = "Stat";
            this.columnHeader7.Width.Absolute = 170;
            // 
            // columnHeader8
            // 
            this.columnHeader8.EditorType = DevComponents.AdvTree.eCellEditorType.NumericDouble;
            this.columnHeader8.Name = "columnHeader8";
            this.columnHeader8.Text = "Value";
            this.columnHeader8.Width.Absolute = 110;
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
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.Color.Transparent;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.cmbTransplantChIndex);
            this.groupPanel1.Controls.Add(this.btnTransplantCh);
            this.groupPanel1.Controls.Add(this.chkUnlockChSlots);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Location = new System.Drawing.Point(7, 101);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(165, 245);
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
            this.groupPanel1.TabIndex = 26;
            this.groupPanel1.Text = "General";
            // 
            // cmbTransplantChIndex
            // 
            this.cmbTransplantChIndex.DisplayMember = "Text";
            this.cmbTransplantChIndex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbTransplantChIndex.FormattingEnabled = true;
            this.cmbTransplantChIndex.ItemHeight = 14;
            this.cmbTransplantChIndex.Location = new System.Drawing.Point(13, 44);
            this.cmbTransplantChIndex.Name = "cmbTransplantChIndex";
            this.cmbTransplantChIndex.Size = new System.Drawing.Size(133, 20);
            this.cmbTransplantChIndex.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbTransplantChIndex.TabIndex = 138;
            // 
            // btnTransplantCh
            // 
            this.btnTransplantCh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTransplantCh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTransplantCh.FocusCuesEnabled = false;
            this.btnTransplantCh.Image = global::Horizon.Properties.Resources.RightArrow;
            this.btnTransplantCh.Location = new System.Drawing.Point(13, 76);
            this.btnTransplantCh.Name = "btnTransplantCh";
            this.btnTransplantCh.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnTransplantCh.Size = new System.Drawing.Size(133, 50);
            this.btnTransplantCh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTransplantCh.TabIndex = 136;
            this.btnTransplantCh.Text = "Copy Current\r\n Character ";
            this.btnTransplantCh.Click += new System.EventHandler(this.BtnClick_TransplantCharacter);
            // 
            // chkUnlockChSlots
            // 
            this.chkUnlockChSlots.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.chkUnlockChSlots.AutoCheckOnClick = true;
            this.chkUnlockChSlots.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.chkUnlockChSlots.FocusCuesEnabled = false;
            this.chkUnlockChSlots.Image = global::Horizon.Properties.Resources.Star;
            this.chkUnlockChSlots.Location = new System.Drawing.Point(13, 125);
            this.chkUnlockChSlots.Name = "chkUnlockChSlots";
            this.chkUnlockChSlots.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.chkUnlockChSlots.Size = new System.Drawing.Size(133, 76);
            this.chkUnlockChSlots.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkUnlockChSlots.TabIndex = 136;
            this.chkUnlockChSlots.Text = "All Character Slots Unlocked";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(13, 14);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(133, 23);
            this.labelX3.TabIndex = 135;
            this.labelX3.Text = "New Character Index:";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.Color.Transparent;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.btnMaxZeni);
            this.groupPanel2.Controls.Add(this.intZeni);
            this.groupPanel2.Location = new System.Drawing.Point(7, 3);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(165, 92);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 27;
            this.groupPanel2.Text = "Zeni";
            // 
            // btnMaxZeni
            // 
            this.btnMaxZeni.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxZeni.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxZeni.FocusCuesEnabled = false;
            this.btnMaxZeni.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxZeni.Location = new System.Drawing.Point(4, 31);
            this.btnMaxZeni.Name = "btnMaxZeni";
            this.btnMaxZeni.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxZeni.Size = new System.Drawing.Size(156, 25);
            this.btnMaxZeni.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxZeni.TabIndex = 139;
            this.btnMaxZeni.Text = "Max Zeni";
            this.btnMaxZeni.Click += new System.EventHandler(this.BtnClick_MaxZeni);
            // 
            // intZeni
            // 
            // 
            // 
            // 
            this.intZeni.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intZeni.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intZeni.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intZeni.Location = new System.Drawing.Point(4, 6);
            this.intZeni.MinValue = 0;
            this.intZeni.Name = "intZeni";
            this.intZeni.ShowUpDown = true;
            this.intZeni.Size = new System.Drawing.Size(156, 20);
            this.intZeni.TabIndex = 140;
            // 
            // DragonballXenoVerse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 412);
            this.Name = "DragonballXenoVerse";
            this.Text = "Dragon Ball XenoVerse";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.gpEquipment.ResumeLayout(false);
            this.gpBattle.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listPlayerStats)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intZeni)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gpEquipment;
        private ButtonX bUnlockAllLowerBody;
        private ButtonX bUnlockAllUpperBody;
        private ButtonX bUnlockAllZSoul;
        private ButtonX bUnlockAllAccessory;
        private ButtonX bUnlockAllFeet;
        private ButtonX bUnlockAllHands;
        private DevComponents.DotNetBar.Controls.GroupPanel gpBattle;
        private ButtonX bUnlockAllMixingItems;
        private ButtonX bUnlockAllCapsules;
        private ButtonX bUnlockAllSkillSets;
        private ButtonX bUnlockAllImportantItems;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.AdvTree.AdvTree listPlayerStats;
        private DevComponents.AdvTree.ColumnHeader columnHeader7;
        private DevComponents.AdvTree.ColumnHeader columnHeader8;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private ElementStyle elementStyle2;
        private ButtonX btnMaxAllStats;
        private LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPlayerName;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private ButtonX btnTransplantCh;
        private ButtonX chkUnlockChSlots;
        private LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbTransplantChIndex;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbPlayerIndex;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private ButtonX btnMaxZeni;
        private DevComponents.Editors.IntegerInput intZeni;
    }
}
