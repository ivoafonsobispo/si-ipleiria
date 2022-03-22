using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ei_si_worksheet4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonGenerateKeys_Click(object sender, EventArgs e)
        {
            // Criamos o algoritmo RSA usando o using para nao lidar com memoria
            using (RSACryptoServiceProvider algorithm = new RSACryptoServiceProvider())
            {
                // Criamos a chave public em XML, escolhemos a opção false, pois apenas queremos apenas crie para a publica
                tbPublicKey.Text = algorithm.ToXmlString(false);
                // Criamos uma chave privada em XML, escolhemos a opção true, pois queremos que crie para ambas as chaves publica e privada
                tbBothKeys.Text = algorithm.ToXmlString(true);

            }
        }

        private void ButtonImportKeys_Click(object sender, EventArgs e)
        {
            if (!File.Exists("PrivatePublicKey.txt"))
                return;

            using (RSACryptoServiceProvider algorithm = new RSACryptoServiceProvider())
            {
                // Lê-mos o ficheiro da chave publica
                algorithm.FromXmlString(File.ReadAllText("PrivatePublicKey.txt"));

                // Criamos a chave public em XML, escolhemos a opção false, pois apenas queremos apenas crie para a publica
                tbPublicKey.Text = algorithm.ToXmlString(false);
                // Criamos uma chave privada em XML, escolhemos a opção true, pois queremos que crie para ambas as chaves publica e privada
                tbBothKeys.Text = algorithm.ToXmlString(true);
            }
        }

        private void ButtonSavePublicKey_Click(object sender, EventArgs e)
        {
            // Grava ficheiro com chave publica
            File.WriteAllText("PublicKey.txt", tbPublicKey.Text);
            MessageBox.Show("Public Key File Writen");
        }

        private void ButtonSaveKeys_Click(object sender, EventArgs e)
        {
            // Grava ficheiro com chave publica e privada
            File.WriteAllText("PrivatePublicKey.txt", tbBothKeys.Text);
            MessageBox.Show("Private and Public Key File Writen");
        }

        private void ButtonGenerateSymmetricKey_Click(object sender, EventArgs e)
        {
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            // TODO:

        }

        private void ButtonEncryptFile_Click(object sender, EventArgs e)
        {
            // Criamos o algoritmo RSA usando o using para nao lidar com memoria
            using (RSACryptoServiceProvider algorithm = new RSACryptoServiceProvider())
            {
                // Lê-mos o ficheiro da chave publica
                string publicKey = File.ReadAllText("PublicKey.txt");
                // Fazemos com que o algoritmo use a chave publica
                algorithm.FromXmlString(publicKey);

                // Array dos dados
                byte[] symmetricKey = Encoding.UTF8.GetBytes(tbSymmetricKeyEncrypted.Text);

                // Kpub -> Data -> Kpri
                // Encriptamos os dados, enviamos os dados e usamos o para usar versões mais atualizadas do OS
                byte[] encryptedSymmetricKey = algorithm.Encrypt(symmetricKey, true);

                // Escrevemos o texto encriptado
                tbSymmetricKeyEncrypted.Text = Convert.ToBase64String(encryptedSymmetricKey);

                // Escremos os bits, length responde em bytes multiplicamos por 8 para ter os bits
                tbBitSize.Text = (encryptedSymmetricKey.Length * 8).ToString();
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            // Criamos o algoritmo RSA usando o using para nao lidar com memoria
            using (RSACryptoServiceProvider algorithm = new RSACryptoServiceProvider())
            {
                // Lê-mos o ficheiro da chave publica e privada
                string bothKeys = File.ReadAllText("PrivatePublicKey.txt");

                // Fazemos com que o algoritmo use a chave publica
                algorithm.FromXmlString(bothKeys);

                // Array dos dados encriptados
                byte[] encryptedSymmetricKey = Convert.FromBase64String(tbSymmetricKeyEncrypted.Text);

                // Desencriptamos os dados
                byte[] decryptedSymmetricKey = algorithm.Decrypt(encryptedSymmetricKey, true);

            }
        }
    }
}
