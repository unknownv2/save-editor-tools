namespace Horizon.PackageEditors.Watch_Dogs.Controls
{
    partial class WDInventoryCarsOnDemandList
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
            this.btnUnlockAllCars = new DevComponents.DotNetBar.ButtonX();
            this.treeCarList = new DevComponents.AdvTree.AdvTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector2 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle2 = new DevComponents.DotNetBar.ElementStyle();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeCarList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnUnlockAllCars);
            this.groupPanel1.Controls.Add(this.treeCarList);
            this.groupPanel1.Location = new System.Drawing.Point(3, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(384, 313);
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
            this.groupPanel1.TabIndex = 2;
            this.groupPanel1.Text = "Cars On Demand";
            // 
            // btnUnlockAllCars
            // 
            this.btnUnlockAllCars.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAllCars.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAllCars.FocusCuesEnabled = false;
            this.btnUnlockAllCars.Location = new System.Drawing.Point(3, 266);
            this.btnUnlockAllCars.Name = "btnUnlockAllCars";
            this.btnUnlockAllCars.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAllCars.Size = new System.Drawing.Size(371, 23);
            this.btnUnlockAllCars.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAllCars.TabIndex = 128;
            this.btnUnlockAllCars.Text = "Unlock All Cars on Demand";
            this.btnUnlockAllCars.Click += new System.EventHandler(this.BtnClick_UnlockAllCarsOnDemand);
            // 
            // treeCarList
            // 
            this.treeCarList.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeCarList.AllowDrop = true;
            this.treeCarList.AllowUserToReorderColumns = true;
            this.treeCarList.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeCarList.BackgroundStyle.Class = "TreeBorderKey";
            this.treeCarList.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeCarList.Columns.Add(this.columnHeader1);
            this.treeCarList.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeCarList.Location = new System.Drawing.Point(3, 2);
            this.treeCarList.Name = "treeCarList";
            this.treeCarList.NodesConnector = this.nodeConnector2;
            this.treeCarList.NodeStyle = this.elementStyle2;
            this.treeCarList.PathSeparator = ";";
            this.treeCarList.Size = new System.Drawing.Size(371, 257);
            this.treeCarList.Styles.Add(this.elementStyle2);
            this.treeCarList.TabIndex = 127;
            this.treeCarList.Text = "advTree1";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Car";
            this.columnHeader1.Width.Absolute = 350;
            // 
            // nodeConnector2
            // 
            this.nodeConnector2.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle2
            // 
            this.elementStyle2.Class = "";
            this.elementStyle2.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle2.Name = "elementStyle2";
            this.elementStyle2.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // WDInventoryCarsOnDemandList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupPanel1);
            this.Name = "WDInventoryCarsOnDemandList";
            this.Size = new System.Drawing.Size(394, 316);
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeCarList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.AdvTree.AdvTree treeCarList;
        private DevComponents.AdvTree.ColumnHeader columnHeader1;
        private DevComponents.AdvTree.NodeConnector nodeConnector2;
        private DevComponents.DotNetBar.ElementStyle elementStyle2;
        private DevComponents.DotNetBar.ButtonX btnUnlockAllCars;
    }
}
