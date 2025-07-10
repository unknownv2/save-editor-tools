namespace Horizon.PackageEditors.Star_Wars_TFU_II
{
    partial class StarWarsTFUII
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
            this.listValues = new DevComponents.AdvTree.AdvTree();
            this.colSetting = new DevComponents.AdvTree.ColumnHeader();
            this.colValue = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(519, 378);
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
            this.panelMain.Controls.Add(this.listValues);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(519, 323);
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
            this.tabMain.Text = "Persistence Values";
            // 
            // listValues
            // 
            this.listValues.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.listValues.AllowDrop = true;
            this.listValues.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.listValues.BackgroundStyle.Class = "TreeBorderKey";
            this.listValues.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listValues.CellEdit = true;
            this.listValues.Columns.Add(this.colSetting);
            this.listValues.Columns.Add(this.colValue);
            this.listValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listValues.DragDropEnabled = false;
            this.listValues.DragDropNodeCopyEnabled = false;
            this.listValues.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.listValues.Location = new System.Drawing.Point(3, 0);
            this.listValues.MultiNodeDragDropAllowed = false;
            this.listValues.Name = "listValues";
            this.listValues.NodesConnector = this.nodeConnector1;
            this.listValues.NodeStyle = this.elementStyle1;
            this.listValues.PathSeparator = ";";
            this.listValues.Size = new System.Drawing.Size(513, 320);
            this.listValues.Styles.Add(this.elementStyle1);
            this.listValues.TabIndex = 0;
            this.listValues.CellEditEnding += new DevComponents.AdvTree.CellEditEventHandler(this.listValues_CellEditEnding);
            // 
            // colSetting
            // 
            this.colSetting.DoubleClickAutoSize = false;
            this.colSetting.Editable = false;
            this.colSetting.Image = global::Horizon.Properties.Resources.GreenDot;
            this.colSetting.Name = "colSetting";
            this.colSetting.Text = "Setting";
            this.colSetting.Width.Absolute = 330;
            // 
            // colValue
            // 
            this.colValue.DoubleClickAutoSize = false;
            this.colValue.MaxInputLength = 50;
            this.colValue.Name = "colValue";
            this.colValue.Text = "Value";
            this.colValue.Width.Absolute = 175;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // StarWarsTFUII
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 381);
            this.Name = "StarWarsTFUII";
            this.Text = "Star Wars: TFU II Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.AdvTree.AdvTree listValues;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.AdvTree.ColumnHeader colSetting;
        private DevComponents.AdvTree.ColumnHeader colValue;
    }
}