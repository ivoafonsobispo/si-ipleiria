using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using WindowsClient.AuthServiceReference;

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

            using (AuthServiceClient client = new AuthServiceClient())
            {
                string login = txtLogin.Text;
                string pasword = txtPassword.Text;

                UsersMessage message = client.GetUsers(login, pasword);

                if(!message.Metadata.Status)
                {
                    MessageBox.Show(message.Metadata.Message);
                    return;
                }

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

            // todo: linha selecionada na listbox....
            string loginUser = ((string)lboxUsers.SelectedValue);
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            using ( AuthServiceClient client = new AuthServiceClient())
            {
                DescriptionMessage message =  client.GetUserDescription(login, password, loginUser);
                if (!message.Metadata.Status)
                {
                    MessageBox.Show(message.Metadata.Message);
                    return;
                }
                txtDescription.Text = message.Description;
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

                using ( AuthServiceClient client = new AuthServiceClient())
                {
                    string login = txtLogin.Text;
                    string password = txtPassword.Text;
                    string description = txtMyDescription.Text;
                    ServiceMetadata result = client.UpdateUserDescription(login, password, description);
                    MessageBox.Show(result.Message);
                   
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

        private void buttonGetUsersByCertificate_Click(object sender, EventArgs e)
        {
            using (AuthServiceClient client = new AuthServiceClient())
            using (X509Certificate2 certificate = new X509Certificate2("si.cert.d.pfx","ei.si"))
            {
                string login = txtLogin.Text;
                string pasword = txtPassword.Text;

                ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes("Auth Request"));

                CmsSigner cmsSigner = new CmsSigner(certificate);

                SignedCms signedCms = new SignedCms(contentInfo);

                signedCms.ComputeSignature(cmsSigner);

                byte[] pkcs7 = signedCms.Encode();

                string pkcs7Base64 = Convert.ToBase64String(pkcs7);


                UsersMessage message = client.GetUsersByCertificate(pkcs7Base64);

                if (!message.Metadata.Status)
                {
                    MessageBox.Show(message.Metadata.Message);
                    return;
                }

                lboxUsers.DataSource = message.Users;
                lboxUsers.DisplayMember = "Name";
                lboxUsers.ValueMember = "Login";

            }
        }

        private void buttonSetMyDescriptionByCertificate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                // lembrar de usar o "using"

                using (AuthServiceClient client = new AuthServiceClient())
                using (X509Certificate2 certificate = new X509Certificate2("estg.ei.si.a.pfx","ei.si"))
                {
                    string description = txtMyDescription.Text;

                    ContentInfo contentInfo = new ContentInfo(Encoding.UTF8.GetBytes(description));

                    CmsSigner cmsSigner = new CmsSigner(certificate);

                    SignedCms signedCms = new SignedCms(contentInfo);

                    signedCms.ComputeSignature(cmsSigner);

                    string pkcs7Base64 = Convert.ToBase64String(signedCms.Encode());


                    ServiceMetadata result = client.UpdateUserDescriptionByCertificate(pkcs7Base64);
                    MessageBox.Show(result.Message);

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

        private void buttonChangeSelectedUserPassword_Click(object sender, EventArgs e)
        {
            string login = (string) lboxUsers.SelectedValue;
            string password = (string) textBoxNewPassword.Text;

            using (X509Certificate2 clientCertificate = new X509Certificate2("si.cert.d.pfx", "ei.si"))
            using (X509Certificate2 serverCertificate = new X509Certificate2("si.cert.c.cer"))
            using (AuthServiceClient client = new AuthServiceClient())
            {
                #region Encrypet With Service Certificate
                // Vou buscar os dados
                ContentInfo encryptedContentInfo = new ContentInfo(Encoding.UTF8.GetBytes(password));

                CmsRecipient cmsRecipient = new CmsRecipient(serverCertificate);
                EnvelopedCms envelopedCms = new EnvelopedCms(encryptedContentInfo);

                envelopedCms.Encrypt(cmsRecipient);


                byte[] pksc7EncryptedData = envelopedCms.Encode();
                #endregion

                #region Digital Signature for Autentication
                ContentInfo signatureContentInfo = new ContentInfo(pksc7EncryptedData);
                CmsSigner cmsSigner = new CmsSigner(clientCertificate);
                SignedCms signedCms = new SignedCms(signatureContentInfo);
                signedCms.ComputeSignature(cmsSigner);
                byte[] signedPKCS7 = signedCms.Encode();
                #endregion

                string pkcs7Base = Convert.ToBase64String(signedPKCS7);


                client.UpdateUserPasswordByCertificate(pkcs7Base,login);

            }

           
        }
    }
}
