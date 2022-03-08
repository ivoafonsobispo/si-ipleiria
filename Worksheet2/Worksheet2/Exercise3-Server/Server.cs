using EI.SI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exercise3_Server
{
    internal class Server
    {
        static void Main(string[] args)
        {
            // Instanciacao da biblioteca fornecida
            ProtocolSI protocol = new ProtocolSI();
            // IP + Porto
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9999);
            // Fica a escuta num porto de rede
            TcpListener listener = null;
            // Representa o cliente
            TcpClient client = null;
            // Stream para envio dos dados
            NetworkStream stream = null;
            // Numero de bytes lidos
            int bytesRead = 0;
            // Acknoledge para saber que a ligacao funcionou, assim como transforma a string em binario
            byte[] ack = protocol.Make(ProtocolSICmdType.ACK);

            // Try Catch para verifica se tudo funcionou
            try
            {
                // Inicializacao do listener
                listener = new TcpListener(endPoint);
                listener.Start();

                // Fica a espera que o cliente se ligue (chamada bloqueante)
                client = listener.AcceptTcpClient();
                // Buscar a stream
                stream = client.GetStream();

                #region String

                // READ
                Console.WriteLine("Waiting for Client...");
                // Comeca a ler o buffer e guarda o ser valor
                stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                // Transforma e escreve a mensagem recebida usando a biblioteca
                Console.WriteLine("Received {0}", protocol.GetStringFromData());

                // WRITE
                Console.WriteLine("Sending ACK");
                // Envia ACK ao cliente
                stream.Write(ack, 0, ack.Length);

                #endregion

                #region Integer

                // READ
                Console.WriteLine("Waiting for Client...");
                // Comeca a ler o buffer e guarda o ser valor
                stream.Read(protocol.Buffer, 0, protocol.Buffer.Length);
                // Transforma e escreve a mensagem recebida usando a biblioteca
                Console.WriteLine("Received {0}", protocol.GetStringFromData());

                // WRITE
                Console.WriteLine("Sending ACK");
                // Envia ACK ao cliente
                stream.Write(ack, 0, ack.Length);

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
                if (listener != null) listener.Stop();
                // Consola fica a espera de um enter para fechar, para pudermos ler o output
                Console.ReadLine();
            }
        }
    }
}
