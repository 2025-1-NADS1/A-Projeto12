using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EXEMPLO_02_SERVIDOR
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener servidor = null;
            try
            {
                int porta = 30000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                servidor = new TcpListener(localAddr, porta);
                servidor.Start();

                Console.WriteLine($"Servidor TCP escutando em {localAddr}:{porta}...");

                while (true)
                {
                    TcpClient cliente = servidor.AcceptTcpClient();
                    Console.WriteLine("Cliente conectado.");

                    NetworkStream stream = cliente.GetStream();

                    byte[] buffer = new byte[cliente.ReceiveBufferSize];
                    int bytesLidos = stream.Read(buffer, 0, buffer.Length);
                    string mensagemRecebida = Encoding.UTF8.GetString(buffer, 0, bytesLidos);

                    Console.WriteLine("Mensagem recebida: " + mensagemRecebida);

                    // Simulando resposta com base no comando
                    string resposta = "";
                    if (mensagemRecebida.StartsWith("GET AMBIENTE"))
                    {
                        string ambiente = mensagemRecebida.Substring("GET AMBIENTE ".Length).Trim();
                        resposta = $"Ambiente {ambiente} -> Temp: 23°C, Umidade: 50%, Presença: Sim";
                    }
                    else
                    {
                        resposta = "Comando não reconhecido.";
                    }

                    byte[] respostaBytes = Encoding.UTF8.GetBytes(resposta);
                    stream.Write(respostaBytes, 0, respostaBytes.Length);

                    cliente.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro no servidor: " + e.Message);
            }
            finally
            {
                servidor?.Stop();
            }
        }
    }
}