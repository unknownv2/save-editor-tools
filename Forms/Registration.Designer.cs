namespace Horizon.Forms
{
    partial class Registration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Registration));
            this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
            this.cmdRegister = new DevComponents.DotNetBar.Office2007StartButton();
            this.cmdTheRules = new DevComponents.DotNetBar.Office2007StartButton();
            this.cmdCancel = new DevComponents.DotNetBar.Office2007StartButton();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.qatCustomizeItem1 = new DevComponents.DotNetBar.QatCustomizeItem();
            this.panelRegistration = new DevComponents.DotNetBar.PanelEx();
            this.panelHuman = new DevComponents.DotNetBar.PanelEx();
            this.cmdRefresh = new DevComponents.DotNetBar.ButtonX();
            this.cmdHumanQuestion = new DevComponents.DotNetBar.ButtonX();
            this.txtHuman = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pbHuman = new System.Windows.Forms.PictureBox();
            this.panelPassword = new DevComponents.DotNetBar.PanelEx();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            this.txtPassword2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtPassword1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblPassword = new DevComponents.DotNetBar.LabelX();
            this.panelEmail = new DevComponents.DotNetBar.PanelEx();
            this.pbEmail = new System.Windows.Forms.PictureBox();
            this.txtEmail2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtEmail = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblEmail = new DevComponents.DotNetBar.LabelX();
            this.panelAgreement = new DevComponents.DotNetBar.PanelEx();
            this.lblAgreement = new DevComponents.DotNetBar.LabelX();
            this.panelUsername = new DevComponents.DotNetBar.PanelEx();
            this.pbUsername = new System.Windows.Forms.PictureBox();
            this.txtUsername = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblUsername = new DevComponents.DotNetBar.LabelX();
            this.panelRegistration.SuspendLayout();
            this.panelHuman.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHuman)).BeginInit();
            this.panelPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            this.panelEmail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmail)).BeginInit();
            this.panelAgreement.SuspendLayout();
            this.panelUsername.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUsername)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            // 
            // 
            // 
            this.ribbonControl1.BackgroundStyle.Class = "";
            this.ribbonControl1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ribbonControl1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.cmdRegister,
            this.cmdTheRules,
            this.cmdCancel});
            this.ribbonControl1.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.ribbonControl1.Location = new System.Drawing.Point(0, 316);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.ribbonControl1.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.qatCustomizeItem1});
            this.ribbonControl1.Size = new System.Drawing.Size(284, 25);
            this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl1.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.ribbonControl1.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.ribbonControl1.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.ribbonControl1.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.ribbonControl1.SystemText.QatDialogAddButton = "&Add >>";
            this.ribbonControl1.SystemText.QatDialogCancelButton = "Cancel";
            this.ribbonControl1.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.ribbonControl1.SystemText.QatDialogOkButton = "OK";
            this.ribbonControl1.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatDialogRemoveButton = "&Remove";
            this.ribbonControl1.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.ribbonControl1.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.ribbonControl1.TabGroupHeight = 14;
            this.ribbonControl1.TabIndex = 0;
            this.ribbonControl1.Text = "ribbonControl1";
            // 
            // cmdRegister
            // 
            this.cmdRegister.CanCustomize = false;
            this.cmdRegister.FixedSize = new System.Drawing.Size(154, 23);
            this.cmdRegister.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdRegister.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdRegister.ImagePaddingHorizontal = 0;
            this.cmdRegister.ImagePaddingVertical = 0;
            this.cmdRegister.Name = "cmdRegister";
            this.cmdRegister.ShowSubItems = false;
            this.cmdRegister.Text = "Agree and Create Account";
            this.cmdRegister.Click += new System.EventHandler(this.cmdRegister_Click);
            // 
            // cmdTheRules
            // 
            this.cmdTheRules.CanCustomize = false;
            this.cmdTheRules.ColorTable = DevComponents.DotNetBar.eButtonColor.Magenta;
            this.cmdTheRules.FixedSize = new System.Drawing.Size(70, 23);
            this.cmdTheRules.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdTheRules.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdTheRules.ImagePaddingHorizontal = 0;
            this.cmdTheRules.ImagePaddingVertical = 0;
            this.cmdTheRules.Name = "cmdTheRules";
            this.cmdTheRules.ShowSubItems = false;
            this.cmdTheRules.Text = "The Rules";
            this.cmdTheRules.Click += new System.EventHandler(this.cmdTheRules_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.CanCustomize = false;
            this.cmdCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.cmdCancel.FixedSize = new System.Drawing.Size(57, 23);
            this.cmdCancel.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.Image;
            this.cmdCancel.ImageFixedSize = new System.Drawing.Size(16, 16);
            this.cmdCancel.ImagePaddingHorizontal = 0;
            this.cmdCancel.ImagePaddingVertical = 0;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.ShowSubItems = false;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // qatCustomizeItem1
            // 
            this.qatCustomizeItem1.Name = "qatCustomizeItem1";
            // 
            // panelRegistration
            // 
            this.panelRegistration.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelRegistration.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelRegistration.Controls.Add(this.panelHuman);
            this.panelRegistration.Controls.Add(this.panelPassword);
            this.panelRegistration.Controls.Add(this.panelEmail);
            this.panelRegistration.Controls.Add(this.panelAgreement);
            this.panelRegistration.Controls.Add(this.panelUsername);
            this.panelRegistration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegistration.Location = new System.Drawing.Point(0, 0);
            this.panelRegistration.Name = "panelRegistration";
            this.panelRegistration.Size = new System.Drawing.Size(284, 316);
            this.panelRegistration.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelRegistration.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelRegistration.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelRegistration.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelRegistration.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelRegistration.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelRegistration.Style.GradientAngle = 90;
            this.panelRegistration.TabIndex = 1;
            // 
            // panelHuman
            // 
            this.panelHuman.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelHuman.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelHuman.Controls.Add(this.cmdRefresh);
            this.panelHuman.Controls.Add(this.cmdHumanQuestion);
            this.panelHuman.Controls.Add(this.txtHuman);
            this.panelHuman.Controls.Add(this.pbHuman);
            this.panelHuman.Location = new System.Drawing.Point(12, 164);
            this.panelHuman.Name = "panelHuman";
            this.panelHuman.Size = new System.Drawing.Size(260, 80);
            this.panelHuman.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelHuman.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelHuman.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelHuman.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelHuman.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelHuman.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelHuman.Style.GradientAngle = 90;
            this.panelHuman.TabIndex = 7;
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdRefresh.FocusCuesEnabled = false;
            this.cmdRefresh.Image = global::Horizon.Properties.Resources.Refresh;
            this.cmdRefresh.Location = new System.Drawing.Point(200, 60);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdRefresh.Size = new System.Drawing.Size(60, 20);
            this.cmdRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdRefresh.TabIndex = 3;
            this.cmdRefresh.TabStop = false;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // cmdHumanQuestion
            // 
            this.cmdHumanQuestion.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdHumanQuestion.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdHumanQuestion.FocusCuesEnabled = false;
            this.cmdHumanQuestion.Image = global::Horizon.Properties.Resources.Question;
            this.cmdHumanQuestion.Location = new System.Drawing.Point(200, 0);
            this.cmdHumanQuestion.Name = "cmdHumanQuestion";
            this.cmdHumanQuestion.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
            this.cmdHumanQuestion.Size = new System.Drawing.Size(60, 61);
            this.cmdHumanQuestion.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdHumanQuestion.TabIndex = 2;
            this.cmdHumanQuestion.TabStop = false;
            this.cmdHumanQuestion.Click += new System.EventHandler(this.cmdHumanQuestion_Click);
            // 
            // txtHuman
            // 
            // 
            // 
            // 
            this.txtHuman.Border.Class = "TextBoxBorder";
            this.txtHuman.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtHuman.Location = new System.Drawing.Point(0, 60);
            this.txtHuman.MaxLength = 10;
            this.txtHuman.Name = "txtHuman";
            this.txtHuman.Size = new System.Drawing.Size(201, 20);
            this.txtHuman.TabIndex = 5;
            this.txtHuman.WatermarkText = "Enter code here...";
            // 
            // pbHuman
            // 
            this.pbHuman.ImageLocation = "";
            this.pbHuman.Location = new System.Drawing.Point(1, 1);
            this.pbHuman.Name = "pbHuman";
            this.pbHuman.Size = new System.Drawing.Size(200, 60);
            this.pbHuman.TabIndex = 0;
            this.pbHuman.TabStop = false;
            // 
            // panelPassword
            // 
            this.panelPassword.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelPassword.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelPassword.Controls.Add(this.pbPassword);
            this.panelPassword.Controls.Add(this.txtPassword2);
            this.panelPassword.Controls.Add(this.txtPassword1);
            this.panelPassword.Controls.Add(this.lblPassword);
            this.panelPassword.Location = new System.Drawing.Point(12, 48);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(260, 52);
            this.panelPassword.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelPassword.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelPassword.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelPassword.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelPassword.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelPassword.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelPassword.Style.GradientAngle = 90;
            this.panelPassword.TabIndex = 1;
            // 
            // pbPassword
            // 
            this.pbPassword.Image = global::Horizon.Properties.Resources.GrayDot;
            this.pbPassword.Location = new System.Drawing.Point(236, 5);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(19, 17);
            this.pbPassword.TabIndex = 3;
            this.pbPassword.TabStop = false;
            // 
            // txtPassword2
            // 
            // 
            // 
            // 
            this.txtPassword2.Border.Class = "TextBoxBorder";
            this.txtPassword2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPassword2.Location = new System.Drawing.Point(132, 26);
            this.txtPassword2.MaxLength = 50;
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(123, 20);
            this.txtPassword2.TabIndex = 2;
            this.txtPassword2.UseSystemPasswordChar = true;
            this.txtPassword2.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtPassword1
            // 
            // 
            // 
            // 
            this.txtPassword1.Border.Class = "TextBoxBorder";
            this.txtPassword1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPassword1.Location = new System.Drawing.Point(5, 26);
            this.txtPassword1.MaxLength = 50;
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.Size = new System.Drawing.Size(123, 20);
            this.txtPassword1.TabIndex = 1;
            this.txtPassword1.UseSystemPasswordChar = true;
            this.txtPassword1.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblPassword
            // 
            // 
            // 
            // 
            this.lblPassword.BackgroundStyle.Class = "";
            this.lblPassword.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPassword.Location = new System.Drawing.Point(5, 5);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(225, 17);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password / Confirm:";
            // 
            // panelEmail
            // 
            this.panelEmail.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEmail.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEmail.Controls.Add(this.pbEmail);
            this.panelEmail.Controls.Add(this.txtEmail2);
            this.panelEmail.Controls.Add(this.txtEmail);
            this.panelEmail.Controls.Add(this.lblEmail);
            this.panelEmail.Location = new System.Drawing.Point(12, 106);
            this.panelEmail.Name = "panelEmail";
            this.panelEmail.Size = new System.Drawing.Size(260, 52);
            this.panelEmail.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEmail.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEmail.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEmail.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEmail.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEmail.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEmail.Style.GradientAngle = 90;
            this.panelEmail.TabIndex = 2;
            // 
            // pbEmail
            // 
            this.pbEmail.Image = global::Horizon.Properties.Resources.GrayDot;
            this.pbEmail.Location = new System.Drawing.Point(236, 5);
            this.pbEmail.Name = "pbEmail";
            this.pbEmail.Size = new System.Drawing.Size(19, 17);
            this.pbEmail.TabIndex = 4;
            this.pbEmail.TabStop = false;
            // 
            // txtEmail2
            // 
            // 
            // 
            // 
            this.txtEmail2.Border.Class = "TextBoxBorder";
            this.txtEmail2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEmail2.Location = new System.Drawing.Point(132, 26);
            this.txtEmail2.MaxLength = 50;
            this.txtEmail2.Name = "txtEmail2";
            this.txtEmail2.Size = new System.Drawing.Size(123, 20);
            this.txtEmail2.TabIndex = 4;
            this.txtEmail2.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // txtEmail
            // 
            // 
            // 
            // 
            this.txtEmail.Border.Class = "TextBoxBorder";
            this.txtEmail.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEmail.Location = new System.Drawing.Point(5, 26);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(123, 20);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // lblEmail
            // 
            // 
            // 
            // 
            this.lblEmail.BackgroundStyle.Class = "";
            this.lblEmail.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblEmail.Location = new System.Drawing.Point(5, 5);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(225, 17);
            this.lblEmail.TabIndex = 1;
            this.lblEmail.Text = "Email / Confirm:";
            // 
            // panelAgreement
            // 
            this.panelAgreement.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelAgreement.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelAgreement.Controls.Add(this.lblAgreement);
            this.panelAgreement.Location = new System.Drawing.Point(12, 250);
            this.panelAgreement.Name = "panelAgreement";
            this.panelAgreement.Size = new System.Drawing.Size(260, 52);
            this.panelAgreement.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelAgreement.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelAgreement.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelAgreement.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelAgreement.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelAgreement.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelAgreement.Style.GradientAngle = 90;
            this.panelAgreement.TabIndex = 4;
            // 
            // lblAgreement
            // 
            // 
            // 
            // 
            this.lblAgreement.BackgroundStyle.Class = "";
            this.lblAgreement.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblAgreement.Location = new System.Drawing.Point(3, 5);
            this.lblAgreement.Name = "lblAgreement";
            this.lblAgreement.Size = new System.Drawing.Size(254, 42);
            this.lblAgreement.TabIndex = 3;
            this.lblAgreement.Text = "Upon registering an account, you agree that you are at least 13 years o" +
                "f age and that you will abide by all of the forum rules in the link below.";
            this.lblAgreement.TextAlignment = System.Drawing.StringAlignment.Center;
            this.lblAgreement.WordWrap = true;
            // 
            // panelUsername
            // 
            this.panelUsername.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelUsername.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelUsername.Controls.Add(this.pbUsername);
            this.panelUsername.Controls.Add(this.txtUsername);
            this.panelUsername.Controls.Add(this.lblUsername);
            this.panelUsername.Location = new System.Drawing.Point(12, 12);
            this.panelUsername.Name = "panelUsername";
            this.panelUsername.Size = new System.Drawing.Size(260, 30);
            this.panelUsername.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelUsername.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelUsername.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelUsername.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelUsername.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelUsername.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelUsername.Style.GradientAngle = 90;
            this.panelUsername.TabIndex = 0;
            // 
            // pbUsername
            // 
            this.pbUsername.Image = global::Horizon.Properties.Resources.GrayDot;
            this.pbUsername.Location = new System.Drawing.Point(236, 7);
            this.pbUsername.Name = "pbUsername";
            this.pbUsername.Size = new System.Drawing.Size(19, 17);
            this.pbUsername.TabIndex = 4;
            this.pbUsername.TabStop = false;
            // 
            // txtUsername
            // 
            // 
            // 
            // 
            this.txtUsername.Border.Class = "TextBoxBorder";
            this.txtUsername.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtUsername.Location = new System.Drawing.Point(72, 5);
            this.txtUsername.MaxLength = 15;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(158, 20);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // lblUsername
            // 
            // 
            // 
            // 
            this.lblUsername.BackgroundStyle.Class = "";
            this.lblUsername.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblUsername.Location = new System.Drawing.Point(5, 3);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(61, 22);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "Username:";
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 341);
            this.Controls.Add(this.panelRegistration);
            this.Controls.Add(this.ribbonControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Registration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Registration_FormClosing);
            this.panelRegistration.ResumeLayout(false);
            this.panelHuman.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbHuman)).EndInit();
            this.panelPassword.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            this.panelEmail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbEmail)).EndInit();
            this.panelAgreement.ResumeLayout(false);
            this.panelUsername.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbUsername)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.RibbonControl ribbonControl1;
        private DevComponents.DotNetBar.Office2007StartButton cmdRegister;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.QatCustomizeItem qatCustomizeItem1;
        private DevComponents.DotNetBar.Office2007StartButton cmdCancel;
        private DevComponents.DotNetBar.PanelEx panelRegistration;
        private DevComponents.DotNetBar.LabelX lblAgreement;
        private DevComponents.DotNetBar.PanelEx panelUsername;
        private DevComponents.DotNetBar.Controls.TextBoxX txtUsername;
        private DevComponents.DotNetBar.LabelX lblUsername;
        private DevComponents.DotNetBar.Office2007StartButton cmdTheRules;
        private DevComponents.DotNetBar.PanelEx panelPassword;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPassword1;
        private DevComponents.DotNetBar.LabelX lblPassword;
        private DevComponents.DotNetBar.PanelEx panelEmail;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEmail2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEmail;
        private DevComponents.DotNetBar.LabelX lblEmail;
        private DevComponents.DotNetBar.PanelEx panelAgreement;
        private System.Windows.Forms.PictureBox pbPassword;
        private System.Windows.Forms.PictureBox pbEmail;
        private DevComponents.DotNetBar.PanelEx panelHuman;
        private System.Windows.Forms.PictureBox pbHuman;
        private DevComponents.DotNetBar.Controls.TextBoxX txtHuman;
        private DevComponents.DotNetBar.ButtonX cmdHumanQuestion;
        private DevComponents.DotNetBar.ButtonX cmdRefresh;
        private System.Windows.Forms.PictureBox pbUsername;
    }
}