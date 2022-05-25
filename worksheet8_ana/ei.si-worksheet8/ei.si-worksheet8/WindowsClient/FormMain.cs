using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using WindowsClient.authServiceReference;

namespace WindowsClient
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Obter a lista de utilizadores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetUsers_Click(object sender, EventArgs e)
        {
            //// versão 1
            //lboxUsers.DataSource = users;
            //lboxUsers.DisplayMember = "Name";
            //lboxUsers.ValueMember = "Login";


            //// versão 2
            //lboxUsers.Items.Clear();
            //foreach (User user in users)
            //{
            //  lboxUsers.Items.Add(user.Login);
            //}

            using (AuthServiceClient client = new AuthServiceClient()) {
                string login = txtLogin.Text;
                string password = txtPassword.Text;

                
                    UserMessage message = client.GetUsers(login, password);

                    if (!message.Metadata.Status)
                    {
                        MessageBox.Show(message.Metadata.Message);
                        return;
                    }

                    //versão 1
                    lboxUsers.DataSource = message.Users;
                    lboxUsers.DisplayMember = "Name";
                    lboxUsers.ValueMember = "Login";
                
            }
        }


        /// <summary>
        /// Obter a descrição de um utilizador 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGetDescription_Click(object sender, EventArgs e)
        {
            //// proteção para que não se execute esta funcionalidade sem que um utilizador esteja selecionado
            if (lboxUsers.SelectedIndex == -1)
            {
                MessageBox.Show("tem que escolher um utilizador!");
                return;
            }

            // todo: linha selecionada na listbox.... ((string)lboxUsers.SelectedValue)
            var selectedUser = ((User)lboxUsers.SelectedItem);

            using (AuthServiceClient client = new AuthServiceClient())
            {
                User user = client.GetUser(selectedUser.Id);
                txtDescription.Text = user.Description;
            }


        }

        /// <summary>
        /// Atualizar a descrição de um utilizador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSetDescription_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // lembrar de usar o "using"
                using (AuthServiceClient client = new AuthServiceClient())
               {
                    string description = txtMyDescription.Text;
                    string login = txtLogin.Text;
                    string password= txtPassword.Text;

                    ServiceMetadata messsage = client.UpdateUserDescription(login, password, description);
                    MessageBox.Show(messsage.Message);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnGetUsersByCertificate_Click(object sender, EventArgs e)
        {
            using (AuthServiceClient client = new AuthServiceClient())
            {
                string login = txtLogin.Text;
                string password = txtPassword.Text;

                using (X509Certificate2 certificate = new X509Certificate2("estg.ei.si.a.pfx", "ei.si"))
                {
                    ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes(login));

                    CmsSigner cmsSigner = new CmsSigner(certificate);

                    SignedCms signedCms = new SignedCms(contentInfo, false);

                    signedCms.ComputeSignature(cmsSigner);

                    byte[] pkcs7 = signedCms.Encode();

                    string pkcs7Base64 = Convert.ToBase64String(pkcs7);

                    UserMessage message = client.GetUsersByCertificate(pkcs7Base64);

                    if (!message.Metadata.Status)
                    {
                        MessageBox.Show(message.Metadata.Message);
                        return;
                    }

                    //versão 1
                    lboxUsers.DataSource = message.Users;
                    lboxUsers.DisplayMember = "Name";
                    lboxUsers.ValueMember = "Login";
                }
            }
        }

        private void setMyDescriptioByCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                var result = openFileDialogCertificate.ShowDialog();
                string fileName = "";
                if (result == DialogResult.OK) {
                    fileName = openFileDialogCertificate.FileName;
                }

                // lembrar de usar o "using"
                using (AuthServiceClient client = new AuthServiceClient())
                using (X509Certificate2 certificate = new X509Certificate2(fileName, "ei.si"))
                {
                    string description = txtMyDescription.Text;

                    ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes(description));

                    CmsSigner cmsSigner = new CmsSigner(certificate);

                    SignedCms signedCms = new SignedCms(contentInfo);
                    
                    signedCms.ComputeSignature(cmsSigner);

                    byte[] pcks7 = signedCms.Encode();

                    string pcks7Base64 = Convert.ToBase64String(pcks7);

                    ServiceMetadata messsage = client.UpdateUserDescriptionByCertificate(pcks7Base64);
                    MessageBox.Show(messsage.Message);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void openFileDialogCertificate_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void changeSelectedUserPassword_Click(object sender, EventArgs e)
        {
            byte[] newPassword = Encoding.UTF8.GetBytes(txtNewPassword.Text);

            //Get user 
            if (lboxUsers.SelectedIndex == -1)
            {
                MessageBox.Show("tem que escolher um utilizador!");
                return;
            }

            
            string fileName_a = AppDomain.CurrentDomain.BaseDirectory + @"estg.ei.si.a.pfx";
            string fileName_b = AppDomain.CurrentDomain.BaseDirectory + @"estg.ei.si.b.cer";
            //CurrentName -> project folder


            // todo: linha selecionada na listbox.... ((string)lboxUsers.SelectedValue)
            var selectedUser = ((User)lboxUsers.SelectedItem);

            using (AuthServiceClient client = new AuthServiceClient())
            using (X509Certificate2 certificate_a = new X509Certificate2(fileName_a, "ei.si"))
            using (X509Certificate2 certificate_b = new X509Certificate2(fileName_b))
            {
                // certificado a -> cliente
                // certificado b -> servidor

                // cifrar com b, assinar com a
                ContentInfo contentInfoEncryption = new ContentInfo(newPassword);

                // Cifrar com b (Server)
                CmsRecipient cmsRecipient = new CmsRecipient(certificate_b);

                // Assinar com a (cliente)
                CmsSigner cmsSigner = new CmsSigner(certificate_a);

                // Vai conter a password cifrada
                EnvelopedCms envelopedCms = new EnvelopedCms(contentInfoEncryption);
                envelopedCms.Encrypt(cmsRecipient);

                
                ContentInfo contentInfoSignature = new ContentInfo(envelopedCms.Encode());

                // Vai conter a assinatura da password cifrada
                SignedCms signedCms = new SignedCms(contentInfoSignature);
                signedCms.ComputeSignature(cmsSigner);


                byte[] pcks7 = signedCms.Encode();
                string pkcs7Base64 = Convert.ToBase64String(pcks7);

                ServiceMetadata messsage = client.UpdateUserPassword(selectedUser.Id, pkcs7Base64);
                MessageBox.Show(messsage.Message);
            }

        }
    }
}
