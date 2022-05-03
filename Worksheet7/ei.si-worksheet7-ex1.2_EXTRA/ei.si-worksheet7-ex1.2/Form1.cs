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
            // Usamos os certificamos PFX, pois estamos a fazer uma assinatura, ou seja, usamos o que tem a componente privada
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX,pwdfileCertPFX))
            {
                ShowCertificate(certificate);

                // Guardamos os dados em binario
                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);
                // Guardamos os conteudos dos dados
                ContentInfo contentInfo = new ContentInfo(data);

                // Instanciamos um assinante
                CmsSigner signer = new CmsSigner(certificate);

                // Instanciamos um ficheiro assinado, com os conteudos
                SignedCms signedCms = new SignedCms(contentInfo, false); // false -> with data (default)

                // Comutacionamos o ficheiro para o assinante
                signedCms.ComputeSignature(signer);

                // Criamos a assinatura, que vai conter os seguintes dados abaixo
                byte[] pkcs7Signature = signedCms.Encode();
                // Signature priCert(hash(data))
                // Certificate (multiple)
                // Data
                // Metadata

                this.tempDigitalSignature = pkcs7Signature;
                MessageBox.Show("Signed!");
            }
        }

        private void ButtonVerifySignatureWithData_Click(object sender, EventArgs e)
        {
            // Instanciamos um cms assinado
            SignedCms signedCms = new SignedCms();
            
            // Vai fazer o decode dos dados
            signedCms.Decode(this.tempDigitalSignature);

            try
            {
                // Vai fazer a verificacao da assinatura 
                signedCms.CheckSignature(false); // false signature + cert | true signature
                MessageBox.Show("Valid");
            } catch (Exception ex) { 
                MessageBox.Show("Invalid");
                MessageBox.Show(ex.Message);

            }

        }



        private void ButtonSignatureWithoutData_Click(object sender, EventArgs e)
        {
            using (X509Store store = new X509Store(StoreName.My,StoreLocation.CurrentUser))
            {
                // Abre a store le o ficheiro
                store.Open(OpenFlags.ReadOnly);
                // Apresenta o UI com os certificados
                var certs = X509Certificate2UI.SelectFromCollection(store.Certificates, "Cert", "cert", X509SelectionFlag.SingleSelection);
                // Apresenta os certificados
                ShowCertificate(certs[0]);

                // Guarda os dados em binario da textboxinfo
                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);
                // Guarda os conteudos
                ContentInfo contentInfo = new ContentInfo(data);
                // Cria um novo signer com o certificado
                CmsSigner cmsSigner = new CmsSigner(certs[0]);

                // Cms ja assinado
                SignedCms signedCms = new SignedCms(contentInfo, true);

                // Realiza a assinatura
                signedCms.ComputeSignature(cmsSigner);

                // Guarda os valores na variavel global
                this.tempDigitalSignature = signedCms.Encode();
                // Signature priCert(hash(data))
                // Certificate (multiple)
                // Metadata

                MessageBox.Show("DONE");
            }
        }

        private void ButtonVerifySignatureWithoutData_Click(object sender, EventArgs e)
        {
            // Guarda os dados em binario da textboxinfo
            byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);
            // Guarda os conteudos
            ContentInfo contentInfo = new ContentInfo(data);

            // Vai buscar os dados 
            SignedCms signedCms = new SignedCms(contentInfo, true);

            // Vai fazer o decode dos dados
            signedCms.Decode(this.tempDigitalSignature);

            try
            {
                // Vai fazer a verificacao da assinatura 
                signedCms.CheckSignature(false); // false signature + cert | true signature
                MessageBox.Show("Valid");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid");
                MessageBox.Show(ex.Message);

            }
        }



        private void ButtonEncryptWithCER_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                ShowCertificate(certificate);

                // Vou buscar os dados
                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

                ContentInfo contentInfo = new ContentInfo(data);

                CmsRecipient recipient = new CmsRecipient(certificate);
                EnvelopedCms enveloped = new EnvelopedCms(contentInfo);

                enveloped.Encrypt(recipient); // multiple

                byte[] pksc7EncryptedData = enveloped.Encode();
                // pubCertificate1(SecretKey)
                // pubCertificate2(SecretKey)
                // EncryptedData SecretKey(data)
                // Certificate 1
                // Certificate 2
                // MetaData

                this.tempEnvelope = pksc7EncryptedData;

                MessageBox.Show("DONE");
            }
        }

        private void ButtonDecryptFromPFX_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX)) { 

                EnvelopedCms enveloped = new EnvelopedCms();
                enveloped.Decode(this.tempEnvelope);
                enveloped.Decrypt( new X509Certificate2Collection(certificate)); // Personal Certificate Store

                MessageBox.Show(Encoding.UTF8.GetString(enveloped.ContentInfo.Content));
            }
        }



        private void ButtonEncryptWithStore_Click(object sender, EventArgs e)
        {
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                // Abre a store le o ficheiro
                store.Open(OpenFlags.ReadOnly);
                // Apresenta o UI com os certificados
                var certs = X509Certificate2UI.SelectFromCollection(store.Certificates, "Cert", "cert", X509SelectionFlag.SingleSelection);
                // Apresenta os certificados
                ShowCertificate(certs[0]);

                // Guarda os dados em binario da textboxinfo
                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);
                // Guarda os conteudos
                ContentInfo contentInfo = new ContentInfo(data);

                CmsRecipient recipient = new CmsRecipient(certs[0]);
                EnvelopedCms enveloped = new EnvelopedCms(contentInfo);

                enveloped.Encrypt(recipient);

                this.tempEnvelope = enveloped.Encode();

                MessageBox.Show("DONE");
            }
        }

        private void ButtonDecryptFromStore_Click(object sender, EventArgs e)
        {
            EnvelopedCms enveloped = new EnvelopedCms();
            enveloped.Decode(this.tempEnvelope);
            enveloped.Decrypt(); // Personal Certificate Store

            MessageBox.Show(Encoding.UTF8.GetString(enveloped.ContentInfo.Content));
        }



        private void ButtonEncryptAndSign_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX))
            {
                ShowCertificate(certificate);

                byte[] data = Encoding.UTF8.GetBytes(textBoxInfo.Text);

                ContentInfo contentInfo = new ContentInfo(data);

                CmsRecipient recipient = new CmsRecipient(certificate);
                EnvelopedCms enveloped = new EnvelopedCms(contentInfo);

                enveloped.Encrypt(recipient); // multiple

                byte[] pksc7EncryptedData = enveloped.Encode();

                this.tempEnvelope = pksc7EncryptedData;

                MessageBox.Show("Data has been encrypted");

                CmsSigner signer = new CmsSigner(certificate);

                contentInfo = new ContentInfo(pksc7EncryptedData);

                SignedCms signedCms = new SignedCms(contentInfo, false); // false -> with data (default)

                signedCms.ComputeSignature(signer);

                byte[] pkcs7Signature = signedCms.Encode();

                this.tempDigitalSignature = pkcs7Signature;
                MessageBox.Show("Signed!");

            }
        }

        private void ButtonVerifyAndDecrypt_Click(object sender, EventArgs e)
        {
            SignedCms signedCms = new SignedCms();

            signedCms.Decode(this.tempDigitalSignature);

            try
            {
                signedCms.CheckSignature(false); // false signature + cert | true signature
                MessageBox.Show("Valid");

                using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX))
                {

                    EnvelopedCms enveloped = new EnvelopedCms();
                    enveloped.Decode(this.tempEnvelope);
                    enveloped.Decrypt(new X509Certificate2Collection(certificate)); // Personal Certificate Store

                    MessageBox.Show(Encoding.UTF8.GetString(enveloped.ContentInfo.Content));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid");
                MessageBox.Show(ex.Message);

            }
        }

        private void textBoxInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
