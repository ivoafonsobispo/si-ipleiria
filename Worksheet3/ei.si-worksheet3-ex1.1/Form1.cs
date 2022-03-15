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
        // Variaveis para sabes a chave e o IV
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
                    textBoxEncryptedText.Text = Convert.ToBase64String(cipherText);
                }
            }
        }

        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {
            // Altera texto excriptado de base64 de volta para string
            byte[] cipherText = Convert.FromBase64String(textBoxEncryptedText.Text);

            // Instanciar Algortimo
            AesCryptoServiceProvider algorithm = new AesCryptoServiceProvider();

            // Adiciona a chave e o iv da encriptacao ao algoritmo de desencriptar
            algorithm.Key = Key;
            algorithm.IV = IV;

            // Criar stream de memoria
            using (MemoryStream memStream = new MemoryStream(cipherText))
            {
                // Criar stream para desencriptar em leitura
                using (CryptoStream cryptoStream = new CryptoStream(memStream,algorithm.CreateDecryptor(),CryptoStreamMode.Read))
                {
                    // Cria um array de plain text do tamanho do texto cifrado
                    byte[] plainText = new byte[cipherText.Length];
                    // Guarda o tamanho dos bytes lidos no text
                    int bytesRead = cryptoStream.Read(plainText, 0, plainText.Length);
                    // Volta a organizar o array
                    Array.Resize(ref plainText, bytesRead);
                    // Escreve o texto decriptado na text box
                    textBoxDecryptedText.Text = Encoding.UTF8.GetString(plainText);
                }
            }
        }
    }
}
