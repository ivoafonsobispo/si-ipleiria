using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ei.si_worksheet7
{
    public partial class Form1 : Form
    {
        // The digital certificates should be copied to ./bin/debug directory
        const string fileCertPFX = @"estg.ei.si.b.pfx";
        const string fileCertCER = @"estg.ei.si.b.cer";
        static readonly string pwdfileCertPFX = Properties.Settings.Default.PwdCertPFX;

        private byte[] tempCertRaw = null;
        private byte[] tempData = null;


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
            textBoxInfo.Text += "Has Private Key: " + cert.HasPrivateKey + Environment.NewLine;
            textBoxInfo.Text += "CA Issuer Name: " + cert.Issuer + Environment.NewLine;
            textBoxInfo.Text += "Valid Between: " + cert.NotBefore + " and " + cert.NotAfter + Environment.NewLine;

            textBoxInfo.Text += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + Environment.NewLine;
            textBoxInfo.Text += Environment.NewLine;
        }



        private void ButtonOpenPFX_Click(object sender, EventArgs e)
        {
            // Criamos as classes para os certificados, cer e pwd
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX))
            {
                // Mostra o certificado
                ShowCertificate(certificate);
            }
        }

        private void ButtonOpenCER_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                // Mostra o certificado
                ShowCertificate(certificate);
            }
        }



        private void ButtonOpenFromStore_Click(object sender, EventArgs e)
        {
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                // Abrimos apenas para leitura
                store.Open(OpenFlags.ReadOnly);
                // Criar uma var com o cert, para apresentar ao user os certificados deles
                var certs = X509Certificate2UI.SelectFromCollection(store.Certificates, "Certificates", "Choose a Certificate", X509SelectionFlag.SingleSelection);

                ShowCertificate(certs[0]);
            }
        }

        private void ButtonAddToStoreCER_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
                // permite adicionar ao mmc, na nossa pasta personal assim como no current user
                using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                // Mostra o certificado
                ShowCertificate(certificate);
                // Abre para Leitura e Escrita
                store.Open(OpenFlags.ReadWrite);
                // Envia o certifica para ser adicionar
                store.Add(certificate);
                MessageBox.Show("Certificated Added");
            }
        }



        private void ButtonVerifyCert_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                // Mostra o certificado
                ShowCertificate(certificate);
                // Valida o certificado
                if (certificate.Verify())
                {
                    MessageBox.Show("Valid");
                } else
                {
                    MessageBox.Show("Invalid");
                }
            }
        }
            
        private void ButtonVerifyCertChain_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX))
            {
                if (certificate.Verify())
                {
                    MessageBox.Show("Valid");
                }
                else
                {
                    MessageBox.Show("Invalid");
                }
            }
        }



        private void ButtonExportPublicCert_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                // Mostra o certificado
                ShowCertificate(certificate);

                File.WriteAllBytes("newcert.cer",certificate.Export(X509ContentType.Cert));

                MessageBox.Show("Created");
            }
        }

        private void ButtonImportPublicCert_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {

                // Mostra o certificado
                ShowCertificate(certificate);

                certificate.Import(File.ReadAllBytes("newcert.cer"));

                MessageBox.Show("Imported");
            }
        }



        private void ButtonExportPrivateCert_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX,pwdfileCertPFX, X509KeyStorageFlags.Exportable))
            {
                // Mostra o certificado
                ShowCertificate(certificate);

                File.WriteAllBytes("newcert.pfx", certificate.Export(X509ContentType.Pfx,pwdfileCertPFX));

                MessageBox.Show("Exported!");
            }
        }

        private void ButtonImportPrivateCert_Click(object sender, EventArgs e)
        {
            // Criamos a classe apenas para o cer
            using (X509Certificate2 certificate = new X509Certificate2())
            {
                certificate.Import(File.ReadAllBytes("newcert.pfx"), pwdfileCertPFX,X509KeyStorageFlags.Exportable);

                ShowCertificate(certificate);

            }
        }



    }
}
