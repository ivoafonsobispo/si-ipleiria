using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        private void ButtonGererateKeys_Click(object sender, EventArgs e)
        {
            // Criamos o algoritmo RSA usando o using para nao lidar com memoria
            using (RSACryptoServiceProvider algorithm = new RSACryptoServiceProvider())
            {
                // Criamos a chave public em XML, escolhemos a opção false, pois apenas queremos apenas crie para a publica
                textboxPublicKey.Text = algorithm.ToXmlString(false);
                // Criamos uma chave privada em XML, escolhemos a opção true, pois queremos que crie para ambas as chaves publica e privada
                textboxBothKeys.Text = algorithm.ToXmlString(true);
                
            }
        }

        private void ButtonSavePublicKey_Click(object sender, EventArgs e)
        {
            // Grava ficheiro com chave publica
            File.WriteAllText("PublicKey.txt", textboxPublicKey.Text);
            MessageBox.Show("Public Key File Writen");
        }

        private void ButtonSaveKeys_Click(object sender, EventArgs e)
        {
            // Grava ficheiro com chave publica e privada
            File.WriteAllText("PrivatePublicKey.txt", textboxBothKeys.Text);
            MessageBox.Show("Private and Public Key File Writen");


        }

        private void ButtonEncrypt_Click(object sender, EventArgs e)
        {
            // Criamos o algoritmo RSA usando o using para nao lidar com memoria
            using (RSACryptoServiceProvider algorithm = new RSACryptoServiceProvider())
            {
                // Lê-mos o ficheiro da chave publica
                string publicKey = File.ReadAllText("PublicKey.txt");
                // Fazemos com que o algoritmo use a chave publica
                algorithm.FromXmlString(publicKey);

                // Array dos dados
                byte[] symmetricKey = Encoding.UTF8.GetBytes(textboxSymmetricKey.Text);

                // Kpub -> Data -> Kpri
                // Encriptamos os dados, enviamos os dados e usamos o para usar versões mais atualizadas do OS
                byte[] encryptedSymmetricKey = algorithm.Encrypt(symmetricKey, true);

                // Escrevemos o texto encriptado
                textboxSymmetricKeyEncrypted.Text = Convert.ToBase64String(encryptedSymmetricKey);

                // Escremos os bits, length responde em bytes multiplicamos por 8 para ter os bits
                textboxBitSize.Text = (encryptedSymmetricKey.Length * 8).ToString();
            }
        }

        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {
            // Criamos o algoritmo RSA usando o using para nao lidar com memoria
            using (RSACryptoServiceProvider algorithm = new RSACryptoServiceProvider())
            {
                // Lê-mos o ficheiro da chave publica e privada
                string bothKeys = File.ReadAllText("PrivatePublicKey.txt");

                // Fazemos com que o algoritmo use a chave publica
                algorithm.FromXmlString(bothKeys);

                // Array dos dados encriptados
                byte[] encryptedSymmetricKey = Convert.FromBase64String(textboxSymmetricKeyEncrypted.Text);

                // Desencriptamos os dados
                byte[] decryptedSymmetricKey = algorithm.Decrypt(encryptedSymmetricKey, true);

                // Apresentamos o codigo desencriptado
                textboxSymmetricKeyDecrypted.Text = Encoding.UTF8.GetString(decryptedSymmetricKey); 
            }
        }
    }
}
