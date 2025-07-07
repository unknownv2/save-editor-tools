namespace Horizon.PackageEditors.Alice
{
    partial class Alice
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
            this.lblTeeth = new DevComponents.DotNetBar.LabelX();
            this.intTeeth = new DevComponents.Editors.IntegerInput();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intTeeth)).BeginInit();
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
            this.cmdSave});
            this.rbPackageEditor.Size = new System.Drawing.Size(433, 237);
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
            this.panelMain.Controls.Add(this.intTeeth);
            this.panelMain.Controls.Add(this.lblTeeth);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(433, 182);
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
            // lblTeeth
            // 
            this.lblTeeth.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblTeeth.BackgroundStyle.Class = "";
            this.lblTeeth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTeeth.Location = new System.Drawing.Point(51, 31);
            this.lblTeeth.Name = "lblTeeth";
            this.lblTeeth.Size = new System.Drawing.Size(62, 20);
            this.lblTeeth.TabIndex = 0;
            this.lblTeeth.Text = "Teeth:";
            // 
            // intTeeth
            // 
            // 
            // 
            // 
            this.intTeeth.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intTeeth.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intTeeth.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intTeeth.Location = new System.Drawing.Point(119, 31);
            this.intTeeth.MinValue = 0;
            this.intTeeth.Name = "intTeeth";
            this.intTeeth.ShowUpDown = true;
            this.intTeeth.Size = new System.Drawing.Size(84, 20);
            this.intTeeth.TabIndex = 1;
            // 
            // Alice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 240);
            this.Name = "Alice";
            this.Text = "Alice: Madness Returns";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intTeeth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.IntegerInput intTeeth;
        private DevComponents.DotNetBar.LabelX lblTeeth;
    }
}