namespace Horizon.PackageEditors
{
    partial class EditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorControl));
            this.rbPackageEditor = new DevComponents.DotNetBar.RibbonControl();
            this.panelMain = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdOpen = new DevComponents.DotNetBar.Office2007StartButton();
            this.cmdSave = new DevComponents.DotNetBar.Office2007StartButton();
            this.tabMain = new DevComponents.DotNetBar.RibbonTabItem();
            this.cmdOpenPackageManager = new DevComponents.DotNetBar.ButtonItem();
            this.rbPackageEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            this.rbPackageEditor.AutoExpand = false;
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.CanCustomize = false;
            this.rbPackageEditor.CaptionVisible = true;
            this.rbPackageEditor.Controls.Add(this.panelMain);
            this.rbPackageEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbPackageEditor.EnableQatPlacement = false;
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdOpen,
            this.cmdSave,
            this.tabMain});
            this.rbPackageEditor.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.rbPackageEditor.Location = new System.Drawing.Point(5, 1);
            this.rbPackageEditor.Margin = new System.Windows.Forms.Padding(0);
            this.rbPackageEditor.Name = "rbPackageEditor";
            this.rbPackageEditor.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.rbPackageEditor.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdOpenPackageManager});
            this.rbPackageEditor.Size = new System.Drawing.Size(337, 172);
            this.rbPackageEditor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
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
            this.rbPackageEditor.TabGroupHeight = 14;
            this.rbPackageEditor.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panelMain.Size = new System.Drawing.Size(337, 117);
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
            this.panelMain.TabIndex = 1;
            // 
            // cmdOpen
            // 
            this.cmdOpen.CanCustomize = false;
            this.cmdOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.cmdOpen.FixedSize = new System.Drawing.Size(60, 23);
            this.cmdOpen.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdOpen.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdOpen.ImagePaddingHorizontal = 0;
            this.cmdOpen.ImagePaddingVertical = 0;
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.ShowSubItems = false;
            this.cmdOpen.Text = "Open";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.CanCustomize = false;
            this.cmdSave.FixedSize = new System.Drawing.Size(60, 23);
            this.cmdSave.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdSave.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdSave.ImagePaddingHorizontal = 0;
            this.cmdSave.ImagePaddingVertical = 0;
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.ShowSubItems = false;
            this.cmdSave.Text = "Save";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // tabMain
            // 
            this.tabMain.Checked = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.Panel = this.panelMain;
            this.tabMain.Text = "Package Editor";
            // 
            // cmdOpenPackageManager
            // 
            this.cmdOpenPackageManager.Image = ((System.Drawing.Image)(resources.GetObject("cmdOpenPackageManager.Image")));
            this.cmdOpenPackageManager.ImageFixedSize = new System.Drawing.Size(14, 14);
            this.cmdOpenPackageManager.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.cmdOpenPackageManager.Name = "cmdOpenPackageManager";
            this.cmdOpenPackageManager.Click += new System.EventHandler(this.cmdOpenPackageManager_Click);
            // 
            // EditorControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BottomLeftCornerSize = 0;
            this.BottomRightCornerSize = 0;
            this.ClientSize = new System.Drawing.Size(347, 175);
            this.Controls.Add(this.rbPackageEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditorControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditorControl_FormClosing);
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DevComponents.DotNetBar.RibbonControl rbPackageEditor;
        public DevComponents.DotNetBar.Office2007StartButton cmdOpen;
        public DevComponents.DotNetBar.Office2007StartButton cmdSave;
        public DevComponents.DotNetBar.RibbonPanel panelMain;
        public DevComponents.DotNetBar.RibbonTabItem tabMain;
        private DevComponents.DotNetBar.ButtonItem cmdOpenPackageManager;
    }
}