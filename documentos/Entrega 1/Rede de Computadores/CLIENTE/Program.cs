using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EXEMPLO_02_CLIENT
{
    class Program
    {
        public static String IPServer = "127.0.0.1";
        public static String msg;
        public static TcpClient client;
        public static NetworkStream stream;
        public static Byte[] dados;
        public static String ambienteEscolha, numEscolha;
        static void Main(string[] args)
        {


            #region nova Entrada
            /*
            client = new TcpClient();
            client.Connect(IPServer, 80);
            msg = "SET SUMMOR TIME" ;            
            dados = System.Text.Encoding.UTF8.GetBytes(msg);
            stream = client.GetStream();
            stream.Write(dados, 0, dados.Length);
            client.Close();*/
            #endregion

            NewConnection();
            EscolhaAmbiente();
            //ambienteEscolha = "Cozinha";
            NewMensage(ambienteEscolha);

            try
            {
                dados = new Byte[client.ReceiveBufferSize];
                int tentativas = 1;

                while (!stream.DataAvailable && tentativas < 5)
                {
                    tentativas++;
                    Thread.Sleep(30);
                }

                int numBytes = stream.Read(dados, 0, dados.Length); // <- pode lançar IOException
                String resposta = "";

                tentativas = 0;
                while (numBytes > 0)
                {
                    resposta += System.Text.Encoding.UTF8.GetString(dados, 0, numBytes);
                    tentativas = 0;
                    numBytes = 0;

                    while (!stream.DataAvailable && tentativas < 5)
                    {
                        tentativas++;
                        Thread.Sleep(30);
                    }

                    if (stream.DataAvailable)
                    {
                        numBytes = stream.Read(dados, 0, dados.Length);
                    }
                }

                Console.WriteLine("Resposta recebida: " + resposta);
                Console.ReadKey();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erro de leitura de conexão: " + ex.Message);
                Console.ReadKey();
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Erro de socket: " + ex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro inesperado: " + ex.Message);
                Console.ReadKey();
            }

        }

        public static void NewConnection()
        {
            client = new TcpClient();
            client.Connect(IPServer, 30000);

        }

        public static void NewMensage(String ambiente)
        {
            msg = "GET AMBIENTE " + ambiente;
            dados = System.Text.Encoding.UTF8.GetBytes(msg);
            stream = client.GetStream();
            stream.Write(dados, 0, dados.Length);
        }
        public static void EscolhaAmbiente()
        {
            bool[] luz = { false, false, false, false };
            bool[] umidificador = { false, false, false, false };
            bool escolhaValida = false;
            while (escolhaValida != true)
            {
                Console.WriteLine("ESCOLHA O NÚMERO DO AMBIENTE");

                Console.WriteLine("1 - Quarto 1 - Lig/Desl \n2 - Quarto 2 - Lig/Desl\n3 - Sala - Lig/Desl\n4 - Cozinha - Lig/Desl\n");

                numEscolha = Console.ReadLine().ToString();

                switch (numEscolha)
                {
                    case "1":
                        ambienteEscolha = "QUARTO_1";
                        InformacoesAmbiente(luz[0], umidificador[0]);
                        string opc = menuLigaDesliga();
                        switch (opc)
                        {
                            case "1":
                                luz[0] = LigaDesliga(luz[0]);
                                break;
                            case "2":
                                umidificador[0] = LigaDesliga(umidificador[0]);
                                break;
                            case "3":
                                luz[0] = LigaDesliga(luz[0]);
                                umidificador[0] = LigaDesliga(umidificador[0]);
                                break;
                            default:
                                Console.WriteLine("Opção não valida");
                                break;
                        }
                        InformacoesAmbiente(luz[0], umidificador[0]);

                        escolhaValida = false;
                        break;
                    case "2":
                        ambienteEscolha = "QUARTO_2";
                        InformacoesAmbiente(luz[1], umidificador[1]);
                        opc = menuLigaDesliga();
                        switch (opc)
                        {
                            case "1":
                                luz[1] = LigaDesliga(luz[1]);
                                break;
                            case "2":
                                umidificador[1] = LigaDesliga(umidificador[1]);
                                break;
                            case "3":
                                luz[1] = LigaDesliga(luz[1]);
                                umidificador[1] = LigaDesliga(umidificador[1]);
                                break;
                            default:
                                Console.WriteLine("Opção não valida");
                                break;
                        }
                        InformacoesAmbiente(luz[1], umidificador[1]);

                        escolhaValida = false;
                        break;

                    case "3":
                        ambienteEscolha = "SALA";
                        InformacoesAmbiente(luz[2], umidificador[2]);
                        opc = menuLigaDesliga();
                        switch (opc)
                        {
                            case "1":
                                luz[2] = LigaDesliga(luz[2]);
                                break;
                            case "2":
                                umidificador[2] = LigaDesliga(umidificador[2]);
                                break;
                            case "3":
                                luz[2] = LigaDesliga(luz[2]);
                                umidificador[2] = LigaDesliga(umidificador[2]);
                                break;
                            default:
                                Console.WriteLine("Opção não valida");
                                break;
                        }
                        InformacoesAmbiente(luz[2], umidificador[2]);

                        escolhaValida = false;

                        break;
                    case "4":
                        ambienteEscolha = "COZINHA";
                        InformacoesAmbiente(luz[3], umidificador[3]);
                        opc = menuLigaDesliga();
                        switch (opc)
                        {
                            case "1":
                                luz[3] = LigaDesliga(luz[3]);
                                break;
                            case "2":
                                umidificador[3] = LigaDesliga(umidificador[3]);
                                break;
                            case "3":
                                luz[3] = LigaDesliga(luz[3]);
                                umidificador[3] = LigaDesliga(umidificador[3]);
                                break;
                            default:
                                Console.WriteLine("Opção não valida");
                                break;
                        }
                        InformacoesAmbiente(luz[3], umidificador[3]);

                        escolhaValida = false;

                        break;

                    default:

                        Console.WriteLine("Opção inválida. Tente novamente.");
                        escolhaValida = true;
                        break;
                }
            }

        }

        public static void InformacoesAmbiente(bool luz, bool umidificador)
        {

            if (luz == false && umidificador == false)
            {
                Console.WriteLine("Luz: Apagada"
                    + "\nUmidificador: Desligado\n");
            }
            else if (luz == true && umidificador == false)
            {
                Console.WriteLine("Luz: Aceso"
             + "\nUmidificador: Desligado\n");
            }
            else if (luz == true && umidificador == true)
            {
                Console.WriteLine("Luz: Aceso"
             + "\nUmidificador: Ativo\n");
            }
            else
            {
                Console.WriteLine("Luz: Apagado"
        + "\nUmidificador: Ativo\n");

            }
        }

        public static string menuLigaDesliga()
        {
            string opc;
            Console.WriteLine("ESCOLHA O QUE DESEJA LIGAR OU DESLIGAR");
            Console.WriteLine("1 - LUZ - Lig/Desl \n2 - UMIDIFICADOR \n3 - TUDO");
            opc = Console.ReadLine();
            return opc;
        }
        public static bool LigaDesliga(bool sensor)
        {
            if (sensor == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
