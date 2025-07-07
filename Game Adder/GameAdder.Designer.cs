namespace Horizon.PackageEditors.Game_Adder
{
    partial class GameAdder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabQueue = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdAddToProfile = new DevComponents.DotNetBar.ButtonX();
            this.cmdGoList = new DevComponents.DotNetBar.ButtonX();
            this.cmdRemove = new DevComponents.DotNetBar.ButtonX();
            this.listQueue = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colTile = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listTitles = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colTitleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTitleID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGamerscore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAchievements = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabAchievements = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
            this.listAchievements = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdGoQueue = new DevComponents.DotNetBar.ButtonX();
            this.cmdAdd = new DevComponents.DotNetBar.ButtonX();
            this.tabAwards = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel3 = new DevComponents.DotNetBar.RibbonPanel();
            this.listAwards = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelQuickSearch = new DevComponents.DotNetBar.PanelEx();
            this.cmdNext = new DevComponents.DotNetBar.ButtonX();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cmdFilter = new DevComponents.DotNetBar.ButtonItem();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listTitles)).BeginInit();
            this.ribbonPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listAchievements)).BeginInit();
            this.ribbonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listAwards)).BeginInit();
            this.panelQuickSearch.SuspendLayout();
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
            this.rbPackageEditor.Controls.Add(this.ribbonPanel3);
            this.rbPackageEditor.Controls.Add(this.ribbonPanel2);
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabQueue,
            this.tabAchievements,
            this.tabAwards,
            this.cmdFilter});
            this.rbPackageEditor.Size = new System.Drawing.Size(547, 329);
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
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel2, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel3, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.ribbonPanel1, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelQuickSearch);
            this.panelMain.Controls.Add(this.cmdGoQueue);
            this.panelMain.Controls.Add(this.cmdAdd);
            this.panelMain.Controls.Add(this.listTitles);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(547, 274);
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
            this.tabMain.Text = "Game List";
            // 
            // tabQueue
            // 
            this.tabQueue.Name = "tabQueue";
            this.tabQueue.Panel = this.ribbonPanel1;
            this.tabQueue.Text = "Queue";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.cmdAddToProfile);
            this.ribbonPanel1.Controls.Add(this.cmdGoList);
            this.ribbonPanel1.Controls.Add(this.cmdRemove);
            this.ribbonPanel1.Controls.Add(this.listQueue);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(547, 274);
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
            // cmdAddToProfile
            // 
            this.cmdAddToProfile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAddToProfile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdAddToProfile.FocusCuesEnabled = false;
            this.cmdAddToProfile.Image = global::Horizon.Properties.Resources.Plus;
            this.cmdAddToProfile.Location = new System.Drawing.Point(263, 240);
            this.cmdAddToProfile.Name = "cmdAddToProfile";
            this.cmdAddToProfile.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdAddToProfile.Size = new System.Drawing.Size(281, 31);
            this.cmdAddToProfile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdAddToProfile.TabIndex = 4;
            this.cmdAddToProfile.Text = "Add Queue to Profile";
            this.cmdAddToProfile.Click += new System.EventHandler(this.cmdAddToProfile_Click);
            // 
            // cmdGoList
            // 
            this.cmdGoList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdGoList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdGoList.FocusCuesEnabled = false;
            this.cmdGoList.Image = global::Horizon.Properties.Resources.LeftArrow;
            this.cmdGoList.Location = new System.Drawing.Point(3, 240);
            this.cmdGoList.Name = "cmdGoList";
            this.cmdGoList.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdGoList.Size = new System.Drawing.Size(125, 31);
            this.cmdGoList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdGoList.TabIndex = 3;
            this.cmdGoList.Text = "Go to List";
            this.cmdGoList.Click += new System.EventHandler(this.cmdGoList_Click);
            // 
            // cmdRemove
            // 
            this.cmdRemove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdRemove.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdRemove.FocusCuesEnabled = false;
            this.cmdRemove.Image = global::Horizon.Properties.Resources.Delete;
            this.cmdRemove.Location = new System.Drawing.Point(130, 240);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdRemove.Size = new System.Drawing.Size(131, 31);
            this.cmdRemove.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdRemove.TabIndex = 2;
            this.cmdRemove.Text = "Remove Selected";
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // listQueue
            // 
            this.listQueue.AllowUserToAddRows = false;
            this.listQueue.AllowUserToDeleteRows = false;
            this.listQueue.AllowUserToResizeColumns = false;
            this.listQueue.AllowUserToResizeRows = false;
            this.listQueue.BackgroundColor = System.Drawing.Color.Silver;
            this.listQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.listQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTile,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listQueue.DefaultCellStyle = dataGridViewCellStyle4;
            this.listQueue.Dock = System.Windows.Forms.DockStyle.Top;
            this.listQueue.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.listQueue.Location = new System.Drawing.Point(3, 0);
            this.listQueue.Name = "listQueue";
            this.listQueue.ReadOnly = true;
            this.listQueue.RowHeadersVisible = false;
            this.listQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listQueue.ShowEditingIcon = false;
            this.listQueue.Size = new System.Drawing.Size(541, 238);
            this.listQueue.TabIndex = 1;
            this.listQueue.SelectionChanged += new System.EventHandler(this.listQueue_SelectionChanged);
            // 
            // colTile
            // 
            this.colTile.HeaderText = "";
            this.colTile.Name = "colTile";
            this.colTile.ReadOnly = true;
            this.colTile.Width = 66;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Title Info";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn3.HeaderText = "GS";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn3.Width = 50;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "Ach #";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // listTitles
            // 
            this.listTitles.AllowUserToAddRows = false;
            this.listTitles.AllowUserToDeleteRows = false;
            this.listTitles.AllowUserToResizeColumns = false;
            this.listTitles.AllowUserToResizeRows = false;
            this.listTitles.BackgroundColor = System.Drawing.Color.Silver;
            this.listTitles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.listTitles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTitleName,
            this.colTitleID,
            this.colGamerscore,
            this.colAchievements});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listTitles.DefaultCellStyle = dataGridViewCellStyle16;
            this.listTitles.Dock = System.Windows.Forms.DockStyle.Top;
            this.listTitles.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.listTitles.Location = new System.Drawing.Point(3, 0);
            this.listTitles.Name = "listTitles";
            this.listTitles.ReadOnly = true;
            this.listTitles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.listTitles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listTitles.ShowEditingIcon = false;
            this.listTitles.ShowRowErrors = false;
            this.listTitles.Size = new System.Drawing.Size(541, 238);
            this.listTitles.TabIndex = 0;
            this.listTitles.SelectionChanged += new System.EventHandler(this.listTitles_SelectionChanged);
            // 
            // colTitleName
            // 
            this.colTitleName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            this.colTitleName.DefaultCellStyle = dataGridViewCellStyle12;
            this.colTitleName.HeaderText = "Title Name";
            this.colTitleName.MinimumWidth = 100;
            this.colTitleName.Name = "colTitleName";
            this.colTitleName.ReadOnly = true;
            this.colTitleName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colTitleID
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colTitleID.DefaultCellStyle = dataGridViewCellStyle13;
            this.colTitleID.HeaderText = "Title ID";
            this.colTitleID.MinimumWidth = 50;
            this.colTitleID.Name = "colTitleID";
            this.colTitleID.ReadOnly = true;
            this.colTitleID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colTitleID.Width = 75;
            // 
            // colGamerscore
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colGamerscore.DefaultCellStyle = dataGridViewCellStyle14;
            this.colGamerscore.HeaderText = "GS";
            this.colGamerscore.Name = "colGamerscore";
            this.colGamerscore.ReadOnly = true;
            this.colGamerscore.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colGamerscore.Width = 50;
            // 
            // colAchievements
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colAchievements.DefaultCellStyle = dataGridViewCellStyle15;
            this.colAchievements.HeaderText = "Ach #";
            this.colAchievements.Name = "colAchievements";
            this.colAchievements.ReadOnly = true;
            this.colAchievements.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAchievements.Width = 50;
            // 
            // tabAchievements
            // 
            this.tabAchievements.Name = "tabAchievements";
            this.tabAchievements.Panel = this.ribbonPanel2;
            this.tabAchievements.Text = "Achievements";
            this.tabAchievements.Visible = false;
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel2.Controls.Add(this.listAchievements);
            this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel2.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel2.Size = new System.Drawing.Size(547, 274);
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
            // listAchievements
            // 
            this.listAchievements.AllowUserToAddRows = false;
            this.listAchievements.AllowUserToDeleteRows = false;
            this.listAchievements.AllowUserToResizeColumns = false;
            this.listAchievements.AllowUserToResizeRows = false;
            this.listAchievements.BackgroundColor = System.Drawing.Color.Silver;
            this.listAchievements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.listAchievements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listAchievements.DefaultCellStyle = dataGridViewCellStyle11;
            this.listAchievements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAchievements.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.listAchievements.Location = new System.Drawing.Point(3, 0);
            this.listAchievements.MultiSelect = false;
            this.listAchievements.Name = "listAchievements";
            this.listAchievements.ReadOnly = true;
            this.listAchievements.RowHeadersVisible = false;
            this.listAchievements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listAchievements.ShowEditingIcon = false;
            this.listAchievements.Size = new System.Drawing.Size(541, 271);
            this.listAchievements.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn5.HeaderText = "Achievement";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn5.Width = 140;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn7.HeaderText = "GS";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn7.Width = 35;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn8.HeaderText = "Description";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // cmdGoQueue
            // 
            this.cmdGoQueue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdGoQueue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdGoQueue.FocusCuesEnabled = false;
            this.cmdGoQueue.Image = global::Horizon.Properties.Resources.RightArrow;
            this.cmdGoQueue.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.cmdGoQueue.Location = new System.Drawing.Point(419, 240);
            this.cmdGoQueue.Name = "cmdGoQueue";
            this.cmdGoQueue.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdGoQueue.Size = new System.Drawing.Size(125, 31);
            this.cmdGoQueue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdGoQueue.TabIndex = 2;
            this.cmdGoQueue.Text = "Go to Queue";
            this.cmdGoQueue.Click += new System.EventHandler(this.cmdGoQueue_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdAdd.FocusCuesEnabled = false;
            this.cmdAdd.Image = global::Horizon.Properties.Resources.Plus;
            this.cmdAdd.Location = new System.Drawing.Point(208, 240);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdAdd.Size = new System.Drawing.Size(209, 31);
            this.cmdAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdAdd.TabIndex = 1;
            this.cmdAdd.Text = "Add Selected Games to Queue";
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // tabAwards
            // 
            this.tabAwards.Name = "tabAwards";
            this.tabAwards.Panel = this.ribbonPanel3;
            this.tabAwards.Text = "Awards";
            this.tabAwards.Visible = false;
            // 
            // ribbonPanel3
            // 
            this.ribbonPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel3.Controls.Add(this.listAwards);
            this.ribbonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel3.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel3.Name = "ribbonPanel3";
            this.ribbonPanel3.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel3.Size = new System.Drawing.Size(547, 274);
            // 
            // 
            // 
            this.ribbonPanel3.Style.Class = "";
            this.ribbonPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel3.StyleMouseDown.Class = "";
            this.ribbonPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel3.StyleMouseOver.Class = "";
            this.ribbonPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel3.TabIndex = 4;
            this.ribbonPanel3.Visible = false;
            // 
            // listAwards
            // 
            this.listAwards.AllowUserToAddRows = false;
            this.listAwards.AllowUserToDeleteRows = false;
            this.listAwards.AllowUserToResizeColumns = false;
            this.listAwards.AllowUserToResizeRows = false;
            this.listAwards.BackgroundColor = System.Drawing.Color.Silver;
            this.listAwards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.listAwards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn10});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.listAwards.DefaultCellStyle = dataGridViewCellStyle7;
            this.listAwards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAwards.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(113)))), ((int)(((byte)(113)))));
            this.listAwards.Location = new System.Drawing.Point(3, 0);
            this.listAwards.MultiSelect = false;
            this.listAwards.Name = "listAwards";
            this.listAwards.ReadOnly = true;
            this.listAwards.RowHeadersVisible = false;
            this.listAwards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.listAwards.ShowEditingIcon = false;
            this.listAwards.Size = new System.Drawing.Size(541, 271);
            this.listAwards.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn6.HeaderText = "Awards";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn6.Width = 140;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn10.HeaderText = "Description";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // panelQuickSearch
            // 
            this.panelQuickSearch.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelQuickSearch.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelQuickSearch.Controls.Add(this.cmdNext);
            this.panelQuickSearch.Controls.Add(this.txtSearch);
            this.panelQuickSearch.Location = new System.Drawing.Point(3, 240);
            this.panelQuickSearch.Name = "panelQuickSearch";
            this.panelQuickSearch.Size = new System.Drawing.Size(203, 31);
            this.panelQuickSearch.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelQuickSearch.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelQuickSearch.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelQuickSearch.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelQuickSearch.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelQuickSearch.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelQuickSearch.Style.GradientAngle = 90;
            this.panelQuickSearch.TabIndex = 3;
            // 
            // cmdNext
            // 
            this.cmdNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdNext.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdNext.Dock = System.Windows.Forms.DockStyle.Right;
            this.cmdNext.FocusCuesEnabled = false;
            this.cmdNext.Location = new System.Drawing.Point(150, 0);
            this.cmdNext.Name = "cmdNext";
            this.cmdNext.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdNext.Size = new System.Drawing.Size(53, 31);
            this.cmdNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdNext.TabIndex = 1;
            this.cmdNext.Text = "Next";
            this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.Location = new System.Drawing.Point(5, 5);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(141, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.WatermarkText = "Quick search...";
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cmdFilter
            // 
            this.cmdFilter.AutoCheckOnClick = true;
            this.cmdFilter.Checked = true;
            this.cmdFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdFilter.Enabled = false;
            this.cmdFilter.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.cmdFilter.Name = "cmdFilter";
            this.cmdFilter.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdFilter.Text = "Title Filter";
            this.cmdFilter.CheckedChanged += new System.EventHandler(this.cmdFilter_CheckedChanged);
            // 
            // GameAdder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 332);
            this.Name = "GameAdder";
            this.Text = "Horizon Game Adder";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listTitles)).EndInit();
            this.ribbonPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listAchievements)).EndInit();
            this.ribbonPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listAwards)).EndInit();
            this.panelQuickSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.RibbonTabItem tabQueue;
        private DevComponents.DotNetBar.Controls.DataGridViewX listTitles;
        private DevComponents.DotNetBar.ButtonX cmdAdd;
        private DevComponents.DotNetBar.ButtonX cmdGoQueue;
        private DevComponents.DotNetBar.ButtonX cmdGoList;
        private DevComponents.DotNetBar.ButtonX cmdRemove;
        private DevComponents.DotNetBar.Controls.DataGridViewX listQueue;
        private DevComponents.DotNetBar.ButtonX cmdAddToProfile;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel2;
        private DevComponents.DotNetBar.Controls.DataGridViewX listAchievements;
        private DevComponents.DotNetBar.RibbonTabItem tabAchievements;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitleID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGamerscore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAchievements;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel3;
        private DevComponents.DotNetBar.RibbonTabItem tabAwards;
        private DevComponents.DotNetBar.Controls.DataGridViewX listAwards;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DevComponents.DotNetBar.PanelEx panelQuickSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private DevComponents.DotNetBar.ButtonX cmdNext;
        private System.Windows.Forms.DataGridViewImageColumn colTile;
        private DevComponents.DotNetBar.Controls.DataGridViewLabelXColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DevComponents.DotNetBar.ButtonItem cmdFilter;
    }
}
