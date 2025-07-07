namespace Horizon.PackageEditors.Assassin_s_Creed_Revelations
{
    partial class ACRevelations
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
            this.cmdMaxMoney = new DevComponents.DotNetBar.ButtonX();
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
            this.rbPackageEditor.Size = new System.Drawing.Size(246, 81);
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
            this.panelMain.Controls.Add(this.intMoney);
            this.panelMain.Controls.Add(this.cmdMaxMoney);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(246, 26);
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
            // intMoney
            // 
            // 
            // 
            // 
            this.intMoney.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intMoney.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intMoney.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intMoney.Location = new System.Drawing.Point(5, 2);
            this.intMoney.MaxValue = 999999999;
            this.intMoney.MinValue = 0;
            this.intMoney.Name = "intMoney";
            this.intMoney.ShowUpDown = true;
            this.intMoney.Size = new System.Drawing.Size(122, 20);
            this.intMoney.TabIndex = 2;
            // 
            // cmdMaxMoney
            // 
            this.cmdMaxMoney.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdMaxMoney.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdMaxMoney.FocusCuesEnabled = false;
            this.cmdMaxMoney.Image = global::Horizon.Properties.Resources.UpArrow;
            this.cmdMaxMoney.Location = new System.Drawing.Point(132, 2);
            this.cmdMaxMoney.Name = "cmdMaxMoney";
            this.cmdMaxMoney.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdMaxMoney.Size = new System.Drawing.Size(109, 20);
            this.cmdMaxMoney.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdMaxMoney.TabIndex = 3;
            this.cmdMaxMoney.Text = "Max Florins";
            this.cmdMaxMoney.Click += new System.EventHandler(this.BtnClick_MaxMoney);
            // 
            // ACRevelations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 84);
            this.Name = "ACRevelations";
            this.Text = "Assassin\'s Creed: Revelations";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intMoney)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.IntegerInput intMoney;
        private DevComponents.DotNetBar.ButtonX cmdMaxMoney;
    }
}