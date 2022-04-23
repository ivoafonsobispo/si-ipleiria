using EI.SI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FormEISIPracticalExam : Form
    {
        // Assimetric Keys
        RSACryptoServiceProvider rsaClient = null;
        RSACryptoServiceProvider rsaServer = null;

        // Symetric
        AesCryptoServiceProvider aes = null;
        SymmetricsSI symmetricsSI = null;

        // Comunication
        IPEndPoint serverEndPoint;
        TcpClient client = null;
        ProtocolSI protocol = null;
        NetworkStream netStream = null;

        SHA256CryptoServiceProvider sha256 = null;

        byte[] msg = null;

        public FormEISIPracticalExam()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonGenerateClientAssymetricalKeys_Click(object sender, EventArgs e)
        {
            // Assimetric Client Algorithm
            rsaClient = new RSACryptoServiceProvider();
            MessageBox.Show("Client Assymetric Key Has Been Generated");
        }

        private void buttonGenerateRecipientAsymmetricalKeys_Click(object sender, EventArgs e)
        {
            // Assimetric Recipient Algorithm
            rsaServer = new RSACryptoServiceProvider();
            MessageBox.Show("Recepient Assymetric Key Has Been Generated");
        }

        private void buttonGenerateSymmetricalElementsRecipient_Click(object sender, EventArgs e)
        {
            // Symmetric Algorithm
            aes = new AesCryptoServiceProvider();
            symmetricsSI = new SymmetricsSI(aes);
            MessageBox.Show("Symmetric Algorithm Has Been Generated");
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (textBoxServerIPAddress.Text.Length == 0 || textBoxServerPort.Text.Length == 0)
            {
                MessageBox.Show("Please fill both fields");
                return;
            }

            //--- IP
            string ip = textBoxServerIPAddress.Text;
            //--- Porto
            int port = Int32.Parse(textBoxServerPort.Text);

            // Client/Server Protocol to SI
            protocol = new ProtocolSI();

            // Defenitions for TcpClient: IP:port (127.0.0.1:9999)
            serverEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // Connects to Server ...
            client = new TcpClient();
            client.Connect(serverEndPoint);
            netStream = client.GetStream();

            MessageBox.Show("Connected to the server with success");
        }

        private void buttonExchangeAsymmetricalKeys_Click(object sender, EventArgs e)
        {
            #region Exchange Public Keys
            msg = protocol.Make(ProtocolSICmdType.PUBLIC_KEY, rsaClient.ToXmlString(false));
            netStream.Write(msg, 0, msg.Length);

            netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
            rsaServer.FromXmlString(protocol.GetStringFromData());
            MessageBox.Show("Public Key shared");
            #endregion
        }

        private void buttonSendSymmetricElementsRecipient_Click(object sender, EventArgs e)
        {
            // Hash Algorithm
            sha256 = new SHA256CryptoServiceProvider();

            // Send key...
            byte[] key = rsaServer.Encrypt(aes.Key, true);
            byte[] encryptedKey = protocol.Make(ProtocolSICmdType.SECRET_KEY, key);
            netStream.Write(encryptedKey, 0, encryptedKey.Length);


            // Receive the cipher
            netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
            byte[] signature = protocol.GetData();

            if (rsaServer.VerifyData(key, sha256, signature))
            {
                MessageBox.Show("Signature Valid");
            }
            else
            {
                MessageBox.Show("Signature Invalid");
            }
        }

        private void buttonEncryptSendMessageRecipient_Click(object sender, EventArgs e)
        {
            // Hash Algorithm
            sha256 = new SHA256CryptoServiceProvider();

            // Send data...
            byte[] clearData = Encoding.UTF8.GetBytes(textBoxMessageToSend.Text);
            byte[] encryptedData = symmetricsSI.Encrypt(clearData);
            msg = protocol.Make(ProtocolSICmdType.DATA, encryptedData);
            netStream.Write(msg, 0, msg.Length);

            // Receive the cipher
            netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
            byte[] signature = protocol.GetData();

            if (rsaServer.VerifyData(encryptedData, sha256, signature))
            {
                MessageBox.Show("Signature Valid");
            }
            else
            {
                MessageBox.Show("Signature Invalid");
            }
        }
    }
}
