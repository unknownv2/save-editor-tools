namespace Horizon.PackageEditors.Red_Faction_Guerrilla
{
    partial class RedFactionGuerrilla
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
            this.intXP = new DevComponents.Editors.IntegerInput();
            this.lblXP = new DevComponents.DotNetBar.LabelX();
            this.cmdMaxValue = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intXP)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(222, 82);
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
            this.panelMain.Controls.Add(this.cmdMaxValue);
            this.panelMain.Controls.Add(this.intXP);
            this.panelMain.Controls.Add(this.lblXP);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(222, 27);
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
            this.tabMain.Text = "Profile XP";
            // 
            // intXP
            // 
            this.intXP.AllowEmptyState = false;
            // 
            // 
            // 
            this.intXP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intXP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intXP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intXP.Location = new System.Drawing.Point(31, 3);
            this.intXP.MaxValue = 16777215;
            this.intXP.MinValue = 0;
            this.intXP.Name = "intXP";
            this.intXP.ShowUpDown = true;
            this.intXP.Size = new System.Drawing.Size(81, 20);
            this.intXP.TabIndex = 0;
            // 
            // lblXP
            // 
            this.lblXP.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblXP.BackgroundStyle.Class = "";
            this.lblXP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblXP.Location = new System.Drawing.Point(6, 3);
            this.lblXP.Name = "lblXP";
            this.lblXP.Size = new System.Drawing.Size(19, 20);
            this.lblXP.TabIndex = 1;
            this.lblXP.Text = "XP:";
            // 
            // cmdMaxValue
            // 
            this.cmdMaxValue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxValue.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxValue.FocusCuesEnabled = false;
            this.cmdMaxValue.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMaxValue.Location = new System.Drawing.Point(118, 3);
            this.cmdMaxValue.Name = "cmdMaxValue";
            this.cmdMaxValue.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxValue.Size = new System.Drawing.Size(100, 20);
            this.cmdMaxValue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxValue.TabIndex = 2;
            this.cmdMaxValue.Text = "Max Value";
            this.cmdMaxValue.Click += new System.EventHandler(this.cmdMaxValue_Click);
            // 
            // RedFactionGuerrilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 85);
            this.Name = "RedFactionGuerrilla";
            this.Text = "Red Faction Guerrilla";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intXP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblXP;
        private DevComponents.Editors.IntegerInput intXP;
        private DevComponents.DotNetBar.ButtonX cmdMaxValue;
    }
}
