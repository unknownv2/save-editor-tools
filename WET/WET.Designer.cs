namespace Horizon.PackageEditors.WET
{
    partial class WET
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
            this.gpSkill = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.intSkillPoints = new DevComponents.Editors.IntegerInput();
            this.lblPoints = new DevComponents.DotNetBar.LabelX();
            this.gpAmmo = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.intBlowDart = new DevComponents.Editors.IntegerInput();
            this.lblBlowDart = new DevComponents.DotNetBar.LabelX();
            this.intMachineGun = new DevComponents.Editors.IntegerInput();
            this.lblMachineGun = new DevComponents.DotNetBar.LabelX();
            this.intShotGun = new DevComponents.Editors.IntegerInput();
            this.lblShotgun = new DevComponents.DotNetBar.LabelX();
            this.rbPackageEditor.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.gpSkill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intSkillPoints)).BeginInit();
            this.gpAmmo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intBlowDart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intMachineGun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.intShotGun)).BeginInit();
            this.SuspendLayout();
            // 
            // rbPackageEditor
            // 
            // 
            // 
            // 
            this.rbPackageEditor.BackgroundStyle.Class = "";
            this.rbPackageEditor.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rbPackageEditor.Size = new System.Drawing.Size(322, 123);
            this.rbPackageEditor.Controls.SetChildIndex(this.panelMain, 0);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gpSkill);
            this.panelMain.Controls.Add(this.gpAmmo);
            this.panelMain.Location = new System.Drawing.Point(0, 53);
            this.panelMain.Size = new System.Drawing.Size(322, 68);
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
            this.tabMain.Text = "Skill / Ammo";
            // 
            // gpSkill
            // 
            this.gpSkill.BackColor = System.Drawing.Color.Transparent;
            this.gpSkill.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpSkill.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpSkill.Controls.Add(this.intSkillPoints);
            this.gpSkill.Controls.Add(this.lblPoints);
            this.gpSkill.Location = new System.Drawing.Point(6, 3);
            this.gpSkill.Name = "gpSkill";
            this.gpSkill.Size = new System.Drawing.Size(94, 59);
            // 
            // 
            // 
            this.gpSkill.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpSkill.Style.BackColorGradientAngle = 90;
            this.gpSkill.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpSkill.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSkill.Style.BorderBottomWidth = 1;
            this.gpSkill.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpSkill.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSkill.Style.BorderLeftWidth = 1;
            this.gpSkill.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSkill.Style.BorderRightWidth = 1;
            this.gpSkill.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpSkill.Style.BorderTopWidth = 1;
            this.gpSkill.Style.Class = "";
            this.gpSkill.Style.CornerDiameter = 4;
            this.gpSkill.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpSkill.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpSkill.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpSkill.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpSkill.StyleMouseDown.Class = "";
            this.gpSkill.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpSkill.StyleMouseOver.Class = "";
            this.gpSkill.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpSkill.TabIndex = 0;
            this.gpSkill.Text = "Skill";
            // 
            // intSkillPoints
            // 
            // 
            // 
            // 
            this.intSkillPoints.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intSkillPoints.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intSkillPoints.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intSkillPoints.Location = new System.Drawing.Point(3, 19);
            this.intSkillPoints.MaxValue = 16777215;
            this.intSkillPoints.MinValue = 0;
            this.intSkillPoints.Name = "intSkillPoints";
            this.intSkillPoints.ShowUpDown = true;
            this.intSkillPoints.Size = new System.Drawing.Size(86, 20);
            this.intSkillPoints.TabIndex = 1;
            // 
            // lblPoints
            // 
            this.lblPoints.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblPoints.BackgroundStyle.Class = "";
            this.lblPoints.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPoints.Location = new System.Drawing.Point(3, 3);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(86, 36);
            this.lblPoints.TabIndex = 0;
            this.lblPoints.Text = "Points:";
            this.lblPoints.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // gpAmmo
            // 
            this.gpAmmo.BackColor = System.Drawing.Color.Transparent;
            this.gpAmmo.CanvasColor = System.Drawing.SystemColors.Control;
            this.gpAmmo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpAmmo.Controls.Add(this.intBlowDart);
            this.gpAmmo.Controls.Add(this.lblBlowDart);
            this.gpAmmo.Controls.Add(this.intMachineGun);
            this.gpAmmo.Controls.Add(this.lblMachineGun);
            this.gpAmmo.Controls.Add(this.intShotGun);
            this.gpAmmo.Controls.Add(this.lblShotgun);
            this.gpAmmo.Location = new System.Drawing.Point(106, 3);
            this.gpAmmo.Name = "gpAmmo";
            this.gpAmmo.Size = new System.Drawing.Size(210, 59);
            // 
            // 
            // 
            this.gpAmmo.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gpAmmo.Style.BackColorGradientAngle = 90;
            this.gpAmmo.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gpAmmo.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAmmo.Style.BorderBottomWidth = 1;
            this.gpAmmo.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpAmmo.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAmmo.Style.BorderLeftWidth = 1;
            this.gpAmmo.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAmmo.Style.BorderRightWidth = 1;
            this.gpAmmo.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gpAmmo.Style.BorderTopWidth = 1;
            this.gpAmmo.Style.Class = "";
            this.gpAmmo.Style.CornerDiameter = 4;
            this.gpAmmo.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpAmmo.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpAmmo.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpAmmo.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpAmmo.StyleMouseDown.Class = "";
            this.gpAmmo.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpAmmo.StyleMouseOver.Class = "";
            this.gpAmmo.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpAmmo.TabIndex = 1;
            this.gpAmmo.Text = "Ammo";
            // 
            // intBlowDart
            // 
            // 
            // 
            // 
            this.intBlowDart.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intBlowDart.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intBlowDart.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intBlowDart.Location = new System.Drawing.Point(147, 19);
            this.intBlowDart.MaxValue = 255;
            this.intBlowDart.MinValue = 0;
            this.intBlowDart.Name = "intBlowDart";
            this.intBlowDart.ShowUpDown = true;
            this.intBlowDart.Size = new System.Drawing.Size(58, 20);
            this.intBlowDart.TabIndex = 6;
            // 
            // lblBlowDart
            // 
            this.lblBlowDart.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblBlowDart.BackgroundStyle.Class = "";
            this.lblBlowDart.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBlowDart.Location = new System.Drawing.Point(147, 3);
            this.lblBlowDart.Name = "lblBlowDart";
            this.lblBlowDart.Size = new System.Drawing.Size(58, 36);
            this.lblBlowDart.TabIndex = 5;
            this.lblBlowDart.Text = "Blow Dart:";
            this.lblBlowDart.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // intMachineGun
            // 
            // 
            // 
            // 
            this.intMachineGun.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intMachineGun.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intMachineGun.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intMachineGun.Location = new System.Drawing.Point(65, 19);
            this.intMachineGun.MaxValue = 255;
            this.intMachineGun.MinValue = 0;
            this.intMachineGun.Name = "intMachineGun";
            this.intMachineGun.ShowUpDown = true;
            this.intMachineGun.Size = new System.Drawing.Size(76, 20);
            this.intMachineGun.TabIndex = 4;
            // 
            // lblMachineGun
            // 
            this.lblMachineGun.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblMachineGun.BackgroundStyle.Class = "";
            this.lblMachineGun.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMachineGun.Location = new System.Drawing.Point(65, 3);
            this.lblMachineGun.Name = "lblMachineGun";
            this.lblMachineGun.Size = new System.Drawing.Size(76, 36);
            this.lblMachineGun.TabIndex = 3;
            this.lblMachineGun.Text = "Machine Gun:";
            this.lblMachineGun.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // intShotGun
            // 
            // 
            // 
            // 
            this.intShotGun.BackgroundStyle.Class = "DateTimeInputBackground";
            this.intShotGun.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.intShotGun.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.intShotGun.Location = new System.Drawing.Point(3, 19);
            this.intShotGun.MaxValue = 255;
            this.intShotGun.MinValue = 0;
            this.intShotGun.Name = "intShotGun";
            this.intShotGun.ShowUpDown = true;
            this.intShotGun.Size = new System.Drawing.Size(56, 20);
            this.intShotGun.TabIndex = 2;
            // 
            // lblShotgun
            // 
            this.lblShotgun.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lblShotgun.BackgroundStyle.Class = "";
            this.lblShotgun.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblShotgun.Location = new System.Drawing.Point(3, 3);
            this.lblShotgun.Name = "lblShotgun";
            this.lblShotgun.Size = new System.Drawing.Size(56, 36);
            this.lblShotgun.TabIndex = 0;
            this.lblShotgun.Text = "Shotgun:";
            this.lblShotgun.TextLineAlignment = System.Drawing.StringAlignment.Near;
            // 
            // WET
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 126);
            this.Name = "WET";
            this.Text = "WET Save Editor";
            this.rbPackageEditor.ResumeLayout(false);
            this.rbPackageEditor.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.gpSkill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intSkillPoints)).EndInit();
            this.gpAmmo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intBlowDart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intMachineGun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.intShotGun)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gpSkill;
        private DevComponents.Editors.IntegerInput intSkillPoints;
        private DevComponents.DotNetBar.LabelX lblPoints;
        private DevComponents.DotNetBar.Controls.GroupPanel gpAmmo;
        private DevComponents.Editors.IntegerInput intBlowDart;
        private DevComponents.DotNetBar.LabelX lblBlowDart;
        private DevComponents.Editors.IntegerInput intMachineGun;
        private DevComponents.DotNetBar.LabelX lblMachineGun;
        private DevComponents.Editors.IntegerInput intShotGun;
        private DevComponents.DotNetBar.LabelX lblShotgun;
    }
}
