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
using WindowsClient.BankServiceReference;

namespace WindowsClient
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void buttonRquestBalance_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                using(BankServiceClient client = new BankServiceClient())
                using(X509Certificate2 clientCertificate = new X509Certificate2("si.cert.c.pfx", "ei.si"))
                {
                    int accountID = Int32.Parse(textBoxAccountID.Text);

                    #region Signature
                    ContentInfo signatureContentInfo = new ContentInfo(BitConverter.GetBytes(accountID));

                    CmsSigner cmsSigner = new CmsSigner(clientCertificate);
                    SignedCms signedCms = new SignedCms(signatureContentInfo);

                    signedCms.ComputeSignature(cmsSigner);
                    byte[] pkcs7 = signedCms.Encode();
                    #endregion

                    string pkcs7Base64 = Convert.ToBase64String(pkcs7);
                    var response = client.GetBalance(pkcs7Base64);

                    if (response.StatusCode != StatusCode.OK)
                    {
                        MessageBox.Show(response.Message);
                        return;
                    }

                    string pkcs7Base64Server = response.PKCS7Base64Balance;

                    EnvelopedCms envelopedCms = new EnvelopedCms();
                    envelopedCms.Decode(Convert.FromBase64String(pkcs7Base64Server));
                    try
                    {
                        envelopedCms.Decrypt(new X509Certificate2Collection(clientCertificate));
                    } catch
                    {
                        MessageBox.Show("Decryption Failure");
                        return;
                    }

                    SignedCms serverSignedCms = new SignedCms();
                    serverSignedCms.Decode(envelopedCms.ContentInfo.Content);

                    try
                    {
                        serverSignedCms.CheckSignature(false);
                    } catch
                    {
                        MessageBox.Show("Signature Invalid");
                        return;
                    }

                    double balance = BitConverter.ToDouble(serverSignedCms.ContentInfo.Content, 0);

                    textBoxAccountBalance.Text = balance.ToString();
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
    }
}
