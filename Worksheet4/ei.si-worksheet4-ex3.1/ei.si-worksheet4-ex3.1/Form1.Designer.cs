namespace ei_si_worksheet4
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonGenerateKeys = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPublicKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbBothKeys = new System.Windows.Forms.TextBox();
            this.buttonSavePublicKey = new System.Windows.Forms.Button();
            this.buttonSaveKeys = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonDecryptFile = new System.Windows.Forms.Button();
            this.buttonEncryptFile = new System.Windows.Forms.Button();
            this.textBoxSymmetricKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbBitSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSymmetricKeyEncrypted = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGenerateSymmetricKey = new System.Windows.Forms.Button();
            this.buttonImportKeys = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonGenerateKeys
            // 
            this.buttonGenerateKeys.Location = new System.Drawing.Point(26, 25);
            this.buttonGenerateKeys.Margin = new System.Windows.Forms.Padding(6);
            this.buttonGenerateKeys.Name = "buttonGenerateKeys";
            this.buttonGenerateKeys.Size = new System.Drawing.Size(398, 44);
            this.buttonGenerateKeys.TabIndex = 0;
            this.buttonGenerateKeys.Text = "Generate Keys (Private / Public)";
            this.buttonGenerateKeys.UseVisualStyleBackColor = true;
            this.buttonGenerateKeys.Click += new System.EventHandler(this.ButtonGenerateKeys_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 119);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Public Key";
            // 
            // tbPublicKey
            // 
            this.tbPublicKey.Location = new System.Drawing.Point(26, 150);
            this.tbPublicKey.Margin = new System.Windows.Forms.Padding(6);
            this.tbPublicKey.Multiline = true;
            this.tbPublicKey.Name = "tbPublicKey";
            this.tbPublicKey.Size = new System.Drawing.Size(980, 98);
            this.tbPublicKey.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 273);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Private / Public Key";
            // 
            // tbBothKeys
            // 
            this.tbBothKeys.Location = new System.Drawing.Point(26, 319);
            this.tbBothKeys.Margin = new System.Windows.Forms.Padding(6);
            this.tbBothKeys.Multiline = true;
            this.tbBothKeys.Name = "tbBothKeys";
            this.tbBothKeys.Size = new System.Drawing.Size(980, 187);
            this.tbBothKeys.TabIndex = 4;
            // 
            // buttonSavePublicKey
            // 
            this.buttonSavePublicKey.Location = new System.Drawing.Point(728, 94);
            this.buttonSavePublicKey.Margin = new System.Windows.Forms.Padding(6);
            this.buttonSavePublicKey.Name = "buttonSavePublicKey";
            this.buttonSavePublicKey.Size = new System.Drawing.Size(278, 44);
            this.buttonSavePublicKey.TabIndex = 5;
            this.buttonSavePublicKey.Text = "Save PublicKey.txt";
            this.buttonSavePublicKey.UseVisualStyleBackColor = true;
            this.buttonSavePublicKey.Click += new System.EventHandler(this.ButtonSavePublicKey_Click);
            // 
            // buttonSaveKeys
            // 
            this.buttonSaveKeys.Location = new System.Drawing.Point(730, 263);
            this.buttonSaveKeys.Margin = new System.Windows.Forms.Padding(6);
            this.buttonSaveKeys.Name = "buttonSaveKeys";
            this.buttonSaveKeys.Size = new System.Drawing.Size(276, 44);
            this.buttonSaveKeys.TabIndex = 6;
            this.buttonSaveKeys.Text = "Save PrivatePublicKey.txt";
            this.buttonSaveKeys.UseVisualStyleBackColor = true;
            this.buttonSaveKeys.Click += new System.EventHandler(this.ButtonSaveKeys_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonDecryptFile);
            this.panel1.Controls.Add(this.buttonEncryptFile);
            this.panel1.Controls.Add(this.textBoxSymmetricKey);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbBitSize);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbSymmetricKeyEncrypted);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.buttonGenerateSymmetricKey);
            this.panel1.Location = new System.Drawing.Point(24, 521);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 327);
            this.panel1.TabIndex = 7;
            // 
            // buttonDecryptFile
            // 
            this.buttonDecryptFile.Location = new System.Drawing.Point(532, 252);
            this.buttonDecryptFile.Margin = new System.Windows.Forms.Padding(6);
            this.buttonDecryptFile.Name = "buttonDecryptFile";
            this.buttonDecryptFile.Size = new System.Drawing.Size(440, 44);
            this.buttonDecryptFile.TabIndex = 14;
            this.buttonDecryptFile.Text = "Choose File to Decrypt and Save File";
            this.buttonDecryptFile.UseVisualStyleBackColor = true;
            this.buttonDecryptFile.Click += new System.EventHandler(this.buttonDecryptFile_Click);
            // 
            // buttonEncryptFile
            // 
            this.buttonEncryptFile.Location = new System.Drawing.Point(18, 252);
            this.buttonEncryptFile.Margin = new System.Windows.Forms.Padding(6);
            this.buttonEncryptFile.Name = "buttonEncryptFile";
            this.buttonEncryptFile.Size = new System.Drawing.Size(412, 44);
            this.buttonEncryptFile.TabIndex = 12;
            this.buttonEncryptFile.Text = "Choose File, Encrypt and Save all Resources";
            this.buttonEncryptFile.UseVisualStyleBackColor = true;
            this.buttonEncryptFile.Click += new System.EventHandler(this.ButtonEncryptFile_Click);
            // 
            // textBoxSymmetricKey
            // 
            this.textBoxSymmetricKey.Location = new System.Drawing.Point(514, 19);
            this.textBoxSymmetricKey.Margin = new System.Windows.Forms.Padding(6);
            this.textBoxSymmetricKey.Name = "textBoxSymmetricKey";
            this.textBoxSymmetricKey.Size = new System.Drawing.Size(452, 31);
            this.textBoxSymmetricKey.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Key";
            // 
            // tbBitSize
            // 
            this.tbBitSize.Location = new System.Drawing.Point(24, 177);
            this.tbBitSize.Margin = new System.Windows.Forms.Padding(6);
            this.tbBitSize.Name = "tbBitSize";
            this.tbBitSize.Size = new System.Drawing.Size(196, 31);
            this.tbBitSize.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 127);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Number of Bits";
            // 
            // tbSymmetricKeyEncrypted
            // 
            this.tbSymmetricKeyEncrypted.Location = new System.Drawing.Point(276, 75);
            this.tbSymmetricKeyEncrypted.Margin = new System.Windows.Forms.Padding(6);
            this.tbSymmetricKeyEncrypted.Multiline = true;
            this.tbSymmetricKeyEncrypted.Name = "tbSymmetricKeyEncrypted";
            this.tbSymmetricKeyEncrypted.Size = new System.Drawing.Size(692, 139);
            this.tbSymmetricKeyEncrypted.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 75);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(258, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Encrypted Symmetric Key";
            // 
            // buttonGenerateSymmetricKey
            // 
            this.buttonGenerateSymmetricKey.Location = new System.Drawing.Point(18, 15);
            this.buttonGenerateSymmetricKey.Margin = new System.Windows.Forms.Padding(6);
            this.buttonGenerateSymmetricKey.Name = "buttonGenerateSymmetricKey";
            this.buttonGenerateSymmetricKey.Size = new System.Drawing.Size(418, 44);
            this.buttonGenerateSymmetricKey.TabIndex = 2;
            this.buttonGenerateSymmetricKey.Text = "Generate and Encrypt Symmetric Key";
            this.buttonGenerateSymmetricKey.UseVisualStyleBackColor = true;
            this.buttonGenerateSymmetricKey.Click += new System.EventHandler(this.ButtonGenerateSymmetricKey_Click);
            // 
            // buttonImportKeys
            // 
            this.buttonImportKeys.Location = new System.Drawing.Point(438, 23);
            this.buttonImportKeys.Margin = new System.Windows.Forms.Padding(6);
            this.buttonImportKeys.Name = "buttonImportKeys";
            this.buttonImportKeys.Size = new System.Drawing.Size(572, 44);
            this.buttonImportKeys.TabIndex = 8;
            this.buttonImportKeys.Text = "Import Keys from File (Private / Public)";
            this.buttonImportKeys.UseVisualStyleBackColor = true;
            this.buttonImportKeys.Click += new System.EventHandler(this.ButtonImportKeys_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 883);
            this.Controls.Add(this.buttonImportKeys);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSaveKeys);
            this.Controls.Add(this.buttonSavePublicKey);
            this.Controls.Add(this.tbBothKeys);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPublicKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGenerateKeys);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Form1";
            this.Text = "Assymmetric Algorithms";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGenerateKeys;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPublicKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBothKeys;
        private System.Windows.Forms.Button buttonSavePublicKey;
        private System.Windows.Forms.Button buttonSaveKeys;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbBitSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSymmetricKeyEncrypted;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGenerateSymmetricKey;
        private System.Windows.Forms.Button buttonEncryptFile;
        private System.Windows.Forms.TextBox textBoxSymmetricKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonDecryptFile;
        private System.Windows.Forms.Button buttonImportKeys;
    }
}


