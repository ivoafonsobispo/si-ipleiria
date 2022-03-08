using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2_Server
{
    internal class Server
    {
        static void Main(string[] args)
        {
            // IP + Porto
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 9999);
            // Fica a escuta num porto de rede
            TcpListener listener = null;
            // Representa o cliente
            TcpClient client = null;
            // Stream para envio dos dados
            NetworkStream stream = null;
            // Tamanho para ler a frame de internet
            int N = 1400;
            // Buffer de tamanho N
            byte[] buffer = new byte[N];
            // Numero de bytes lidos
            int bytesRead = 0;
            // Acknoledge para saber que a ligacao funcionou, assim como transforma a string em binario
            byte[] ack = Encoding.UTF8.GetBytes("ACK");

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

                #region Byte Array

                // READ
                Console.WriteLine("Waiting for Client...");
                // Comeca a ler o buffer e guarda o ser valor
                bytesRead = stream.Read(buffer, 0, N);
                // Transforma e escreve a mensagem recebida em ASCII para String
                Console.WriteLine("Received {0}", Encoding.ASCII.GetString(buffer,0,bytesRead));

                // WRITE
                Console.WriteLine("Sending ACK");
                // Envia ACK ao cliente
                stream.Write(ack,0,ack.Length);

                #endregion

                #region String

                // READ
                Console.WriteLine("Waiting for Client...");
                // Comeca a ler o buffer e guarda o ser valor
                bytesRead = stream.Read(buffer, 0, N);
                // Transforma e escreve a mensagem recebida em UTF8 para String
                Console.WriteLine("Received {0}", Encoding.UTF8.GetString(buffer, 0, bytesRead));

                // WRITE
                Console.WriteLine("Sending ACK");
                // Envia ACK ao cliente
                stream.Write(ack, 0, ack.Length);

                #endregion

                #region Integer

                // READ
                Console.WriteLine("Waiting for Client...");
                // Comeca a ler o buffer e guarda o ser valor
                bytesRead = stream.Read(buffer, 0, N);
                // Transforma e escreve a mensagem recebida numa integer de 32 bits
                Console.WriteLine("Received {0}", BitConverter.ToInt32(buffer,0));

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
