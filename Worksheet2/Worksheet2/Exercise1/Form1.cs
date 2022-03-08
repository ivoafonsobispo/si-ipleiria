using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Ao clicar no botao do form
        private void buttonCopyFile_Click(object sender, EventArgs e)
        {
            // Path imagem de destino
            string origin = "security.jpg";
            // Path + nome para a imagem duplicada
            string destination = "security_copy.jpg";
            // Tamanho do buffer
            int N = 20480;
            // Numero de bytes lidos
            int bytesRead = 0;
            // buffer do tamanho do N
            byte[] buffer = new byte[N];

            // using -> libertar os recursos ( Free do C ) / lazy programming
            // Stream para leitura
            using (FileStream originStream = new FileStream(origin, FileMode.Open))
            // Stream para escrita
            using (FileStream destinationStream = new FileStream(destination, FileMode.Create))
            {
                // ciclo para ler do originStream e escrever no destinationStream
                // le (do originStream) o numero de bytes N para o buffer
                while((bytesRead = originStream.Read(buffer,0,N)) > 0)
                {
                    // escreve (para o destinationStream) o numero de bytes lidos do bytesRead
                    destinationStream.Write(buffer,0,bytesRead);
                }
            }
            // Mensagem de Debug
            MessageBox.Show("File Copied");


        }
    }
}
