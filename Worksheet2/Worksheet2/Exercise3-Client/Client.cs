using EI.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3_Client
{
    internal class Client
    {
        static void Main(string[] args)
        {
            // Instanciacao da biblioteca fornecida
            ProtocolSI protocol = new ProtocolSI();
            // IP + Porto
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 9999);
            // Representa o cliente
            TcpClient client = null;
            // Stream para envio dos dados
            NetworkStream stream = null;
            // Numero de bytes lidos
            int bytesRead = 0;
            // biblioteca protocol coloca logo o ack no tipo certo
            byte[] ack = protocol.Make(ProtocolSICmdType.ACK);

            // Try Catch para verifica se tudo funcionou
            try
            {
                // Cria um novo cliente
                client = new TcpClient();
                // Fica a espera que a ligacao ocorra, chamada bloqueante
                client.Connect(endPoint);
                // Buscar a stream
                stream = client.GetStream();

                #region String

                // WRITE
                // A bilbioteca trata da trandormacao sozinha
                byte[] msg = protocol.Make(ProtocolSICmdType.NORMAL, "SI");
                Console.WriteLine("Sending String");
                // Envia ACK ao cliente
                stream.Write(msg, 0, msg.Length);

                // READ
                Console.WriteLine("Waiting for ACK...");
                // Comeca a ler o buffer e guarda o ser valor
                stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                Console.WriteLine("Received");

                #endregion

                #region Integer

                // WRITE
                // Transforma a mensagem em UTF8
                msg = protocol.Make(ProtocolSICmdType.DATA, 42);
                Console.WriteLine("Sending Integer");
                // Envia ACK ao cliente
                stream.Write(msg, 0, msg.Length);

                // READ
                Console.WriteLine("Waiting for ACK...");
                // Comeca a ler o buffer e guarda o ser valor
                stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                Console.WriteLine("Received");

                #endregion

            }
            catch (Exception ex)
            {

            }
            finally
            {
                // Fechar pela ordem inversa que foram abertas
                if (stream != null) stream.Dispose();
                if (client != null) stream.Dispose();
                // Consola fica a espera de um enter para fechar, para pudermos ler o output
                Console.ReadLine();
            }
        }
    }
}
