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
            textBoxInfo.Text += "CA: " + cert.Issuer + Environment.NewLine;
            textBoxInfo.Text += "Valid between: " + cert.NotBefore + " and "+ cert.NotAfter + Environment.NewLine;
            textBoxInfo.Text += "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" + Environment.NewLine;
            textBoxInfo.Text += Environment.NewLine;
        }



        private void ButtonOpenPFX_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX, pwdfileCertPFX))
            {
                ShowCertificate(certificate);
            }
        }

        private void ButtonOpenCER_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                ShowCertificate(certificate);
            }
        }



        private void ButtonOpenFromStore_Click(object sender, EventArgs e)
        {
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);

                var certs = X509Certificate2UI.SelectFromCollection(store.Certificates,
                                                        "Certificates",
                                                        "Choose a certificate",
                                                        X509SelectionFlag.SingleSelection);
                ShowCertificate(certs[0]);
            }
        }

        private void ButtonAddToStoreCER_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                ShowCertificate(certificate);
                store.Open(OpenFlags.ReadWrite);
                store.Add(certificate);

                MessageBox.Show("Certificated Added");
            }
        }



        private void ButtonVerifyCert_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                ShowCertificate(certificate);
                if (certificate.Verify())
                {
                    MessageBox.Show("VALID");
                } else
                {
                    MessageBox.Show("INVALID");
                }

            }
        }

        private void ButtonVerifyCertChain_Click(object sender, EventArgs e)
        {

        }



        private void ButtonExportPublicCert_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertCER))
            {
                ShowCertificate(certificate);

                File.WriteAllBytes("newcert.cer", certificate.Export(X509ContentType.Cert));

                MessageBox.Show("Exported");
            }
        }

        private void ButtonImportPublicCert_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2())
            {

                certificate.Import(File.ReadAllBytes("newcert.cer"));

                ShowCertificate(certificate);
       
            }
        }



        private void ButtonExportPrivateCert_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2(fileCertPFX,pwdfileCertPFX,X509KeyStorageFlags.Exportable))
            {
                ShowCertificate(certificate);

                File.WriteAllBytes("newcert.pfx", certificate.Export(X509ContentType.Pfx,"xpto"));

                MessageBox.Show("Exported");
            }
        }

        private void ButtonImportPrivateCert_Click(object sender, EventArgs e)
        {
            using (X509Certificate2 certificate = new X509Certificate2())
            {

                certificate.Import(File.ReadAllBytes("newcert.pfx"),
                                   "xpto", 
                                   X509KeyStorageFlags.Exportable);

                ShowCertificate(certificate);

            }
        }



    }
}
