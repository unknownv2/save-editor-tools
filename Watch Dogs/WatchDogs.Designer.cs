namespace Horizon.PackageEditors.Watch_Dogs
{
    partial class WatchDogs
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
            this.propGridCampaignSave = new DevComponents.DotNetBar.AdvPropertyGrid();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.stcInventory = new DevComponents.DotNetBar.SuperTabControl();
            this.treeInventory = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propGridCampaignSave)).BeginInit();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stcInventory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeInventory)).BeginInit();
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
            this.rbPackageEditor.Size = new System.Drawing.Size(707, 438);
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
            this.panelMain.Controls.Add(this.propGridCampaignSave);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(707, 383);
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
            this.tabMain.Click += new System.EventHandler(this.BtnTab_Click);
            // 
            // propGridCampaignSave
            // 
            this.propGridCampaignSave.GridLinesColor = System.Drawing.Color.WhiteSmoke;
            this.propGridCampaignSave.Location = new System.Drawing.Point(6, 3);
            this.propGridCampaignSave.Name = "propGridCampaignSave";
            this.propGridCampaignSave.Size = new System.Drawing.Size(444, 377);
            this.propGridCampaignSave.TabIndex = 2;
            this.propGridCampaignSave.Text = "advPropertyGrid1";
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "Inventory";
            this.ribbonTabItem1.Click += new System.EventHandler(this.BtnTab_Click);
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.stcInventory);
            this.ribbonPanel1.Controls.Add(this.treeInventory);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(707, 383);
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
            // stcInventory
            // 
            this.stcInventory.CloseButtonOnTabsVisible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.stcInventory.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.stcInventory.ControlBox.MenuBox.Name = "";
            this.stcInventory.ControlBox.Name = "";
            this.stcInventory.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.stcInventory.ControlBox.MenuBox,
            this.stcInventory.ControlBox.CloseBox});
            this.stcInventory.Dock = System.Windows.Forms.DockStyle.Right;
            this.stcInventory.Location = new System.Drawing.Point(264, 0);
            this.stcInventory.Name = "stcInventory";
            this.stcInventory.ReorderTabsEnabled = true;
            this.stcInventory.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.stcInventory.SelectedTabIndex = -1;
            this.stcInventory.Size = new System.Drawing.Size(440, 380);
            this.stcInventory.TabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stcInventory.TabIndex = 2;
            this.stcInventory.Text = "superTabControl1";
            this.stcInventory.TabItemClose += new System.EventHandler<DevComponents.DotNetBar.SuperTabStripTabItemCloseEventArgs>(this.SuperTabControl_TabItemClose);
            // 
            // treeInventory
            // 
            this.treeInventory.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeInventory.AllowDrop = true;
            this.treeInventory.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeInventory.BackgroundStyle.Class = "TreeBorderKey";
            this.treeInventory.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeInventory.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeInventory.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeInventory.Location = new System.Drawing.Point(3, 0);
            this.treeInventory.Name = "treeInventory";
            this.treeInventory.NodesConnector = this.nodeConnector3;
            this.treeInventory.NodeStyle = this.elementStyle3;
            this.treeInventory.PathSeparator = ";";
            this.treeInventory.Size = new System.Drawing.Size(259, 380);
            this.treeInventory.Styles.Add(this.elementStyle3);
            this.treeInventory.TabIndex = 1;
            this.treeInventory.Text = "advTree1";
            this.treeInventory.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreePlayer_AfterNodeSelect);
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
            // WatchDogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 441);
            this.Name = "WatchDogs";
            this.Text = "Watch Dogs Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propGridCampaignSave)).EndInit();
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stcInventory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeInventory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private DevComponents.DotNetBar.RibbonControl rbPackageEditor;
       // private DevComponents.DotNetBar.RibbonPanel panelMain;
        //private DevComponents.DotNetBar.Office2007StartButton cmdOpen;
        //private DevComponents.DotNetBar.Office2007StartButton cmdSave;
        //private DevComponents.DotNetBar.RibbonTabItem tabMain;
        private DevComponents.DotNetBar.AdvPropertyGrid propGridCampaignSave;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.AdvTree.AdvTree treeInventory;
        private DevComponents.AdvTree.NodeConnector nodeConnector3;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.SuperTabControl stcInventory;
    }
}
