namespace Horizon.PackageEditors.Magic_2013
{
    partial class Magic2013
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
            this.listViewLockedDeck = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.chkUnlockAllCards = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkPremiumFoil = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.listViewUnlockedDecks = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.cmdAddItem = new DevComponents.DotNetBar.ButtonX();
            this.cmdDeleteGroup = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(337, 436);
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
            // panelMain
            // 
            this.panelMain.Controls.Add(this.cmdDeleteGroup);
            this.panelMain.Controls.Add(this.cmdAddItem);
            this.panelMain.Controls.Add(this.chkPremiumFoil);
            this.panelMain.Controls.Add(this.chkUnlockAllCards);
            this.panelMain.Controls.Add(this.listViewUnlockedDecks);
            this.panelMain.Controls.Add(this.listViewLockedDeck);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(337, 381);
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
            this.tabMain.Text = "Deck Editor";
            // 
            // listViewLockedDeck
            // 
            // 
            // 
            // 
            this.listViewLockedDeck.Border.Class = "ListViewBorder";
            this.listViewLockedDeck.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listViewLockedDeck.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewLockedDeck.FullRowSelect = true;
            this.listViewLockedDeck.GridLines = true;
            this.listViewLockedDeck.Location = new System.Drawing.Point(6, 3);
            this.listViewLockedDeck.MultiSelect = false;
            this.listViewLockedDeck.Name = "listViewLockedDeck";
            this.listViewLockedDeck.Size = new System.Drawing.Size(157, 155);
            this.listViewLockedDeck.TabIndex = 0;
            this.listViewLockedDeck.UseCompatibleStateImageBehavior = false;
            this.listViewLockedDeck.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Deck Name";
            this.columnHeader1.Width = 139;
            // 
            // chkUnlockAllCards
            // 
            this.chkUnlockAllCards.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkUnlockAllCards.BackgroundStyle.Class = "";
            this.chkUnlockAllCards.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkUnlockAllCards.Location = new System.Drawing.Point(102, 255);
            this.chkUnlockAllCards.Name = "chkUnlockAllCards";
            this.chkUnlockAllCards.Size = new System.Drawing.Size(110, 23);
            this.chkUnlockAllCards.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkUnlockAllCards.TabIndex = 1;
            this.chkUnlockAllCards.Text = "Unlock All Cards";
            // 
            // chkPremiumFoil
            // 
            this.chkPremiumFoil.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chkPremiumFoil.BackgroundStyle.Class = "";
            this.chkPremiumFoil.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkPremiumFoil.Location = new System.Drawing.Point(102, 284);
            this.chkPremiumFoil.Name = "chkPremiumFoil";
            this.chkPremiumFoil.Size = new System.Drawing.Size(124, 23);
            this.chkPremiumFoil.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkPremiumFoil.TabIndex = 1;
            this.chkPremiumFoil.Text = "Premium Foil Deck";
            // 
            // listViewUnlockedDecks
            // 
            // 
            // 
            // 
            this.listViewUnlockedDecks.Border.Class = "ListViewBorder";
            this.listViewUnlockedDecks.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listViewUnlockedDecks.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listViewUnlockedDecks.FullRowSelect = true;
            this.listViewUnlockedDecks.GridLines = true;
            this.listViewUnlockedDecks.Location = new System.Drawing.Point(176, 3);
            this.listViewUnlockedDecks.MultiSelect = false;
            this.listViewUnlockedDecks.Name = "listViewUnlockedDecks";
            this.listViewUnlockedDecks.Size = new System.Drawing.Size(157, 155);
            this.listViewUnlockedDecks.TabIndex = 0;
            this.listViewUnlockedDecks.UseCompatibleStateImageBehavior = false;
            this.listViewUnlockedDecks.View = System.Windows.Forms.View.Details;
            this.listViewUnlockedDecks.SelectedIndexChanged += new System.EventHandler(this.DeckListViewIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Deck Name";
            this.columnHeader2.Width = 139;
            // 
            // cmdAddItem
            // 
            this.cmdAddItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAddItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdAddItem.FocusCuesEnabled = false;
            this.cmdAddItem.Image = global::Horizon.Properties.Resources.Plus;
            this.cmdAddItem.Location = new System.Drawing.Point(6, 164);
            this.cmdAddItem.Name = "cmdAddItem";
            this.cmdAddItem.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdAddItem.Size = new System.Drawing.Size(157, 21);
            this.cmdAddItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdAddItem.TabIndex = 27;
            this.cmdAddItem.Text = "Unlock Deck\r\n";
            this.cmdAddItem.Click += new System.EventHandler(this.BtnClickUnlockDeck);
            // 
            // cmdDeleteGroup
            // 
            this.cmdDeleteGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdDeleteGroup.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdDeleteGroup.FocusCuesEnabled = false;
            this.cmdDeleteGroup.Image = global::Horizon.Properties.Resources.Delete;
            this.cmdDeleteGroup.Location = new System.Drawing.Point(176, 164);
            this.cmdDeleteGroup.Name = "cmdDeleteGroup";
            this.cmdDeleteGroup.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdDeleteGroup.Size = new System.Drawing.Size(157, 22);
            this.cmdDeleteGroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdDeleteGroup.TabIndex = 28;
            this.cmdDeleteGroup.Text = "Lock Deck";
            this.cmdDeleteGroup.Click += new System.EventHandler(this.BtnClickLockDeck);
            // 
            // Magic2013
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 439);
            this.Name = "Magic2013";
            this.Text = "Magic 2013";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx listViewLockedDeck;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkPremiumFoil;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkUnlockAllCards;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewUnlockedDecks;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private DevComponents.DotNetBar.ButtonX cmdAddItem;
        private DevComponents.DotNetBar.ButtonX cmdDeleteGroup;
    }
}
