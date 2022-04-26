namespace Client
{
    partial class FormEISIPracticalExam
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEISIPracticalExam));
            this.groupBoxNetworkConfig = new System.Windows.Forms.GroupBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxServerPort = new System.Windows.Forms.TextBox();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.textBoxServerIPAddress = new System.Windows.Forms.TextBox();
            this.labelServerIPAddress = new System.Windows.Forms.Label();
            this.groupBoxCryptographicSetupServer = new System.Windows.Forms.GroupBox();
            this.buttonGenerateClientAssymetricalKeys = new System.Windows.Forms.Button();
            this.buttonGenerateRecipientAsymmetricalKeys = new System.Windows.Forms.Button();
            this.groupBoxSecureChannel = new System.Windows.Forms.GroupBox();
            this.buttonExchangeAsymmetricalKeys = new System.Windows.Forms.Button();
            this.groupBoxEmailService = new System.Windows.Forms.GroupBox();
            this.buttonEncryptSendMessageRecipient = new System.Windows.Forms.Button();
            this.buttonSendSymmetricElementsRecipient = new System.Windows.Forms.Button();
            this.textBoxMessageToSend = new System.Windows.Forms.TextBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.groupBoxCryptographicSetupRecipient = new System.Windows.Forms.GroupBox();
            this.buttonGenerateSymmetricalElementsRecipient = new System.Windows.Forms.Button();
            this.groupBoxNetworkConfig.SuspendLayout();
            this.groupBoxCryptographicSetupServer.SuspendLayout();
            this.groupBoxSecureChannel.SuspendLayout();
            this.groupBoxEmailService.SuspendLayout();
            this.groupBoxCryptographicSetupRecipient.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxNetworkConfig
            // 
            this.groupBoxNetworkConfig.Controls.Add(this.buttonConnect);
            this.groupBoxNetworkConfig.Controls.Add(this.textBoxServerPort);
            this.groupBoxNetworkConfig.Controls.Add(this.labelServerPort);
            this.groupBoxNetworkConfig.Controls.Add(this.textBoxServerIPAddress);
            this.groupBoxNetworkConfig.Controls.Add(this.labelServerIPAddress);
            this.groupBoxNetworkConfig.Location = new System.Drawing.Point(24, 367);
            this.groupBoxNetworkConfig.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxNetworkConfig.Name = "groupBoxNetworkConfig";
            this.groupBoxNetworkConfig.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxNetworkConfig.Size = new System.Drawing.Size(627, 194);
            this.groupBoxNetworkConfig.TabIndex = 0;
            this.groupBoxNetworkConfig.TabStop = false;
            this.groupBoxNetworkConfig.Text = "Network";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(207, 130);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(6);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(367, 42);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "3) Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxServerPort
            // 
            this.textBoxServerPort.Location = new System.Drawing.Point(209, 89);
            this.textBoxServerPort.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxServerPort.Name = "textBoxServerPort";
            this.textBoxServerPort.Size = new System.Drawing.Size(365, 29);
            this.textBoxServerPort.TabIndex = 3;
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Location = new System.Drawing.Point(29, 102);
            this.labelServerPort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(110, 25);
            this.labelServerPort.TabIndex = 2;
            this.labelServerPort.Text = "Server Port";
            // 
            // textBoxServerIPAddress
            // 
            this.textBoxServerIPAddress.Location = new System.Drawing.Point(209, 41);
            this.textBoxServerIPAddress.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxServerIPAddress.Name = "textBoxServerIPAddress";
            this.textBoxServerIPAddress.Size = new System.Drawing.Size(365, 29);
            this.textBoxServerIPAddress.TabIndex = 1;
            this.textBoxServerIPAddress.Text = "127.0.0.1";
            // 
            // labelServerIPAddress
            // 
            this.labelServerIPAddress.AutoSize = true;
            this.labelServerIPAddress.Location = new System.Drawing.Point(29, 46);
            this.labelServerIPAddress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelServerIPAddress.Name = "labelServerIPAddress";
            this.labelServerIPAddress.Size = new System.Drawing.Size(171, 25);
            this.labelServerIPAddress.TabIndex = 0;
            this.labelServerIPAddress.Text = "Server IP Address";
            // 
            // groupBoxCryptographicSetupServer
            // 
            this.groupBoxCryptographicSetupServer.Controls.Add(this.buttonGenerateClientAssymetricalKeys);
            this.groupBoxCryptographicSetupServer.ForeColor = System.Drawing.Color.Green;
            this.groupBoxCryptographicSetupServer.Location = new System.Drawing.Point(22, 22);
            this.groupBoxCryptographicSetupServer.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxCryptographicSetupServer.Name = "groupBoxCryptographicSetupServer";
            this.groupBoxCryptographicSetupServer.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxCryptographicSetupServer.Size = new System.Drawing.Size(627, 138);
            this.groupBoxCryptographicSetupServer.TabIndex = 1;
            this.groupBoxCryptographicSetupServer.TabStop = false;
            this.groupBoxCryptographicSetupServer.Text = "Cryptography Setup - Server";
            // 
            // buttonGenerateClientAssymetricalKeys
            // 
            this.buttonGenerateClientAssymetricalKeys.Location = new System.Drawing.Point(35, 63);
            this.buttonGenerateClientAssymetricalKeys.Margin = new System.Windows.Forms.Padding(6);
            this.buttonGenerateClientAssymetricalKeys.Name = "buttonGenerateClientAssymetricalKeys";
            this.buttonGenerateClientAssymetricalKeys.Size = new System.Drawing.Size(543, 42);
            this.buttonGenerateClientAssymetricalKeys.TabIndex = 0;
            this.buttonGenerateClientAssymetricalKeys.Text = "1) Generate Client Asymmetric Keys";
            this.buttonGenerateClientAssymetricalKeys.UseVisualStyleBackColor = true;
            this.buttonGenerateClientAssymetricalKeys.Click += new System.EventHandler(this.buttonGenerateClientAssymetricalKeys_Click);
            // 
            // buttonGenerateRecipientAsymmetricalKeys
            // 
            this.buttonGenerateRecipientAsymmetricalKeys.Location = new System.Drawing.Point(35, 35);
            this.buttonGenerateRecipientAsymmetricalKeys.Margin = new System.Windows.Forms.Padding(6);
            this.buttonGenerateRecipientAsymmetricalKeys.Name = "buttonGenerateRecipientAsymmetricalKeys";
            this.buttonGenerateRecipientAsymmetricalKeys.Size = new System.Drawing.Size(543, 42);
            this.buttonGenerateRecipientAsymmetricalKeys.TabIndex = 1;
            this.buttonGenerateRecipientAsymmetricalKeys.Text = "2) Generate Recipient Asymmetric Keys";
            this.buttonGenerateRecipientAsymmetricalKeys.UseVisualStyleBackColor = true;
            this.buttonGenerateRecipientAsymmetricalKeys.Click += new System.EventHandler(this.buttonGenerateRecipientAsymmetricalKeys_Click);
            // 
            // groupBoxSecureChannel
            // 
            this.groupBoxSecureChannel.Controls.Add(this.buttonExchangeAsymmetricalKeys);
            this.groupBoxSecureChannel.Location = new System.Drawing.Point(22, 598);
            this.groupBoxSecureChannel.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxSecureChannel.Name = "groupBoxSecureChannel";
            this.groupBoxSecureChannel.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxSecureChannel.Size = new System.Drawing.Size(627, 131);
            this.groupBoxSecureChannel.TabIndex = 2;
            this.groupBoxSecureChannel.TabStop = false;
            this.groupBoxSecureChannel.Text = "Secure Channel";
            // 
            // buttonExchangeAsymmetricalKeys
            // 
            this.buttonExchangeAsymmetricalKeys.Location = new System.Drawing.Point(35, 54);
            this.buttonExchangeAsymmetricalKeys.Margin = new System.Windows.Forms.Padding(6);
            this.buttonExchangeAsymmetricalKeys.Name = "buttonExchangeAsymmetricalKeys";
            this.buttonExchangeAsymmetricalKeys.Size = new System.Drawing.Size(543, 42);
            this.buttonExchangeAsymmetricalKeys.TabIndex = 0;
            this.buttonExchangeAsymmetricalKeys.Text = "4) Exchange Asymmetric Keys";
            this.buttonExchangeAsymmetricalKeys.UseVisualStyleBackColor = true;
            this.buttonExchangeAsymmetricalKeys.Click += new System.EventHandler(this.buttonExchangeAsymmetricalKeys_Click);
            // 
            // groupBoxEmailService
            // 
            this.groupBoxEmailService.Controls.Add(this.buttonEncryptSendMessageRecipient);
            this.groupBoxEmailService.Controls.Add(this.buttonSendSymmetricElementsRecipient);
            this.groupBoxEmailService.Controls.Add(this.textBoxMessageToSend);
            this.groupBoxEmailService.Controls.Add(this.labelMessage);
            this.groupBoxEmailService.Location = new System.Drawing.Point(24, 740);
            this.groupBoxEmailService.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxEmailService.Name = "groupBoxEmailService";
            this.groupBoxEmailService.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxEmailService.Size = new System.Drawing.Size(627, 244);
            this.groupBoxEmailService.TabIndex = 3;
            this.groupBoxEmailService.TabStop = false;
            this.groupBoxEmailService.Text = "Email Service";
            // 
            // buttonEncryptSendMessageRecipient
            // 
            this.buttonEncryptSendMessageRecipient.Location = new System.Drawing.Point(35, 181);
            this.buttonEncryptSendMessageRecipient.Margin = new System.Windows.Forms.Padding(6);
            this.buttonEncryptSendMessageRecipient.Name = "buttonEncryptSendMessageRecipient";
            this.buttonEncryptSendMessageRecipient.Size = new System.Drawing.Size(543, 42);
            this.buttonEncryptSendMessageRecipient.TabIndex = 3;
            this.buttonEncryptSendMessageRecipient.Text = "6) Encrypt and Send Message for Recipient";
            this.buttonEncryptSendMessageRecipient.UseVisualStyleBackColor = true;
            this.buttonEncryptSendMessageRecipient.Click += new System.EventHandler(this.buttonEncryptSendMessageRecipient_Click);
            // 
            // buttonSendSymmetricElementsRecipient
            // 
            this.buttonSendSymmetricElementsRecipient.Location = new System.Drawing.Point(35, 127);
            this.buttonSendSymmetricElementsRecipient.Margin = new System.Windows.Forms.Padding(6);
            this.buttonSendSymmetricElementsRecipient.Name = "buttonSendSymmetricElementsRecipient";
            this.buttonSendSymmetricElementsRecipient.Size = new System.Drawing.Size(543, 42);
            this.buttonSendSymmetricElementsRecipient.TabIndex = 2;
            this.buttonSendSymmetricElementsRecipient.Text = "5) Send Symmetric Elements for Recipient";
            this.buttonSendSymmetricElementsRecipient.UseVisualStyleBackColor = true;
            this.buttonSendSymmetricElementsRecipient.Click += new System.EventHandler(this.buttonSendSymmetricElementsRecipient_Click);
            // 
            // textBoxMessageToSend
            // 
            this.textBoxMessageToSend.ForeColor = System.Drawing.Color.Red;
            this.textBoxMessageToSend.Location = new System.Drawing.Point(203, 48);
            this.textBoxMessageToSend.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxMessageToSend.Multiline = true;
            this.textBoxMessageToSend.Name = "textBoxMessageToSend";
            this.textBoxMessageToSend.Size = new System.Drawing.Size(371, 67);
            this.textBoxMessageToSend.TabIndex = 1;
            this.textBoxMessageToSend.Text = "C# Cryptography is easy!!";
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Location = new System.Drawing.Point(29, 52);
            this.labelMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(162, 25);
            this.labelMessage.TabIndex = 0;
            this.labelMessage.Text = "Message to send";
            // 
            // groupBoxCryptographicSetupRecipient
            // 
            this.groupBoxCryptographicSetupRecipient.Controls.Add(this.buttonGenerateSymmetricalElementsRecipient);
            this.groupBoxCryptographicSetupRecipient.Controls.Add(this.buttonGenerateRecipientAsymmetricalKeys);
            this.groupBoxCryptographicSetupRecipient.ForeColor = System.Drawing.Color.Green;
            this.groupBoxCryptographicSetupRecipient.Location = new System.Drawing.Point(24, 190);
            this.groupBoxCryptographicSetupRecipient.Margin = new System.Windows.Forms.Padding(6);
            this.groupBoxCryptographicSetupRecipient.Name = "groupBoxCryptographicSetupRecipient";
            this.groupBoxCryptographicSetupRecipient.Padding = new System.Windows.Forms.Padding(6);
            this.groupBoxCryptographicSetupRecipient.Size = new System.Drawing.Size(627, 148);
            this.groupBoxCryptographicSetupRecipient.TabIndex = 4;
            this.groupBoxCryptographicSetupRecipient.TabStop = false;
            this.groupBoxCryptographicSetupRecipient.Text = "Cryptography Setup - Email Recipient";
            // 
            // buttonGenerateSymmetricalElementsRecipient
            // 
            this.buttonGenerateSymmetricalElementsRecipient.Location = new System.Drawing.Point(35, 94);
            this.buttonGenerateSymmetricalElementsRecipient.Margin = new System.Windows.Forms.Padding(6);
            this.buttonGenerateSymmetricalElementsRecipient.Name = "buttonGenerateSymmetricalElementsRecipient";
            this.buttonGenerateSymmetricalElementsRecipient.Size = new System.Drawing.Size(543, 42);
            this.buttonGenerateSymmetricalElementsRecipient.TabIndex = 2;
            this.buttonGenerateSymmetricalElementsRecipient.Text = "2) Generate Email Symmetric Elements";
            this.buttonGenerateSymmetricalElementsRecipient.UseVisualStyleBackColor = true;
            this.buttonGenerateSymmetricalElementsRecipient.Click += new System.EventHandler(this.buttonGenerateSymmetricalElementsRecipient_Click);
            // 
            // FormEISIPracticalExam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 1025);
            this.Controls.Add(this.groupBoxCryptographicSetupRecipient);
            this.Controls.Add(this.groupBoxEmailService);
            this.Controls.Add(this.groupBoxSecureChannel);
            this.Controls.Add(this.groupBoxCryptographicSetupServer);
            this.Controls.Add(this.groupBoxNetworkConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "FormEISIPracticalExam";
            this.Text = " EI/SI Practical Exam";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxNetworkConfig.ResumeLayout(false);
            this.groupBoxNetworkConfig.PerformLayout();
            this.groupBoxCryptographicSetupServer.ResumeLayout(false);
            this.groupBoxSecureChannel.ResumeLayout(false);
            this.groupBoxEmailService.ResumeLayout(false);
            this.groupBoxEmailService.PerformLayout();
            this.groupBoxCryptographicSetupRecipient.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxNetworkConfig;
        private System.Windows.Forms.Label labelServerPort;
        private System.Windows.Forms.TextBox textBoxServerIPAddress;
        private System.Windows.Forms.Label labelServerIPAddress;
        private System.Windows.Forms.GroupBox groupBoxCryptographicSetupServer;
        private System.Windows.Forms.Button buttonGenerateRecipientAsymmetricalKeys;
        private System.Windows.Forms.Button buttonGenerateClientAssymetricalKeys;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxServerPort;
        private System.Windows.Forms.GroupBox groupBoxSecureChannel;
        private System.Windows.Forms.Button buttonExchangeAsymmetricalKeys;
        private System.Windows.Forms.GroupBox groupBoxEmailService;
        private System.Windows.Forms.GroupBox groupBoxCryptographicSetupRecipient;
        private System.Windows.Forms.Button buttonEncryptSendMessageRecipient;
        private System.Windows.Forms.Button buttonSendSymmetricElementsRecipient;
        private System.Windows.Forms.TextBox textBoxMessageToSend;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Button buttonGenerateSymmetricalElementsRecipient;
    }
}

