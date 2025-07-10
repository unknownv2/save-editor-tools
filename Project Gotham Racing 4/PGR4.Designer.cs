namespace Horizon.PackageEditors.Project_Gotham_Racing_4
{
    partial class PGR4
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
            this.btnMaxKudos = new DevComponents.DotNetBar.ButtonX();
            this.intKudosPoints = new DevComponents.Editors.IntegerInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intKudosPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(269, 92);
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
            this.panelMain.Controls.Add(this.btnMaxKudos);
            this.panelMain.Controls.Add(this.intKudosPoints);
            this.panelMain.Controls.Add(this.labelX1);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(269, 37);
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
            // btnMaxKudos
            // 
            this.btnMaxKudos.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnMaxKudos.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnMaxKudos.FocusCuesEnabled = false;
            this.btnMaxKudos.Image = global::Horizon.Properties.Resources.Plus;
            this.btnMaxKudos.Location = new System.Drawing.Point(174, 8);
            this.btnMaxKudos.Name = "btnMaxKudos";
            this.btnMaxKudos.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.btnMaxKudos.Size = new System.Drawing.Size(89, 20);
            this.btnMaxKudos.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnMaxKudos.TabIndex = 18;
            this.btnMaxKudos.Text = "Max Kudos";
            this.btnMaxKudos.Click += new System.EventHandler(this.BtnClick_MaxKudos);
            // 
            // intKudosPoints
            // 
            // 
            // 
            // 
            this.intKudosPoints.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intKudosPoints.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intKudosPoints.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intKudosPoints.Location = new System.Drawing.Point(50, 8);
            this.intKudosPoints.MinValue = 0;
            this.intKudosPoints.Name = "intKudosPoints";
            this.intKudosPoints.ShowUpDown = true;
            this.intKudosPoints.Size = new System.Drawing.Size(114, 20);
            this.intKudosPoints.TabIndex = 17;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(6, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(58, 23);
            this.labelX1.TabIndex = 16;
            this.labelX1.Text = "Kudos:";
            // 
            // PGR4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 95);
            this.Name = "PGR4";
            this.Text = "PGR 4 Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intKudosPoints)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnMaxKudos;
        private DevComponents.Editors.IntegerInput intKudosPoints;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}
