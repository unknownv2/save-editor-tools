namespace Horizon.Forms
{
    partial class TitleIDFinder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleIDFinder));
            this.rbTitleIDFinder = new DevComponents.DotNetBar.RibbonControl();
            this.rpSearch = new DevComponents.DotNetBar.RibbonPanel();
            this.cmdCopy = new DevComponents.DotNetBar.ButtonX();
            this.cmdSearch = new DevComponents.DotNetBar.ButtonX();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tabSearch = new DevComponents.DotNetBar.RibbonTabItem();
            this.listGames = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.pbGameImage = new System.Windows.Forms.PictureBox();
            this.rbTitleIDFinder.SuspendLayout();
            this.rpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGameImage)).BeginInit();
            this.SuspendLayout();
            // 
            // rbTitleIDFinder
            // 
            this.rbTitleIDFinder.AutoExpand = false;
            // 
            // 
            // 
            this.rbTitleIDFinder.BackgroundStyle.Class = "";
            this.rbTitleIDFinder.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbTitleIDFinder.CanCustomize = false;
            this.rbTitleIDFinder.CaptionVisible = true;
            this.rbTitleIDFinder.Controls.Add(this.rpSearch);
            this.rbTitleIDFinder.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbTitleIDFinder.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabSearch});
            this.rbTitleIDFinder.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.rbTitleIDFinder.Location = new System.Drawing.Point(5, 1);
            this.rbTitleIDFinder.Name = "rbTitleIDFinder";
            this.rbTitleIDFinder.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.rbTitleIDFinder.Size = new System.Drawing.Size(429, 84);
            this.rbTitleIDFinder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rbTitleIDFinder.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.rbTitleIDFinder.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.rbTitleIDFinder.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.rbTitleIDFinder.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.rbTitleIDFinder.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.rbTitleIDFinder.SystemText.QatDialogAddButton = "&Add >>";
            this.rbTitleIDFinder.SystemText.QatDialogCancelButton = "Cancel";
            this.rbTitleIDFinder.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.rbTitleIDFinder.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.rbTitleIDFinder.SystemText.QatDialogOkButton = "OK";
            this.rbTitleIDFinder.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.rbTitleIDFinder.SystemText.QatDialogRemoveButton = "&Remove";
            this.rbTitleIDFinder.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.rbTitleIDFinder.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.rbTitleIDFinder.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.rbTitleIDFinder.TabGroupHeight = 14;
            this.rbTitleIDFinder.TabIndex = 0;
            // 
            // rpSearch
            // 
            this.rpSearch.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.rpSearch.Controls.Add(this.cmdCopy);
            this.rpSearch.Controls.Add(this.cmdSearch);
            this.rpSearch.Controls.Add(this.txtSearch);
            this.rpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpSearch.Location = new System.Drawing.Point(0, 53);
            this.rpSearch.Name = "rpSearch";
            this.rpSearch.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.rpSearch.Size = new System.Drawing.Size(429, 29);
            // 
            // 
            // 
            this.rpSearch.Style.Class = "";
            this.rpSearch.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.rpSearch.StyleMouseDown.Class = "";
            this.rpSearch.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.rpSearch.StyleMouseOver.Class = "";
            this.rpSearch.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rpSearch.TabIndex = 1;
            // 
            // cmdCopy
            // 
            this.cmdCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdCopy.BackColor = System.Drawing.Color.Transparent;
            this.cmdCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdCopy.FocusCuesEnabled = false;
            this.cmdCopy.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCopy.Location = new System.Drawing.Point(338, 3);
            this.cmdCopy.Name = "cmdCopy";
            this.cmdCopy.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdCopy.Size = new System.Drawing.Size(85, 21);
            this.cmdCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdCopy.TabIndex = 3;
            this.cmdCopy.Text = "Copy Title ID";
            this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdSearch.BackColor = System.Drawing.Color.Transparent;
            this.cmdSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdSearch.FocusCuesEnabled = false;
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = global::Horizon.Properties.Resources.Magnifier;
            this.cmdSearch.Location = new System.Drawing.Point(233, 3);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdSearch.Size = new System.Drawing.Size(99, 21);
            this.cmdSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Search";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtSearch
            // 
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.Location = new System.Drawing.Point(6, 3);
            this.txtSearch.MaxLength = 100;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(221, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.WatermarkText = "Search...";
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // tabSearch
            // 
            this.tabSearch.Checked = true;
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Panel = this.rpSearch;
            this.tabSearch.Text = "Search for a Game";
            // 
            // listGames
            // 
            // 
            // 
            // 
            this.listGames.Border.Class = "ListViewBorder";
            this.listGames.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listGames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listGames.FullRowSelect = true;
            this.listGames.GridLines = true;
            this.listGames.Location = new System.Drawing.Point(5, 85);
            this.listGames.MultiSelect = false;
            this.listGames.Name = "listGames";
            this.listGames.Size = new System.Drawing.Size(332, 132);
            this.listGames.TabIndex = 1;
            this.listGames.UseCompatibleStateImageBehavior = false;
            this.listGames.View = System.Windows.Forms.View.Details;
            this.listGames.SelectedIndexChanged += new System.EventHandler(this.cmdCopy_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Game Name";
            this.columnHeader1.Width = 186;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Title ID";
            this.columnHeader2.Width = 90;
            // 
            // pbGameImage
            // 
            this.pbGameImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGameImage.Image = global::Horizon.Properties.Resources.TitleIdFinder_Default;
            this.pbGameImage.ImageLocation = "";
            this.pbGameImage.Location = new System.Drawing.Point(343, 91);
            this.pbGameImage.Name = "pbGameImage";
            this.pbGameImage.Size = new System.Drawing.Size(85, 120);
            this.pbGameImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGameImage.TabIndex = 2;
            this.pbGameImage.TabStop = false;
            this.pbGameImage.Click += new System.EventHandler(this.pbGameImage_Click);
            // 
            // TitleIDFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BottomLeftCornerSize = 0;
            this.BottomRightCornerSize = 0;
            this.ClientSize = new System.Drawing.Size(439, 222);
            this.Controls.Add(this.pbGameImage);
            this.Controls.Add(this.listGames);
            this.Controls.Add(this.rbTitleIDFinder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(439, 222);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(439, 222);
            this.Name = "TitleIDFinder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title ID Finder";
            this.rbTitleIDFinder.ResumeLayout(false);
            this.rbTitleIDFinder.PerformLayout();
            this.rpSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGameImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl rbTitleIDFinder;
        private DevComponents.DotNetBar.RibbonPanel rpSearch;
        private DevComponents.DotNetBar.RibbonTabItem tabSearch;
        private DevComponents.DotNetBar.ButtonX cmdSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private DevComponents.DotNetBar.Controls.ListViewEx listGames;
        private System.Windows.Forms.PictureBox pbGameImage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private DevComponents.DotNetBar.ButtonX cmdCopy;
    }
}