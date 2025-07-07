namespace Horizon.PackageEditors.Diablo_III
{
    partial class DiabloIII
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
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdSetValue = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.listValues = new DevComponents.AdvTree.AdvTree();
            this.colSetting = new DevComponents.AdvTree.ColumnHeader();
            this.colValue = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rbPackageEditor.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
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
            this.rbPackageEditor.Size = new System.Drawing.Size(593, 487);
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
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel1, 0);
            // 
            // panelMain
            // 
            this.panelMain.Location = new System.Drawing.Point(0, 56);
            this.panelMain.Size = new System.Drawing.Size(593, 429);
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
            this.tabMain.Checked = false;
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Checked = true;
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "Preferences";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.textBoxX1);
            this.ribbonPanel1.Controls.Add(this.cmdSetValue);
            this.ribbonPanel1.Controls.Add(this.labelX2);
            this.ribbonPanel1.Controls.Add(this.listValues);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 56);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(593, 429);
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
            // 
            // cmdSetValue
            // 
            this.cmdSetValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSetValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSetValue.FocusCuesEnabled = false;
            this.cmdSetValue.Location = new System.Drawing.Point(520, 399);
            this.cmdSetValue.Name = "cmdSetValue";
            this.cmdSetValue.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSetValue.Size = new System.Drawing.Size(61, 20);
            this.cmdSetValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSetValue.TabIndex = 21;
            this.cmdSetValue.Text = "Set";
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 397);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(62, 21);
            this.labelX2.TabIndex = 20;
            this.labelX2.Text = "Edit Value:";
            // 
            // listValues
            // 
            this.listValues.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listValues.AllowDrop = true;
            this.listValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.listValues.ExpandWidth = 5;
            this.listValues.Indent = 5;
            this.listValues.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listValues.Location = new System.Drawing.Point(12, 13);
            this.listValues.MultiNodeDragDropAllowed = false;
            this.listValues.Name = "listValues";
            this.listValues.NodesConnector = this.nodeConnector1;
            this.listValues.NodeStyle = this.elementStyle1;
            this.listValues.PathSeparator = ";";
            this.listValues.Size = new System.Drawing.Size(569, 380);
            this.listValues.Styles.Add(this.elementStyle1);
            this.listValues.TabIndex = 19;
            // 
            // colSetting
            // 
            this.colSetting.DoubleClickAutoSize = false;
            this.colSetting.Editable = false;
            this.colSetting.Name = "colSetting";
            this.colSetting.Text = "Setting";
            this.colSetting.Width.Absolute = 200;
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
            // textBoxX1
            // 
            this.textBoxX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(74, 399);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(440, 20);
            this.textBoxX1.TabIndex = 23;
            // 
            // DiabloIII
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 490);
            this.Name = "DiabloIII";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.ButtonX cmdSetValue;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.AdvTree.AdvTree listValues;
        private DevComponents.AdvTree.ColumnHeader colSetting;
        private DevComponents.AdvTree.ColumnHeader colValue;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
    }
}
