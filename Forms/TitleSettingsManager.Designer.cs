namespace Horizon.Forms
{
    partial class TitleSettingsManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleSettingsManager));
            this.rbMain = new DevComponents.DotNetBar.RibbonControl();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.panelType = new DevComponents.DotNetBar.PanelEx();
            this.comboType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.cmdInject = new DevComponents.DotNetBar.ButtonX();
            this.cmdExtract = new DevComponents.DotNetBar.ButtonX();
            this.cmdOpen = new DevComponents.DotNetBar.Office2007StartButton();
            this.tabMain = new DevComponents.DotNetBar.RibbonTabItem();
            this.rbMain.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            this.panelType.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbMain
            // 
            // 
            // 
            // 
            this.rbMain.BackgroundStyle.Class = "";
            this.rbMain.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbMain.CanCustomize = false;
            this.rbMain.CaptionVisible = true;
            this.rbMain.Controls.Add(this.ribbonPanel1);
            this.rbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbMain.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdOpen,
            this.tabMain});
            this.rbMain.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.rbMain.Location = new System.Drawing.Point(5, 1);
            this.rbMain.Name = "rbMain";
            this.rbMain.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.rbMain.Size = new System.Drawing.Size(246, 130);
            this.rbMain.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rbMain.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.rbMain.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.rbMain.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.rbMain.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.rbMain.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.rbMain.SystemText.QatDialogAddButton = "&Add >>";
            this.rbMain.SystemText.QatDialogCancelButton = "Cancel";
            this.rbMain.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.rbMain.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.rbMain.SystemText.QatDialogOkButton = "OK";
            this.rbMain.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.rbMain.SystemText.QatDialogRemoveButton = "&Remove";
            this.rbMain.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.rbMain.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.rbMain.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.rbMain.TabGroupHeight = 14;
            this.rbMain.TabIndex = 0;
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.panelType);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(246, 75);
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
            this.ribbonPanel1.TabIndex = 1;
            // 
            // panelType
            // 
            this.panelType.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelType.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelType.Controls.Add(this.comboType);
            this.panelType.Controls.Add(this.cmdInject);
            this.panelType.Controls.Add(this.cmdExtract);
            this.panelType.Enabled = false;
            this.panelType.Location = new System.Drawing.Point(6, 3);
            this.panelType.Name = "panelType";
            this.panelType.Size = new System.Drawing.Size(234, 66);
            this.panelType.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelType.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelType.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelType.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelType.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelType.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelType.Style.GradientAngle = 90;
            this.panelType.TabIndex = 3;
            // 
            // comboType
            // 
            this.comboType.DisplayMember = "Text";
            this.comboType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FormattingEnabled = true;
            this.comboType.ItemHeight = 14;
            this.comboType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5});
            this.comboType.Location = new System.Drawing.Point(3, 3);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(228, 20);
            this.comboType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboType.TabIndex = 2;
            this.comboType.SelectedIndexChanged += new System.EventHandler(this.comboType_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "Select a Title Setting...";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "Title Specific 1";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "Title Specific 2";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "Title Specific 3";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "All Title Specific";
            // 
            // cmdInject
            // 
            this.cmdInject.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdInject.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdInject.Enabled = false;
            this.cmdInject.FocusCuesEnabled = false;
            this.cmdInject.Location = new System.Drawing.Point(124, 29);
            this.cmdInject.Name = "cmdInject";
            this.cmdInject.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdInject.Size = new System.Drawing.Size(107, 34);
            this.cmdInject.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdInject.TabIndex = 0;
            this.cmdInject.Text = "Replace Setting";
            this.cmdInject.Click += new System.EventHandler(this.cmdInject_Click);
            // 
            // cmdExtract
            // 
            this.cmdExtract.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExtract.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExtract.Enabled = false;
            this.cmdExtract.FocusCuesEnabled = false;
            this.cmdExtract.Location = new System.Drawing.Point(3, 29);
            this.cmdExtract.Name = "cmdExtract";
            this.cmdExtract.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdExtract.Size = new System.Drawing.Size(115, 34);
            this.cmdExtract.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExtract.TabIndex = 1;
            this.cmdExtract.Text = "Extract Setting";
            this.cmdExtract.Click += new System.EventHandler(this.cmdExtract_Click);
            // 
            // cmdOpen
            // 
            this.cmdOpen.CanCustomize = false;
            this.cmdOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.cmdOpen.FixedSize = new System.Drawing.Size(80, 23);
            this.cmdOpen.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdOpen.Image = ((System.Drawing.Image)(resources.GetObject("cmdOpen.Image")));
            this.cmdOpen.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdOpen.ImagePaddingHorizontal = 0;
            this.cmdOpen.ImagePaddingVertical = 0;
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.ShowSubItems = false;
            this.cmdOpen.Text = "Open GPD";
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // tabMain
            // 
            this.tabMain.Checked = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.Panel = this.ribbonPanel1;
            this.tabMain.Text = "GPD Title Settings";
            // 
            // TitleSettingsManager
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BottomLeftCornerSize = 0;
            this.BottomRightCornerSize = 0;
            this.ClientSize = new System.Drawing.Size(256, 133);
            this.Controls.Add(this.rbMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(256, 133);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(256, 133);
            this.Name = "TitleSettingsManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title Settings Manager";
            this.rbMain.ResumeLayout(false);
            this.rbMain.PerformLayout();
            this.ribbonPanel1.ResumeLayout(false);
            this.panelType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl rbMain;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.Office2007StartButton cmdOpen;
        private DevComponents.DotNetBar.RibbonTabItem tabMain;
        private DevComponents.DotNetBar.ButtonX cmdExtract;
        private DevComponents.DotNetBar.ButtonX cmdInject;
        private DevComponents.DotNetBar.PanelEx panelType;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboType;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
    }
}