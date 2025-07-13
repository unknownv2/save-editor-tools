namespace Horizon.PackageEditors.Quake_Arena_Arcade
{
    partial class QuakeArenaArcade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuakeArenaArcade));
            this.tabExample = new DevComponents.DotNetBar.RibbonTabItem();
            this.panelExample = new DevComponents.DotNetBar.RibbonPanel();
            this.txtExamples = new System.Windows.Forms.RichTextBox();
            this.txtConfig = new System.Windows.Forms.RichTextBox();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelExample.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Controls.Add(this.panelExample);
            this.rbPackageEditor.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.tabExample});
            this.rbPackageEditor.Size = new System.Drawing.Size(418, 318);
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
            this.rbPackageEditor.Controls.SetChildIndex(this.panelExample, 0);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.txtConfig);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(418, 263);
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
            this.tabMain.Text = "Config Editor";
            // 
            // tabExample
            // 
            this.tabExample.Name = "tabExample";
            this.tabExample.Panel = this.panelExample;
            this.tabExample.Text = "Example Commands / Settings";
            // 
            // panelExample
            // 
            this.panelExample.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelExample.Controls.Add(this.txtExamples);
            this.panelExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExample.Location = new System.Drawing.Point(0, 53);
            this.panelExample.Name = "panelExample";
            this.panelExample.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.panelExample.Size = new System.Drawing.Size(418, 263);
            // 
            // 
            // 
            this.panelExample.Style.Class = "";
            this.panelExample.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelExample.StyleMouseDown.Class = "";
            this.panelExample.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.panelExample.StyleMouseOver.Class = "";
            this.panelExample.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.panelExample.TabIndex = 2;
            this.panelExample.Visible = false;
            // 
            // txtExamples
            // 
            this.txtExamples.DetectUrls = false;
            this.txtExamples.Location = new System.Drawing.Point(6, 4);
            this.txtExamples.MaxLength = 3200;
            this.txtExamples.Name = "txtExamples";
            this.txtExamples.ReadOnly = true;
            this.txtExamples.Size = new System.Drawing.Size(407, 254);
            this.txtExamples.TabIndex = 1;
            this.txtExamples.Text = resources.GetString("txtExamples.Text");
            // 
            // txtConfig
            // 
            this.txtConfig.DetectUrls = false;
            this.txtConfig.Location = new System.Drawing.Point(6, 4);
            this.txtConfig.MaxLength = 3200;
            this.txtConfig.Name = "txtConfig";
            this.txtConfig.Size = new System.Drawing.Size(407, 254);
            this.txtConfig.TabIndex = 0;
            this.txtConfig.Text = "";
            // 
            // QuakeArenaArcade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 321);
            this.Name = "QuakeArenaArcade";
            this.Text = "Quake Arena Arcade Configuration Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelExample.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonPanel panelExample;
        private DevComponents.DotNetBar.RibbonTabItem tabExample;
        private System.Windows.Forms.RichTextBox txtConfig;
        private System.Windows.Forms.RichTextBox txtExamples;
    }
}