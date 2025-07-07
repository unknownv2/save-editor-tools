namespace Horizon.PackageEditors.Sleeping_Dogs
{
    partial class SleepingDogs
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
            this.listMain = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.btnUnlockAll = new DevComponents.DotNetBar.ButtonX();
            this.cmdCollectAll = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listMain)).BeginInit();
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
            this.cmdSave,
            this.btnInjectData});
            this.rbPackageEditor.Size = new System.Drawing.Size(436, 419);
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
            this.panelMain.Controls.Add(this.cmdCollectAll);
            this.panelMain.Controls.Add(this.btnUnlockAll);
            this.panelMain.Controls.Add(this.listMain);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(436, 364);
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
            this.btnInjectData.Text = "Inject Data";
            this.btnInjectData.Visible = false;
            this.btnInjectData.Click += new System.EventHandler(this.BtnClickInjectData);
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
            this.listMain.Size = new System.Drawing.Size(428, 324);
            this.listMain.Styles.Add(this.elementStyle2);
            this.listMain.TabIndex = 38;
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
            // btnUnlockAll
            // 
            this.btnUnlockAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAll.FocusCuesEnabled = false;
            this.btnUnlockAll.Image = global::Horizon.Properties.Resources.Star;
            this.btnUnlockAll.Location = new System.Drawing.Point(3, 330);
            this.btnUnlockAll.Name = "btnUnlockAll";
            this.btnUnlockAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAll.Size = new System.Drawing.Size(214, 28);
            this.btnUnlockAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAll.TabIndex = 39;
            this.btnUnlockAll.Text = "Unlock All Upgrades";
            this.btnUnlockAll.Click += new System.EventHandler(this.UnlockAllUpgrades);
            // 
            // cmdCollectAll
            // 
            this.cmdCollectAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdCollectAll.AutoCheckOnClick = true;
            this.cmdCollectAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdCollectAll.FocusCuesEnabled = false;
            this.cmdCollectAll.Image = global::Horizon.Properties.Resources.Star;
            this.cmdCollectAll.Location = new System.Drawing.Point(216, 330);
            this.cmdCollectAll.Name = "cmdCollectAll";
            this.cmdCollectAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdCollectAll.Size = new System.Drawing.Size(214, 28);
            this.cmdCollectAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdCollectAll.TabIndex = 39;
            this.cmdCollectAll.Text = "Unlock All Collectibles";
            // 
            // SleepingDogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 422);
            this.Name = "SleepingDogs";
            this.Text = "Sleeping Dogs";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem btnInjectData;
        private DevComponents.AdvTree.AdvTree listMain;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.ButtonX cmdCollectAll;
        private DevComponents.DotNetBar.ButtonX btnUnlockAll;
    }
}
