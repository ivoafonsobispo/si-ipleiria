using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exercise2_Client
{
    internal class Client
    {
        static void Main(string[] args)
        {
            // IP + Porto
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 9999);
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
                // Cria um novo cliente
                client = new TcpClient();
                // Fica a espera que a ligacao ocorra, chamada bloqueante
                client.Connect(endPoint);
                // Buscar a stream
                stream = client.GetStream();

                #region Byte Array

                // WRITE
                // Mensagem de envio
                byte[] msg = new byte[] { 69, 73 };
                Console.WriteLine("Sending Byte Array");
                // Envia ACK ao cliente
                stream.Write(msg, 0, msg.Length);

                // READ
                Console.WriteLine("Waiting for ACK...");
                // Comeca a ler o buffer e guarda o ser valor
                bytesRead = stream.Read(buffer, 0, N);
                // Transforma e escreve a mensagem recebida em UTF8 para String
                Console.WriteLine("Received {0}", Encoding.UTF8.GetString(buffer,0,bytesRead));

                #endregion

                #region String

                // WRITE
                // Transforma a mensagem em UTF8
                msg = Encoding.UTF8.GetBytes("SI");
                Console.WriteLine("Sending String");
                // Envia ACK ao cliente
                stream.Write(msg, 0, msg.Length);

                // READ
                Console.WriteLine("Waiting for ACK...");
                // Comeca a ler o buffer e guarda o ser valor
                bytesRead = stream.Read(buffer, 0, N);
                // Transforma e escreve a mensagem recebida de UTF8 para String
                Console.WriteLine("Received {0}", Encoding.UTF8.GetString(buffer, 0, bytesRead));

                #endregion

                #region Integer

                // WRITE
                // Transforma a mensagem em UTF8
                msg = BitConverter.GetBytes(42);
                Console.WriteLine("Sending Integer");
                // Envia ACK ao cliente
                stream.Write(msg, 0, msg.Length);

                // READ
                Console.WriteLine("Waiting for ACK...");
                // Comeca a ler o buffer e guarda o ser valor
                bytesRead = stream.Read(buffer, 0, N);
                // Transforma e escreve a mensagem recebida em UTF8 para String
                Console.WriteLine("Received {0}", Encoding.UTF8.GetString(buffer, 0, bytesRead));

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
