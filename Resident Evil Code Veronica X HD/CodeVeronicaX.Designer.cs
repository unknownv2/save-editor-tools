namespace Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD
{
    partial class CodeVeronicaX
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
            this.treeSaveSlots = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.stcItemList = new DevComponents.DotNetBar.SuperTabControl();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeSaveSlots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stcItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(729, 345);
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
            this.panelMain.Controls.Add(this.stcItemList);
            this.panelMain.Controls.Add(this.treeSaveSlots);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(729, 290);
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
            this.tabMain.Text = "Campaign";
            // 
            // treeSaveSlots
            // 
            this.treeSaveSlots.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeSaveSlots.AllowDrop = true;
            this.treeSaveSlots.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeSaveSlots.BackgroundStyle.Class = "TreeBorderKey";
            this.treeSaveSlots.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeSaveSlots.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeSaveSlots.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeSaveSlots.Location = new System.Drawing.Point(3, 0);
            this.treeSaveSlots.Name = "treeSaveSlots";
            this.treeSaveSlots.NodesConnector = this.nodeConnector3;
            this.treeSaveSlots.NodeStyle = this.elementStyle3;
            this.treeSaveSlots.PathSeparator = ";";
            this.treeSaveSlots.Size = new System.Drawing.Size(260, 287);
            this.treeSaveSlots.Styles.Add(this.elementStyle3);
            this.treeSaveSlots.TabIndex = 2;
            this.treeSaveSlots.Text = "advTree1";
            this.treeSaveSlots.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreeSaveData_AfterNodeSelect);
            this.treeSaveSlots.AfterNodeDeselect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreeSaveData_AfterNodeDeselect);
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
            // stcItemList
            // 
            this.stcItemList.CloseButtonOnTabsVisible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.stcItemList.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.stcItemList.ControlBox.MenuBox.Name = "";
            this.stcItemList.ControlBox.Name = "";
            this.stcItemList.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.stcItemList.ControlBox.MenuBox,
            this.stcItemList.ControlBox.CloseBox});
            this.stcItemList.Dock = System.Windows.Forms.DockStyle.Right;
            this.stcItemList.Location = new System.Drawing.Point(266, 0);
            this.stcItemList.Name = "stcItemList";
            this.stcItemList.ReorderTabsEnabled = true;
            this.stcItemList.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.stcItemList.SelectedTabIndex = -1;
            this.stcItemList.Size = new System.Drawing.Size(460, 287);
            this.stcItemList.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stcItemList.TabIndex = 3;
            this.stcItemList.Text = "superTabControl1";
            // 
            // CodeVeronicaX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 348);
            this.Name = "CodeVeronicaX";
            this.Text = "Resident Evil Code: Veronica X HD";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeSaveSlots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stcItemList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree treeSaveSlots;
        private DevComponents.AdvTree.NodeConnector nodeConnector3;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.SuperTabControl stcItemList;
    }
}
