namespace WindowsClient
{
    partial class FormMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnGetUsers = new System.Windows.Forms.Button();
            this.lboxUsers = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGetDescription = new System.Windows.Forms.Button();
            this.txtMyDescription = new System.Windows.Forms.TextBox();
            this.btnSetDescription = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGetUsersByCertificate = new System.Windows.Forms.Button();
            this.buttonSetMyDescriptionByCertificate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonChangeSelectedUserPassword = new System.Windows.Forms.Button();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(24, 46);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(415, 26);
            this.txtLogin.TabIndex = 1;
            this.txtLogin.Text = "si";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(24, 111);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(415, 26);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "123";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnGetUsers
            // 
            this.btnGetUsers.Location = new System.Drawing.Point(24, 151);
            this.btnGetUsers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGetUsers.Name = "btnGetUsers";
            this.btnGetUsers.Size = new System.Drawing.Size(417, 35);
            this.btnGetUsers.TabIndex = 4;
            this.btnGetUsers.Text = "Get Users";
            this.btnGetUsers.UseVisualStyleBackColor = true;
            this.btnGetUsers.Click += new System.EventHandler(this.BtnGetUsers_Click);
            // 
            // lboxUsers
            // 
            this.lboxUsers.FormattingEnabled = true;
            this.lboxUsers.ItemHeight = 20;
            this.lboxUsers.Location = new System.Drawing.Point(478, 46);
            this.lboxUsers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lboxUsers.Name = "lboxUsers";
            this.lboxUsers.Size = new System.Drawing.Size(390, 184);
            this.lboxUsers.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Users";
            // 
            // btnGetDescription
            // 
            this.btnGetDescription.Location = new System.Drawing.Point(478, 242);
            this.btnGetDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGetDescription.Name = "btnGetDescription";
            this.btnGetDescription.Size = new System.Drawing.Size(390, 35);
            this.btnGetDescription.TabIndex = 7;
            this.btnGetDescription.Text = "Get Selected User Description";
            this.btnGetDescription.UseVisualStyleBackColor = true;
            this.btnGetDescription.Click += new System.EventHandler(this.BtnGetDescription_Click);
            // 
            // txtMyDescription
            // 
            this.txtMyDescription.Location = new System.Drawing.Point(18, 282);
            this.txtMyDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMyDescription.Multiline = true;
            this.txtMyDescription.Name = "txtMyDescription";
            this.txtMyDescription.Size = new System.Drawing.Size(415, 121);
            this.txtMyDescription.TabIndex = 8;
            // 
            // btnSetDescription
            // 
            this.btnSetDescription.Location = new System.Drawing.Point(18, 414);
            this.btnSetDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSetDescription.Name = "btnSetDescription";
            this.btnSetDescription.Size = new System.Drawing.Size(417, 35);
            this.btnSetDescription.TabIndex = 9;
            this.btnSetDescription.Text = "Set My Description";
            this.btnSetDescription.UseVisualStyleBackColor = true;
            this.btnSetDescription.Click += new System.EventHandler(this.BtnSetDescription_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(478, 286);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(388, 116);
            this.txtDescription.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 257);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Description";
            // 
            // buttonGetUsersByCertificate
            // 
            this.buttonGetUsersByCertificate.Location = new System.Drawing.Point(24, 195);
            this.buttonGetUsersByCertificate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGetUsersByCertificate.Name = "buttonGetUsersByCertificate";
            this.buttonGetUsersByCertificate.Size = new System.Drawing.Size(417, 35);
            this.buttonGetUsersByCertificate.TabIndex = 11;
            this.buttonGetUsersByCertificate.Text = "Get Users By Certificate";
            this.buttonGetUsersByCertificate.UseVisualStyleBackColor = true;
            this.buttonGetUsersByCertificate.Click += new System.EventHandler(this.buttonGetUsersByCertificate_Click);
            // 
            // buttonSetMyDescriptionByCertificate
            // 
            this.buttonSetMyDescriptionByCertificate.Location = new System.Drawing.Point(18, 460);
            this.buttonSetMyDescriptionByCertificate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSetMyDescriptionByCertificate.Name = "buttonSetMyDescriptionByCertificate";
            this.buttonSetMyDescriptionByCertificate.Size = new System.Drawing.Size(417, 35);
            this.buttonSetMyDescriptionByCertificate.TabIndex = 12;
            this.buttonSetMyDescriptionByCertificate.Text = "Set My Description by Certificate";
            this.buttonSetMyDescriptionByCertificate.UseVisualStyleBackColor = true;
            this.buttonSetMyDescriptionByCertificate.Click += new System.EventHandler(this.buttonSetMyDescriptionByCertificate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(477, 460);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "New Password";
            // 
            // buttonChangeSelectedUserPassword
            // 
            this.buttonChangeSelectedUserPassword.Location = new System.Drawing.Point(478, 414);
            this.buttonChangeSelectedUserPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonChangeSelectedUserPassword.Name = "buttonChangeSelectedUserPassword";
            this.buttonChangeSelectedUserPassword.Size = new System.Drawing.Size(391, 35);
            this.buttonChangeSelectedUserPassword.TabIndex = 14;
            this.buttonChangeSelectedUserPassword.Text = "Change Selected User\'s Password (Certificate) ";
            this.buttonChangeSelectedUserPassword.UseVisualStyleBackColor = true;
            this.buttonChangeSelectedUserPassword.Click += new System.EventHandler(this.buttonChangeSelectedUserPassword_Click);
            // 
            // textBoxNewPassword
            // 
            this.textBoxNewPassword.Location = new System.Drawing.Point(598, 457);
            this.textBoxNewPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            this.textBoxNewPassword.Size = new System.Drawing.Size(268, 26);
            this.textBoxNewPassword.TabIndex = 15;
            this.textBoxNewPassword.UseSystemPasswordChar = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 505);
            this.Controls.Add(this.textBoxNewPassword);
            this.Controls.Add(this.buttonChangeSelectedUserPassword);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonSetMyDescriptionByCertificate);
            this.Controls.Add(this.buttonGetUsersByCertificate);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.btnSetDescription);
            this.Controls.Add(this.txtMyDescription);
            this.Controls.Add(this.btnGetDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lboxUsers);
            this.Controls.Add(this.btnGetUsers);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "WindowsClient";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnGetUsers;
        private System.Windows.Forms.ListBox lboxUsers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGetDescription;
        private System.Windows.Forms.TextBox txtMyDescription;
        private System.Windows.Forms.Button btnSetDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGetUsersByCertificate;
        private System.Windows.Forms.Button buttonSetMyDescriptionByCertificate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonChangeSelectedUserPassword;
        private System.Windows.Forms.TextBox textBoxNewPassword;
    }
}

