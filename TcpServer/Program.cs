using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.LayoutRenderers;

namespace TcpServer
{
    /// <summary>
    /// Запуск TCP-сервера.
    /// </summary>
    /// <remarks>
    /// Без параметров читает файл test.in в текущем каталоге и прослушивает порт 11000
    /// Номер порта можно передать в первом параметре, имя файла - во втором
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string fileName = "test.in";
                int portNumber = 11000;

                if (args.Length > 0)
                    portNumber = int.Parse(args[0]);
                if (args.Length > 1)
                    fileName = args[1];

                Server server = new Server(portNumber, fileName);

                if (File.Exists(fileName))
                {
                    server.RunAsync().Wait();
                }
                else
                {
                    logger.Error("File {0} not found, server not run", fileName);
                }
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

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
