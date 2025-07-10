namespace Horizon.PackageEditors.Plants_vs_Zombies
{
    partial class PlantsvsZombies
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
            this.lblSuns = new DevComponents.DotNetBar.LabelX();
            this.intSuns = new DevComponents.Editors.IntegerInput();
            this.cmdMax = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intSuns)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(225, 82);
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
            this.panelMain.Controls.Add(this.cmdMax);
            this.panelMain.Controls.Add(this.intSuns);
            this.panelMain.Controls.Add(this.lblSuns);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(225, 27);
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
            this.tabMain.Text = "Suns Editor";
            // 
            // lblSuns
            // 
            this.lblSuns.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblSuns.BackgroundStyle.Class = "";
            this.lblSuns.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSuns.Location = new System.Drawing.Point(6, 3);
            this.lblSuns.Name = "lblSuns";
            this.lblSuns.Size = new System.Drawing.Size(34, 18);
            this.lblSuns.TabIndex = 0;
            this.lblSuns.Text = "Suns:";
            // 
            // intSuns
            // 
            // 
            // 
            // 
            this.intSuns.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSuns.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSuns.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSuns.Location = new System.Drawing.Point(46, 3);
            this.intSuns.MinValue = 0;
            this.intSuns.Name = "intSuns";
            this.intSuns.ShowUpDown = true;
            this.intSuns.Size = new System.Drawing.Size(90, 20);
            this.intSuns.TabIndex = 1;
            // 
            // cmdMax
            // 
            this.cmdMax.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMax.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMax.FocusCuesEnabled = false;
            this.cmdMax.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMax.Location = new System.Drawing.Point(142, 3);
            this.cmdMax.Name = "cmdMax";
            this.cmdMax.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMax.Size = new System.Drawing.Size(79, 20);
            this.cmdMax.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMax.TabIndex = 2;
            this.cmdMax.Text = "Max";
            this.cmdMax.Click += new System.EventHandler(this.cmdMax_Click);
            // 
            // PlantsvsZombies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 85);
            this.Name = "PlantsvsZombies";
            this.Text = "Plants vs. Zombies Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intSuns)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblSuns;
        private DevComponents.DotNetBar.ButtonX cmdMax;
        private DevComponents.Editors.IntegerInput intSuns;
    }
}
