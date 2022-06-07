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
            this.buttonRquestBalance = new System.Windows.Forms.Button();
            this.textBoxAccountBalance = new System.Windows.Forms.TextBox();
            this.labelBalance = new System.Windows.Forms.Label();
            this.textBoxAccountID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRquestBalance
            // 
            this.buttonRquestBalance.Location = new System.Drawing.Point(176, 49);
            this.buttonRquestBalance.Name = "buttonRquestBalance";
            this.buttonRquestBalance.Size = new System.Drawing.Size(140, 23);
            this.buttonRquestBalance.TabIndex = 1;
            this.buttonRquestBalance.Text = "Request Balance";
            this.buttonRquestBalance.UseVisualStyleBackColor = true;
            this.buttonRquestBalance.Click += new System.EventHandler(this.buttonRquestBalance_Click);
            // 
            // textBoxAccountBalance
            // 
            this.textBoxAccountBalance.Enabled = false;
            this.textBoxAccountBalance.Location = new System.Drawing.Point(113, 102);
            this.textBoxAccountBalance.Name = "textBoxAccountBalance";
            this.textBoxAccountBalance.Size = new System.Drawing.Size(203, 20);
            this.textBoxAccountBalance.TabIndex = 2;
            // 
            // labelBalance
            // 
            this.labelBalance.AutoSize = true;
            this.labelBalance.Location = new System.Drawing.Point(6, 109);
            this.labelBalance.Name = "labelBalance";
            this.labelBalance.Size = new System.Drawing.Size(89, 13);
            this.labelBalance.TabIndex = 3;
            this.labelBalance.Text = "Account Balance";
            // 
            // textBoxAccountID
            // 
            this.textBoxAccountID.Location = new System.Drawing.Point(6, 52);
            this.textBoxAccountID.Name = "textBoxAccountID";
            this.textBoxAccountID.Size = new System.Drawing.Size(139, 20);
            this.textBoxAccountID.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Account ID";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxAccountBalance);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonRquestBalance);
            this.groupBox1.Controls.Add(this.labelBalance);
            this.groupBox1.Controls.Add(this.textBoxAccountID);
            this.groupBox1.Location = new System.Drawing.Point(21, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 158);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bank Service Account Balance Request";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 200);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "WindowsClient";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonRquestBalance;
        private System.Windows.Forms.TextBox textBoxAccountBalance;
        private System.Windows.Forms.Label labelBalance;
        private System.Windows.Forms.TextBox textBoxAccountID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

