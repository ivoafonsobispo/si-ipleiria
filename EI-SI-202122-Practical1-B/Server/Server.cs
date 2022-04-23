using EI.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Server
    {
        static void Main(string[] args)
        {
            IPEndPoint listenEndPoint;
            TcpListener listener = null;
            TcpClient client = null;
            NetworkStream networkStream = null;
            ProtocolSI protocol = null;
            RSACryptoServiceProvider rsaClient = null;
            RSACryptoServiceProvider rsaServer = null;
            SHA256CryptoServiceProvider sha256 = null;

            try
            {
                Console.WriteLine($"** SERVER: Practical Exam on {DateTime.Today.ToLongDateString()} - Version B **");

                listenEndPoint = new IPEndPoint(IPAddress.Any, 10000);
                listener = new TcpListener(listenEndPoint);

                Console.Write("Waiting for client... ");
                listener.Start();
                client = listener.AcceptTcpClient();
                networkStream = client.GetStream();
                Console.WriteLine("OK.");

                protocol = new ProtocolSI();
                byte[] ack = protocol.Make(ProtocolSICmdType.ACK);
                byte[] msg = null;

                rsaServer = new RSACryptoServiceProvider();
                rsaClient = new RSACryptoServiceProvider();
                sha256 = new SHA256CryptoServiceProvider();

                Console.Write("Reading Public Key... ");
                networkStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                Console.WriteLine("OK.");
                String clientPublicKey = protocol.GetStringFromData();
                byte[] packet = protocol.Make(ProtocolSICmdType.PUBLIC_KEY, rsaServer.ToXmlString(false));
                Console.WriteLine("Sending Public Key... OK.");
                networkStream.Write(packet, 0, packet.Length);


                Console.Write("Reading Recipient Symmetric Elements... ");
                networkStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                byte[] encryptedSymKey = protocol.GetData();
                Console.WriteLine("OK.");

                Console.WriteLine("Signing and sending....");
                byte[] signature = rsaServer.SignData(encryptedSymKey, sha256);
                msg = protocol.Make(ProtocolSICmdType.DIGITAL_SIGNATURE, signature);
                networkStream.Write(msg, 0, msg.Length);

                Console.Write("Reading Recipient Message...");
                networkStream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                byte[] encryptedMessage = protocol.GetData();
                Console.WriteLine("OK.");

                Console.WriteLine("Signing and sending....");
                signature = rsaServer.SignData(encryptedMessage, sha256);
                msg = protocol.Make(ProtocolSICmdType.DIGITAL_SIGNATURE, signature);
                networkStream.Write(msg, 0, msg.Length);
                Console.WriteLine("OK.");



            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.ToString()}");
            } finally
            {
                if (sha256 != null)
                    sha256.Dispose();
                if (rsaClient != null)
                    rsaClient.Dispose();
                if (rsaServer != null)
                    rsaServer.Dispose();
                if (networkStream != null)
                    networkStream.Dispose();
                if (client != null)
                    client.Close();
                if (listener != null)
                    listener.Stop();
            }

            Console.Write("End: Press a key...");
            Console.ReadKey();

        } // main
    } // class
} // namaspace
