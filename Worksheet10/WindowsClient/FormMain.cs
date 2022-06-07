using System;
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

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void buttonRquestBalance_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                string accountIDString = textBoxAccountID.Text;

                using (X509Certificate2 clientCertificate = new X509Certificate2("si.cert.b.pfx", "ei.si"))
                using (BankServiceClient client = new BankServiceClient())
                {
                    #region Encrypte With Service Certificate
                    // Vou buscar os dados
                    byte[] pksc7Data = Encoding.UTF8.GetBytes(accountIDString);
                    #endregion

                    #region Digital Signature for Autentication
                    ContentInfo signatureContentInfo = new ContentInfo(pksc7Data);
                    CmsSigner cmsSigner = new CmsSigner(clientCertificate);
                    SignedCms signedCms = new SignedCms(signatureContentInfo);
                    signedCms.ComputeSignature(cmsSigner);
                    byte[] signedPKCS7 = signedCms.Encode();
                    #endregion

                    string pkcs7Base = Convert.ToBase64String(signedPKCS7);


                    client.GetBalance(pkcs7Base);

                    // Decrypt
                    MessageBox.Show(pkcs7Base);
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
