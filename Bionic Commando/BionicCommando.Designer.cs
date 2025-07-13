namespace Horizon.PackageEditors.Bionic_Commando
{
    partial class BionicCommando
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
            this.listValues = new DevComponents.AdvTree.AdvTree();
            this.colSetting = new DevComponents.AdvTree.ColumnHeader();
            this.colType = new DevComponents.AdvTree.ColumnHeader();
            this.colValue = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.cmdSetValue = new DevComponents.DotNetBar.ButtonX();
            this.txtValue = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmbxValue = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.intValue = new DevComponents.Editors.IntegerInput();
            this.floatValue = new DevComponents.Editors.DoubleInput();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatValue)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(477, 347);
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
            this.panelMain.Controls.Add(this.floatValue);
            this.panelMain.Controls.Add(this.intValue);
            this.panelMain.Controls.Add(this.cmbxValue);
            this.panelMain.Controls.Add(this.cmdSetValue);
            this.panelMain.Controls.Add(this.txtValue);
            this.panelMain.Controls.Add(this.labelX2);
            this.panelMain.Controls.Add(this.listValues);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(477, 292);
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
            this.listValues.Location = new System.Drawing.Point(6, 4);
            this.listValues.MultiNodeDragDropAllowed = false;
            this.listValues.Name = "listValues";
            this.listValues.NodesConnector = this.nodeConnector1;
            this.listValues.NodeStyle = this.elementStyle1;
            this.listValues.PathSeparator = ";";
            this.listValues.Size = new System.Drawing.Size(466, 259);
            this.listValues.Styles.Add(this.elementStyle1);
            this.listValues.TabIndex = 3;
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
            // cmdSetValue
            // 
            this.cmdSetValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSetValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSetValue.FocusCuesEnabled = false;
            this.cmdSetValue.Location = new System.Drawing.Point(411, 267);
            this.cmdSetValue.Name = "cmdSetValue";
            this.cmdSetValue.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSetValue.Size = new System.Drawing.Size(61, 20);
            this.cmdSetValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSetValue.TabIndex = 15;
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
            this.txtValue.Location = new System.Drawing.Point(74, 267);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(331, 20);
            this.txtValue.TabIndex = 14;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 266);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 21);
            this.labelX2.TabIndex = 13;
            this.labelX2.Text = "Edit Value:";
            // 
            // cmbxValue
            // 
            this.cmbxValue.DisplayMember = "Text";
            this.cmbxValue.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbxValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxValue.FocusCuesEnabled = false;
            this.cmbxValue.FormattingEnabled = true;
            this.cmbxValue.ItemHeight = 14;
            this.cmbxValue.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
            this.cmbxValue.Location = new System.Drawing.Point(74, 267);
            this.cmbxValue.Name = "cmbxValue";
            this.cmbxValue.Size = new System.Drawing.Size(331, 20);
            this.cmbxValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbxValue.TabIndex = 16;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "False";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "True";
            // 
            // intValue
            // 
            // 
            // 
            // 
            this.intValue.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intValue.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intValue.Location = new System.Drawing.Point(74, 267);
            this.intValue.Name = "intValue";
            this.intValue.ShowUpDown = true;
            this.intValue.Size = new System.Drawing.Size(331, 20);
            this.intValue.TabIndex = 17;
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
            this.floatValue.Location = new System.Drawing.Point(74, 267);
            this.floatValue.MaxValue = 3.402823E+38;
            this.floatValue.MinValue = -3.402823E+38;
            this.floatValue.Name = "floatValue";
            this.floatValue.ShowUpDown = true;
            this.floatValue.Size = new System.Drawing.Size(331, 20);
            this.floatValue.TabIndex = 18;
            // 
            // BionicCommando
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 350);
            this.Name = "BionicCommando";
            this.Text = "Bionic Commando Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.floatValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree listValues;
        private DevComponents.AdvTree.ColumnHeader colSetting;
        private DevComponents.AdvTree.ColumnHeader colValue;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.ColumnHeader colType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbxValue;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.DotNetBar.ButtonX cmdSetValue;
        private DevComponents.DotNetBar.Controls.TextBoxX txtValue;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DoubleInput floatValue;
        private DevComponents.Editors.IntegerInput intValue;
    }
}
