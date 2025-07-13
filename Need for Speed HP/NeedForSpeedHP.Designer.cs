namespace Horizon.PackageEditors.Need_for_Speed_HP
{
    partial class NeedForSpeedHP
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
            this.intRacer = new DevComponents.Editors.IntegerInput();
            this.intCop = new DevComponents.Editors.IntegerInput();
            this.cmdMax = new DevComponents.DotNetBar.ButtonX();
            this.lblRacer = new DevComponents.DotNetBar.LabelX();
            this.lblCop = new DevComponents.DotNetBar.LabelX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intRacer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intCop)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(266, 103);
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
            this.panelMain.Controls.Add(this.lblCop);
            this.panelMain.Controls.Add(this.lblRacer);
            this.panelMain.Controls.Add(this.cmdMax);
            this.panelMain.Controls.Add(this.intCop);
            this.panelMain.Controls.Add(this.intRacer);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(266, 48);
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
            this.tabMain.Text = "Bounty Editor";
            // 
            // intRacer
            // 
            // 
            // 
            // 
            this.intRacer.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intRacer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intRacer.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intRacer.Location = new System.Drawing.Point(84, 3);
            this.intRacer.MaxValue = 2000000;
            this.intRacer.MinValue = 0;
            this.intRacer.Name = "intRacer";
            this.intRacer.ShowUpDown = true;
            this.intRacer.Size = new System.Drawing.Size(74, 20);
            this.intRacer.TabIndex = 1;
            // 
            // intCop
            // 
            // 
            // 
            // 
            this.intCop.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intCop.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intCop.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intCop.Location = new System.Drawing.Point(84, 25);
            this.intCop.MaxValue = 2000000;
            this.intCop.MinValue = 0;
            this.intCop.Name = "intCop";
            this.intCop.ShowUpDown = true;
            this.intCop.Size = new System.Drawing.Size(74, 20);
            this.intCop.TabIndex = 2;
            // 
            // cmdMax
            // 
            this.cmdMax.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMax.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMax.FocusCuesEnabled = false;
            this.cmdMax.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMax.Location = new System.Drawing.Point(160, 3);
            this.cmdMax.Name = "cmdMax";
            this.cmdMax.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMax.Size = new System.Drawing.Size(103, 42);
            this.cmdMax.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMax.TabIndex = 3;
            this.cmdMax.Text = "Max Bounty";
            this.cmdMax.Click += new System.EventHandler(this.cmdMax_Click);
            // 
            // lblRacer
            // 
            this.lblRacer.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblRacer.BackgroundStyle.Class = "";
            this.lblRacer.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblRacer.Location = new System.Drawing.Point(6, 3);
            this.lblRacer.Name = "lblRacer";
            this.lblRacer.Size = new System.Drawing.Size(72, 20);
            this.lblRacer.TabIndex = 4;
            this.lblRacer.Text = "Racer Bounty:";
            // 
            // lblCop
            // 
            this.lblCop.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblCop.BackgroundStyle.Class = "";
            this.lblCop.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblCop.Location = new System.Drawing.Point(6, 25);
            this.lblCop.Name = "lblCop";
            this.lblCop.Size = new System.Drawing.Size(72, 20);
            this.lblCop.TabIndex = 5;
            this.lblCop.Text = "Cop Bounty:";
            // 
            // NeedForSpeedHP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 106);
            this.Name = "NeedForSpeedHP";
            this.Text = "NFS: Hot Pursuit Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intRacer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intCop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.IntegerInput intRacer;
        private DevComponents.Editors.IntegerInput intCop;
        private DevComponents.DotNetBar.ButtonX cmdMax;
        private DevComponents.DotNetBar.LabelX lblRacer;
        private DevComponents.DotNetBar.LabelX lblCop;
    }
}