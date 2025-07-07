namespace Horizon.PackageEditors.Achievement_Unlocker
{
    partial class AchievementUnlocker
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AchievementUnlocker));
            this.tabGame = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdExtractGPD = new DevComponents.DotNetBar.ButtonX();
            this.cmdUnlockAll = new DevComponents.DotNetBar.ButtonX();
            this.pGameAchievements = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.pGameGamerscore = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.lblLastPlayed = new DevComponents.DotNetBar.LabelX();
            this.lblTitleID = new DevComponents.DotNetBar.LabelX();
            this.lblGame = new DevComponents.DotNetBar.LabelX();
            this.pbGame = new System.Windows.Forms.PictureBox();
            this.tabAchievement = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
            this.panelAchievement = new DevComponents.DotNetBar.PanelEx();
            this.cmdSetAchievement = new DevComponents.DotNetBar.ButtonX();
            this.lblGamerscore = new DevComponents.DotNetBar.LabelX();
            this.dateUnlocked = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.ckUnlockedOffline = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckUnlockedOnline = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.lblUnlockedDescription = new DevComponents.DotNetBar.LabelX();
            this.lblLockedDescription = new DevComponents.DotNetBar.LabelX();
            this.pbAchievement = new System.Windows.Forms.PictureBox();
            this.listGames = new System.Windows.Forms.ListView();
            this.col1 = new System.Windows.Forms.ColumnHeader();
            this.col2 = new System.Windows.Forms.ColumnHeader();
            this.col3 = new System.Windows.Forms.ColumnHeader();
            this.gpGameSearch = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmdClearGameSearch = new DevComponents.DotNetBar.ButtonX();
            this.txtGameSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.listAchievements = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.colAchievement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpAchievementSearch = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmdClearAchievementSearch = new DevComponents.DotNetBar.ButtonX();
            this.ckAchievementLocked = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckAchievementUnlockedOnline = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.ckAchievementUnlockedOffline = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtAchievementSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblMetaBio = new DevComponents.DotNetBar.LabelX();
            this.pProfileAchievements = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.pProfileGamerscore = new DevComponents.DotNetBar.Controls.ProgressBarX();
            this.lblProfileID = new DevComponents.DotNetBar.LabelX();
            this.lblMetaLocation = new DevComponents.DotNetBar.LabelX();
            this.lblMetaName = new DevComponents.DotNetBar.LabelX();
            this.lblMetaMotto = new DevComponents.DotNetBar.LabelX();
            this.lblMetaGamerZone = new DevComponents.DotNetBar.LabelX();
            this.pbProfile = new System.Windows.Forms.PictureBox();
            this.cmdUnlockAllAchievements = new DevComponents.DotNetBar.Office2007StartButton();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGame)).BeginInit();
            this.ribbonPanel2.SuspendLayout();
            this.panelAchievement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateUnlocked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAchievement)).BeginInit();
            this.gpGameSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listAchievements)).BeginInit();
            this.gpAchievementSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Controls.Add(this.ribbonPanel2);
            this.rbPackageEditor.Controls.Add(this.ribbonPanel1);
            this.rbPackageEditor.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabGame,
            this.tabAchievement,
            this.cmdUnlockAllAchievements});
            this.rbPackageEditor.Size = new System.Drawing.Size(840, 133);
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
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel2, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblMetaBio);
            this.panelMain.Controls.Add(this.pProfileAchievements);
            this.panelMain.Controls.Add(this.pProfileGamerscore);
            this.panelMain.Controls.Add(this.lblProfileID);
            this.panelMain.Controls.Add(this.lblMetaLocation);
            this.panelMain.Controls.Add(this.lblMetaName);
            this.panelMain.Controls.Add(this.lblMetaMotto);
            this.panelMain.Controls.Add(this.lblMetaGamerZone);
            this.panelMain.Controls.Add(this.pbProfile);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(840, 78);
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
            this.tabMain.Text = "Profile";
            // 
            // tabGame
            // 
            this.tabGame.Name = "tabGame";
            this.tabGame.Panel = this.ribbonPanel1;
            this.tabGame.Text = "Game";
            this.tabGame.Visible = false;
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.cmdExtractGPD);
            this.ribbonPanel1.Controls.Add(this.cmdUnlockAll);
            this.ribbonPanel1.Controls.Add(this.pGameAchievements);
            this.ribbonPanel1.Controls.Add(this.pGameGamerscore);
            this.ribbonPanel1.Controls.Add(this.lblLastPlayed);
            this.ribbonPanel1.Controls.Add(this.lblTitleID);
            this.ribbonPanel1.Controls.Add(this.lblGame);
            this.ribbonPanel1.Controls.Add(this.pbGame);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(840, 78);
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
            // cmdExtractGPD
            // 
            this.cmdExtractGPD.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExtractGPD.BackColor = System.Drawing.Color.Transparent;
            this.cmdExtractGPD.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExtractGPD.FocusCuesEnabled = false;
            this.cmdExtractGPD.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExtractGPD.Image = global::Horizon.Properties.Resources.UpArrowSilver;
            this.cmdExtractGPD.Location = new System.Drawing.Point(391, 40);
            this.cmdExtractGPD.Name = "cmdExtractGPD";
            this.cmdExtractGPD.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdExtractGPD.Size = new System.Drawing.Size(119, 31);
            this.cmdExtractGPD.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExtractGPD.TabIndex = 20;
            this.cmdExtractGPD.Text = "Extract GPD";
            this.cmdExtractGPD.Click += new System.EventHandler(this.cmdExtractGPD_Click);
            // 
            // cmdUnlockAll
            // 
            this.cmdUnlockAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdUnlockAll.BackColor = System.Drawing.Color.Transparent;
            this.cmdUnlockAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdUnlockAll.FocusCuesEnabled = false;
            this.cmdUnlockAll.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUnlockAll.Location = new System.Drawing.Point(391, 3);
            this.cmdUnlockAll.Name = "cmdUnlockAll";
            this.cmdUnlockAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdUnlockAll.Size = new System.Drawing.Size(119, 31);
            this.cmdUnlockAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdUnlockAll.TabIndex = 19;
            this.cmdUnlockAll.Text = "Unlock All Displayed";
            this.cmdUnlockAll.Click += new System.EventHandler(this.cmdUnlockAll_Click);
            // 
            // pGameAchievements
            // 
            this.pGameAchievements.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pGameAchievements.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pGameAchievements.BackgroundStyle.Class = "";
            this.pGameAchievements.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pGameAchievements.Location = new System.Drawing.Point(516, 40);
            this.pGameAchievements.Name = "pGameAchievements";
            this.pGameAchievements.Size = new System.Drawing.Size(320, 31);
            this.pGameAchievements.TabIndex = 18;
            this.pGameAchievements.TextVisible = true;
            // 
            // pGameGamerscore
            // 
            this.pGameGamerscore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pGameGamerscore.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pGameGamerscore.BackgroundStyle.Class = "";
            this.pGameGamerscore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pGameGamerscore.Location = new System.Drawing.Point(516, 3);
            this.pGameGamerscore.Name = "pGameGamerscore";
            this.pGameGamerscore.Size = new System.Drawing.Size(320, 31);
            this.pGameGamerscore.TabIndex = 17;
            this.pGameGamerscore.TextVisible = true;
            // 
            // lblLastPlayed
            // 
            this.lblLastPlayed.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblLastPlayed.BackgroundStyle.Class = "";
            this.lblLastPlayed.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblLastPlayed.Location = new System.Drawing.Point(76, 53);
            this.lblLastPlayed.Name = "lblLastPlayed";
            this.lblLastPlayed.Size = new System.Drawing.Size(302, 19);
            this.lblLastPlayed.TabIndex = 16;
            this.lblLastPlayed.UseMnemonic = false;
            // 
            // lblTitleID
            // 
            this.lblTitleID.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTitleID.BackgroundStyle.Class = "";
            this.lblTitleID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTitleID.Location = new System.Drawing.Point(76, 28);
            this.lblTitleID.Name = "lblTitleID";
            this.lblTitleID.Size = new System.Drawing.Size(302, 19);
            this.lblTitleID.TabIndex = 15;
            this.lblTitleID.UseMnemonic = false;
            // 
            // lblGame
            // 
            this.lblGame.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblGame.BackgroundStyle.Class = "";
            this.lblGame.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGame.Location = new System.Drawing.Point(76, 3);
            this.lblGame.Name = "lblGame";
            this.lblGame.Size = new System.Drawing.Size(302, 19);
            this.lblGame.TabIndex = 14;
            this.lblGame.UseMnemonic = false;
            // 
            // pbGame
            // 
            this.pbGame.BackColor = System.Drawing.Color.Transparent;
            this.pbGame.Location = new System.Drawing.Point(6, 6);
            this.pbGame.Name = "pbGame";
            this.pbGame.Size = new System.Drawing.Size(64, 64);
            this.pbGame.TabIndex = 13;
            this.pbGame.TabStop = false;
            // 
            // tabAchievement
            // 
            this.tabAchievement.Name = "tabAchievement";
            this.tabAchievement.Panel = this.ribbonPanel2;
            this.tabAchievement.Text = "Achievement";
            this.tabAchievement.Visible = false;
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel2.Controls.Add(this.panelAchievement);
            this.ribbonPanel2.Controls.Add(this.lblUnlockedDescription);
            this.ribbonPanel2.Controls.Add(this.lblLockedDescription);
            this.ribbonPanel2.Controls.Add(this.pbAchievement);
            this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel2.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel2.Size = new System.Drawing.Size(840, 78);
            // 
            // 
            // 
            this.ribbonPanel2.Style.Class = "";
            this.ribbonPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseDown.Class = "";
            this.ribbonPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseOver.Class = "";
            this.ribbonPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel2.TabIndex = 3;
            this.ribbonPanel2.Visible = false;
            // 
            // panelAchievement
            // 
            this.panelAchievement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAchievement.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelAchievement.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelAchievement.Controls.Add(this.cmdSetAchievement);
            this.panelAchievement.Controls.Add(this.lblGamerscore);
            this.panelAchievement.Controls.Add(this.dateUnlocked);
            this.panelAchievement.Controls.Add(this.ckUnlockedOffline);
            this.panelAchievement.Controls.Add(this.ckUnlockedOnline);
            this.panelAchievement.Location = new System.Drawing.Point(595, -1);
            this.panelAchievement.Name = "panelAchievement";
            this.panelAchievement.Size = new System.Drawing.Size(251, 76);
            this.panelAchievement.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelAchievement.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelAchievement.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelAchievement.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelAchievement.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelAchievement.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelAchievement.Style.GradientAngle = 90;
            this.panelAchievement.TabIndex = 10;
            // 
            // cmdSetAchievement
            // 
            this.cmdSetAchievement.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSetAchievement.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSetAchievement.FocusCuesEnabled = false;
            this.cmdSetAchievement.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSetAchievement.Location = new System.Drawing.Point(192, 6);
            this.cmdSetAchievement.Name = "cmdSetAchievement";
            this.cmdSetAchievement.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSetAchievement.Size = new System.Drawing.Size(52, 39);
            this.cmdSetAchievement.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSetAchievement.TabIndex = 5;
            this.cmdSetAchievement.Text = "Set";
            this.cmdSetAchievement.Click += new System.EventHandler(this.cmdSetAchievement_Click);
            // 
            // lblGamerscore
            // 
            // 
            // 
            // 
            this.lblGamerscore.BackgroundStyle.Class = "";
            this.lblGamerscore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblGamerscore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGamerscore.Image = global::Horizon.Properties.Resources.Gamerscore;
            this.lblGamerscore.Location = new System.Drawing.Point(115, 6);
            this.lblGamerscore.Name = "lblGamerscore";
            this.lblGamerscore.Size = new System.Drawing.Size(71, 39);
            this.lblGamerscore.TabIndex = 4;
            this.lblGamerscore.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dateUnlocked
            // 
            // 
            // 
            // 
            this.dateUnlocked.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateUnlocked.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateUnlocked.ButtonDropDown.Visible = true;
            this.dateUnlocked.CustomFormat = "MM/dd/yyyy h:m:s tt";
            this.dateUnlocked.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dateUnlocked.Location = new System.Drawing.Point(5, 51);
            this.dateUnlocked.MaxDate = new System.DateTime(2015, 12, 31, 0, 0, 0, 0);
            this.dateUnlocked.MinDate = new System.DateTime(2005, 9, 1, 0, 0, 0, 0);
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dateUnlocked.MonthCalendar.BackgroundStyle.Class = "";
            this.dateUnlocked.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dateUnlocked.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.MonthCalendar.DisplayMonth = new System.DateTime(2010, 7, 1, 0, 0, 0, 0);
            this.dateUnlocked.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateUnlocked.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dateUnlocked.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateUnlocked.MonthCalendar.TodayButtonVisible = true;
            this.dateUnlocked.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateUnlocked.Name = "dateUnlocked";
            this.dateUnlocked.Size = new System.Drawing.Size(239, 20);
            this.dateUnlocked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dateUnlocked.TabIndex = 3;
            // 
            // ckUnlockedOffline
            // 
            // 
            // 
            // 
            this.ckUnlockedOffline.BackgroundStyle.Class = "";
            this.ckUnlockedOffline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckUnlockedOffline.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckUnlockedOffline.FocusCuesEnabled = false;
            this.ckUnlockedOffline.Location = new System.Drawing.Point(3, 2);
            this.ckUnlockedOffline.Name = "ckUnlockedOffline";
            this.ckUnlockedOffline.Size = new System.Drawing.Size(106, 20);
            this.ckUnlockedOffline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckUnlockedOffline.TabIndex = 0;
            this.ckUnlockedOffline.Text = "Unlocked Offline";
            this.ckUnlockedOffline.CheckedChanged += new System.EventHandler(this.ckUnlocked_CheckedChanged);
            // 
            // ckUnlockedOnline
            // 
            // 
            // 
            // 
            this.ckUnlockedOnline.BackgroundStyle.Class = "";
            this.ckUnlockedOnline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckUnlockedOnline.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.ckUnlockedOnline.FocusCuesEnabled = false;
            this.ckUnlockedOnline.Location = new System.Drawing.Point(3, 27);
            this.ckUnlockedOnline.Name = "ckUnlockedOnline";
            this.ckUnlockedOnline.Size = new System.Drawing.Size(106, 20);
            this.ckUnlockedOnline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckUnlockedOnline.TabIndex = 1;
            this.ckUnlockedOnline.Text = "Unlocked Online";
            this.ckUnlockedOnline.CheckedChanged += new System.EventHandler(this.ckUnlocked_CheckedChanged);
            // 
            // lblUnlockedDescription
            // 
            this.lblUnlockedDescription.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblUnlockedDescription.BackgroundStyle.Class = "";
            this.lblUnlockedDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblUnlockedDescription.Location = new System.Drawing.Point(76, 40);
            this.lblUnlockedDescription.Name = "lblUnlockedDescription";
            this.lblUnlockedDescription.Size = new System.Drawing.Size(507, 30);
            this.lblUnlockedDescription.TabIndex = 9;
            this.lblUnlockedDescription.UseMnemonic = false;
            // 
            // lblLockedDescription
            // 
            this.lblLockedDescription.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblLockedDescription.BackgroundStyle.Class = "";
            this.lblLockedDescription.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblLockedDescription.Location = new System.Drawing.Point(76, 6);
            this.lblLockedDescription.Name = "lblLockedDescription";
            this.lblLockedDescription.Size = new System.Drawing.Size(507, 30);
            this.lblLockedDescription.TabIndex = 8;
            this.lblLockedDescription.UseMnemonic = false;
            // 
            // pbAchievement
            // 
            this.pbAchievement.BackColor = System.Drawing.Color.Transparent;
            this.pbAchievement.Image = global::Horizon.Properties.Resources.Unearned;
            this.pbAchievement.Location = new System.Drawing.Point(6, 6);
            this.pbAchievement.Name = "pbAchievement";
            this.pbAchievement.Size = new System.Drawing.Size(64, 64);
            this.pbAchievement.TabIndex = 7;
            this.pbAchievement.TabStop = false;
            // 
            // listGames
            // 
            this.listGames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col1,
            this.col2,
            this.col3});
            this.listGames.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listGames.FullRowSelect = true;
            this.listGames.Location = new System.Drawing.Point(5, 132);
            this.listGames.MultiSelect = false;
            this.listGames.Name = "listGames";
            this.listGames.Size = new System.Drawing.Size(229, 507);
            this.listGames.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listGames.TabIndex = 5;
            this.listGames.UseCompatibleStateImageBehavior = false;
            this.listGames.View = System.Windows.Forms.View.Tile;
            this.listGames.SelectedIndexChanged += new System.EventHandler(this.listGames_SelectedIndexChanged);
            // 
            // gpGameSearch
            // 
            this.gpGameSearch.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpGameSearch.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpGameSearch.Controls.Add(this.cmdClearGameSearch);
            this.gpGameSearch.Controls.Add(this.txtGameSearch);
            this.gpGameSearch.Location = new System.Drawing.Point(5, 640);
            this.gpGameSearch.Name = "gpGameSearch";
            this.gpGameSearch.Size = new System.Drawing.Size(229, 28);
            // 
            // 
            // 
            this.gpGameSearch.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpGameSearch.Style.BackColorGradientAngle = 90;
            this.gpGameSearch.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpGameSearch.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpGameSearch.Style.BorderBottomWidth = 1;
            this.gpGameSearch.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpGameSearch.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpGameSearch.Style.BorderLeftWidth = 1;
            this.gpGameSearch.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpGameSearch.Style.BorderRightWidth = 1;
            this.gpGameSearch.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpGameSearch.Style.BorderTopWidth = 1;
            this.gpGameSearch.Style.Class = "";
            this.gpGameSearch.Style.CornerDiameter = 4;
            this.gpGameSearch.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpGameSearch.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpGameSearch.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpGameSearch.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpGameSearch.StyleMouseDown.Class = "";
            this.gpGameSearch.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpGameSearch.StyleMouseOver.Class = "";
            this.gpGameSearch.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpGameSearch.TabIndex = 3;
            // 
            // cmdClearGameSearch
            // 
            this.cmdClearGameSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdClearGameSearch.BackColor = System.Drawing.Color.Transparent;
            this.cmdClearGameSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdClearGameSearch.FocusCuesEnabled = false;
            this.cmdClearGameSearch.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClearGameSearch.Location = new System.Drawing.Point(175, 3);
            this.cmdClearGameSearch.Name = "cmdClearGameSearch";
            this.cmdClearGameSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdClearGameSearch.Size = new System.Drawing.Size(49, 20);
            this.cmdClearGameSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdClearGameSearch.TabIndex = 1;
            this.cmdClearGameSearch.Text = "Clear";
            this.cmdClearGameSearch.Click += new System.EventHandler(this.cmdClearGameSearch_Click);
            // 
            // txtGameSearch
            // 
            // 
            // 
            // 
            this.txtGameSearch.Border.Class = "TextBoxBorder";
            this.txtGameSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGameSearch.Location = new System.Drawing.Point(3, 3);
            this.txtGameSearch.MaxLength = 64;
            this.txtGameSearch.Name = "txtGameSearch";
            this.txtGameSearch.Size = new System.Drawing.Size(169, 20);
            this.txtGameSearch.TabIndex = 0;
            this.txtGameSearch.WatermarkText = "Find game or title ID...";
            this.txtGameSearch.TextChanged += new System.EventHandler(this.txtGameSearch_TextChanged);
            // 
            // listAchievements
            // 
            this.listAchievements.AllowUserToAddRows = false;
            this.listAchievements.AllowUserToDeleteRows = false;
            this.listAchievements.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listAchievements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.listAchievements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listAchievements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcon,
            this.colAchievement,
            this.colDescription});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listAchievements.DefaultCellStyle = dataGridViewCellStyle4;
            this.listAchievements.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.listAchievements.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.listAchievements.HighlightSelectedColumnHeaders = false;
            this.listAchievements.Location = new System.Drawing.Point(235, 132);
            this.listAchievements.Name = "listAchievements";
            this.listAchievements.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.listAchievements.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.listAchievements.RowHeadersVisible = false;
            this.listAchievements.SelectAllSignVisible = false;
            this.listAchievements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listAchievements.ShowCellErrors = false;
            this.listAchievements.ShowCellToolTips = false;
            this.listAchievements.ShowEditingIcon = false;
            this.listAchievements.ShowRowErrors = false;
            this.listAchievements.Size = new System.Drawing.Size(610, 507);
            this.listAchievements.TabIndex = 7;
            this.listAchievements.SelectionChanged += new System.EventHandler(this.listAchievements_SelectionChanged);
            // 
            // colIcon
            // 
            this.colIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colIcon.HeaderText = "Icon";
            this.colIcon.MinimumWidth = 66;
            this.colIcon.Name = "colIcon";
            this.colIcon.ReadOnly = true;
            this.colIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colIcon.Width = 66;
            // 
            // colAchievement
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F);
            this.colAchievement.DefaultCellStyle = dataGridViewCellStyle2;
            this.colAchievement.HeaderText = "Achievement";
            this.colAchievement.Name = "colAchievement";
            this.colAchievement.ReadOnly = true;
            this.colAchievement.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAchievement.Width = 175;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Unicode MS", 8.25F);
            this.colDescription.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // gpAchievementSearch
            // 
            this.gpAchievementSearch.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpAchievementSearch.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpAchievementSearch.Controls.Add(this.cmdClearAchievementSearch);
            this.gpAchievementSearch.Controls.Add(this.ckAchievementLocked);
            this.gpAchievementSearch.Controls.Add(this.ckAchievementUnlockedOnline);
            this.gpAchievementSearch.Controls.Add(this.ckAchievementUnlockedOffline);
            this.gpAchievementSearch.Controls.Add(this.txtAchievementSearch);
            this.gpAchievementSearch.Location = new System.Drawing.Point(235, 640);
            this.gpAchievementSearch.Name = "gpAchievementSearch";
            this.gpAchievementSearch.Size = new System.Drawing.Size(610, 28);
            // 
            // 
            // 
            this.gpAchievementSearch.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpAchievementSearch.Style.BackColorGradientAngle = 90;
            this.gpAchievementSearch.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpAchievementSearch.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAchievementSearch.Style.BorderBottomWidth = 1;
            this.gpAchievementSearch.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpAchievementSearch.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAchievementSearch.Style.BorderLeftWidth = 1;
            this.gpAchievementSearch.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAchievementSearch.Style.BorderRightWidth = 1;
            this.gpAchievementSearch.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAchievementSearch.Style.BorderTopWidth = 1;
            this.gpAchievementSearch.Style.Class = "";
            this.gpAchievementSearch.Style.CornerDiameter = 4;
            this.gpAchievementSearch.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpAchievementSearch.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpAchievementSearch.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpAchievementSearch.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpAchievementSearch.StyleMouseDown.Class = "";
            this.gpAchievementSearch.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpAchievementSearch.StyleMouseOver.Class = "";
            this.gpAchievementSearch.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpAchievementSearch.TabIndex = 4;
            // 
            // cmdClearAchievementSearch
            // 
            this.cmdClearAchievementSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdClearAchievementSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClearAchievementSearch.BackColor = System.Drawing.Color.Transparent;
            this.cmdClearAchievementSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdClearAchievementSearch.FocusCuesEnabled = false;
            this.cmdClearAchievementSearch.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClearAchievementSearch.Location = new System.Drawing.Point(248, 3);
            this.cmdClearAchievementSearch.Name = "cmdClearAchievementSearch";
            this.cmdClearAchievementSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdClearAchievementSearch.Size = new System.Drawing.Size(49, 20);
            this.cmdClearAchievementSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdClearAchievementSearch.TabIndex = 4;
            this.cmdClearAchievementSearch.Text = "Clear";
            this.cmdClearAchievementSearch.Click += new System.EventHandler(this.cmdClearAchievementSearch_Click);
            // 
            // ckAchievementLocked
            // 
            this.ckAchievementLocked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckAchievementLocked.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ckAchievementLocked.BackgroundStyle.Class = "";
            this.ckAchievementLocked.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckAchievementLocked.Checked = true;
            this.ckAchievementLocked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAchievementLocked.CheckValue = "Y";
            this.ckAchievementLocked.FocusCuesEnabled = false;
            this.ckAchievementLocked.Location = new System.Drawing.Point(303, 3);
            this.ckAchievementLocked.Name = "ckAchievementLocked";
            this.ckAchievementLocked.Size = new System.Drawing.Size(63, 20);
            this.ckAchievementLocked.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckAchievementLocked.TabIndex = 3;
            this.ckAchievementLocked.Text = "Locked";
            this.ckAchievementLocked.CheckedChanged += new System.EventHandler(this.populateAchievementList);
            // 
            // ckAchievementUnlockedOnline
            // 
            this.ckAchievementUnlockedOnline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckAchievementUnlockedOnline.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ckAchievementUnlockedOnline.BackgroundStyle.Class = "";
            this.ckAchievementUnlockedOnline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckAchievementUnlockedOnline.Checked = true;
            this.ckAchievementUnlockedOnline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAchievementUnlockedOnline.CheckValue = "Y";
            this.ckAchievementUnlockedOnline.FocusCuesEnabled = false;
            this.ckAchievementUnlockedOnline.Location = new System.Drawing.Point(482, 3);
            this.ckAchievementUnlockedOnline.Name = "ckAchievementUnlockedOnline";
            this.ckAchievementUnlockedOnline.Size = new System.Drawing.Size(102, 20);
            this.ckAchievementUnlockedOnline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckAchievementUnlockedOnline.TabIndex = 2;
            this.ckAchievementUnlockedOnline.Text = "Unlocked Online";
            this.ckAchievementUnlockedOnline.CheckedChanged += new System.EventHandler(this.populateAchievementList);
            // 
            // ckAchievementUnlockedOffline
            // 
            this.ckAchievementUnlockedOffline.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckAchievementUnlockedOffline.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ckAchievementUnlockedOffline.BackgroundStyle.Class = "";
            this.ckAchievementUnlockedOffline.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckAchievementUnlockedOffline.Checked = true;
            this.ckAchievementUnlockedOffline.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckAchievementUnlockedOffline.CheckValue = "Y";
            this.ckAchievementUnlockedOffline.FocusCuesEnabled = false;
            this.ckAchievementUnlockedOffline.Location = new System.Drawing.Point(370, 3);
            this.ckAchievementUnlockedOffline.Name = "ckAchievementUnlockedOffline";
            this.ckAchievementUnlockedOffline.Size = new System.Drawing.Size(104, 20);
            this.ckAchievementUnlockedOffline.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ckAchievementUnlockedOffline.TabIndex = 1;
            this.ckAchievementUnlockedOffline.Text = "Unlocked Offline";
            this.ckAchievementUnlockedOffline.CheckedChanged += new System.EventHandler(this.populateAchievementList);
            // 
            // txtAchievementSearch
            // 
            this.txtAchievementSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtAchievementSearch.Border.Class = "TextBoxBorder";
            this.txtAchievementSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAchievementSearch.Location = new System.Drawing.Point(3, 3);
            this.txtAchievementSearch.MaxLength = 64;
            this.txtAchievementSearch.Name = "txtAchievementSearch";
            this.txtAchievementSearch.Size = new System.Drawing.Size(242, 20);
            this.txtAchievementSearch.TabIndex = 0;
            this.txtAchievementSearch.WatermarkText = "Find an achievement...";
            this.txtAchievementSearch.TextChanged += new System.EventHandler(this.populateAchievementList);
            // 
            // lblMetaBio
            // 
            this.lblMetaBio.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMetaBio.BackgroundStyle.Class = "";
            this.lblMetaBio.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMetaBio.Location = new System.Drawing.Point(503, 0);
            this.lblMetaBio.Name = "lblMetaBio";
            this.lblMetaBio.Size = new System.Drawing.Size(334, 77);
            this.lblMetaBio.TabIndex = 18;
            this.lblMetaBio.Text = "<b>Bio:</b> N/A";
            this.lblMetaBio.UseMnemonic = false;
            this.lblMetaBio.Visible = false;
            this.lblMetaBio.WordWrap = true;
            // 
            // pProfileAchievements
            // 
            this.pProfileAchievements.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pProfileAchievements.BackgroundStyle.Class = "";
            this.pProfileAchievements.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pProfileAchievements.Location = new System.Drawing.Point(516, 40);
            this.pProfileAchievements.Name = "pProfileAchievements";
            this.pProfileAchievements.Size = new System.Drawing.Size(320, 31);
            this.pProfileAchievements.TabIndex = 17;
            this.pProfileAchievements.Text = "0/0 Achievements Unlocked";
            this.pProfileAchievements.TextVisible = true;
            // 
            // pProfileGamerscore
            // 
            this.pProfileGamerscore.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.pProfileGamerscore.BackgroundStyle.Class = "";
            this.pProfileGamerscore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.pProfileGamerscore.Location = new System.Drawing.Point(516, 3);
            this.pProfileGamerscore.Name = "pProfileGamerscore";
            this.pProfileGamerscore.Size = new System.Drawing.Size(320, 31);
            this.pProfileGamerscore.TabIndex = 16;
            this.pProfileGamerscore.Text = "0/0 Gamerscore";
            this.pProfileGamerscore.TextVisible = true;
            // 
            // lblProfileID
            // 
            this.lblProfileID.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblProfileID.BackgroundStyle.Class = "";
            this.lblProfileID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblProfileID.Location = new System.Drawing.Point(232, 28);
            this.lblProfileID.Name = "lblProfileID";
            this.lblProfileID.Size = new System.Drawing.Size(265, 19);
            this.lblProfileID.TabIndex = 15;
            this.lblProfileID.Text = "<b>Profile ID:</b> N/A";
            this.lblProfileID.UseMnemonic = false;
            // 
            // lblMetaLocation
            // 
            this.lblMetaLocation.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMetaLocation.BackgroundStyle.Class = "";
            this.lblMetaLocation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMetaLocation.Location = new System.Drawing.Point(232, 3);
            this.lblMetaLocation.Name = "lblMetaLocation";
            this.lblMetaLocation.Size = new System.Drawing.Size(265, 19);
            this.lblMetaLocation.TabIndex = 14;
            this.lblMetaLocation.Text = "<b>Location:</b> N/A";
            this.lblMetaLocation.UseMnemonic = false;
            // 
            // lblMetaName
            // 
            this.lblMetaName.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMetaName.BackgroundStyle.Class = "";
            this.lblMetaName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMetaName.Location = new System.Drawing.Point(76, 53);
            this.lblMetaName.Name = "lblMetaName";
            this.lblMetaName.Size = new System.Drawing.Size(421, 19);
            this.lblMetaName.TabIndex = 13;
            this.lblMetaName.Text = "<b>Name:</b> N/A";
            this.lblMetaName.UseMnemonic = false;
            // 
            // lblMetaMotto
            // 
            this.lblMetaMotto.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMetaMotto.BackgroundStyle.Class = "";
            this.lblMetaMotto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMetaMotto.Location = new System.Drawing.Point(76, 28);
            this.lblMetaMotto.Name = "lblMetaMotto";
            this.lblMetaMotto.Size = new System.Drawing.Size(150, 19);
            this.lblMetaMotto.TabIndex = 12;
            this.lblMetaMotto.Text = "<b>Motto:</b> N/A";
            this.lblMetaMotto.UseMnemonic = false;
            // 
            // lblMetaGamerZone
            // 
            this.lblMetaGamerZone.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMetaGamerZone.BackgroundStyle.Class = "";
            this.lblMetaGamerZone.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMetaGamerZone.Location = new System.Drawing.Point(76, 3);
            this.lblMetaGamerZone.Name = "lblMetaGamerZone";
            this.lblMetaGamerZone.Size = new System.Drawing.Size(150, 19);
            this.lblMetaGamerZone.TabIndex = 11;
            this.lblMetaGamerZone.Text = "<b>Gamerzone:</b> N/A";
            this.lblMetaGamerZone.UseMnemonic = false;
            // 
            // pbProfile
            // 
            this.pbProfile.BackColor = System.Drawing.Color.Transparent;
            this.pbProfile.Image = global::Horizon.Properties.Resources.Console;
            this.pbProfile.Location = new System.Drawing.Point(6, 6);
            this.pbProfile.Name = "pbProfile";
            this.pbProfile.Size = new System.Drawing.Size(64, 64);
            this.pbProfile.TabIndex = 10;
            this.pbProfile.TabStop = false;
            this.pbProfile.MouseLeave += new System.EventHandler(this.pbProfile_Mouse);
            this.pbProfile.MouseEnter += new System.EventHandler(this.pbProfile_Mouse);
            // 
            // cmdUnlockAllAchievements
            // 
            this.cmdUnlockAllAchievements.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            this.cmdUnlockAllAchievements.CanCustomize = false;
            this.cmdUnlockAllAchievements.ColorTable = DevComponents.DotNetBar.eButtonColor.Magenta;
            this.cmdUnlockAllAchievements.FixedSize = new System.Drawing.Size(110, 23);
            this.cmdUnlockAllAchievements.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdUnlockAllAchievements.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdUnlockAllAchievements.ImagePaddingHorizontal = 0;
            this.cmdUnlockAllAchievements.ImagePaddingVertical = 0;
            this.cmdUnlockAllAchievements.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.cmdUnlockAllAchievements.Name = "cmdUnlockAllAchievements";
            this.cmdUnlockAllAchievements.ShowSubItems = false;
            this.cmdUnlockAllAchievements.Text = "Unlock Everything";
            this.cmdUnlockAllAchievements.Visible = false;
            this.cmdUnlockAllAchievements.Click += new System.EventHandler(this.cmdUnlockAllAchievements_Click);
            // 
            // AchievementUnlocker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(850, 672);
            this.Controls.Add(this.listGames);
            this.Controls.Add(this.gpGameSearch);
            this.Controls.Add(this.listAchievements);
            this.Controls.Add(this.gpAchievementSearch);
            this.EnableGlass = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(850, 672);
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(850, 672);
            this.Name = "AchievementUnlocker";
            this.Text = "Achievement Unlocker";
            this.Controls.SetChildIndex(this.rbPackageEditor, 0);
            this.Controls.SetChildIndex(this.gpAchievementSearch, 0);
            this.Controls.SetChildIndex(this.listAchievements, 0);
            this.Controls.SetChildIndex(this.gpGameSearch, 0);
            this.Controls.SetChildIndex(this.listGames, 0);
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGame)).EndInit();
            this.ribbonPanel2.ResumeLayout(false);
            this.panelAchievement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateUnlocked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAchievement)).EndInit();
            this.gpGameSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listAchievements)).EndInit();
            this.gpAchievementSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem tabGame;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel2;
        private DevComponents.DotNetBar.RibbonTabItem tabAchievement;
        private System.Windows.Forms.ListView listGames;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ColumnHeader col2;
        private System.Windows.Forms.ColumnHeader col3;
        private DevComponents.DotNetBar.Controls.GroupPanel gpGameSearch;
        private DevComponents.DotNetBar.ButtonX cmdClearGameSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX txtGameSearch;
        private DevComponents.DotNetBar.Controls.DataGridViewX listAchievements;
        private DevComponents.DotNetBar.Controls.GroupPanel gpAchievementSearch;
        private DevComponents.DotNetBar.ButtonX cmdClearAchievementSearch;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckAchievementLocked;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckAchievementUnlockedOnline;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckAchievementUnlockedOffline;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAchievementSearch;
        private DevComponents.DotNetBar.LabelX lblMetaBio;
        private DevComponents.DotNetBar.Controls.ProgressBarX pProfileAchievements;
        private DevComponents.DotNetBar.Controls.ProgressBarX pProfileGamerscore;
        private DevComponents.DotNetBar.LabelX lblProfileID;
        private DevComponents.DotNetBar.LabelX lblMetaLocation;
        private DevComponents.DotNetBar.LabelX lblMetaName;
        private DevComponents.DotNetBar.LabelX lblMetaMotto;
        private DevComponents.DotNetBar.LabelX lblMetaGamerZone;
        private System.Windows.Forms.PictureBox pbProfile;
        private DevComponents.DotNetBar.ButtonX cmdExtractGPD;
        private DevComponents.DotNetBar.ButtonX cmdUnlockAll;
        private DevComponents.DotNetBar.Controls.ProgressBarX pGameAchievements;
        private DevComponents.DotNetBar.Controls.ProgressBarX pGameGamerscore;
        private DevComponents.DotNetBar.LabelX lblLastPlayed;
        private DevComponents.DotNetBar.LabelX lblTitleID;
        private DevComponents.DotNetBar.LabelX lblGame;
        private System.Windows.Forms.PictureBox pbGame;
        private DevComponents.DotNetBar.PanelEx panelAchievement;
        private DevComponents.DotNetBar.ButtonX cmdSetAchievement;
        private DevComponents.DotNetBar.LabelX lblGamerscore;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateUnlocked;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckUnlockedOffline;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckUnlockedOnline;
        private DevComponents.DotNetBar.LabelX lblUnlockedDescription;
        private DevComponents.DotNetBar.LabelX lblLockedDescription;
        private System.Windows.Forms.PictureBox pbAchievement;
        private System.Windows.Forms.DataGridViewImageColumn colIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAchievement;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private DevComponents.DotNetBar.Office2007StartButton cmdUnlockAllAchievements;
    }
}
