namespace Horizon.PackageEditors.BioShock_Infinite
{
    partial class BioShockInfinite
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
            this.btnExtract = new DevComponents.DotNetBar.ButtonItem();
            this.listMain = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.btnInjectSaveData = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.btnUnlockAllGears = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.cmbSlotBox = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.listGears = new DevComponents.AdvTree.AdvTree();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader4 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.ribbonTabItem2 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
            this.txtGearDesc = new System.Windows.Forms.RichTextBox();
            this.listGearNames = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMain)).BeginInit();
            this.ribbonPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listGears)).BeginInit();
            this.ribbonPanel2.SuspendLayout();
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
            this.btnExtract,
            this.btnInjectSaveData});
            this.rbPackageEditor.Size = new System.Drawing.Size(365, 309);
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
            this.panelMain.Controls.Add(this.labelX2);
            this.panelMain.Controls.Add(this.listMain);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(365, 254);
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
            // btnExtract
            // 
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Text = "Extract";
            this.btnExtract.Visible = false;
            this.btnExtract.Click += new System.EventHandler(this.BtnClickExtractBuffer);
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
            this.listMain.Location = new System.Drawing.Point(3, 3);
            this.listMain.MultiNodeDragCountVisible = false;
            this.listMain.MultiNodeDragDropAllowed = false;
            this.listMain.Name = "listMain";
            this.listMain.NodesConnector = this.nodeConnector2;
            this.listMain.NodeStyle = this.elementStyle2;
            this.listMain.PathSeparator = ";";
            this.listMain.Size = new System.Drawing.Size(357, 202);
            this.listMain.Styles.Add(this.elementStyle2);
            this.listMain.TabIndex = 39;
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
            // btnInjectSaveData
            // 
            this.btnInjectSaveData.Name = "btnInjectSaveData";
            this.btnInjectSaveData.Text = "Inject";
            this.btnInjectSaveData.Visible = false;
            this.btnInjectSaveData.Click += new System.EventHandler(this.BtnClickInjectBuffer);
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "Gears";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.labelX3);
            this.ribbonPanel1.Controls.Add(this.btnUnlockAllGears);
            this.ribbonPanel1.Controls.Add(this.panelEx1);
            this.ribbonPanel1.Controls.Add(this.listGears);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(365, 254);
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
            // btnUnlockAllGears
            // 
            this.btnUnlockAllGears.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAllGears.AutoCheckOnClick = true;
            this.btnUnlockAllGears.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAllGears.FocusCuesEnabled = false;
            this.btnUnlockAllGears.Image = global::Horizon.Properties.Resources.Star;
            this.btnUnlockAllGears.Location = new System.Drawing.Point(182, 178);
            this.btnUnlockAllGears.Name = "btnUnlockAllGears";
            this.btnUnlockAllGears.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAllGears.Size = new System.Drawing.Size(179, 28);
            this.btnUnlockAllGears.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAllGears.TabIndex = 42;
            this.btnUnlockAllGears.Text = "Unlock All Gears";
            this.btnUnlockAllGears.Click += new System.EventHandler(this.BtnClickUnlockAllGears);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.cmbSlotBox);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Location = new System.Drawing.Point(4, 178);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(180, 28);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 41;
            // 
            // cmbSlotBox
            // 
            this.cmbSlotBox.DisplayMember = "Text";
            this.cmbSlotBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSlotBox.FormattingEnabled = true;
            this.cmbSlotBox.ItemHeight = 14;
            this.cmbSlotBox.Location = new System.Drawing.Point(62, 4);
            this.cmbSlotBox.Name = "cmbSlotBox";
            this.cmbSlotBox.Size = new System.Drawing.Size(110, 20);
            this.cmbSlotBox.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSlotBox.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(9, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(41, 19);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Slot:";
            // 
            // listGears
            // 
            this.listGears.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listGears.AllowDrop = true;
            this.listGears.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listGears.BackgroundStyle.Class = "TreeBorderKey";
            this.listGears.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listGears.Columns.Add(this.columnHeader3);
            this.listGears.Columns.Add(this.columnHeader4);
            this.listGears.DragDropEnabled = false;
            this.listGears.DragDropNodeCopyEnabled = false;
            this.listGears.HotTracking = true;
            this.listGears.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listGears.Location = new System.Drawing.Point(4, 3);
            this.listGears.MultiNodeDragCountVisible = false;
            this.listGears.MultiNodeDragDropAllowed = false;
            this.listGears.Name = "listGears";
            this.listGears.NodesConnector = this.nodeConnector1;
            this.listGears.NodeStyle = this.elementStyle1;
            this.listGears.PathSeparator = ";";
            this.listGears.Size = new System.Drawing.Size(357, 176);
            this.listGears.Styles.Add(this.elementStyle1);
            this.listGears.TabIndex = 40;
            this.listGears.SelectedIndexChanged += new System.EventHandler(this.GearsTreeSelectedNodeChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Gear Name";
            this.columnHeader3.Width.Absolute = 250;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Name = "columnHeader4";
            this.columnHeader4.Text = "Enabled";
            this.columnHeader4.Width.Absolute = 60;
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
            // ribbonTabItem2
            // 
            this.ribbonTabItem2.Name = "ribbonTabItem2";
            this.ribbonTabItem2.Panel = this.ribbonPanel2;
            this.ribbonTabItem2.Text = "Gears Info";
            this.ribbonTabItem2.Click += new System.EventHandler(this.GearInfoClicked);
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel2.Controls.Add(this.txtGearDesc);
            this.ribbonPanel2.Controls.Add(this.listGearNames);
            this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel2.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel2.Size = new System.Drawing.Size(365, 254);
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
            // txtGearDesc
            // 
            this.txtGearDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGearDesc.Location = new System.Drawing.Point(9, 112);
            this.txtGearDesc.Name = "txtGearDesc";
            this.txtGearDesc.Size = new System.Drawing.Size(348, 136);
            this.txtGearDesc.TabIndex = 3;
            this.txtGearDesc.Text = "";
            // 
            // listGearNames
            // 
            // 
            // 
            // 
            this.listGearNames.Border.Class = "ListViewBorder";
            this.listGearNames.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listGearNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5});
            this.listGearNames.FullRowSelect = true;
            this.listGearNames.GridLines = true;
            this.listGearNames.Location = new System.Drawing.Point(10, 5);
            this.listGearNames.MultiSelect = false;
            this.listGearNames.Name = "listGearNames";
            this.listGearNames.Size = new System.Drawing.Size(346, 102);
            this.listGearNames.TabIndex = 2;
            this.listGearNames.UseCompatibleStateImageBehavior = false;
            this.listGearNames.View = System.Windows.Forms.View.Details;
            this.listGearNames.SelectedIndexChanged += new System.EventHandler(this.GearsTitleListSelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Gear Name";
            this.columnHeader5.Width = 258;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(10, 211);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(350, 35);
            this.labelX2.TabIndex = 40;
            this.labelX2.Text = "<b>Notice:</b> Do not modify your save file if you have loaded the \"Clash of the " +
                "Clouds\" DLC. It will corrupt your save file.";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(8, 211);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(350, 35);
            this.labelX3.TabIndex = 43;
            this.labelX3.Text = "<b>Notice:</b> Do not modify your save file if you have loaded the \"Clash of the " +
                "Clouds\" DLC. It will corrupt your save file.";
            // 
            // BioShockInfinite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 312);
            this.Name = "BioShockInfinite";
            this.Text = "BioShock Infinite";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listMain)).EndInit();
            this.ribbonPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listGears)).EndInit();
            this.ribbonPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnExtract;
        private DevComponents.AdvTree.AdvTree listMain;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.ButtonItem btnInjectSaveData;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.AdvTree.AdvTree listGears;
        private DevComponents.AdvTree.ColumnHeader columnHeader3;
        private DevComponents.AdvTree.ColumnHeader columnHeader4;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSlotBox;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnUnlockAllGears;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel2;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem2;
        private System.Windows.Forms.RichTextBox txtGearDesc;
        private DevComponents.DotNetBar.Controls.ListViewEx listGearNames;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;

    }
}
