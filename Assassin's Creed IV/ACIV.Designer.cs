namespace Horizon.PackageEditors.Assassin_s_Creed_IV
{
    partial class ACIV
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
            this.btnInjectData = new DevComponents.DotNetBar.ButtonItem();
            this.listInventory = new DevComponents.AdvTree.AdvTree();
            this.columnHeader5 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader6 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.cmdMaxAllItems = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listInventory)).BeginInit();
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
            this.btnInjectData});
            this.rbPackageEditor.Size = new System.Drawing.Size(329, 338);
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
            this.panelMain.Controls.Add(this.cmdMaxAllItems);
            this.panelMain.Controls.Add(this.listInventory);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(329, 283);
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
            // btnInjectData
            // 
            this.btnInjectData.Name = "btnInjectData";
            this.btnInjectData.Text = "Inject";
            this.btnInjectData.Visible = false;
            this.btnInjectData.Click += new System.EventHandler(this.BtnClick_InjectData);
            // 
            // listInventory
            // 
            this.listInventory.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listInventory.AllowDrop = true;
            this.listInventory.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listInventory.BackgroundStyle.Class = "TreeBorderKey";
            this.listInventory.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listInventory.Columns.Add(this.columnHeader5);
            this.listInventory.Columns.Add(this.columnHeader6);
            this.listInventory.DoubleClickTogglesNode = false;
            this.listInventory.DragDropEnabled = false;
            this.listInventory.DragDropNodeCopyEnabled = false;
            this.listInventory.ExpandButtonSize = new System.Drawing.Size(1, 15);
            this.listInventory.ExpandWidth = 5;
            this.listInventory.HotTracking = true;
            this.listInventory.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listInventory.Location = new System.Drawing.Point(4, 4);
            this.listInventory.MultiNodeDragCountVisible = false;
            this.listInventory.MultiNodeDragDropAllowed = false;
            this.listInventory.Name = "listInventory";
            this.listInventory.NodeHorizontalSpacing = 5;
            this.listInventory.NodesConnector = this.nodeConnector3;
            this.listInventory.NodeSpacing = 5;
            this.listInventory.NodeStyle = this.elementStyle3;
            this.listInventory.PathSeparator = ";";
            this.listInventory.Size = new System.Drawing.Size(321, 254);
            this.listInventory.Styles.Add(this.elementStyle3);
            this.listInventory.TabIndex = 114;
            // 
            // columnHeader5
            // 
            this.columnHeader5.MinimumWidth = 100;
            this.columnHeader5.Name = "columnHeader5";
            this.columnHeader5.Text = "Item";
            this.columnHeader5.Width.Absolute = 170;
            // 
            // columnHeader6
            // 
            this.columnHeader6.EditorType = DevComponents.AdvTree.eCellEditorType.NumericInteger;
            this.columnHeader6.Name = "columnHeader6";
            this.columnHeader6.Text = "Value";
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
            // cmdMaxAllItems
            // 
            this.cmdMaxAllItems.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxAllItems.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxAllItems.FocusCuesEnabled = false;
            this.cmdMaxAllItems.Location = new System.Drawing.Point(4, 257);
            this.cmdMaxAllItems.Name = "cmdMaxAllItems";
            this.cmdMaxAllItems.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxAllItems.Size = new System.Drawing.Size(321, 22);
            this.cmdMaxAllItems.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxAllItems.TabIndex = 115;
            this.cmdMaxAllItems.Text = "Max All Items";
            this.cmdMaxAllItems.Click += new System.EventHandler(this.cmdMaxAllInventoryItems_Click);
            // 
            // ACIV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 341);
            this.Name = "ACIV";
            this.Text = "Assassin\'s Creed IV: Black Flag";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listInventory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnInjectData;
        private DevComponents.AdvTree.AdvTree listInventory;
        private DevComponents.AdvTree.ColumnHeader columnHeader5;
        private DevComponents.AdvTree.ColumnHeader columnHeader6;
        private DevComponents.AdvTree.NodeConnector nodeConnector3;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.ButtonX cmdMaxAllItems;
    }
}
