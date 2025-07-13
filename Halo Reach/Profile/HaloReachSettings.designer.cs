namespace Horizon.PackageEditors.Halo_Reach
{
    partial class HaloReachSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HaloReachSettings));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.numCredits = new DevComponents.Editors.IntegerInput();
            this.cmdMax = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnExtract = new DevComponents.DotNetBar.ButtonItem();
            this.btnUnlockAll = new DevComponents.DotNetBar.ButtonX();
            this.advUnlockablesTree = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advUnlockablesTree)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnExtract});
            this.rbPackageEditor.Size = new System.Drawing.Size(256, 280);
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
            this.panelMain.Controls.Add(this.btnUnlockAll);
            this.panelMain.Controls.Add(this.advUnlockablesTree);
            this.panelMain.Controls.Add(this.panelEx1);
            this.panelMain.Controls.Add(this.cmdMax);
            this.panelMain.Controls.Add(this.numCredits);
            this.panelMain.Controls.Add(this.labelX1);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(256, 225);
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
            this.tabMain.Text = "Profile";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.SingleLineColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Size = new System.Drawing.Size(69, 20);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Add Credits:";
            // 
            // numCredits
            // 
            // 
            // 
            // 
            this.numCredits.BackgroundStyle.Class = "DateTimeInputBackground";
            this.numCredits.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.numCredits.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.numCredits.Location = new System.Drawing.Point(73, 3);
            this.numCredits.MinValue = 0;
            this.numCredits.Name = "numCredits";
            this.numCredits.ShowUpDown = true;
            this.numCredits.Size = new System.Drawing.Size(75, 20);
            this.numCredits.TabIndex = 2;
            // 
            // cmdMax
            // 
            this.cmdMax.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMax.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMax.FocusCuesEnabled = false;
            this.cmdMax.Image = ((System.Drawing.Image)(resources.GetObject("cmdMax.Image")));
            this.cmdMax.Location = new System.Drawing.Point(154, 3);
            this.cmdMax.Name = "cmdMax";
            this.cmdMax.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMax.Size = new System.Drawing.Size(96, 20);
            this.cmdMax.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMax.TabIndex = 3;
            this.cmdMax.Text = "Add Credits";
            this.cmdMax.Click += new System.EventHandler(this.CmdMaxClick);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Location = new System.Drawing.Point(6, 27);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(244, 31);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 4;
            this.panelEx1.Text = "<b>Notice:</b> Do not add more than 1,000 credits if you plan to connect to Xbox " +
                "LIVE.";
            // 
            // btnExtract
            // 
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Text = "Extract";
            this.btnExtract.Visible = false;
            this.btnExtract.Click += new System.EventHandler(this.BtnClickExtractProfileData);
            // 
            // btnUnlockAll
            // 
            this.btnUnlockAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAll.FocusCuesEnabled = false;
            this.btnUnlockAll.Image = ((System.Drawing.Image)(resources.GetObject("btnUnlockAll.Image")));
            this.btnUnlockAll.Location = new System.Drawing.Point(6, 189);
            this.btnUnlockAll.Name = "btnUnlockAll";
            this.btnUnlockAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAll.Size = new System.Drawing.Size(244, 28);
            this.btnUnlockAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAll.TabIndex = 18;
            this.btnUnlockAll.Text = "Unlock All";
            this.btnUnlockAll.Click += new System.EventHandler(this.BtnClickUnlockAll);
            // 
            // advUnlockablesTree
            // 
            this.advUnlockablesTree.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advUnlockablesTree.AllowDrop = true;
            this.advUnlockablesTree.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advUnlockablesTree.BackgroundStyle.Class = "TreeBorderKey";
            this.advUnlockablesTree.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advUnlockablesTree.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.advUnlockablesTree.Location = new System.Drawing.Point(6, 63);
            this.advUnlockablesTree.Name = "advUnlockablesTree";
            this.advUnlockablesTree.NodesConnector = this.nodeConnector1;
            this.advUnlockablesTree.NodeStyle = this.elementStyle1;
            this.advUnlockablesTree.PathSeparator = ";";
            this.advUnlockablesTree.Size = new System.Drawing.Size(244, 120);
            this.advUnlockablesTree.Styles.Add(this.elementStyle1);
            this.advUnlockablesTree.TabIndex = 17;
            this.advUnlockablesTree.Text = "advTree1";
            this.advUnlockablesTree.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.AdvUnlockablesAfterCheck);
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
            // HaloReachSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 283);
            this.Name = "HaloReachSettings";
            this.Text = "Halo Reach Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCredits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advUnlockablesTree)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.IntegerInput numCredits;
        private DevComponents.DotNetBar.ButtonX cmdMax;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonItem btnExtract;
        private DevComponents.DotNetBar.ButtonX btnUnlockAll;
        private DevComponents.AdvTree.AdvTree advUnlockablesTree;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
    }
}