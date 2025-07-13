namespace Horizon.PackageEditors.Ninja_Gaiden_3_RE
{
    partial class NinjaGaiden3RE
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
            this.btnMaxKarmaRyu = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.intKarmaRyu = new DevComponents.Editors.IntegerInput();
            this.treeUnlocks = new DevComponents.AdvTree.AdvTree();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.btnUnlockAll = new DevComponents.DotNetBar.ButtonX();
            this.intKarmaAyane = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnMaxKarmaAyane = new DevComponents.DotNetBar.ButtonX();
            this.intKarmaScore = new DevComponents.Editors.IntegerInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.btnMaxKarmaScore = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intKarmaRyu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeUnlocks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intKarmaAyane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intKarmaScore)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(251, 490);
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
            this.panelMain.Controls.Add(this.groupPanel1);
            this.panelMain.Controls.Add(this.btnUnlockAll);
            this.panelMain.Controls.Add(this.treeUnlocks);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(251, 435);
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
            this.tabMain.Text = "Player";
            // 
            // btnMaxKarmaRyu
            // 
            this.btnMaxKarmaRyu.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxKarmaRyu.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxKarmaRyu.FocusCuesEnabled = false;
            this.btnMaxKarmaRyu.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxKarmaRyu.Location = new System.Drawing.Point(169, 3);
            this.btnMaxKarmaRyu.Name = "btnMaxKarmaRyu";
            this.btnMaxKarmaRyu.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxKarmaRyu.Size = new System.Drawing.Size(60, 24);
            this.btnMaxKarmaRyu.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxKarmaRyu.TabIndex = 7;
            this.btnMaxKarmaRyu.Text = "Max";
            this.btnMaxKarmaRyu.Click += new System.EventHandler(this.BtnClickMaxKarma);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(6, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(47, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "Ryu:";
            // 
            // intKarmaRyu
            // 
            // 
            // 
            // 
            this.intKarmaRyu.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intKarmaRyu.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intKarmaRyu.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intKarmaRyu.Location = new System.Drawing.Point(59, 4);
            this.intKarmaRyu.MaxValue = 999999999;
            this.intKarmaRyu.MinValue = 0;
            this.intKarmaRyu.Name = "intKarmaRyu";
            this.intKarmaRyu.ShowUpDown = true;
            this.intKarmaRyu.Size = new System.Drawing.Size(92, 20);
            this.intKarmaRyu.TabIndex = 6;
            // 
            // treeUnlocks
            // 
            this.treeUnlocks.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.treeUnlocks.AllowDrop = true;
            this.treeUnlocks.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.treeUnlocks.BackgroundStyle.Class = "TreeBorderKey";
            this.treeUnlocks.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.treeUnlocks.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.treeUnlocks.Location = new System.Drawing.Point(6, 117);
            this.treeUnlocks.Name = "treeUnlocks";
            this.treeUnlocks.NodesConnector = this.nodeConnector1;
            this.treeUnlocks.NodeStyle = this.elementStyle1;
            this.treeUnlocks.PathSeparator = ";";
            this.treeUnlocks.Size = new System.Drawing.Size(237, 284);
            this.treeUnlocks.Styles.Add(this.elementStyle1);
            this.treeUnlocks.TabIndex = 9;
            this.treeUnlocks.Text = "advTree1";
            this.treeUnlocks.AfterCheck += new DevComponents.AdvTree.AdvTreeCellEventHandler(this.UnlockTreeNodeAfterCheck);
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
            // btnUnlockAll
            // 
            this.btnUnlockAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnUnlockAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnUnlockAll.FocusCuesEnabled = false;
            this.btnUnlockAll.Image = global::Horizon.Properties.Resources.Star;
            this.btnUnlockAll.Location = new System.Drawing.Point(6, 400);
            this.btnUnlockAll.Name = "btnUnlockAll";
            this.btnUnlockAll.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnUnlockAll.Size = new System.Drawing.Size(237, 28);
            this.btnUnlockAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnUnlockAll.TabIndex = 17;
            this.btnUnlockAll.Text = "Unlock All";
            this.btnUnlockAll.Click += new System.EventHandler(this.BtnClickUnlockAll);
            // 
            // intKarmaAyane
            // 
            // 
            // 
            // 
            this.intKarmaAyane.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intKarmaAyane.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intKarmaAyane.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intKarmaAyane.Location = new System.Drawing.Point(59, 33);
            this.intKarmaAyane.MaxValue = 999999999;
            this.intKarmaAyane.MinValue = 0;
            this.intKarmaAyane.Name = "intKarmaAyane";
            this.intKarmaAyane.ShowUpDown = true;
            this.intKarmaAyane.Size = new System.Drawing.Size(92, 20);
            this.intKarmaAyane.TabIndex = 6;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 33);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 23);
            this.labelX1.TabIndex = 5;
            this.labelX1.Text = "Ayane:";
            // 
            // btnMaxKarmaAyane
            // 
            this.btnMaxKarmaAyane.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxKarmaAyane.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxKarmaAyane.FocusCuesEnabled = false;
            this.btnMaxKarmaAyane.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxKarmaAyane.Location = new System.Drawing.Point(169, 32);
            this.btnMaxKarmaAyane.Name = "btnMaxKarmaAyane";
            this.btnMaxKarmaAyane.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxKarmaAyane.Size = new System.Drawing.Size(60, 24);
            this.btnMaxKarmaAyane.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxKarmaAyane.TabIndex = 7;
            this.btnMaxKarmaAyane.Text = "Max";
            this.btnMaxKarmaAyane.Click += new System.EventHandler(this.BtnClickMaxKarma);
            // 
            // intKarmaScore
            // 
            // 
            // 
            // 
            this.intKarmaScore.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intKarmaScore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intKarmaScore.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intKarmaScore.Location = new System.Drawing.Point(59, 62);
            this.intKarmaScore.MaxValue = 999999999;
            this.intKarmaScore.MinValue = 0;
            this.intKarmaScore.Name = "intKarmaScore";
            this.intKarmaScore.ShowUpDown = true;
            this.intKarmaScore.Size = new System.Drawing.Size(92, 20);
            this.intKarmaScore.TabIndex = 6;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(6, 62);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(47, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "Score:";
            // 
            // btnMaxKarmaScore
            // 
            this.btnMaxKarmaScore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxKarmaScore.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxKarmaScore.FocusCuesEnabled = false;
            this.btnMaxKarmaScore.Image = global::Horizon.Properties.Resources.UpArrow;
            this.btnMaxKarmaScore.Location = new System.Drawing.Point(169, 61);
            this.btnMaxKarmaScore.Name = "btnMaxKarmaScore";
            this.btnMaxKarmaScore.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxKarmaScore.Size = new System.Drawing.Size(60, 24);
            this.btnMaxKarmaScore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxKarmaScore.TabIndex = 7;
            this.btnMaxKarmaScore.Text = "Max";
            this.btnMaxKarmaScore.Click += new System.EventHandler(this.BtnClickMaxKarma);
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.btnMaxKarmaScore);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.btnMaxKarmaAyane);
            this.groupPanel1.Controls.Add(this.intKarmaRyu);
            this.groupPanel1.Controls.Add(this.btnMaxKarmaRyu);
            this.groupPanel1.Controls.Add(this.intKarmaAyane);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.Controls.Add(this.intKarmaScore);
            this.groupPanel1.Location = new System.Drawing.Point(6, 2);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(237, 110);
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
            this.groupPanel1.TabIndex = 18;
            this.groupPanel1.Text = "Karma";
            // 
            // NinjaGaiden3RE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 493);
            this.Name = "NinjaGaiden3RE";
            this.Text = "Ninja Gaiden 3: Razor\'s Edge";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intKarmaRyu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeUnlocks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intKarmaAyane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intKarmaScore)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnMaxKarmaRyu;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.IntegerInput intKarmaRyu;
        private DevComponents.AdvTree.AdvTree treeUnlocks;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private DevComponents.DotNetBar.ButtonX btnUnlockAll;
        private DevComponents.DotNetBar.ButtonX btnMaxKarmaScore;
        private DevComponents.DotNetBar.ButtonX btnMaxKarmaAyane;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.Editors.IntegerInput intKarmaScore;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.IntegerInput intKarmaAyane;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
    }
}
