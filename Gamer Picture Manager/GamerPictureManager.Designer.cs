namespace Horizon.PackageEditors.Gamer_Picture_Manager
{
    partial class GamerPictureManager
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
            this.panelGamerpics = new DevComponents.DotNetBar.PanelEx();
            this.cmdOpenTitleIdFinder = new DevComponents.DotNetBar.ButtonX();
            this.exTitleIdFinder = new DevComponents.DotNetBar.ExpandablePanel();
            this.cmdUseThisGame = new DevComponents.DotNetBar.ButtonX();
            this.listTitles = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.colTitleName = new System.Windows.Forms.ColumnHeader();
            this.cmdSearchForTitle = new DevComponents.DotNetBar.ButtonX();
            this.txtGameName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelProfile = new DevComponents.DotNetBar.PanelEx();
            this.cmdCreatePack = new DevComponents.DotNetBar.ButtonX();
            this.panelProfilePics = new DevComponents.DotNetBar.PanelEx();
            this.panelProfileSelected = new DevComponents.DotNetBar.PanelEx();
            this.pbSelected = new System.Windows.Forms.PictureBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelProfileCurrent = new DevComponents.DotNetBar.PanelEx();
            this.pbCurrent = new System.Windows.Forms.PictureBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.panelTitleId = new DevComponents.DotNetBar.PanelEx();
            this.txtTitleId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmdGetGamerpics = new DevComponents.DotNetBar.ButtonX();
            this.lblTitleId = new DevComponents.DotNetBar.LabelX();
            this.listMyPics = new System.Windows.Forms.ListView();
            this.txtPackName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmdClearPics = new DevComponents.DotNetBar.ButtonX();
            this.listPics = new System.Windows.Forms.ListView();
            this.progressSearch = new System.Windows.Forms.ProgressBar();
            this.cmdStopSearch = new DevComponents.DotNetBar.ButtonX();
            this.cmdAddAll = new DevComponents.DotNetBar.ButtonX();
            this.cmdSaveToDevice = new DevComponents.DotNetBar.ButtonItem();
            this.cmdRefreshingDevices = new DevComponents.DotNetBar.ButtonItem();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelGamerpics.SuspendLayout();
            this.exTitleIdFinder.SuspendLayout();
            this.panelProfile.SuspendLayout();
            this.panelProfilePics.SuspendLayout();
            this.panelProfileSelected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelected)).BeginInit();
            this.panelProfileCurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrent)).BeginInit();
            this.panelTitleId.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdSaveToDevice});
            this.rbPackageEditor.Size = new System.Drawing.Size(540, 325);
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
            this.cmdOpen.FixedSize = new System.Drawing.Size(200, 23);
            this.cmdOpen.Text = "Open Profile or Gamerpic Package";
            // 
            // cmdSave
            // 
            this.cmdSave.FixedSize = new System.Drawing.Size(70, 23);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.cmdAddAll);
            this.panelMain.Controls.Add(this.cmdStopSearch);
            this.panelMain.Controls.Add(this.progressSearch);
            this.panelMain.Controls.Add(this.listPics);
            this.panelMain.Controls.Add(this.panelGamerpics);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(540, 270);
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
            this.tabMain.Text = "Gamer Pictures";
            // 
            // panelGamerpics
            // 
            this.panelGamerpics.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelGamerpics.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelGamerpics.Controls.Add(this.cmdOpenTitleIdFinder);
            this.panelGamerpics.Controls.Add(this.exTitleIdFinder);
            this.panelGamerpics.Controls.Add(this.panelProfile);
            this.panelGamerpics.Controls.Add(this.panelTitleId);
            this.panelGamerpics.Controls.Add(this.listMyPics);
            this.panelGamerpics.Controls.Add(this.txtPackName);
            this.panelGamerpics.Controls.Add(this.cmdClearPics);
            this.panelGamerpics.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelGamerpics.Location = new System.Drawing.Point(373, 0);
            this.panelGamerpics.Name = "panelGamerpics";
            this.panelGamerpics.Size = new System.Drawing.Size(164, 267);
            this.panelGamerpics.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelGamerpics.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelGamerpics.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelGamerpics.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelGamerpics.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelGamerpics.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelGamerpics.Style.GradientAngle = 90;
            this.panelGamerpics.TabIndex = 1;
            // 
            // cmdOpenTitleIdFinder
            // 
            this.cmdOpenTitleIdFinder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdOpenTitleIdFinder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdOpenTitleIdFinder.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmdOpenTitleIdFinder.FocusCuesEnabled = false;
            this.cmdOpenTitleIdFinder.Image = global::Horizon.Properties.Resources.DownArrow;
            this.cmdOpenTitleIdFinder.Location = new System.Drawing.Point(0, 0);
            this.cmdOpenTitleIdFinder.Name = "cmdOpenTitleIdFinder";
            this.cmdOpenTitleIdFinder.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdOpenTitleIdFinder.Size = new System.Drawing.Size(164, 25);
            this.cmdOpenTitleIdFinder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdOpenTitleIdFinder.TabIndex = 5;
            this.cmdOpenTitleIdFinder.Text = "Search for a Game";
            this.cmdOpenTitleIdFinder.Click += new System.EventHandler(this.cmdOpenTitleIdFinder_Click);
            // 
            // exTitleIdFinder
            // 
            this.exTitleIdFinder.CanvasColor = System.Drawing.SystemColors.Control;
            this.exTitleIdFinder.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.exTitleIdFinder.Controls.Add(this.cmdUseThisGame);
            this.exTitleIdFinder.Controls.Add(this.listTitles);
            this.exTitleIdFinder.Controls.Add(this.cmdSearchForTitle);
            this.exTitleIdFinder.Controls.Add(this.txtGameName);
            this.exTitleIdFinder.Expanded = false;
            this.exTitleIdFinder.ExpandedBounds = new System.Drawing.Rectangle(0, 5, 164, 262);
            this.exTitleIdFinder.Location = new System.Drawing.Point(0, 5);
            this.exTitleIdFinder.Name = "exTitleIdFinder";
            this.exTitleIdFinder.Size = new System.Drawing.Size(164, 20);
            this.exTitleIdFinder.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.exTitleIdFinder.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exTitleIdFinder.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.exTitleIdFinder.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.exTitleIdFinder.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.exTitleIdFinder.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.exTitleIdFinder.Style.GradientAngle = 90;
            this.exTitleIdFinder.TabIndex = 7;
            this.exTitleIdFinder.TitleHeight = 20;
            this.exTitleIdFinder.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.exTitleIdFinder.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.exTitleIdFinder.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.exTitleIdFinder.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.exTitleIdFinder.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.exTitleIdFinder.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.exTitleIdFinder.TitleStyle.GradientAngle = 90;
            this.exTitleIdFinder.TitleText = "Title Bar";
            // 
            // cmdUseThisGame
            // 
            this.cmdUseThisGame.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUseThisGame.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUseThisGame.FocusCuesEnabled = false;
            this.cmdUseThisGame.Image = global::Horizon.Properties.Resources.GreenDot;
            this.cmdUseThisGame.Location = new System.Drawing.Point(3, 233);
            this.cmdUseThisGame.Name = "cmdUseThisGame";
            this.cmdUseThisGame.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUseThisGame.Size = new System.Drawing.Size(158, 26);
            this.cmdUseThisGame.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUseThisGame.TabIndex = 6;
            this.cmdUseThisGame.Text = "Use this Game";
            this.cmdUseThisGame.Click += new System.EventHandler(this.cmdUseThisGame_Click);
            // 
            // listTitles
            // 
            this.listTitles.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            // 
            // 
            // 
            this.listTitles.Border.Class = "ListViewBorder";
            this.listTitles.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listTitles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTitleName});
            this.listTitles.FullRowSelect = true;
            this.listTitles.GridLines = true;
            this.listTitles.Location = new System.Drawing.Point(3, 67);
            this.listTitles.MultiSelect = false;
            this.listTitles.Name = "listTitles";
            this.listTitles.Size = new System.Drawing.Size(158, 167);
            this.listTitles.TabIndex = 7;
            this.listTitles.UseCompatibleStateImageBehavior = false;
            this.listTitles.View = System.Windows.Forms.View.Details;
            this.listTitles.ItemActivate += new System.EventHandler(this.listTitles_ItemActivate);
            // 
            // colTitleName
            // 
            this.colTitleName.Text = "Game Name";
            this.colTitleName.Width = 150;
            // 
            // cmdSearchForTitle
            // 
            this.cmdSearchForTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSearchForTitle.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSearchForTitle.FocusCuesEnabled = false;
            this.cmdSearchForTitle.Location = new System.Drawing.Point(3, 41);
            this.cmdSearchForTitle.Name = "cmdSearchForTitle";
            this.cmdSearchForTitle.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSearchForTitle.Size = new System.Drawing.Size(158, 24);
            this.cmdSearchForTitle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSearchForTitle.TabIndex = 2;
            this.cmdSearchForTitle.Text = "Search";
            this.cmdSearchForTitle.Click += new System.EventHandler(this.cmdSearchForTitle_Click);
            // 
            // txtGameName
            // 
            // 
            // 
            // 
            this.txtGameName.Border.Class = "TextBoxBorder";
            this.txtGameName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGameName.Location = new System.Drawing.Point(3, 22);
            this.txtGameName.Name = "txtGameName";
            this.txtGameName.Size = new System.Drawing.Size(158, 20);
            this.txtGameName.TabIndex = 1;
            this.txtGameName.WatermarkText = "Enter a Game Name...";
            this.txtGameName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGameName_KeyPress);
            // 
            // panelProfile
            // 
            this.panelProfile.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelProfile.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelProfile.Controls.Add(this.cmdCreatePack);
            this.panelProfile.Controls.Add(this.panelProfilePics);
            this.panelProfile.Location = new System.Drawing.Point(0, 79);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Size = new System.Drawing.Size(164, 188);
            this.panelProfile.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelProfile.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelProfile.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelProfile.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelProfile.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelProfile.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelProfile.Style.GradientAngle = 90;
            this.panelProfile.TabIndex = 9;
            this.panelProfile.Visible = false;
            // 
            // cmdCreatePack
            // 
            this.cmdCreatePack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdCreatePack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdCreatePack.FocusCuesEnabled = false;
            this.cmdCreatePack.Location = new System.Drawing.Point(3, 153);
            this.cmdCreatePack.Name = "cmdCreatePack";
            this.cmdCreatePack.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdCreatePack.Size = new System.Drawing.Size(158, 32);
            this.cmdCreatePack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdCreatePack.TabIndex = 6;
            this.cmdCreatePack.Text = "Create Picture Pack";
            this.cmdCreatePack.Click += new System.EventHandler(this.cmdCreatePack_Click);
            // 
            // panelProfilePics
            // 
            this.panelProfilePics.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelProfilePics.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelProfilePics.Controls.Add(this.panelProfileSelected);
            this.panelProfilePics.Controls.Add(this.panelProfileCurrent);
            this.panelProfilePics.Location = new System.Drawing.Point(3, 3);
            this.panelProfilePics.Name = "panelProfilePics";
            this.panelProfilePics.Size = new System.Drawing.Size(158, 148);
            this.panelProfilePics.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelProfilePics.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelProfilePics.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelProfilePics.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelProfilePics.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelProfilePics.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelProfilePics.Style.GradientAngle = 90;
            this.panelProfilePics.TabIndex = 1;
            // 
            // panelProfileSelected
            // 
            this.panelProfileSelected.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelProfileSelected.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelProfileSelected.Controls.Add(this.pbSelected);
            this.panelProfileSelected.Controls.Add(this.labelX1);
            this.panelProfileSelected.Location = new System.Drawing.Point(3, 75);
            this.panelProfileSelected.Name = "panelProfileSelected";
            this.panelProfileSelected.Size = new System.Drawing.Size(152, 70);
            this.panelProfileSelected.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelProfileSelected.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelProfileSelected.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelProfileSelected.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelProfileSelected.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelProfileSelected.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelProfileSelected.Style.GradientAngle = 90;
            this.panelProfileSelected.TabIndex = 5;
            // 
            // pbSelected
            // 
            this.pbSelected.Image = global::Horizon.Properties.Resources.Console;
            this.pbSelected.Location = new System.Drawing.Point(85, 3);
            this.pbSelected.Name = "pbSelected";
            this.pbSelected.Size = new System.Drawing.Size(64, 64);
            this.pbSelected.TabIndex = 2;
            this.pbSelected.TabStop = false;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(76, 64);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "Selected\r\nGamerpicture:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelProfileCurrent
            // 
            this.panelProfileCurrent.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelProfileCurrent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelProfileCurrent.Controls.Add(this.pbCurrent);
            this.panelProfileCurrent.Controls.Add(this.labelX2);
            this.panelProfileCurrent.Location = new System.Drawing.Point(3, 3);
            this.panelProfileCurrent.Name = "panelProfileCurrent";
            this.panelProfileCurrent.Size = new System.Drawing.Size(152, 70);
            this.panelProfileCurrent.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelProfileCurrent.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelProfileCurrent.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelProfileCurrent.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelProfileCurrent.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelProfileCurrent.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelProfileCurrent.Style.GradientAngle = 90;
            this.panelProfileCurrent.TabIndex = 4;
            // 
            // pbCurrent
            // 
            this.pbCurrent.Image = global::Horizon.Properties.Resources.Console;
            this.pbCurrent.Location = new System.Drawing.Point(85, 3);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(64, 64);
            this.pbCurrent.TabIndex = 2;
            this.pbCurrent.TabStop = false;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(3, 3);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(76, 64);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "Current\r\nGamerpicture:";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelTitleId
            // 
            this.panelTitleId.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelTitleId.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelTitleId.Controls.Add(this.txtTitleId);
            this.panelTitleId.Controls.Add(this.cmdGetGamerpics);
            this.panelTitleId.Controls.Add(this.lblTitleId);
            this.panelTitleId.Location = new System.Drawing.Point(3, 27);
            this.panelTitleId.Name = "panelTitleId";
            this.panelTitleId.Size = new System.Drawing.Size(158, 50);
            this.panelTitleId.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelTitleId.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelTitleId.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelTitleId.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelTitleId.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelTitleId.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelTitleId.Style.GradientAngle = 90;
            this.panelTitleId.TabIndex = 6;
            // 
            // txtTitleId
            // 
            // 
            // 
            // 
            this.txtTitleId.Border.Class = "TextBoxBorder";
            this.txtTitleId.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTitleId.Location = new System.Drawing.Point(38, 3);
            this.txtTitleId.MaxLength = 64;
            this.txtTitleId.Name = "txtTitleId";
            this.txtTitleId.Size = new System.Drawing.Size(117, 20);
            this.txtTitleId.TabIndex = 0;
            this.txtTitleId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTitleId_KeyPress);
            // 
            // cmdGetGamerpics
            // 
            this.cmdGetGamerpics.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdGetGamerpics.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdGetGamerpics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cmdGetGamerpics.FocusCuesEnabled = false;
            this.cmdGetGamerpics.Image = global::Horizon.Properties.Resources.Magnifier;
            this.cmdGetGamerpics.Location = new System.Drawing.Point(0, 25);
            this.cmdGetGamerpics.Name = "cmdGetGamerpics";
            this.cmdGetGamerpics.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdGetGamerpics.Size = new System.Drawing.Size(158, 25);
            this.cmdGetGamerpics.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdGetGamerpics.TabIndex = 1;
            this.cmdGetGamerpics.Text = "Get Gamer Pictures";
            this.cmdGetGamerpics.Click += new System.EventHandler(this.cmdGetGamerpics_Click);
            // 
            // lblTitleId
            // 
            // 
            // 
            // 
            this.lblTitleId.BackgroundStyle.Class = "";
            this.lblTitleId.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitleId.Location = new System.Drawing.Point(5, 3);
            this.lblTitleId.Name = "lblTitleId";
            this.lblTitleId.Size = new System.Drawing.Size(27, 20);
            this.lblTitleId.TabIndex = 4;
            this.lblTitleId.Text = "Title:";
            // 
            // listMyPics
            // 
            this.listMyPics.BackgroundImage = global::Horizon.Properties.Resources.Tile;
            this.listMyPics.BackgroundImageTiled = true;
            this.listMyPics.Location = new System.Drawing.Point(0, 79);
            this.listMyPics.MultiSelect = false;
            this.listMyPics.Name = "listMyPics";
            this.listMyPics.Size = new System.Drawing.Size(164, 163);
            this.listMyPics.TabIndex = 3;
            this.listMyPics.TileSize = new System.Drawing.Size(69, 69);
            this.listMyPics.UseCompatibleStateImageBehavior = false;
            this.listMyPics.View = System.Windows.Forms.View.Tile;
            this.listMyPics.SelectedIndexChanged += new System.EventHandler(this.listMyPics_SelectedIndexChanged);
            this.listMyPics.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listMyPics_MouseUp);
            // 
            // txtPackName
            // 
            // 
            // 
            // 
            this.txtPackName.Border.Class = "TextBoxBorder";
            this.txtPackName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPackName.Location = new System.Drawing.Point(3, 244);
            this.txtPackName.MaxLength = 64;
            this.txtPackName.Name = "txtPackName";
            this.txtPackName.Size = new System.Drawing.Size(100, 20);
            this.txtPackName.TabIndex = 8;
            this.txtPackName.WatermarkText = "My Picture Pack";
            // 
            // cmdClearPics
            // 
            this.cmdClearPics.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdClearPics.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdClearPics.Enabled = false;
            this.cmdClearPics.FocusCuesEnabled = false;
            this.cmdClearPics.Location = new System.Drawing.Point(102, 244);
            this.cmdClearPics.Name = "cmdClearPics";
            this.cmdClearPics.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdClearPics.Size = new System.Drawing.Size(59, 20);
            this.cmdClearPics.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdClearPics.TabIndex = 10;
            this.cmdClearPics.Text = "Clear";
            this.cmdClearPics.Click += new System.EventHandler(this.cmdClearPics_Click);
            // 
            // listPics
            // 
            this.listPics.BackColor = System.Drawing.SystemColors.ControlLight;
            this.listPics.Location = new System.Drawing.Point(3, 0);
            this.listPics.MultiSelect = false;
            this.listPics.Name = "listPics";
            this.listPics.Size = new System.Drawing.Size(371, 242);
            this.listPics.TabIndex = 2;
            this.listPics.TileSize = new System.Drawing.Size(69, 69);
            this.listPics.UseCompatibleStateImageBehavior = false;
            this.listPics.View = System.Windows.Forms.View.Tile;
            this.listPics.SelectedIndexChanged += new System.EventHandler(this.listPics_SelectedIndexChanged);
            this.listPics.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listPics_MouseUp);
            // 
            // progressSearch
            // 
            this.progressSearch.Location = new System.Drawing.Point(3, 244);
            this.progressSearch.Name = "progressSearch";
            this.progressSearch.Size = new System.Drawing.Size(151, 23);
            this.progressSearch.TabIndex = 3;
            // 
            // cmdStopSearch
            // 
            this.cmdStopSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdStopSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdStopSearch.Enabled = false;
            this.cmdStopSearch.FocusCuesEnabled = false;
            this.cmdStopSearch.Location = new System.Drawing.Point(156, 244);
            this.cmdStopSearch.Name = "cmdStopSearch";
            this.cmdStopSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdStopSearch.Size = new System.Drawing.Size(88, 23);
            this.cmdStopSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdStopSearch.TabIndex = 4;
            this.cmdStopSearch.Text = "Stop Searching";
            this.cmdStopSearch.Click += new System.EventHandler(this.cmdStopSearch_Click);
            // 
            // cmdAddAll
            // 
            this.cmdAddAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAddAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdAddAll.Enabled = false;
            this.cmdAddAll.FocusCuesEnabled = false;
            this.cmdAddAll.Image = global::Horizon.Properties.Resources.Plus;
            this.cmdAddAll.Location = new System.Drawing.Point(243, 244);
            this.cmdAddAll.Name = "cmdAddAll";
            this.cmdAddAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdAddAll.Size = new System.Drawing.Size(128, 23);
            this.cmdAddAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdAddAll.TabIndex = 5;
            this.cmdAddAll.Text = "Add All Displayed";
            this.cmdAddAll.Click += new System.EventHandler(this.cmdAddAll_Click);
            // 
            // cmdSaveToDevice
            // 
            this.cmdSaveToDevice.AutoExpandOnClick = true;
            this.cmdSaveToDevice.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.cmdSaveToDevice.Image = global::Horizon.Properties.Resources.Partition;
            this.cmdSaveToDevice.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.cmdSaveToDevice.Name = "cmdSaveToDevice";
            this.cmdSaveToDevice.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSaveToDevice.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdRefreshingDevices});
            this.cmdSaveToDevice.Text = "Save to Device";
            this.cmdSaveToDevice.PopupOpen += new DevComponents.DotNetBar.DotNetBarManager.PopupOpenEventHandler(this.cmdSaveToDevice_PopupOpen);
            // 
            // cmdRefreshingDevices
            // 
            this.cmdRefreshingDevices.Enabled = false;
            this.cmdRefreshingDevices.Image = global::Horizon.Properties.Resources.Refresh;
            this.cmdRefreshingDevices.Name = "cmdRefreshingDevices";
            this.cmdRefreshingDevices.Text = "Refreshing Devices...";
            // 
            // GamerPictureManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 328);
            this.Name = "GamerPictureManager";
            this.Text = "Horizon Gamer Picture Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GamerPicturePackCreator_FormClosing);
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelGamerpics.ResumeLayout(false);
            this.exTitleIdFinder.ResumeLayout(false);
            this.panelProfile.ResumeLayout(false);
            this.panelProfilePics.ResumeLayout(false);
            this.panelProfileSelected.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelected)).EndInit();
            this.panelProfileCurrent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCurrent)).EndInit();
            this.panelTitleId.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelGamerpics;
        private DevComponents.DotNetBar.ButtonX cmdGetGamerpics;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTitleId;
        private System.Windows.Forms.ListView listPics;
        private System.Windows.Forms.ListView listMyPics;
        private DevComponents.DotNetBar.LabelX lblTitleId;
        private DevComponents.DotNetBar.ButtonX cmdOpenTitleIdFinder;
        private DevComponents.DotNetBar.PanelEx panelTitleId;
        private DevComponents.DotNetBar.ButtonX cmdStopSearch;
        private System.Windows.Forms.ProgressBar progressSearch;
        private DevComponents.DotNetBar.ButtonX cmdAddAll;
        private DevComponents.DotNetBar.ButtonItem cmdSaveToDevice;
        private DevComponents.DotNetBar.ButtonItem cmdRefreshingDevices;
        private DevComponents.DotNetBar.ExpandablePanel exTitleIdFinder;
        private DevComponents.DotNetBar.ButtonX cmdSearchForTitle;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGameName;
        private DevComponents.DotNetBar.ButtonX cmdUseThisGame;
        private DevComponents.DotNetBar.Controls.ListViewEx listTitles;
        private System.Windows.Forms.ColumnHeader colTitleName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPackName;
        private DevComponents.DotNetBar.PanelEx panelProfile;
        private DevComponents.DotNetBar.PanelEx panelProfilePics;
        private DevComponents.DotNetBar.ButtonX cmdCreatePack;
        private DevComponents.DotNetBar.PanelEx panelProfileSelected;
        private System.Windows.Forms.PictureBox pbSelected;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.PanelEx panelProfileCurrent;
        private System.Windows.Forms.PictureBox pbCurrent;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.ButtonX cmdClearPics;
    }
}
