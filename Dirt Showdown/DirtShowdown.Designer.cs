namespace Horizon.PackageEditors.Dirt_Showdown
{
    partial class DirtShowdown
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
            this.intMoney = new DevComponents.Editors.IntegerInput();
            this.cmdMaxBalance = new DevComponents.DotNetBar.ButtonX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intMoney)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(254, 85);
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
            this.panelMain.Controls.Add(this.cmdMaxBalance);
            this.panelMain.Controls.Add(this.intMoney);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(254, 30);
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
            this.tabMain.Text = "Money";
            // 
            // intMoney
            // 
            // 
            // 
            // 
            this.intMoney.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intMoney.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intMoney.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intMoney.Location = new System.Drawing.Point(7, 4);
            this.intMoney.MinValue = 0;
            this.intMoney.Name = "intMoney";
            this.intMoney.ShowUpDown = true;
            this.intMoney.Size = new System.Drawing.Size(126, 20);
            this.intMoney.TabIndex = 0;
            // 
            // cmdMaxBalance
            // 
            this.cmdMaxBalance.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxBalance.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxBalance.FocusCuesEnabled = false;
            this.cmdMaxBalance.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMaxBalance.Location = new System.Drawing.Point(142, 4);
            this.cmdMaxBalance.Name = "cmdMaxBalance";
            this.cmdMaxBalance.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxBalance.Size = new System.Drawing.Size(104, 20);
            this.cmdMaxBalance.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxBalance.TabIndex = 6;
            this.cmdMaxBalance.Text = "Max Balance";
            this.cmdMaxBalance.Click += new System.EventHandler(this.cmdMaxBalance_Click);
            // 
            // DirtShowdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 88);
            this.Name = "DirtShowdown";
            this.Text = "DiRT Showdown";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intMoney)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.IntegerInput intMoney;
        private DevComponents.DotNetBar.ButtonX cmdMaxBalance;
    }
}