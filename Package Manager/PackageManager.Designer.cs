namespace Horizon.PackageEditors.Package_Manager
{
    partial class PackageManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageManager));
            this.tabContents = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.cmdExtractSelected = new DevComponents.DotNetBar.ButtonX();
            this.cmdExtractAll = new DevComponents.DotNetBar.ButtonX();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.treeFolders = new System.Windows.Forms.TreeView();
            this.listFiles = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.menuContents = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdInject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdExtract = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdOverwrite = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.gpProfile = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.pbGamerpic = new System.Windows.Forms.PictureBox();
            this.cmdSwitchProfile = new DevComponents.DotNetBar.ButtonX();
            this.cmdNoProfiles = new DevComponents.DotNetBar.ButtonItem();
            this.lblGamertag = new DevComponents.DotNetBar.LabelX();
            this.cmdManageProfiles = new DevComponents.DotNetBar.ButtonX();
            this.gpIcons = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.pbIcon1 = new System.Windows.Forms.PictureBox();
            this.pbIcon2 = new System.Windows.Forms.PictureBox();
            this.menuIcon1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtTID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblTID = new DevComponents.DotNetBar.LabelX();
            this.txtCID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblCID = new DevComponents.DotNetBar.LabelX();
            this.txtDID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblDID = new DevComponents.DotNetBar.LabelX();
            this.txtPID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblPID = new DevComponents.DotNetBar.LabelX();
            this.txtTitleName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDisplayName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblTitleName = new DevComponents.DotNetBar.LabelX();
            this.lblDisplayName = new DevComponents.DotNetBar.LabelX();
            this.switchAccount = new DevComponents.DotNetBar.SwitchButtonItem();
            this.cmdSaveToDevice = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.cmdModPackage = new DevComponents.DotNetBar.ButtonX();
            this.galStfs = new DevComponents.DotNetBar.GalleryContainer();
            this.cmdViewContents = new DevComponents.DotNetBar.ButtonX();
            this.cmdSaveSet1 = new DevComponents.DotNetBar.ButtonItem();
            this.cmdShare = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            this.menuContents.SuspendLayout();
            this.gpProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGamerpic)).BeginInit();
            this.gpIcons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon2)).BeginInit();
            this.menuIcon1.SuspendLayout();
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
            this.tabContents,
            this.switchAccount});
            this.rbPackageEditor.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdSaveToDevice});
            this.rbPackageEditor.Size = new System.Drawing.Size(437, 254);
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
            // cmdSave
            // 
            this.cmdSave.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.cmdSave.FixedSize = new System.Drawing.Size(150, 23);
            this.cmdSave.Text = "Save, Rehash, and Resign";
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.cmdViewContents);
            this.panelMain.Controls.Add(this.cmdShare);
            this.panelMain.Controls.Add(this.gpProfile);
            this.panelMain.Controls.Add(this.gpIcons);
            this.panelMain.Controls.Add(this.txtTID);
            this.panelMain.Controls.Add(this.lblTID);
            this.panelMain.Controls.Add(this.txtCID);
            this.panelMain.Controls.Add(this.lblCID);
            this.panelMain.Controls.Add(this.txtDID);
            this.panelMain.Controls.Add(this.lblDID);
            this.panelMain.Controls.Add(this.txtPID);
            this.panelMain.Controls.Add(this.lblPID);
            this.panelMain.Controls.Add(this.txtTitleName);
            this.panelMain.Controls.Add(this.txtDisplayName);
            this.panelMain.Controls.Add(this.lblTitleName);
            this.panelMain.Controls.Add(this.lblDisplayName);
            this.panelMain.Controls.Add(this.cmdModPackage);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(437, 199);
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
            this.tabMain.Text = "Package Info";
            // 
            // tabContents
            // 
            this.tabContents.Name = "tabContents";
            this.tabContents.Panel = this.ribbonPanel1;
            this.tabContents.Text = "Contents";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.progress);
            this.ribbonPanel1.Controls.Add(this.cmdExtractSelected);
            this.ribbonPanel1.Controls.Add(this.cmdExtractAll);
            this.ribbonPanel1.Controls.Add(this.txtSearch);
            this.ribbonPanel1.Controls.Add(this.treeFolders);
            this.ribbonPanel1.Controls.Add(this.listFiles);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(437, 199);
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
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(117, 174);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(317, 22);
            this.progress.TabIndex = 10;
            // 
            // cmdExtractSelected
            // 
            this.cmdExtractSelected.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExtractSelected.BackColor = System.Drawing.Color.Transparent;
            this.cmdExtractSelected.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExtractSelected.FocusCuesEnabled = false;
            this.cmdExtractSelected.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExtractSelected.Location = new System.Drawing.Point(5, 148);
            this.cmdExtractSelected.Name = "cmdExtractSelected";
            this.cmdExtractSelected.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdExtractSelected.Size = new System.Drawing.Size(108, 20);
            this.cmdExtractSelected.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExtractSelected.TabIndex = 9;
            this.cmdExtractSelected.Text = "Extract Selected";
            this.cmdExtractSelected.Click += new System.EventHandler(this.cmdExtract_Click);
            // 
            // cmdExtractAll
            // 
            this.cmdExtractAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExtractAll.BackColor = System.Drawing.Color.Transparent;
            this.cmdExtractAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExtractAll.FocusCuesEnabled = false;
            this.cmdExtractAll.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExtractAll.Location = new System.Drawing.Point(5, 174);
            this.cmdExtractAll.Name = "cmdExtractAll";
            this.cmdExtractAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdExtractAll.Size = new System.Drawing.Size(108, 20);
            this.cmdExtractAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExtractAll.TabIndex = 8;
            this.cmdExtractAll.Text = "Extract All";
            this.cmdExtractAll.Click += new System.EventHandler(this.cmdExtractAll_Click);
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.Location = new System.Drawing.Point(5, 122);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(108, 20);
            this.txtSearch.TabIndex = 7;
            this.txtSearch.WatermarkText = "Search Files...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // treeFolders
            // 
            this.treeFolders.BackColor = System.Drawing.SystemColors.ControlLight;
            this.treeFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeFolders.Indent = 12;
            this.treeFolders.Location = new System.Drawing.Point(5, 3);
            this.treeFolders.Name = "treeFolders";
            this.treeFolders.Size = new System.Drawing.Size(108, 113);
            this.treeFolders.TabIndex = 6;
            this.treeFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFolders_AfterSelect);
            // 
            // listFiles
            // 
            // 
            // 
            // 
            this.listFiles.Border.Class = "ListViewBorder";
            this.listFiles.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listFiles.ContextMenuStrip = this.menuContents;
            this.listFiles.FullRowSelect = true;
            this.listFiles.GridLines = true;
            this.listFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listFiles.Location = new System.Drawing.Point(117, 0);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(317, 172);
            this.listFiles.TabIndex = 5;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.View = System.Windows.Forms.View.Details;
            this.listFiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listFiles_AfterLabelEdit);
            this.listFiles.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listFiles_BeforeLabelEdit);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Entry Name";
            this.columnHeader1.Width = 159;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Offset";
            this.columnHeader2.Width = 65;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            // 
            // menuContents
            // 
            this.menuContents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdInject,
            this.toolStripSeparator1,
            this.cmdExtract,
            this.cmdOverwrite,
            this.toolStripSeparator3,
            this.cmdRename,
            this.toolStripSeparator2,
            this.cmdDelete});
            this.menuContents.Name = "menuContents";
            this.menuContents.Size = new System.Drawing.Size(161, 132);
            // 
            // cmdInject
            // 
            this.cmdInject.Image = global::Horizon.Properties.Resources.RightArrow;
            this.cmdInject.Name = "cmdInject";
            this.cmdInject.Size = new System.Drawing.Size(160, 22);
            this.cmdInject.Text = "Insert New File...";
            this.cmdInject.Click += new System.EventHandler(this.cmdInject_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // cmdExtract
            // 
            this.cmdExtract.Image = global::Horizon.Properties.Resources.UpArrowSilver;
            this.cmdExtract.Name = "cmdExtract";
            this.cmdExtract.Size = new System.Drawing.Size(160, 22);
            this.cmdExtract.Text = "Extract...";
            this.cmdExtract.Click += new System.EventHandler(this.cmdExtract_Click);
            // 
            // cmdOverwrite
            // 
            this.cmdOverwrite.Image = global::Horizon.Properties.Resources.DownArrow;
            this.cmdOverwrite.Name = "cmdOverwrite";
            this.cmdOverwrite.Size = new System.Drawing.Size(160, 22);
            this.cmdOverwrite.Text = "Replace...";
            this.cmdOverwrite.Click += new System.EventHandler(this.cmdOverwrite_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // cmdRename
            // 
            this.cmdRename.Image = global::Horizon.Properties.Resources.Pencil;
            this.cmdRename.Name = "cmdRename";
            this.cmdRename.Size = new System.Drawing.Size(160, 22);
            this.cmdRename.Text = "Rename";
            this.cmdRename.Click += new System.EventHandler(this.cmdRename_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = global::Horizon.Properties.Resources.Delete;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(160, 22);
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // gpProfile
            // 
            this.gpProfile.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpProfile.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpProfile.Controls.Add(this.pbGamerpic);
            this.gpProfile.Controls.Add(this.cmdSwitchProfile);
            this.gpProfile.Controls.Add(this.lblGamertag);
            this.gpProfile.Controls.Add(this.cmdManageProfiles);
            this.gpProfile.Location = new System.Drawing.Point(6, 136);
            this.gpProfile.Name = "gpProfile";
            this.gpProfile.Size = new System.Drawing.Size(222, 57);
            // 
            // 
            // 
            this.gpProfile.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpProfile.Style.BackColorGradientAngle = 90;
            this.gpProfile.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpProfile.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpProfile.Style.BorderBottomWidth = 1;
            this.gpProfile.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpProfile.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpProfile.Style.BorderLeftWidth = 1;
            this.gpProfile.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpProfile.Style.BorderRightWidth = 1;
            this.gpProfile.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpProfile.Style.BorderTopWidth = 1;
            this.gpProfile.Style.Class = "";
            this.gpProfile.Style.CornerDiameter = 4;
            this.gpProfile.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpProfile.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpProfile.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpProfile.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpProfile.StyleMouseDown.Class = "";
            this.gpProfile.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpProfile.StyleMouseOver.Class = "";
            this.gpProfile.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpProfile.TabIndex = 39;
            // 
            // pbGamerpic
            // 
            this.pbGamerpic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGamerpic.Image = global::Horizon.Properties.Resources.QuestionMark;
            this.pbGamerpic.Location = new System.Drawing.Point(-2, -1);
            this.pbGamerpic.Name = "pbGamerpic";
            this.pbGamerpic.Size = new System.Drawing.Size(58, 58);
            this.pbGamerpic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGamerpic.TabIndex = 44;
            this.pbGamerpic.TabStop = false;
            // 
            // cmdSwitchProfile
            // 
            this.cmdSwitchProfile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSwitchProfile.AutoExpandOnClick = true;
            this.cmdSwitchProfile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSwitchProfile.FocusCuesEnabled = false;
            this.cmdSwitchProfile.Image = global::Horizon.Properties.Resources.Pencil;
            this.cmdSwitchProfile.Location = new System.Drawing.Point(55, 27);
            this.cmdSwitchProfile.Name = "cmdSwitchProfile";
            this.cmdSwitchProfile.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSwitchProfile.Size = new System.Drawing.Size(80, 29);
            this.cmdSwitchProfile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSwitchProfile.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdNoProfiles});
            this.cmdSwitchProfile.TabIndex = 42;
            this.cmdSwitchProfile.Text = "Change";
            this.cmdSwitchProfile.PopupOpen += new System.EventHandler(this.cmdSwitchProfile_PopupOpen);
            // 
            // cmdNoProfiles
            // 
            this.cmdNoProfiles.Enabled = false;
            this.cmdNoProfiles.GlobalItem = false;
            this.cmdNoProfiles.Image = global::Horizon.Properties.Resources.Info;
            this.cmdNoProfiles.Name = "cmdNoProfiles";
            this.cmdNoProfiles.Text = "No Profiles Found";
            // 
            // lblGamertag
            // 
            this.lblGamertag.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblGamertag.BackgroundStyle.Class = "";
            this.lblGamertag.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGamertag.Location = new System.Drawing.Point(55, 5);
            this.lblGamertag.Name = "lblGamertag";
            this.lblGamertag.Size = new System.Drawing.Size(166, 17);
            this.lblGamertag.TabIndex = 45;
            this.lblGamertag.Text = "Unknown Profile";
            this.lblGamertag.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // cmdManageProfiles
            // 
            this.cmdManageProfiles.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdManageProfiles.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdManageProfiles.FocusCuesEnabled = false;
            this.cmdManageProfiles.Image = global::Horizon.Properties.Resources.Gear;
            this.cmdManageProfiles.Location = new System.Drawing.Point(134, 27);
            this.cmdManageProfiles.Name = "cmdManageProfiles";
            this.cmdManageProfiles.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdManageProfiles.Size = new System.Drawing.Size(87, 29);
            this.cmdManageProfiles.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdManageProfiles.TabIndex = 43;
            this.cmdManageProfiles.Text = "Manage";
            this.cmdManageProfiles.Click += new System.EventHandler(this.cmdManageProfiles_Click);
            // 
            // gpIcons
            // 
            this.gpIcons.BackColor = System.Drawing.Color.Transparent;
            this.gpIcons.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpIcons.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpIcons.Controls.Add(this.pbIcon1);
            this.gpIcons.Controls.Add(this.pbIcon2);
            this.gpIcons.Location = new System.Drawing.Point(285, 5);
            this.gpIcons.Name = "gpIcons";
            this.gpIcons.Size = new System.Drawing.Size(146, 73);
            // 
            // 
            // 
            this.gpIcons.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpIcons.Style.BackColorGradientAngle = 90;
            this.gpIcons.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpIcons.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpIcons.Style.BorderBottomWidth = 1;
            this.gpIcons.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpIcons.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpIcons.Style.BorderLeftWidth = 1;
            this.gpIcons.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpIcons.Style.BorderRightWidth = 1;
            this.gpIcons.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpIcons.Style.BorderTopWidth = 1;
            this.gpIcons.Style.Class = "";
            this.gpIcons.Style.CornerDiameter = 4;
            this.gpIcons.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpIcons.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpIcons.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpIcons.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpIcons.StyleMouseDown.Class = "";
            this.gpIcons.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpIcons.StyleMouseOver.Class = "";
            this.gpIcons.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpIcons.TabIndex = 35;
            // 
            // pbIcon1
            // 
            this.pbIcon1.BackColor = System.Drawing.Color.Transparent;
            this.pbIcon1.Location = new System.Drawing.Point(4, 3);
            this.pbIcon1.Name = "pbIcon1";
            this.pbIcon1.Size = new System.Drawing.Size(64, 64);
            this.pbIcon1.TabIndex = 14;
            this.pbIcon1.TabStop = false;
            // 
            // pbIcon2
            // 
            this.pbIcon2.Location = new System.Drawing.Point(76, 3);
            this.pbIcon2.Name = "pbIcon2";
            this.pbIcon2.Size = new System.Drawing.Size(64, 64);
            this.pbIcon2.TabIndex = 15;
            this.pbIcon2.TabStop = false;
            // 
            // menuIcon1
            // 
            this.menuIcon1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToolStripMenuItem,
            this.replaceToolStripMenuItem});
            this.menuIcon1.Name = "menuIcon1";
            this.menuIcon1.Size = new System.Drawing.Size(125, 48);
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Image = global::Horizon.Properties.Resources.UpArrowSilver;
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.extractToolStripMenuItem.Text = "Extract...";
            this.extractToolStripMenuItem.Click += new System.EventHandler(this.extractToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Image = global::Horizon.Properties.Resources.DownArrow;
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.replaceToolStripMenuItem.Text = "Replace...";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // txtTID
            // 
            // 
            // 
            // 
            this.txtTID.Border.Class = "TextBoxBorder";
            this.txtTID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTID.Location = new System.Drawing.Point(331, 84);
            this.txtTID.MaxLength = 8;
            this.txtTID.Name = "txtTID";
            this.txtTID.ReadOnly = true;
            this.txtTID.Size = new System.Drawing.Size(100, 20);
            this.txtTID.TabIndex = 33;
            this.txtTID.WatermarkText = "None";
            // 
            // lblTID
            // 
            this.lblTID.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTID.BackgroundStyle.Class = "";
            this.lblTID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTID.Location = new System.Drawing.Point(285, 84);
            this.lblTID.Name = "lblTID";
            this.lblTID.Size = new System.Drawing.Size(40, 20);
            this.lblTID.TabIndex = 32;
            this.lblTID.Text = "Title ID:";
            // 
            // txtCID
            // 
            // 
            // 
            // 
            this.txtCID.Border.Class = "TextBoxBorder";
            this.txtCID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCID.Location = new System.Drawing.Point(87, 110);
            this.txtCID.MaxLength = 10;
            this.txtCID.Name = "txtCID";
            this.txtCID.Size = new System.Drawing.Size(141, 20);
            this.txtCID.TabIndex = 31;
            this.txtCID.WatermarkText = "Null";
            // 
            // lblCID
            // 
            this.lblCID.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCID.BackgroundStyle.Class = "";
            this.lblCID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCID.Location = new System.Drawing.Point(6, 110);
            this.lblCID.Name = "lblCID";
            this.lblCID.Size = new System.Drawing.Size(75, 20);
            this.lblCID.TabIndex = 30;
            this.lblCID.Text = "Console ID:";
            // 
            // txtDID
            // 
            // 
            // 
            // 
            this.txtDID.Border.Class = "TextBoxBorder";
            this.txtDID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDID.Location = new System.Drawing.Point(87, 84);
            this.txtDID.MaxLength = 40;
            this.txtDID.Name = "txtDID";
            this.txtDID.Size = new System.Drawing.Size(192, 20);
            this.txtDID.TabIndex = 29;
            this.txtDID.WatermarkText = "Null";
            // 
            // lblDID
            // 
            this.lblDID.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblDID.BackgroundStyle.Class = "";
            this.lblDID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDID.Location = new System.Drawing.Point(6, 84);
            this.lblDID.Name = "lblDID";
            this.lblDID.Size = new System.Drawing.Size(75, 20);
            this.lblDID.TabIndex = 28;
            this.lblDID.Text = "Device ID:";
            // 
            // txtPID
            // 
            // 
            // 
            // 
            this.txtPID.Border.Class = "TextBoxBorder";
            this.txtPID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPID.Location = new System.Drawing.Point(87, 58);
            this.txtPID.MaxLength = 16;
            this.txtPID.Name = "txtPID";
            this.txtPID.Size = new System.Drawing.Size(192, 20);
            this.txtPID.TabIndex = 27;
            this.txtPID.WatermarkText = "Null";
            this.txtPID.TextChanged += new System.EventHandler(this.txtPID_TextChanged);
            // 
            // lblPID
            // 
            this.lblPID.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblPID.BackgroundStyle.Class = "";
            this.lblPID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPID.Location = new System.Drawing.Point(6, 58);
            this.lblPID.Name = "lblPID";
            this.lblPID.Size = new System.Drawing.Size(75, 20);
            this.lblPID.TabIndex = 26;
            this.lblPID.Text = "Profile ID:";
            // 
            // txtTitleName
            // 
            // 
            // 
            // 
            this.txtTitleName.Border.Class = "TextBoxBorder";
            this.txtTitleName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTitleName.Location = new System.Drawing.Point(87, 31);
            this.txtTitleName.MaxLength = 64;
            this.txtTitleName.Name = "txtTitleName";
            this.txtTitleName.Size = new System.Drawing.Size(192, 20);
            this.txtTitleName.TabIndex = 25;
            this.txtTitleName.WatermarkText = "None";
            // 
            // txtDisplayName
            // 
            // 
            // 
            // 
            this.txtDisplayName.Border.Class = "TextBoxBorder";
            this.txtDisplayName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDisplayName.Location = new System.Drawing.Point(87, 5);
            this.txtDisplayName.MaxLength = 128;
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(192, 20);
            this.txtDisplayName.TabIndex = 24;
            this.txtDisplayName.WatermarkText = "None";
            // 
            // lblTitleName
            // 
            this.lblTitleName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTitleName.BackgroundStyle.Class = "";
            this.lblTitleName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitleName.Location = new System.Drawing.Point(6, 31);
            this.lblTitleName.Name = "lblTitleName";
            this.lblTitleName.Size = new System.Drawing.Size(75, 20);
            this.lblTitleName.TabIndex = 23;
            this.lblTitleName.Text = "Title Name:";
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblDisplayName.BackgroundStyle.Class = "";
            this.lblDisplayName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDisplayName.Location = new System.Drawing.Point(6, 5);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(75, 20);
            this.lblDisplayName.TabIndex = 22;
            this.lblDisplayName.Text = "Display Name:";
            // 
            // switchAccount
            // 
            this.switchAccount.ButtonHeight = 22;
            this.switchAccount.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.switchAccount.Name = "switchAccount";
            this.switchAccount.OffText = "Retail";
            this.switchAccount.OnText = "Dev";
            this.switchAccount.Visible = false;
            // 
            // cmdSaveToDevice
            // 
            this.cmdSaveToDevice.AutoExpandOnClick = true;
            this.cmdSaveToDevice.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.cmdSaveToDevice.CanCustomize = false;
            this.cmdSaveToDevice.Image = global::Horizon.Properties.Resources.Partition;
            this.cmdSaveToDevice.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.cmdSaveToDevice.Name = "cmdSaveToDevice";
            this.cmdSaveToDevice.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSaveToDevice.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.cmdSaveToDevice.Text = "Save to Device";
            this.cmdSaveToDevice.Visible = false;
            this.cmdSaveToDevice.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.cmdSaveToDevice_PopupOpen);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Enabled = false;
            this.buttonItem1.Image = global::Horizon.Properties.Resources.Refresh;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "Refreshing Devices...";
            // 
            // cmdModPackage
            // 
            this.cmdModPackage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdModPackage.BackColor = System.Drawing.Color.Transparent;
            this.cmdModPackage.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdModPackage.Enabled = false;
            this.cmdModPackage.FocusCuesEnabled = false;
            this.cmdModPackage.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdModPackage.Image = ((System.Drawing.Image)(resources.GetObject("cmdModPackage.Image")));
            this.cmdModPackage.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.cmdModPackage.ImageTextSpacing = 3;
            this.cmdModPackage.Location = new System.Drawing.Point(234, 110);
            this.cmdModPackage.Name = "cmdModPackage";
            this.cmdModPackage.PopupSide = DevComponents.DotNetBar.ePopupSide.Left;
            this.cmdModPackage.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdModPackage.ShowSubItems = false;
            this.cmdModPackage.Size = new System.Drawing.Size(135, 83);
            this.cmdModPackage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdModPackage.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.galStfs});
            this.cmdModPackage.TabIndex = 41;
            this.cmdModPackage.Text = "Mod Package";
            this.cmdModPackage.Click += new System.EventHandler(this.cmdModPackage_Click);
            // 
            // galStfs
            // 
            // 
            // 
            // 
            this.galStfs.BackgroundStyle.Class = "";
            this.galStfs.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.galStfs.DefaultSize = new System.Drawing.Size(144, 405);
            this.galStfs.GlobalItem = false;
            this.galStfs.MinimumSize = new System.Drawing.Size(58, 58);
            this.galStfs.Name = "galStfs";
            this.galStfs.StretchGallery = true;
            // 
            // cmdViewContents
            // 
            this.cmdViewContents.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdViewContents.BackColor = System.Drawing.Color.Transparent;
            this.cmdViewContents.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdViewContents.FocusCuesEnabled = false;
            this.cmdViewContents.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdViewContents.Image = global::Horizon.Properties.Resources.RightArrow;
            this.cmdViewContents.ImagePosition = DevComponents.DotNetBar.eImagePosition.Bottom;
            this.cmdViewContents.ImageTextSpacing = 3;
            this.cmdViewContents.Location = new System.Drawing.Point(368, 110);
            this.cmdViewContents.Name = "cmdViewContents";
            this.cmdViewContents.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdViewContents.Size = new System.Drawing.Size(63, 83);
            this.cmdViewContents.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdViewContents.TabIndex = 40;
            this.cmdViewContents.Text = "Contents";
            this.cmdViewContents.Click += new System.EventHandler(this.cmdViewContents_Click);
            // 
            // cmdSaveSet1
            // 
            this.cmdSaveSet1.GlobalItem = false;
            this.cmdSaveSet1.Image = global::Horizon.Properties.Resources.SaveIcon;
            this.cmdSaveSet1.Name = "cmdSaveSet1";
            this.cmdSaveSet1.Tag = ((byte)(1));
            this.cmdSaveSet1.Text = "Save Set 1";
            // 
            // PackageManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 257);
            this.Name = "PackageManager";
            this.Text = "Package Manager";
            this.Controls.SetChildIndex(this.rbPackageEditor, 0);
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            this.menuContents.ResumeLayout(false);
            this.gpProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGamerpic)).EndInit();
            this.gpIcons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon2)).EndInit();
            this.menuIcon1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem tabContents;
        private DevComponents.DotNetBar.ButtonX cmdViewContents;
        private DevComponents.DotNetBar.Controls.GroupPanel gpProfile;
        private DevComponents.DotNetBar.Controls.GroupPanel gpIcons;
        private System.Windows.Forms.PictureBox pbIcon1;
        private System.Windows.Forms.PictureBox pbIcon2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTID;
        private DevComponents.DotNetBar.LabelX lblTID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCID;
        private DevComponents.DotNetBar.LabelX lblCID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDID;
        private DevComponents.DotNetBar.LabelX lblDID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPID;
        private DevComponents.DotNetBar.LabelX lblPID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTitleName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDisplayName;
        private DevComponents.DotNetBar.LabelX lblTitleName;
        private DevComponents.DotNetBar.LabelX lblDisplayName;
        private DevComponents.DotNetBar.ButtonX cmdExtractSelected;
        private DevComponents.DotNetBar.ButtonX cmdExtractAll;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private System.Windows.Forms.TreeView treeFolders;
        private DevComponents.DotNetBar.Controls.ListViewEx listFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip menuContents;
        private System.Windows.Forms.ToolStripMenuItem cmdExtract;
        private System.Windows.Forms.ToolStripMenuItem cmdOverwrite;
        private DevComponents.DotNetBar.SwitchButtonItem switchAccount;
        private System.Windows.Forms.ToolStripMenuItem cmdDelete;
        private System.Windows.Forms.ToolStripMenuItem cmdInject;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cmdRename;
        private DevComponents.DotNetBar.ButtonX cmdModPackage;
        private System.Windows.Forms.ContextMenuStrip menuIcon1;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        internal DevComponents.DotNetBar.ButtonItem cmdSaveToDevice;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.GalleryContainer galStfs;
        private DevComponents.DotNetBar.ButtonX cmdManageProfiles;
        private DevComponents.DotNetBar.ButtonX cmdSwitchProfile;
        private DevComponents.DotNetBar.ButtonItem cmdSaveSet1;
        private DevComponents.DotNetBar.LabelX lblGamertag;
        private System.Windows.Forms.PictureBox pbGamerpic;
        private DevComponents.DotNetBar.ButtonItem cmdNoProfiles;
        private System.Windows.Forms.ProgressBar progress;
        private DevComponents.DotNetBar.ButtonX cmdShare;
    }
}
