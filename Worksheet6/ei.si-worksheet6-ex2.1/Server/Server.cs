using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

namespace EI.SI
{
    /// <summary>
    /// Server
    /// Symmetrics (Encryption)
    /// </summary>
    class ServerWithProtocolSI
    {
        public static string SEPARATOR = "...";

        /// <summary>
        /// IMPORTANTE: a cada RECEÇÃO deve seguir-se, obrigatóriamente, um ENVIO de dados
        /// IMPORTANT: each network .Read must be fallowed by a network .Write
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            byte[] msg;
            IPEndPoint listenEndPoint;
            TcpListener server = null;
            TcpClient client = null;
            NetworkStream netStream = null;
            ProtocolSI protocol = null;
            AesCryptoServiceProvider aes = null;
            SymmetricsSI symmetricsSI = null;
            RSACryptoServiceProvider rsaClient = null;
            RSACryptoServiceProvider rsaServer = null;
            SHA512CryptoServiceProvider sha512 = null;

            // Accounts
            Dictionary<int, double> accounts = null;
            
            try
            {
                Console.WriteLine("SERVER");

                #region Defenitions
                // algortimos assimétricos
                rsaClient = new RSACryptoServiceProvider();
                rsaServer = new RSACryptoServiceProvider();

                // algoritmos simétrico a usar...
                aes = new AesCryptoServiceProvider();
                symmetricsSI = new SymmetricsSI(aes);

                // Binding IP/port
                listenEndPoint = new IPEndPoint(IPAddress.Any, 13000);
                
                // Client/Server Protocol to SI
                protocol = new ProtocolSI();

                sha512 = new SHA512CryptoServiceProvider();

                // Accounts
                accounts = new Dictionary<int, double>();
                accounts.Add(123, 100.50);
                accounts.Add(456, 200.50);
                accounts.Add(789, 3000);
                #endregion

                Console.WriteLine(SEPARATOR);
                 
                #region TCP Listner
                // Start TcpListener
                server = new TcpListener(listenEndPoint);
                server.Start();

                // Waits for a client connection (bloqueant wait)
                Console.Write("waiting for a connection... ");
                client = server.AcceptTcpClient();
                netStream = client.GetStream();
                Console.WriteLine("ok");
                #endregion

                Console.WriteLine(SEPARATOR);

                #region Exhange Public Keys
                // Receive client public key
                Console.Write("waiting for client public key...");
                netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                rsaClient.FromXmlString(protocol.GetStringFromData());
                Console.WriteLine("ok");

                // Send public key...
                Console.Write("Sending public key... ");
                msg = protocol.Make(ProtocolSICmdType.PUBLIC_KEY, rsaServer.ToXmlString(false));
                netStream.Write(msg, 0, msg.Length);
                Console.WriteLine("ok");
                #endregion

                Console.WriteLine(SEPARATOR);

                #region Exchange Secret Key               
                // Receive key
                Console.Write("waiting for key...");
                netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                aes.Key = rsaServer.Decrypt(protocol.GetData(), true);
                Console.WriteLine("ok");
                Console.WriteLine("   Received: {0} ", ProtocolSI.ToHexString(aes.Key));

                // Answer with a ACK
                Console.Write("Sending a ACK... ");
                msg = protocol.Make(ProtocolSICmdType.ACK);
                netStream.Write(msg, 0, msg.Length);
                Console.WriteLine("ok");


                // Receive iv
                Console.Write("waiting for iv...");
                netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                aes.IV = rsaServer.Decrypt(protocol.GetData(), true);
                Console.WriteLine("ok");
                Console.WriteLine("   Received: {0} ", ProtocolSI.ToHexString(aes.IV));

                // Answer with a ACK
                Console.Write("Sending a ACK... ");
                msg = protocol.Make(ProtocolSICmdType.ACK);
                netStream.Write(msg, 0, msg.Length);
                Console.WriteLine("ok");
                #endregion

                Console.WriteLine(SEPARATOR);

                #region Exchange Data (Secure channel)                
                // Receive the cipher
                Console.Write("waiting for data...");
                netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                byte[] encryptedData = protocol.GetData();
                byte[] recvData = symmetricsSI.Decrypt(encryptedData);
                Console.WriteLine("ok");
                int accountID = BitConverter.ToInt32(recvData, 0);
                Console.WriteLine("   Encrypted: {0}", ProtocolSI.ToHexString(encryptedData));
                Console.WriteLine("   Data: {0} = {1}", accountID, ProtocolSI.ToHexString(recvData));

                // Answer with a ACK
                Console.WriteLine("Sending a ACK... ");
                msg = protocol.Make(ProtocolSICmdType.ACK);
                netStream.Write(msg, 0, msg.Length);

                // Send Balance...
                byte[] sentData = BitConverter.GetBytes(accounts[accountID]);
                Console.Write("Sending  Balance... ");
                encryptedData = symmetricsSI.Encrypt(sentData);
                msg = protocol.Make(ProtocolSICmdType.DATA, encryptedData);
                netStream.Write(msg, 0, msg.Length);
                Console.WriteLine("ok");
                Console.WriteLine("   Data: {0} = {1}", accounts[accountID], ProtocolSI.ToHexString(sentData));
                Console.WriteLine("   Encrypted: {0}", ProtocolSI.ToHexString(encryptedData));
                #endregion

                Console.WriteLine(SEPARATOR);

                #region Exchange Digital Signature  
                // Receive the cipher
                Console.Write("waiting for USER OPTION...");
                netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                byte[] userOption = protocol.GetData();
                Console.WriteLine("ok");
                Console.WriteLine("   Data: {0}", userOption);

                // Answer with a ACK
                Console.WriteLine("Sending a ACK... ");
                msg = protocol.Make(ProtocolSICmdType.ACK);
                netStream.Write(msg, 0, msg.Length);

                // Send data...
                sentData = BitConverter.GetBytes(accounts[accountID]);
                Console.Write("Sending  Balance... ");
                encryptedData = symmetricsSI.Encrypt(sentData);
                byte[] signature = rsaServer.SignData(encryptedData, sha512);

                Console.Write("Sending  Digital Signature... ");

                msg = protocol.Make(ProtocolSICmdType.DIGITAL_SIGNATURE, signature);
                netStream.Write(msg, 0, msg.Length);
                Console.WriteLine("ok");
                Console.WriteLine("   Data: {0}", ProtocolSI.ToHexString(signature));

                // Receive answer from server
                Console.Write("waiting for ACK / NACK...");
                netStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                if (protocol.GetCmdType() == ProtocolSICmdType.ACK)
                {
                    Console.WriteLine("ok -- Valid Signature");
                }
                else
                {
                    Console.WriteLine("NOT ok -- Invalid Signature");

                }
                #endregion

            }
            catch (Exception ex)
            {
                Console.WriteLine(SEPARATOR);
                Console.WriteLine("Exception: {0}", ex.ToString());
            }
            finally
            {
                // Close connections
                if (netStream != null)
                    netStream.Dispose();
                if (client != null)
                    client.Close();
                if (server != null)
                    server.Stop();
                if (sha512 != null)
                    sha512.Dispose();
                if (rsaClient != null)
                    rsaClient.Dispose();
                if (rsaServer != null)
                    rsaServer.Dispose();
                Console.WriteLine(SEPARATOR);
                Console.WriteLine("Connection with client was closed.");
            }

            Console.WriteLine(SEPARATOR);
            Console.Write("End: Press a key...");
            Console.ReadKey();
        }

    }
}
