using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ei.si.worksheet6
{
    public partial class Form1 : Form
    {
        // guarda a chave publica
        private String PublicKey;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonSignHash_Click(object sender, EventArgs e)
        {
            // algoritmo de hash
            using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
            // algortimo assimétrico
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                PublicKey = rsa.ToXmlString(false);
                // mensagem para assinar
                byte[] messageToSign = Encoding.UTF8.GetBytes(textBoxOriginalMessage.Text); 
                // mensagem assinada hashed
                byte[] signHash = sha.ComputeHash(messageToSign);

                // escreve output nas textbox
                textBoxMessageDigest.Text = Convert.ToBase64String(signHash);
                textBoxMessageDigestBits.Text = (signHash.Length * 8).ToString();

                // usa o algoritmo assimétrico
                byte[] signature = rsa.SignHash(signHash, CryptoConfig.MapNameToOID("SHA256"));

                // escreve nas textbox
                textBoxDigitalSignature.Text = Convert.ToBase64String(signature);
                textBoxDigitalSignatureBits.Text = (signature.Length * 8).ToString();
                MessageBox.Show(CryptoConfig.MapNameToOID("SHA256"));

            }
        }

        private void ButtonSignData_Click(object sender, EventArgs e)
        {
            // algoritmo de hash
            using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
            // algortimo assimétrico
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                PublicKey = rsa.ToXmlString(false);
                // mensagem para assinar
                byte[] messageToSign = Encoding.UTF8.GetBytes(textBoxOriginalMessage.Text);
                textBoxMessageDigest.Text = "";
                textBoxMessageDigestBits.Text = "";

                // usa o algoritmo assimétrico
                byte[] signature = rsa.SignData(messageToSign,sha);

                // escreve nas textbox
                textBoxDigitalSignature.Text = Convert.ToBase64String(signature);
                textBoxDigitalSignatureBits.Text = (signature.Length * 8).ToString();
                MessageBox.Show(CryptoConfig.MapNameToOID("SHA256"));

            }
        }

        private void ButtonVerifyHash_Click(object sender, EventArgs e)
        {
            using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(PublicKey);
                byte[] messageToVerify = Encoding.UTF8.GetBytes(textBoxOriginalMessage.Text);
                byte[] signature = Convert.FromBase64String(textBoxDigitalSignature.Text);

                byte[] hash = sha.ComputeHash(messageToVerify);

                if (rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signature))
                {
                    MessageBox.Show("Valid Signature");
                }
                else
                {
                    MessageBox.Show("Invalid Signature");

                }
            }
        }

        private void ButtonVerifyData_Click(object sender, EventArgs e)
        {
            using (SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider())
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(PublicKey);
                byte[] messageToVerify = Encoding.UTF8.GetBytes(textBoxOriginalMessage.Text);
                byte[] signature = Convert.FromBase64String(textBoxDigitalSignature.Text);

                if(rsa.VerifyData(messageToVerify, sha, signature))
                {
                    MessageBox.Show("Valid Signature");
                } else
                {
                    MessageBox.Show("Invalid Signature");

                }
            }
        }


    }
}
