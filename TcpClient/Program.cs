using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using NLog;

namespace TcpClient
{
    /// <summary>
    /// Реализация клиентской части
    /// </summary>
    /// <remarks>
    /// Если запускать без параметров, то выполняется попытка подключения к localhost:11000
    /// При задании параметров имя хоста передается в первом, порт - во второмпараметре:
    /// TcpClient host_name port_number
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string serverHost;
                int port;

                // Если запуск без параметров, то выставляем значения по дефолту
                if (args.Length == 0)
                {
                    serverHost = "127.0.0.1";
                    port = 11000;
                }
                else
                {
                    serverHost = args[0];
                    port = int.Parse(args[1]);
                }

                WorkWithServerAsync(serverHost, port).Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var innerException in ae.Flatten().InnerExceptions)
                    Console.WriteLine(innerException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async static Task WorkWithServerAsync(string serverHost, int port)
        {
            while (true)
            {
                using (System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient(serverHost, port))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        Console.Write("get ");
                        string prefix = Console.ReadLine();
                        string command = $"get <{prefix}>";

                        logger.Info("get <{0}>", prefix);

                        using (StreamReader sr = new StreamReader(stream, Encoding.ASCII))
                        using (StreamWriter sw = new StreamWriter(stream, Encoding.ASCII))
                        {
                            await sw.WriteLineAsync(command);
                            await sw.FlushAsync();

                            while (!sr.EndOfStream)
                            {
                                string s = await sr.ReadLineAsync();
                                Console.WriteLine(s);
                                logger.Info(s);
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
