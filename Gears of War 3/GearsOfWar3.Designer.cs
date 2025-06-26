namespace Horizon.PackageEditors.Gears_of_War_3
{
    partial class GearsOfWar3
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
            this.cmdExtractStructs = new DevComponents.DotNetBar.ButtonItem();
            this.comboSaves = new DevComponents.DotNetBar.ComboBoxItem();
            this.listChars = new DevComponents.AdvTree.AdvTree();
            this.colCharacters = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.panelChar = new DevComponents.DotNetBar.PanelEx();
            this.lblZ = new DevComponents.DotNetBar.LabelX();
            this.lblY = new DevComponents.DotNetBar.LabelX();
            this.lblX = new DevComponents.DotNetBar.LabelX();
            this.floatZ = new DevComponents.Editors.DoubleInput();
            this.floatY = new DevComponents.Editors.DoubleInput();
            this.floatX = new DevComponents.Editors.DoubleInput();
            this.lblLocation = new DevComponents.DotNetBar.LabelX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblName = new DevComponents.DotNetBar.LabelX();
            this.rbWeapon4 = new System.Windows.Forms.RadioButton();
            this.rbWeapon3 = new System.Windows.Forms.RadioButton();
            this.rbWeapon2 = new System.Windows.Forms.RadioButton();
            this.rbWeapon1 = new System.Windows.Forms.RadioButton();
            this.cbModel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblModel = new DevComponents.DotNetBar.LabelX();
            this.cmdMaxAmmo = new DevComponents.DotNetBar.ButtonX();
            this.intAmmo4 = new DevComponents.Editors.IntegerInput();
            this.intAmmo3 = new DevComponents.Editors.IntegerInput();
            this.intAmmo2 = new DevComponents.Editors.IntegerInput();
            this.intAmmo1 = new DevComponents.Editors.IntegerInput();
            this.cbWeapon4 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbWeapon3 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbWeapon2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbWeapon1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.lblSlot4 = new DevComponents.DotNetBar.LabelX();
            this.lblSlot3 = new DevComponents.DotNetBar.LabelX();
            this.lblSlot2 = new DevComponents.DotNetBar.LabelX();
            this.lblSlot1 = new DevComponents.DotNetBar.LabelX();
            this.cmdInfo = new DevComponents.DotNetBar.ButtonX();
            this.cmdExtractState = new DevComponents.DotNetBar.ButtonItem();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listChars)).BeginInit();
            this.panelChar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.floatZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo1)).BeginInit();
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
            this.comboSaves,
            this.cmdExtractStructs,
            this.cmdExtractState});
            this.rbPackageEditor.Size = new System.Drawing.Size(570, 261);
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
            this.panelMain.Controls.Add(this.cmdInfo);
            this.panelMain.Controls.Add(this.panelChar);
            this.panelMain.Controls.Add(this.listChars);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(570, 206);
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
            this.tabMain.Text = "Character Data";
            // 
            // cmdExtractStructs
            // 
            this.cmdExtractStructs.Name = "cmdExtractStructs";
            this.cmdExtractStructs.Text = "Extract Structs";
            this.cmdExtractStructs.Visible = false;
            this.cmdExtractStructs.Click += new System.EventHandler(this.cmdExtractStructs_Click);
            // 
            // comboSaves
            // 
            this.comboSaves.ComboWidth = 100;
            this.comboSaves.DropDownHeight = 106;
            this.comboSaves.DropDownWidth = 100;
            this.comboSaves.Name = "comboSaves";
            this.comboSaves.SelectedIndexChanged += new System.EventHandler(this.comboSaves_SelectedIndexChanged);
            // 
            // listChars
            // 
            this.listChars.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listChars.AllowDrop = true;
            this.listChars.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listChars.BackgroundStyle.Class = "TreeBorderKey";
            this.listChars.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listChars.Columns.Add(this.colCharacters);
            this.listChars.ExpandWidth = 2;
            this.listChars.GridColumnLines = false;
            this.listChars.GridRowLines = true;
            this.listChars.HotTracking = true;
            this.listChars.Indent = 0;
            this.listChars.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listChars.Location = new System.Drawing.Point(6, 3);
            this.listChars.Name = "listChars";
            this.listChars.NodesConnector = this.nodeConnector1;
            this.listChars.NodeStyle = this.elementStyle1;
            this.listChars.PathSeparator = ";";
            this.listChars.Size = new System.Drawing.Size(139, 162);
            this.listChars.Styles.Add(this.elementStyle1);
            this.listChars.TabIndex = 0;
            this.listChars.SelectedIndexChanged += new System.EventHandler(this.listChars_SelectedIndexChanged);
            // 
            // colCharacters
            // 
            this.colCharacters.Name = "colCharacters";
            this.colCharacters.Text = "Characters";
            this.colCharacters.Width.Absolute = 118;
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
            // panelChar
            // 
            this.panelChar.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelChar.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelChar.Controls.Add(this.lblZ);
            this.panelChar.Controls.Add(this.lblY);
            this.panelChar.Controls.Add(this.lblX);
            this.panelChar.Controls.Add(this.floatZ);
            this.panelChar.Controls.Add(this.floatY);
            this.panelChar.Controls.Add(this.floatX);
            this.panelChar.Controls.Add(this.lblLocation);
            this.panelChar.Controls.Add(this.txtName);
            this.panelChar.Controls.Add(this.lblName);
            this.panelChar.Controls.Add(this.rbWeapon4);
            this.panelChar.Controls.Add(this.rbWeapon3);
            this.panelChar.Controls.Add(this.rbWeapon2);
            this.panelChar.Controls.Add(this.rbWeapon1);
            this.panelChar.Controls.Add(this.cbModel);
            this.panelChar.Controls.Add(this.lblModel);
            this.panelChar.Controls.Add(this.cmdMaxAmmo);
            this.panelChar.Controls.Add(this.intAmmo4);
            this.panelChar.Controls.Add(this.intAmmo3);
            this.panelChar.Controls.Add(this.intAmmo2);
            this.panelChar.Controls.Add(this.intAmmo1);
            this.panelChar.Controls.Add(this.cbWeapon4);
            this.panelChar.Controls.Add(this.cbWeapon3);
            this.panelChar.Controls.Add(this.cbWeapon2);
            this.panelChar.Controls.Add(this.cbWeapon1);
            this.panelChar.Controls.Add(this.lblSlot4);
            this.panelChar.Controls.Add(this.lblSlot3);
            this.panelChar.Controls.Add(this.lblSlot2);
            this.panelChar.Controls.Add(this.lblSlot1);
            this.panelChar.Location = new System.Drawing.Point(151, 3);
            this.panelChar.Name = "panelChar";
            this.panelChar.Size = new System.Drawing.Size(412, 195);
            this.panelChar.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelChar.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelChar.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelChar.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelChar.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelChar.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelChar.Style.GradientAngle = 90;
            this.panelChar.TabIndex = 1;
            // 
            // lblZ
            // 
            // 
            // 
            // 
            this.lblZ.BackgroundStyle.Class = "";
            this.lblZ.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblZ.Location = new System.Drawing.Point(278, 168);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(13, 20);
            this.lblZ.TabIndex = 27;
            this.lblZ.Text = "Z";
            this.lblZ.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblY
            // 
            // 
            // 
            // 
            this.lblY.BackgroundStyle.Class = "";
            this.lblY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblY.Location = new System.Drawing.Point(180, 168);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(13, 20);
            this.lblY.TabIndex = 26;
            this.lblY.Text = "Y";
            this.lblY.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblX
            // 
            // 
            // 
            // 
            this.lblX.BackgroundStyle.Class = "";
            this.lblX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblX.Location = new System.Drawing.Point(79, 168);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(13, 20);
            this.lblX.TabIndex = 25;
            this.lblX.Text = "X";
            this.lblX.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // floatZ
            // 
            // 
            // 
            // 
            this.floatZ.BackgroundStyle.Class = "DateTimeInputBackground";
            this.floatZ.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.floatZ.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.floatZ.Increment = 1;
            this.floatZ.Location = new System.Drawing.Point(297, 168);
            this.floatZ.Name = "floatZ";
            this.floatZ.ShowUpDown = true;
            this.floatZ.Size = new System.Drawing.Size(90, 20);
            this.floatZ.TabIndex = 24;
            // 
            // floatY
            // 
            // 
            // 
            // 
            this.floatY.BackgroundStyle.Class = "DateTimeInputBackground";
            this.floatY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.floatY.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.floatY.Increment = 1;
            this.floatY.Location = new System.Drawing.Point(199, 168);
            this.floatY.Name = "floatY";
            this.floatY.ShowUpDown = true;
            this.floatY.Size = new System.Drawing.Size(73, 20);
            this.floatY.TabIndex = 23;
            // 
            // floatX
            // 
            // 
            // 
            // 
            this.floatX.BackgroundStyle.Class = "DateTimeInputBackground";
            this.floatX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.floatX.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.floatX.Increment = 1;
            this.floatX.Location = new System.Drawing.Point(98, 168);
            this.floatX.Name = "floatX";
            this.floatX.ShowUpDown = true;
            this.floatX.Size = new System.Drawing.Size(76, 20);
            this.floatX.TabIndex = 22;
            // 
            // lblLocation
            // 
            // 
            // 
            // 
            this.lblLocation.BackgroundStyle.Class = "";
            this.lblLocation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblLocation.Location = new System.Drawing.Point(12, 168);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(61, 20);
            this.lblLocation.TabIndex = 21;
            this.lblLocation.Text = "Location:";
            this.lblLocation.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.Location = new System.Drawing.Point(79, 116);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(212, 20);
            this.txtName.TabIndex = 20;
            this.txtName.WatermarkText = "Enter a Name for this Character";
            // 
            // lblName
            // 
            // 
            // 
            // 
            this.lblName.BackgroundStyle.Class = "";
            this.lblName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblName.Location = new System.Drawing.Point(12, 116);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(61, 20);
            this.lblName.TabIndex = 19;
            this.lblName.Text = "Name:";
            this.lblName.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // rbWeapon4
            // 
            this.rbWeapon4.Location = new System.Drawing.Point(393, 90);
            this.rbWeapon4.Name = "rbWeapon4";
            this.rbWeapon4.Size = new System.Drawing.Size(14, 20);
            this.rbWeapon4.TabIndex = 18;
            this.rbWeapon4.TabStop = true;
            this.rbWeapon4.UseVisualStyleBackColor = true;
            // 
            // rbWeapon3
            // 
            this.rbWeapon3.Location = new System.Drawing.Point(393, 64);
            this.rbWeapon3.Name = "rbWeapon3";
            this.rbWeapon3.Size = new System.Drawing.Size(14, 20);
            this.rbWeapon3.TabIndex = 17;
            this.rbWeapon3.TabStop = true;
            this.rbWeapon3.UseVisualStyleBackColor = true;
            // 
            // rbWeapon2
            // 
            this.rbWeapon2.Location = new System.Drawing.Point(393, 38);
            this.rbWeapon2.Name = "rbWeapon2";
            this.rbWeapon2.Size = new System.Drawing.Size(14, 20);
            this.rbWeapon2.TabIndex = 16;
            this.rbWeapon2.TabStop = true;
            this.rbWeapon2.UseVisualStyleBackColor = true;
            // 
            // rbWeapon1
            // 
            this.rbWeapon1.Location = new System.Drawing.Point(393, 12);
            this.rbWeapon1.Name = "rbWeapon1";
            this.rbWeapon1.Size = new System.Drawing.Size(14, 20);
            this.rbWeapon1.TabIndex = 15;
            this.rbWeapon1.TabStop = true;
            this.rbWeapon1.UseVisualStyleBackColor = true;
            // 
            // cbModel
            // 
            this.cbModel.DisplayMember = "Text";
            this.cbModel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModel.FocusCuesEnabled = false;
            this.cbModel.FormattingEnabled = true;
            this.cbModel.ItemHeight = 14;
            this.cbModel.Location = new System.Drawing.Point(79, 142);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(212, 20);
            this.cbModel.Sorted = true;
            this.cbModel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbModel.TabIndex = 14;
            // 
            // lblModel
            // 
            // 
            // 
            // 
            this.lblModel.BackgroundStyle.Class = "";
            this.lblModel.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblModel.Location = new System.Drawing.Point(12, 142);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(61, 20);
            this.lblModel.TabIndex = 13;
            this.lblModel.Text = "Model:";
            this.lblModel.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cmdMaxAmmo
            // 
            this.cmdMaxAmmo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxAmmo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxAmmo.FocusCuesEnabled = false;
            this.cmdMaxAmmo.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMaxAmmo.Location = new System.Drawing.Point(297, 116);
            this.cmdMaxAmmo.Name = "cmdMaxAmmo";
            this.cmdMaxAmmo.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxAmmo.Size = new System.Drawing.Size(90, 46);
            this.cmdMaxAmmo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxAmmo.TabIndex = 12;
            this.cmdMaxAmmo.Text = "Max Ammo";
            this.cmdMaxAmmo.Click += new System.EventHandler(this.cmdMaxAmmo_Click);
            // 
            // intAmmo4
            // 
            // 
            // 
            // 
            this.intAmmo4.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intAmmo4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intAmmo4.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intAmmo4.Location = new System.Drawing.Point(297, 90);
            this.intAmmo4.MaxValue = 10000;
            this.intAmmo4.MinValue = 0;
            this.intAmmo4.Name = "intAmmo4";
            this.intAmmo4.ShowUpDown = true;
            this.intAmmo4.Size = new System.Drawing.Size(90, 20);
            this.intAmmo4.TabIndex = 11;
            // 
            // intAmmo3
            // 
            // 
            // 
            // 
            this.intAmmo3.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intAmmo3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intAmmo3.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intAmmo3.Location = new System.Drawing.Point(297, 64);
            this.intAmmo3.MaxValue = 10000;
            this.intAmmo3.MinValue = 0;
            this.intAmmo3.Name = "intAmmo3";
            this.intAmmo3.ShowUpDown = true;
            this.intAmmo3.Size = new System.Drawing.Size(90, 20);
            this.intAmmo3.TabIndex = 10;
            // 
            // intAmmo2
            // 
            // 
            // 
            // 
            this.intAmmo2.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intAmmo2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intAmmo2.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intAmmo2.Location = new System.Drawing.Point(297, 38);
            this.intAmmo2.MaxValue = 10000;
            this.intAmmo2.MinValue = 0;
            this.intAmmo2.Name = "intAmmo2";
            this.intAmmo2.ShowUpDown = true;
            this.intAmmo2.Size = new System.Drawing.Size(90, 20);
            this.intAmmo2.TabIndex = 9;
            // 
            // intAmmo1
            // 
            // 
            // 
            // 
            this.intAmmo1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intAmmo1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intAmmo1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intAmmo1.Location = new System.Drawing.Point(297, 12);
            this.intAmmo1.MaxValue = 10000;
            this.intAmmo1.MinValue = 0;
            this.intAmmo1.Name = "intAmmo1";
            this.intAmmo1.ShowUpDown = true;
            this.intAmmo1.Size = new System.Drawing.Size(90, 20);
            this.intAmmo1.TabIndex = 8;
            // 
            // cbWeapon4
            // 
            this.cbWeapon4.DisplayMember = "Text";
            this.cbWeapon4.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbWeapon4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeapon4.FocusCuesEnabled = false;
            this.cbWeapon4.FormattingEnabled = true;
            this.cbWeapon4.ItemHeight = 14;
            this.cbWeapon4.Location = new System.Drawing.Point(79, 90);
            this.cbWeapon4.Name = "cbWeapon4";
            this.cbWeapon4.Size = new System.Drawing.Size(212, 20);
            this.cbWeapon4.Sorted = true;
            this.cbWeapon4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbWeapon4.TabIndex = 7;
            this.cbWeapon4.SelectedIndexChanged += new System.EventHandler(this.cbWeapon4_SelectedIndexChanged);
            // 
            // cbWeapon3
            // 
            this.cbWeapon3.DisplayMember = "Text";
            this.cbWeapon3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbWeapon3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeapon3.FocusCuesEnabled = false;
            this.cbWeapon3.FormattingEnabled = true;
            this.cbWeapon3.ItemHeight = 14;
            this.cbWeapon3.Location = new System.Drawing.Point(79, 64);
            this.cbWeapon3.Name = "cbWeapon3";
            this.cbWeapon3.Size = new System.Drawing.Size(212, 20);
            this.cbWeapon3.Sorted = true;
            this.cbWeapon3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbWeapon3.TabIndex = 6;
            this.cbWeapon3.SelectedIndexChanged += new System.EventHandler(this.cbWeapon3_SelectedIndexChanged);
            // 
            // cbWeapon2
            // 
            this.cbWeapon2.DisplayMember = "Text";
            this.cbWeapon2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbWeapon2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeapon2.FocusCuesEnabled = false;
            this.cbWeapon2.FormattingEnabled = true;
            this.cbWeapon2.ItemHeight = 14;
            this.cbWeapon2.Location = new System.Drawing.Point(79, 38);
            this.cbWeapon2.Name = "cbWeapon2";
            this.cbWeapon2.Size = new System.Drawing.Size(212, 20);
            this.cbWeapon2.Sorted = true;
            this.cbWeapon2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbWeapon2.TabIndex = 5;
            this.cbWeapon2.SelectedIndexChanged += new System.EventHandler(this.cbWeapon2_SelectedIndexChanged);
            // 
            // cbWeapon1
            // 
            this.cbWeapon1.DisplayMember = "Text";
            this.cbWeapon1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbWeapon1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeapon1.FocusCuesEnabled = false;
            this.cbWeapon1.FormattingEnabled = true;
            this.cbWeapon1.ItemHeight = 14;
            this.cbWeapon1.Location = new System.Drawing.Point(79, 12);
            this.cbWeapon1.Name = "cbWeapon1";
            this.cbWeapon1.Size = new System.Drawing.Size(212, 20);
            this.cbWeapon1.Sorted = true;
            this.cbWeapon1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbWeapon1.TabIndex = 4;
            this.cbWeapon1.SelectedIndexChanged += new System.EventHandler(this.cbWeapon1_SelectedIndexChanged);
            // 
            // lblSlot4
            // 
            // 
            // 
            // 
            this.lblSlot4.BackgroundStyle.Class = "";
            this.lblSlot4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSlot4.Location = new System.Drawing.Point(12, 90);
            this.lblSlot4.Name = "lblSlot4";
            this.lblSlot4.Size = new System.Drawing.Size(61, 20);
            this.lblSlot4.TabIndex = 3;
            this.lblSlot4.Text = "Right Slot:";
            this.lblSlot4.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblSlot3
            // 
            // 
            // 
            // 
            this.lblSlot3.BackgroundStyle.Class = "";
            this.lblSlot3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSlot3.Location = new System.Drawing.Point(12, 64);
            this.lblSlot3.Name = "lblSlot3";
            this.lblSlot3.Size = new System.Drawing.Size(61, 20);
            this.lblSlot3.TabIndex = 2;
            this.lblSlot3.Text = "Bottom Slot:";
            this.lblSlot3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblSlot2
            // 
            // 
            // 
            // 
            this.lblSlot2.BackgroundStyle.Class = "";
            this.lblSlot2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSlot2.Location = new System.Drawing.Point(12, 38);
            this.lblSlot2.Name = "lblSlot2";
            this.lblSlot2.Size = new System.Drawing.Size(61, 20);
            this.lblSlot2.TabIndex = 1;
            this.lblSlot2.Text = "Left Slot:";
            this.lblSlot2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblSlot1
            // 
            // 
            // 
            // 
            this.lblSlot1.BackgroundStyle.Class = "";
            this.lblSlot1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSlot1.Location = new System.Drawing.Point(12, 12);
            this.lblSlot1.Name = "lblSlot1";
            this.lblSlot1.Size = new System.Drawing.Size(61, 20);
            this.lblSlot1.TabIndex = 0;
            this.lblSlot1.Text = "Top Slot:";
            this.lblSlot1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cmdInfo
            // 
            this.cmdInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdInfo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdInfo.FocusCuesEnabled = false;
            this.cmdInfo.Image = global::Horizon.Properties.Resources.Info;
            this.cmdInfo.Location = new System.Drawing.Point(6, 171);
            this.cmdInfo.Name = "cmdInfo";
            this.cmdInfo.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdInfo.Size = new System.Drawing.Size(139, 27);
            this.cmdInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdInfo.TabIndex = 13;
            this.cmdInfo.Text = "Notice";
            this.cmdInfo.Click += new System.EventHandler(this.cmdInfo_Click);
            // 
            // cmdExtractState
            // 
            this.cmdExtractState.Name = "cmdExtractState";
            this.cmdExtractState.Text = "Extract State";
            this.cmdExtractState.Visible = false;
            this.cmdExtractState.Click += new System.EventHandler(this.cmdExtractState_Click);
            // 
            // GearsOfWar3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 264);
            this.Name = "GearsOfWar3";
            this.Text = "Gears of War 3 Campaign and Co-Op Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listChars)).EndInit();
            this.panelChar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.floatZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intAmmo1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem cmdExtractStructs;
        private DevComponents.DotNetBar.ComboBoxItem comboSaves;
        private DevComponents.AdvTree.AdvTree listChars;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.PanelEx panelChar;
        private DevComponents.DotNetBar.LabelX lblSlot4;
        private DevComponents.DotNetBar.LabelX lblSlot3;
        private DevComponents.DotNetBar.LabelX lblSlot2;
        private DevComponents.DotNetBar.LabelX lblSlot1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbWeapon4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbWeapon3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbWeapon2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbWeapon1;
        private DevComponents.DotNetBar.ButtonX cmdMaxAmmo;
        private DevComponents.Editors.IntegerInput intAmmo4;
        private DevComponents.Editors.IntegerInput intAmmo3;
        private DevComponents.Editors.IntegerInput intAmmo2;
        private DevComponents.Editors.IntegerInput intAmmo1;
        private DevComponents.AdvTree.ColumnHeader colCharacters;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbModel;
        private DevComponents.DotNetBar.LabelX lblModel;
        private System.Windows.Forms.RadioButton rbWeapon4;
        private System.Windows.Forms.RadioButton rbWeapon3;
        private System.Windows.Forms.RadioButton rbWeapon2;
        private System.Windows.Forms.RadioButton rbWeapon1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX lblName;
        private DevComponents.DotNetBar.LabelX lblLocation;
        private DevComponents.DotNetBar.LabelX lblZ;
        private DevComponents.DotNetBar.LabelX lblY;
        private DevComponents.DotNetBar.LabelX lblX;
        private DevComponents.Editors.DoubleInput floatZ;
        private DevComponents.Editors.DoubleInput floatY;
        private DevComponents.Editors.DoubleInput floatX;
        private DevComponents.DotNetBar.ButtonX cmdInfo;
        private DevComponents.DotNetBar.ButtonItem cmdExtractState;
    }
}
