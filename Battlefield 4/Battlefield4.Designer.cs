namespace Horizon.PackageEditors.Battlefield_4
{
    partial class Battlefield4
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
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.comboCategory = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.floatValue = new DevComponents.Editors.DoubleInput();
            this.intValue = new DevComponents.Editors.IntegerInput();
            this.cmdSetValue = new DevComponents.DotNetBar.ButtonX();
            this.txtValue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.listValues = new DevComponents.AdvTree.AdvTree();
            this.colSetting = new DevComponents.AdvTree.ColumnHeader();
            this.colType = new DevComponents.AdvTree.ColumnHeader();
            this.colValue = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.floatValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).BeginInit();
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
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabItem1});
            this.rbPackageEditor.Size = new System.Drawing.Size(487, 393);
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
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel1, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonX1);
            this.panelMain.Location = new System.Drawing.Point(0, 56);
            this.panelMain.Size = new System.Drawing.Size(487, 335);
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
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "Values";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.comboCategory);
            this.ribbonPanel1.Controls.Add(this.labelX1);
            this.ribbonPanel1.Controls.Add(this.floatValue);
            this.ribbonPanel1.Controls.Add(this.intValue);
            this.ribbonPanel1.Controls.Add(this.cmdSetValue);
            this.ribbonPanel1.Controls.Add(this.txtValue);
            this.ribbonPanel1.Controls.Add(this.labelX2);
            this.ribbonPanel1.Controls.Add(this.listValues);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 56);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(487, 335);
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
            // comboCategory
            // 
            this.comboCategory.DisplayMember = "Text";
            this.comboCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCategory.FormattingEnabled = true;
            this.comboCategory.ItemHeight = 14;
            this.comboCategory.Location = new System.Drawing.Point(91, 13);
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.Size = new System.Drawing.Size(382, 20);
            this.comboCategory.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboCategory.TabIndex = 35;
            this.comboCategory.SelectedIndexChanged += new System.EventHandler(this.comboCategory_SelectedIndexChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(10, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 21);
            this.labelX1.TabIndex = 34;
            this.labelX1.Text = "Category:";
            // 
            // floatValue
            // 
            // 
            // 
            // 
            this.floatValue.BackgroundStyle.Class = "DateTimeInputBackground";
            this.floatValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.floatValue.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.floatValue.Increment = 1;
            this.floatValue.Location = new System.Drawing.Point(78, 302);
            this.floatValue.MaxValue = 3.402823E+38;
            this.floatValue.MinValue = -3.402823E+38;
            this.floatValue.Name = "floatValue";
            this.floatValue.ShowUpDown = true;
            this.floatValue.Size = new System.Drawing.Size(331, 20);
            this.floatValue.TabIndex = 33;
            // 
            // intValue
            // 
            // 
            // 
            // 
            this.intValue.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intValue.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intValue.Location = new System.Drawing.Point(78, 302);
            this.intValue.Name = "intValue";
            this.intValue.ShowUpDown = true;
            this.intValue.Size = new System.Drawing.Size(331, 20);
            this.intValue.TabIndex = 32;
            // 
            // cmdSetValue
            // 
            this.cmdSetValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSetValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSetValue.FocusCuesEnabled = false;
            this.cmdSetValue.Location = new System.Drawing.Point(415, 302);
            this.cmdSetValue.Name = "cmdSetValue";
            this.cmdSetValue.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSetValue.Size = new System.Drawing.Size(61, 20);
            this.cmdSetValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSetValue.TabIndex = 31;
            this.cmdSetValue.Text = "Set";
            this.cmdSetValue.Click += new System.EventHandler(this.cmdSetValue_Click);
            // 
            // txtValue
            // 
            // 
            // 
            // 
            this.txtValue.Border.Class = "TextBoxBorder";
            this.txtValue.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtValue.Location = new System.Drawing.Point(78, 302);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(331, 20);
            this.txtValue.TabIndex = 30;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(16, 301);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 21);
            this.labelX2.TabIndex = 29;
            this.labelX2.Text = "Edit Value:";
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
            this.listValues.Columns.Add(this.colType);
            this.listValues.Columns.Add(this.colValue);
            this.listValues.DragDropEnabled = false;
            this.listValues.DragDropNodeCopyEnabled = false;
            this.listValues.ExpandWidth = 5;
            this.listValues.Indent = 5;
            this.listValues.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listValues.Location = new System.Drawing.Point(10, 39);
            this.listValues.MultiNodeDragDropAllowed = false;
            this.listValues.Name = "listValues";
            this.listValues.NodesConnector = this.nodeConnector1;
            this.listValues.NodeStyle = this.elementStyle1;
            this.listValues.PathSeparator = ";";
            this.listValues.Size = new System.Drawing.Size(466, 259);
            this.listValues.Styles.Add(this.elementStyle1);
            this.listValues.TabIndex = 28;
            this.listValues.SelectedIndexChanged += new System.EventHandler(this.listValues_SelectedIndexChanged);
            // 
            // colSetting
            // 
            this.colSetting.DoubleClickAutoSize = false;
            this.colSetting.Editable = false;
            this.colSetting.Name = "colSetting";
            this.colSetting.Text = "Setting";
            this.colSetting.Width.Absolute = 200;
            // 
            // colType
            // 
            this.colType.Editable = false;
            this.colType.Name = "colType";
            this.colType.Text = "Type";
            this.colType.Width.Absolute = 70;
            // 
            // colValue
            // 
            this.colValue.DoubleClickAutoSize = false;
            this.colValue.Editable = false;
            this.colValue.MaxInputLength = 50;
            this.colValue.Name = "colValue";
            this.colValue.Text = "Value";
            this.colValue.Width.Absolute = 120;
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
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(16, 13);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(142, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "Extract body data";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // Battlefield4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 396);
            this.Name = "Battlefield4";
            this.Text = "Battlefield4";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.floatValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboCategory;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.DoubleInput floatValue;
        private DevComponents.Editors.IntegerInput intValue;
        private DevComponents.DotNetBar.ButtonX cmdSetValue;
        private DevComponents.DotNetBar.Controls.TextBoxX txtValue;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.AdvTree.AdvTree listValues;
        private DevComponents.AdvTree.ColumnHeader colSetting;
        private DevComponents.AdvTree.ColumnHeader colType;
        private DevComponents.AdvTree.ColumnHeader colValue;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.DotNetBar.ButtonX buttonX1;

    }
}