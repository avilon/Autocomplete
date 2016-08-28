using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] arg)
        {
            try
            {
                string fileName;
                int portNumber;
                    
                if (arg.Length > 0)
                    fileName = arg[0];
                else
                    fileName = "test.in";

                if (arg.Length > 1)
                    portNumber = int.Parse(arg[1]);
                else
                    portNumber = 11000;

                //Config.InitConfig(file, portNumber);

                Server server = new Server(portNumber, fileName);

                server.RunAsync().Wait();
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
    }
}
