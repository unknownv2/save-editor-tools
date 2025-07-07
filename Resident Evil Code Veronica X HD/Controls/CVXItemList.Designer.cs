namespace Horizon.PackageEditors.Resident_Evil_Code_Veronica_X_HD.Controls
{
    partial class CVXItemList
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
            this.cmbSelectedItem = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnMaxAll = new DevComponents.DotNetBar.ButtonX();
            this.btnMakeAllInfinite = new DevComponents.DotNetBar.ButtonX();
            this.treeItemList = new DevComponents.AdvTree.AdvTree();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader3 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.AutoScroll = true;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.cmbSelectedItem);
            this.groupPanel1.Controls.Add(this.btnMaxAll);
            this.groupPanel1.Controls.Add(this.btnMakeAllInfinite);
            this.groupPanel1.Controls.Add(this.treeItemList);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(451, 238);
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
            this.groupPanel1.Text = "Item List";
            // 
            // cmbSelectedItem
            // 
            this.cmbSelectedItem.DisplayMember = "Text";
            this.cmbSelectedItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbSelectedItem.FormattingEnabled = true;
            this.cmbSelectedItem.ItemHeight = 14;
            this.cmbSelectedItem.Location = new System.Drawing.Point(9, 184);
            this.cmbSelectedItem.Name = "cmbSelectedItem";
            this.cmbSelectedItem.Size = new System.Drawing.Size(230, 20);
            this.cmbSelectedItem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSelectedItem.TabIndex = 138;
            this.cmbSelectedItem.SelectedIndexChanged += new System.EventHandler(this.cmbCurrentItem_SelectedIndexChanged);
            // 
            // btnMaxAll
            // 
            this.btnMaxAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxAll.FocusCuesEnabled = false;
            this.btnMaxAll.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxAll.Location = new System.Drawing.Point(252, 183);
            this.btnMaxAll.Name = "btnMaxAll";
            this.btnMaxAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxAll.Size = new System.Drawing.Size(75, 23);
            this.btnMaxAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxAll.TabIndex = 137;
            this.btnMaxAll.Text = "Max All";
            this.btnMaxAll.Click += new System.EventHandler(this.BtnClick_MaxAll);
            // 
            // btnMakeAllInfinite
            // 
            this.btnMakeAllInfinite.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMakeAllInfinite.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMakeAllInfinite.FocusCuesEnabled = false;
            this.btnMakeAllInfinite.Image = global::Horizon.Properties.Resources.Star;
            this.btnMakeAllInfinite.Location = new System.Drawing.Point(323, 183);
            this.btnMakeAllInfinite.Name = "btnMakeAllInfinite";
            this.btnMakeAllInfinite.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMakeAllInfinite.Size = new System.Drawing.Size(114, 23);
            this.btnMakeAllInfinite.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMakeAllInfinite.TabIndex = 136;
            this.btnMakeAllInfinite.Text = "Make All Infinite";
            this.btnMakeAllInfinite.Click += new System.EventHandler(this.BtnClick_MakeAllInfinite);
            // 
            // treeItemList
            // 
            this.treeItemList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeItemList.AllowDrop = true;
            this.treeItemList.AllowUserToResizeColumns = false;
            this.treeItemList.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeItemList.BackgroundStyle.Class = "TreeBorderKey";
            this.treeItemList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeItemList.Columns.Add(this.columnHeader2);
            this.treeItemList.Columns.Add(this.columnHeader3);
            this.treeItemList.Columns.Add(this.columnHeader1);
            this.treeItemList.DragDropEnabled = false;
            this.treeItemList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeItemList.Location = new System.Drawing.Point(9, 3);
            this.treeItemList.Name = "treeItemList";
            this.treeItemList.NodesConnector = this.nodeConnector1;
            this.treeItemList.NodeStyle = this.elementStyle3;
            this.treeItemList.PathSeparator = ";";
            this.treeItemList.Size = new System.Drawing.Size(428, 167);
            this.treeItemList.Styles.Add(this.elementStyle3);
            this.treeItemList.TabIndex = 135;
            this.treeItemList.Text = "advTree1";
            this.treeItemList.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(this.TreeItemList_AfterNodeSelect);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Item";
            this.columnHeader2.Width.Absolute = 225;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Name = "columnHeader3";
            this.columnHeader3.Text = "Count";
            this.columnHeader3.Width.Absolute = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Is Infinite";
            this.columnHeader1.Width.Absolute = 60;
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
            // CVXItemList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupPanel1);
            this.Name = "CVXItemList";
            this.Size = new System.Drawing.Size(451, 238);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeItemList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.AdvTree.AdvTree treeItemList;
        private DevComponents.AdvTree.ColumnHeader columnHeader2;
        private DevComponents.AdvTree.ColumnHeader columnHeader3;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.DotNetBar.ButtonX btnMakeAllInfinite;
        private DevComponents.DotNetBar.ButtonX btnMaxAll;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbSelectedItem;
    }
}
