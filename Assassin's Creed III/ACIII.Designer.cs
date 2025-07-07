namespace Horizon.PackageEditors.Assassin_s_Creed_III
{
    partial class ACIII
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
            this.listCharacters = new DevComponents.AdvTree.AdvTree();
            this.columnHeader5 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader6 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.cmdMaxAllCharacters = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listCharacters)).BeginInit();
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
            this.cmdOpen,
            this.cmdSave});
            this.rbPackageEditor.Size = new System.Drawing.Size(332, 336);
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
            // cmdOpen
            // 
            this.cmdOpen.ImageFixedSize = new System.Drawing.Size(16, 16);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.cmdMaxAllCharacters);
            this.panelMain.Controls.Add(this.listCharacters);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(332, 281);
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
            this.tabMain.Text = "Money Editor";
            // 
            // listCharacters
            // 
            this.listCharacters.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listCharacters.AllowDrop = true;
            this.listCharacters.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listCharacters.BackgroundStyle.Class = "TreeBorderKey";
            this.listCharacters.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listCharacters.Columns.Add(this.columnHeader5);
            this.listCharacters.Columns.Add(this.columnHeader6);
            this.listCharacters.DoubleClickTogglesNode = false;
            this.listCharacters.DragDropEnabled = false;
            this.listCharacters.DragDropNodeCopyEnabled = false;
            this.listCharacters.ExpandButtonSize = new System.Drawing.Size(1, 15);
            this.listCharacters.ExpandWidth = 5;
            this.listCharacters.HotTracking = true;
            this.listCharacters.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listCharacters.Location = new System.Drawing.Point(6, 3);
            this.listCharacters.MultiNodeDragCountVisible = false;
            this.listCharacters.MultiNodeDragDropAllowed = false;
            this.listCharacters.Name = "listCharacters";
            this.listCharacters.NodeHorizontalSpacing = 5;
            this.listCharacters.NodesConnector = this.nodeConnector3;
            this.listCharacters.NodeSpacing = 5;
            this.listCharacters.NodeStyle = this.elementStyle3;
            this.listCharacters.PathSeparator = ";";
            this.listCharacters.Size = new System.Drawing.Size(321, 254);
            this.listCharacters.Styles.Add(this.elementStyle3);
            this.listCharacters.TabIndex = 115;
            // 
            // columnHeader5
            // 
            this.columnHeader5.MinimumWidth = 100;
            this.columnHeader5.Name = "columnHeader5";
            this.columnHeader5.Text = "Character";
            this.columnHeader5.Width.Absolute = 170;
            // 
            // columnHeader6
            // 
            this.columnHeader6.EditorType = DevComponents.AdvTree.eCellEditorType.NumericInteger;
            this.columnHeader6.Name = "columnHeader6";
            this.columnHeader6.Text = "Pounds";
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
            // cmdMaxAllCharacters
            // 
            this.cmdMaxAllCharacters.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxAllCharacters.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxAllCharacters.FocusCuesEnabled = false;
            this.cmdMaxAllCharacters.Location = new System.Drawing.Point(6, 256);
            this.cmdMaxAllCharacters.Name = "cmdMaxAllCharacters";
            this.cmdMaxAllCharacters.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxAllCharacters.Size = new System.Drawing.Size(321, 22);
            this.cmdMaxAllCharacters.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxAllCharacters.TabIndex = 116;
            this.cmdMaxAllCharacters.Text = "Max Pounds";
            this.cmdMaxAllCharacters.Click += new System.EventHandler(this.cmdMaxAllCharacters_Click);
            // 
            // ACIII
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 339);
            this.Name = "ACIII";
            this.Text = "Assassin\'s Creed III";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listCharacters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree listCharacters;
        private DevComponents.AdvTree.ColumnHeader columnHeader5;
        private DevComponents.AdvTree.ColumnHeader columnHeader6;
        private DevComponents.AdvTree.NodeConnector nodeConnector3;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.ButtonX cmdMaxAllCharacters;
    }
}
