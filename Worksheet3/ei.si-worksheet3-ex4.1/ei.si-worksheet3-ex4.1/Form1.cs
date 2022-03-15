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

namespace ei_si_worksheet3
{
    public partial class Form1 : Form
    {
        private byte[] Key = null;
        private byte[] IV = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ButtonEncrypt_Click(object sender, EventArgs e)
        {
            // Altera plain text da text box para binario
            byte[] plainText = Encoding.UTF8.GetBytes(textBoxTextToEncrypt.Text);
            if (comboBoxAlgorithm.SelectedIndex == 1)
            {
                // Instanciar Algortimo
                AesCryptoServiceProvider algorithm = new AesCryptoServiceProvider();
                // Guarda a chave
                Key = algorithm.Key;
                // Guarda o IV
                IV = algorithm.IV;

                // Criar stream de memoria
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Criar stream para encriptar em escrita
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Escreve os bytes para dentro do crypto memory stream
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        // Fecha a stream, assim que o write tiver terminado
                        cryptoStream.Close();

                        // Criamos um novo array com todo o texto cifrado
                        byte[] cipherText = memoryStream.ToArray();

                        // Transforma-mos o texto encriptado que esta em binario para Base64 para tirar error futuro de dados
                        textBoxTextToEncrypt.Text = Convert.ToBase64String(cipherText);
                    }
                }
            } else if (comboBoxAlgorithm.SelectedIndex == 2)
            {
                TripleDESCryptoServiceProvider algorithm = new TripleDESCryptoServiceProvider();
                // Guarda a chave
                Key = algorithm.Key;
                // Guarda o IV
                IV = algorithm.IV;

                // Criar stream de memoria
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Criar stream para encriptar em escrita
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Escreve os bytes para dentro do crypto memory stream
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        // Fecha a stream, assim que o write tiver terminado
                        cryptoStream.Close();

                        // Criamos um novo array com todo o texto cifrado
                        byte[] cipherText = memoryStream.ToArray();

                        // Transforma-mos o texto encriptado que esta em binario para Base64 para tirar error futuro de dados
                        textBoxTextToEncrypt.Text = Convert.ToBase64String(cipherText);
                    }
                }
            } else
            {
                Console.WriteLine("Select one of the algorithms");
                return;
            }
        }

        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {

        }

        private void ComboBoxAlgorithm_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
