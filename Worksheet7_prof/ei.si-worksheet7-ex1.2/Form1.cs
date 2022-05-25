using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;    // Mandatory to add the reference to System.Security DLL
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ei.si_worksheet7
{
    public partial class Form1 : Form
    {
        // The digital certificates should be copied to ./bin/debug directory
        const string fileCertPFX = @"estg.ei.si.a.pfx";
        const string fileCertCER = @"estg.ei.si.a.cer";
        const string fileCertPFX_B = @"estg.ei.si.b.pfx";
        const string fileCertCER_B = @"estg.ei.si.b.cer";
        static readonly string pwdfileCertPFX = Properties.Settings.Default.PwdCertPFX;

        private byte[] tempDigitalSignature = null;
        private byte[] tempEnvelope = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Show digital certificate info in the textbox
        /// </summary>
        /// <param name="cert">digital certificate</param>
        private void ShowCertificate(X509Certificate2 cert)
        {
            textBoxInfo.Text += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + Environment.NewLine;
            textBoxInfo.Text += "Subject: " + cert.Subject + Environment.NewLine;
            textBoxInfo.Text += "FriendlyName: " + cert.FriendlyName + Environment.NewLine;
            textBoxInfo.Text += "Thumbprint: " + cert.Thumbprint + Environment.NewLine;
            textBoxInfo.Text += "SerialNumber: " + cert.SerialNumber + Environment.NewLine;
            textBoxInfo.Text += "HasPrivateKey: " + cert.HasPrivateKey + Environment.NewLine;
            textBoxInfo.Text += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + Environment.NewLine;
            textBoxInfo.Text += Environment.NewLine;
        }




        private void ButtonSignatureWithData_Click(object sender, EventArgs e)
        {
            using(X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX))
            {
                ShowCertificate(certificate);

                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

                ContentInfo contentInfo = new ContentInfo(data);

                CmsSigner cmsSigner = new CmsSigner(certificate);

                SignedCms signedCms = new SignedCms(contentInfo, false); // false -> with data (default)

                signedCms.ComputeSignature(cmsSigner);

                byte[] pkcs7Signature = signedCms.Encode();
                // Signature priCert(hash(data))
                // Certificate (multiple)
                // Data
                // Metadata

                this.tempDigitalSignature = pkcs7Signature;
                MessageBox.Show("Signed");
            }
        }

        private void ButtonVerifySignatureWithData_Click(object sender, EventArgs e)
        {
            SignedCms signedCms = new SignedCms();

            signedCms.Decode(this.tempDigitalSignature);

            try
            {
                signedCms.CheckSignature(false);  // false signature + cert | true signature
                MessageBox.Show("VALID");
            }
            catch (Exception ex)
            {
                MessageBox.Show("INVALID");
                MessageBox.Show(ex.Message);
            }

        }



        private void ButtonSignatureWithoutData_Click(object sender, EventArgs e)
        {
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                var certs = X509Certificate2UI.SelectFromCollection(store.Certificates,
                                                                    "Cert",
                                                                    "cert",
                                                                    X509SelectionFlag.SingleSelection);
                ShowCertificate(certs[0]);

                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

                ContentInfo contentInfo = new ContentInfo(data);
                CmsSigner cmsSigner = new CmsSigner(certs[0]);

                SignedCms signedCms = new SignedCms(contentInfo, true); // true -> without data

                signedCms.ComputeSignature(cmsSigner);

                this.tempDigitalSignature = signedCms.Encode();
                // Signature priCert(hash(data))
                // Certificate (multiple)
                // Metadata

                MessageBox.Show("Done");


            }
        }

        private void ButtonVerifySignatureWithoutData_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

            ContentInfo contentInfo = new ContentInfo(data);

            SignedCms signedCms = new SignedCms(contentInfo, true);
            // Data

            signedCms.Decode(this.tempDigitalSignature);
            // Signature priCert(hash(data))
            // Certificate (multiple)
            // Data
            // Metadata
            try
            {
                signedCms.CheckSignature(false);  // false signature + cert | true signature
                MessageBox.Show("VALID");
            }
            catch (Exception ex)
            {
                MessageBox.Show("INVALID");
                MessageBox.Show(ex.Message);
            }
        }



        private void ButtonEncryptWithCER_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                ShowCertificate(certificate);

                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

                ContentInfo contentInfo = new ContentInfo(data);

                CmsRecipient cmsRecipient = new CmsRecipient(certificate);

                EnvelopedCms envelopedCms = new EnvelopedCms(contentInfo);

                envelopedCms.Encrypt(cmsRecipient); // multiple

                byte[] pkcs7EncryptedData = envelopedCms.Encode();
                // pubCertificate1(SecretKey)
                // pubCertificate2(SecretKey)
                // EncryptedData SecretKey(data)
                // Certificate 1
                // Certificate 2
                // Metadata

                this.tempEnvelope = pkcs7EncryptedData;
                MessageBox.Show("DONE");
            }
        }

        private void ButtonDecryptFromPFX_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX))
            {

                EnvelopedCms envelopedCms = new EnvelopedCms();

                envelopedCms.Decode(this.tempEnvelope);

                envelopedCms.Decrypt( new X509Certificate2Collection(certificate)); // Personal Certificate Store

                MessageBox.Show(Encoding.UTF8.GetString(envelopedCms.ContentInfo.Content));
            }

        }



        private void ButtonEncryptWithStore_Click(object sender, EventArgs e)
        {
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                var certs = X509Certificate2UI.SelectFromCollection(store.Certificates,
                                                                    "Cert",
                                                                    "cert",
                                                                    X509SelectionFlag.SingleSelection);
                ShowCertificate(certs[0]);

                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

                ContentInfo contentInfo = new ContentInfo(data);

                CmsRecipient cmsRecipient = new CmsRecipient(certs[0]);

                EnvelopedCms envelopedCms = new EnvelopedCms(contentInfo);

                envelopedCms.Encrypt(cmsRecipient);

                this.tempEnvelope = envelopedCms.Encode();

                MessageBox.Show("Done");


            }
        }

        private void ButtonDecryptFromStore_Click(object sender, EventArgs e)
        {
            EnvelopedCms envelopedCms = new EnvelopedCms();

            envelopedCms.Decode(this.tempEnvelope);

            envelopedCms.Decrypt(); // Personal Certificate Store

            MessageBox.Show(Encoding.UTF8.GetString(envelopedCms.ContentInfo.Content));
        }



        private void ButtonEncryptAndSign_Click(object sender, EventArgs e)
        {
            using(X509Certificate2 certificateA = new X509Certificate2(fileCertPFX,pwdfileCertPFX))
            using(X509Certificate2 certificateB = new X509Certificate2(fileCertCER_B))
            {
                ShowCertificate(certificateA);

                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

                ContentInfo contentInfoCypher = new ContentInfo(data);

                CmsRecipient cmsRecipient = new CmsRecipient(certificateB);

                EnvelopedCms envelopedCms = new EnvelopedCms(contentInfoCypher);

                envelopedCms.Encrypt(cmsRecipient);

                ContentInfo contentInfoSignature = new ContentInfo(envelopedCms.Encode());

                CmsSigner cmsSigner = new CmsSigner(certificateA);

                SignedCms signedCms = new SignedCms(contentInfoSignature, false); // false with data

                signedCms.ComputeSignature(cmsSigner);

                this.tempDigitalSignature = signedCms.Encode();

                MessageBox.Show("DONE");



            }

        }

        private void ButtonVerifyAndDecrypt_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificateB = new X509Certificate2(fileCertPFX_B, pwdfileCertPFX))
            {
                SignedCms signedCms = new SignedCms();

                signedCms.Decode(this.tempDigitalSignature);

                try
                {
                    signedCms.CheckSignature(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                // Valid Signature

                EnvelopedCms envelopedCms = new EnvelopedCms();

                envelopedCms.Decode(signedCms.ContentInfo.Content);

                envelopedCms.Decrypt(new X509Certificate2Collection(certificateB));

                MessageBox.Show(
                       Encoding.UTF8.GetString(
                           envelopedCms.ContentInfo.Content));

            }
        }
    }
}
