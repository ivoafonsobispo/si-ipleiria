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

namespace ei.si.worksheet5
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

        private void ButtonMD5ComputeHash_Click(object sender, EventArgs e)
        {
            // discard dos algoritmos quando eles terminam
            using (MD5CryptoServiceProvider algortihn = new MD5CryptoServiceProvider())
            {
                // dados para fazer hash
                byte[] data = Encoding.UTF8.GetBytes(textBoxDataToHash.Text);
                // devolve o hash
                byte[] hash = algortihn.ComputeHash(data);
                // escreve output na text box e transforma os bytes em hex
                textBoxHashBytes.Text = BitConverter.ToString(hash);
                textBoxHashLength.Text = (hash.Length * 8).ToString();
            }
        }

        private void ButtonSHA1ComputeHash_Click(object sender, EventArgs e)
        {
            // discard dos algoritmos quando eles terminam
            using (SHA1CryptoServiceProvider algortihn = new SHA1CryptoServiceProvider())
            {
                // dados para fazer hash
                byte[] data = Encoding.UTF8.GetBytes(textBoxDataToHash.Text);
                // devolve o hash
                byte[] hash = algortihn.ComputeHash(data);
                // escreve output na text box e transforma os bytes em hex
                textBoxHashBytes.Text = BitConverter.ToString(hash);
                textBoxHashLength.Text = (hash.Length * 8).ToString();

            }
        }

        private void ButtonSHA256ComputeHash_Click(object sender, EventArgs e)
        {
            // discard dos algoritmos quando eles terminam
            using (SHA256CryptoServiceProvider algortihn = new SHA256CryptoServiceProvider())
            {
                // dados para fazer hash
                byte[] data = Encoding.UTF8.GetBytes(textBoxDataToHash.Text);
                // devolve o hash
                byte[] hash = algortihn.ComputeHash(data);
                // escreve output na text box e transforma os bytes em hex
                textBoxHashBytes.Text = BitConverter.ToString(hash);
                textBoxHashLength.Text = (hash.Length * 8).ToString();


            }
        }

        private void ButtonSHA512ComputeHash_Click(object sender, EventArgs e)
        {
            // discard dos algoritmos quando eles terminam
            using (SHA512CryptoServiceProvider algortihn = new SHA512CryptoServiceProvider())
            {
                // dados para fazer hash
                byte[] data = Encoding.UTF8.GetBytes(textBoxDataToHash.Text);
                // devolve o hash
                byte[] hash = algortihn.ComputeHash(data);
                // escreve output na text box e transforma os bytes em hex
                textBoxHashBytes.Text = BitConverter.ToString(hash);
                textBoxHashLength.Text = (hash.Length * 8).ToString();


            }
        }
    }
}
