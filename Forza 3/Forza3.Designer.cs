namespace Horizon.PackageEditors.Forza_3
{
    partial class Forza3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Forza3));
            this.numLevel = new System.Windows.Forms.NumericUpDown();
            this.numXP = new System.Windows.Forms.NumericUpDown();
            this.numCredits = new System.Windows.Forms.NumericUpDown();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.advPropertyTree = new DevComponents.AdvTree.AdvTree();
            this.advClmnProperty = new DevComponents.AdvTree.ColumnHeader();
            this.advClmnValue = new DevComponents.AdvTree.ColumnHeader();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).BeginInit();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advPropertyTree)).BeginInit();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(462, 375);
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
            this.panelMain.Controls.Add(this.panelEx2);
            this.panelMain.Controls.Add(this.advPropertyTree);
            this.panelMain.Controls.Add(this.panelEx1);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(462, 320);
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
            this.tabMain.Text = "Stats";
            // 
            // numLevel
            // 
            this.numLevel.Location = new System.Drawing.Point(59, 59);
            this.numLevel.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numLevel.Name = "numLevel";
            this.numLevel.Size = new System.Drawing.Size(100, 20);
            this.numLevel.TabIndex = 5;
            this.numLevel.Validated += new System.EventHandler(this.EditPropLvlValidated);
            // 
            // numXP
            // 
            this.numXP.Location = new System.Drawing.Point(59, 33);
            this.numXP.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numXP.Name = "numXP";
            this.numXP.Size = new System.Drawing.Size(100, 20);
            this.numXP.TabIndex = 4;
            this.numXP.Validated += new System.EventHandler(this.EditPropXPValidated);
            // 
            // numCredits
            // 
            this.numCredits.Location = new System.Drawing.Point(59, 7);
            this.numCredits.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.numCredits.Name = "numCredits";
            this.numCredits.Size = new System.Drawing.Size(100, 20);
            this.numCredits.TabIndex = 3;
            this.numCredits.Validated += new System.EventHandler(this.EditPropCrValidated);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 32);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(41, 20);
            this.labelX2.TabIndex = 1;
            this.labelX2.Text = "XP:";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(12, 6);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(41, 20);
            this.labelX3.TabIndex = 1;
            this.labelX3.Text = "Credits:";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 58);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(41, 20);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Level:";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.numCredits);
            this.panelEx1.Controls.Add(this.numLevel);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.numXP);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Location = new System.Drawing.Point(12, 12);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(166, 86);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 9;
            // 
            // advPropertyTree
            // 
            this.advPropertyTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advPropertyTree.AllowDrop = true;
            this.advPropertyTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advPropertyTree.BackgroundStyle.Class = "TreeBorderKey";
            this.advPropertyTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advPropertyTree.CellEdit = true;
            this.advPropertyTree.Columns.Add(this.advClmnProperty);
            this.advPropertyTree.Columns.Add(this.advClmnValue);
            this.advPropertyTree.GridRowLines = true;
            this.advPropertyTree.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.advPropertyTree.Location = new System.Drawing.Point(12, 104);
            this.advPropertyTree.Name = "advPropertyTree";
            this.advPropertyTree.NodesConnector = this.nodeConnector1;
            this.advPropertyTree.NodeStyle = this.elementStyle1;
            this.advPropertyTree.PathSeparator = ";";
            this.advPropertyTree.Size = new System.Drawing.Size(438, 206);
            this.advPropertyTree.Styles.Add(this.elementStyle1);
            this.advPropertyTree.TabIndex = 10;
            this.advPropertyTree.Text = "advTree1";
            this.advPropertyTree.CellEditEnding += new DevComponents.AdvTree.CellEditEventHandler(this.EditPropertyTreeCellEdit);
            // 
            // advClmnProperty
            // 
            this.advClmnProperty.Image = ((System.Drawing.Image)(resources.GetObject("advClmnProperty.Image")));
            this.advClmnProperty.Name = "advClmnProperty";
            this.advClmnProperty.Text = "Property";
            this.advClmnProperty.Width.Absolute = 275;
            // 
            // advClmnValue
            // 
            this.advClmnValue.MaxInputLength = 255;
            this.advClmnValue.Name = "advClmnValue";
            this.advClmnValue.Text = "Value";
            this.advClmnValue.Width.Absolute = 125;
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
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Location = new System.Drawing.Point(184, 12);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(266, 86);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 11;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(30, 3);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(208, 80);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "<b>Notice:</b> The VIP setting is only available for offline profiles because Xbo" +
                "x LIVE profiles use Turn10 servers to store the VIP setting.";
            // 
            // Forza3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 378);
            this.Name = "Forza3";
            this.Text = "Forza 3 Profile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Forza3_FormClosing);
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numXP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).EndInit();
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advPropertyTree)).EndInit();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.NumericUpDown numCredits;
        private System.Windows.Forms.NumericUpDown numXP;
        private System.Windows.Forms.NumericUpDown numLevel;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.AdvTree.AdvTree advPropertyTree;
        private DevComponents.AdvTree.ColumnHeader advClmnProperty;
        private DevComponents.AdvTree.ColumnHeader advClmnValue;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX4;


    }
}
