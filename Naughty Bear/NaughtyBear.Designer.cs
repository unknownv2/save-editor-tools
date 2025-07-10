namespace Horizon.PackageEditors.Naughty_Bear
{
    partial class NaughtyBear
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NaughtyBear));
            this.intLevelScore = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lstUnlockedLevels = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmdMaxAmmo1 = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intLevelScore)).BeginInit();
            this.ribbonPanel1.SuspendLayout();
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
            this.rbPackageEditor.Size = new System.Drawing.Size(309, 275);
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
            this.panelMain.Controls.Add(this.cmdMaxAmmo1);
            this.panelMain.Controls.Add(this.lstUnlockedLevels);
            this.panelMain.Controls.Add(this.intLevelScore);
            this.panelMain.Controls.Add(this.labelX1);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(309, 220);
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
            // intLevelScore
            // 
            this.intLevelScore.AllowEmptyState = false;
            // 
            // 
            // 
            this.intLevelScore.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intLevelScore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intLevelScore.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intLevelScore.Location = new System.Drawing.Point(169, 164);
            this.intLevelScore.MinValue = 0;
            this.intLevelScore.Name = "intLevelScore";
            this.intLevelScore.ShowUpDown = true;
            this.intLevelScore.Size = new System.Drawing.Size(134, 20);
            this.intLevelScore.TabIndex = 27;
            this.intLevelScore.ValueChanged += new System.EventHandler(this.intLevelScore_ValueChanged);
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 164);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(151, 19);
            this.labelX1.TabIndex = 26;
            this.labelX1.Text = "Score for selected level:";
            // 
            // lstUnlockedLevels
            // 
            // 
            // 
            // 
            this.lstUnlockedLevels.Border.Class = "ListViewBorder";
            this.lstUnlockedLevels.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lstUnlockedLevels.CheckBoxes = true;
            this.lstUnlockedLevels.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstUnlockedLevels.FullRowSelect = true;
            this.lstUnlockedLevels.GridLines = true;
            this.lstUnlockedLevels.Location = new System.Drawing.Point(6, 3);
            this.lstUnlockedLevels.MultiSelect = false;
            this.lstUnlockedLevels.Name = "lstUnlockedLevels";
            this.lstUnlockedLevels.Size = new System.Drawing.Size(297, 155);
            this.lstUnlockedLevels.TabIndex = 28;
            this.lstUnlockedLevels.UseCompatibleStateImageBehavior = false;
            this.lstUnlockedLevels.View = System.Windows.Forms.View.Details;
            this.lstUnlockedLevels.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lstUnlockedLevels_ItemChecked);
            this.lstUnlockedLevels.SelectedIndexChanged += new System.EventHandler(this.lstUnlockedLevels_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Level Name";
            this.columnHeader1.Width = 262;
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "Instructions";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.labelX2);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 53);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ribbonPanel1.Size = new System.Drawing.Size(309, 220);
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
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelX2.Location = new System.Drawing.Point(3, 0);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(303, 217);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = resources.GetString("labelX2.Text");
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            this.labelX2.WordWrap = true;
            // 
            // cmdMaxAmmo1
            // 
            this.cmdMaxAmmo1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxAmmo1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxAmmo1.FocusCuesEnabled = false;
            this.cmdMaxAmmo1.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMaxAmmo1.Location = new System.Drawing.Point(6, 190);
            this.cmdMaxAmmo1.Name = "cmdMaxAmmo1";
            this.cmdMaxAmmo1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxAmmo1.Size = new System.Drawing.Size(297, 24);
            this.cmdMaxAmmo1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxAmmo1.TabIndex = 29;
            this.cmdMaxAmmo1.Text = "Complete All and Max Stats";
            this.cmdMaxAmmo1.Click += new System.EventHandler(this.cmdMaxAmmo1_Click);
            // 
            // NaughtyBear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 278);
            this.Name = "NaughtyBear";
            this.Text = "Naughty Bear Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intLevelScore)).EndInit();
            this.ribbonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.IntegerInput intLevelScore;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ListViewEx lstUnlockedLevels;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabItem1;
        private DevComponents.DotNetBar.ButtonX cmdMaxAmmo1;
    }
}
