namespace Horizon.PackageEditors.Watch_Dogs.Controls
{
    partial class WDInventoryWheelList
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
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.treeInventoryItems = new DevComponents.AdvTree.AdvTree();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.btnMaxAllAmmo = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnUnlockAllOutfits = new DevComponents.DotNetBar.ButtonX();
            this.btnUnlockAllWeapons = new DevComponents.DotNetBar.ButtonX();
            this.btnUnlockAllPlayerSkills = new DevComponents.DotNetBar.ButtonX();
            this.columnHeader6 = new DevComponents.AdvTree.ColumnHeader();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeInventoryItems)).BeginInit();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.treeInventoryItems);
            this.groupPanel1.Controls.Add(this.btnMaxAllAmmo);
            this.groupPanel1.Controls.Add(this.groupPanel2);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(425, 369);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "Inventory";
            // 
            // treeInventoryItems
            // 
            this.treeInventoryItems.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeInventoryItems.AllowDrop = true;
            this.treeInventoryItems.AllowUserToReorderColumns = true;
            this.treeInventoryItems.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeInventoryItems.BackgroundStyle.Class = "TreeBorderKey";
            this.treeInventoryItems.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeInventoryItems.Columns.Add(this.columnHeader2);
            this.treeInventoryItems.Columns.Add(this.columnHeader3);
            this.treeInventoryItems.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeInventoryItems.Location = new System.Drawing.Point(11, 3);
            this.treeInventoryItems.Name = "treeInventoryItems";
            this.treeInventoryItems.NodesConnector = this.nodeConnector1;
            this.treeInventoryItems.NodeStyle = this.elementStyle3;
            this.treeInventoryItems.PathSeparator = ";";
            this.treeInventoryItems.Size = new System.Drawing.Size(399, 167);
            this.treeInventoryItems.Styles.Add(this.elementStyle3);
            this.treeInventoryItems.TabIndex = 134;
            this.treeInventoryItems.Text = "advTree1";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Item";
            this.columnHeader2.Width.Absolute = 175;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Count";
            this.columnHeader3.Width.Absolute = 200;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle3
            // 
            this.elementStyle3.Class = "";
            this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // btnMaxAllAmmo
            // 
            this.btnMaxAllAmmo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxAllAmmo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxAllAmmo.FocusCuesEnabled = false;
            this.btnMaxAllAmmo.Location = new System.Drawing.Point(11, 175);
            this.btnMaxAllAmmo.Name = "btnMaxAllAmmo";
            this.btnMaxAllAmmo.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxAllAmmo.Size = new System.Drawing.Size(399, 23);
            this.btnMaxAllAmmo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxAllAmmo.TabIndex = 129;
            this.btnMaxAllAmmo.Text = "Max All";
            this.btnMaxAllAmmo.Click += new System.EventHandler(this.BtnClick_MaxAllAmmunition);
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.btnUnlockAllOutfits);
            this.groupPanel2.Controls.Add(this.btnUnlockAllWeapons);
            this.groupPanel2.Controls.Add(this.btnUnlockAllPlayerSkills);
            this.groupPanel2.Location = new System.Drawing.Point(9, 216);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(403, 130);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 135;
            this.groupPanel2.Text = "Unlock Items";
            // 
            // btnUnlockAllOutfits
            // 
            this.btnUnlockAllOutfits.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAllOutfits.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAllOutfits.FocusCuesEnabled = false;
            this.btnUnlockAllOutfits.Location = new System.Drawing.Point(14, 13);
            this.btnUnlockAllOutfits.Name = "btnUnlockAllOutfits";
            this.btnUnlockAllOutfits.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAllOutfits.Size = new System.Drawing.Size(372, 23);
            this.btnUnlockAllOutfits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAllOutfits.TabIndex = 129;
            this.btnUnlockAllOutfits.Text = "Unlock All Outfits";
            this.btnUnlockAllOutfits.Click += new System.EventHandler(this.UnlockAllOutfits);
            // 
            // btnUnlockAllWeapons
            // 
            this.btnUnlockAllWeapons.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAllWeapons.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAllWeapons.FocusCuesEnabled = false;
            this.btnUnlockAllWeapons.Location = new System.Drawing.Point(14, 42);
            this.btnUnlockAllWeapons.Name = "btnUnlockAllWeapons";
            this.btnUnlockAllWeapons.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAllWeapons.Size = new System.Drawing.Size(372, 23);
            this.btnUnlockAllWeapons.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAllWeapons.TabIndex = 129;
            this.btnUnlockAllWeapons.Text = "Unlock All Weapons";
            this.btnUnlockAllWeapons.Click += new System.EventHandler(this.UnlockAllWeapons);
            // 
            // btnUnlockAllPlayerSkills
            // 
            this.btnUnlockAllPlayerSkills.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAllPlayerSkills.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAllPlayerSkills.FocusCuesEnabled = false;
            this.btnUnlockAllPlayerSkills.Location = new System.Drawing.Point(14, 71);
            this.btnUnlockAllPlayerSkills.Name = "btnUnlockAllPlayerSkills";
            this.btnUnlockAllPlayerSkills.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAllPlayerSkills.Size = new System.Drawing.Size(372, 23);
            this.btnUnlockAllPlayerSkills.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAllPlayerSkills.TabIndex = 129;
            this.btnUnlockAllPlayerSkills.Text = "Unlock All Skills";
            this.btnUnlockAllPlayerSkills.Click += new System.EventHandler(this.UnlockAllSkillsInProgressTree);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Name = "columnHeader6";
            this.columnHeader6.Text = "Item";
            this.columnHeader6.Width.Absolute = 350;
            // 
            // elementStyle2
            // 
            this.elementStyle2.Class = "";
            this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // WDInventoryWheelList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupPanel1);
            this.Name = "WDInventoryWheelList";
            this.Size = new System.Drawing.Size(425, 369);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeInventoryItems)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.ButtonX btnUnlockAllWeapons;
        private DevComponents.DotNetBar.ButtonX btnUnlockAllOutfits;
        private DevComponents.DotNetBar.ButtonX btnUnlockAllPlayerSkills;
        private DevComponents.AdvTree.ColumnHeader columnHeader6;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.AdvTree.AdvTree treeInventoryItems;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.ColumnHeader columnHeader3;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.ButtonX btnMaxAllAmmo;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
    }
}
