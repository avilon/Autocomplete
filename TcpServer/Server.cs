using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Autocomplete.Library;

namespace TcpServer
{
    /// <summary>
    /// Реализация функционала серверной части
    /// </summary>
    public class Server
    {
        public Server(int port, string fileName)
        {
            _port = port;
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _fileName = fileName;
        }

        /// <summary>
        /// Запуск сервера с default-параметрами - 
        /// </summary>
        public Server()
        {
            _port = 11000;
            _tcpListener = new TcpListener(IPAddress.Any, DEFAULT_PORT);
            _fileName = "test.in";
        }

        /// <summary>
        /// Запуск сервера
        /// </summary>
        /// <returns></returns>
        public async Task RunAsync()
        {
            CreateAutocomplete();
            _tcpListener.Start();
            Console.WriteLine("Server run (port: {0}, file = {1}) . . .", _port, _fileName);
            await WorkWithClientAsync();
        }

        /// <summary>
        /// Обслуживание клиентских пдключений
        /// </summary>
        /// <returns></returns>
        private async Task WorkWithClientAsync()
        {
            while (true)
            {
                // Ожидание подключения клиента и неблокирующая обработка запроса
                TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                ReadClientInputAsync(client);
            }
        }

        /// <summary>
        /// Обработка запроса клиента
        /// </summary>
        /// <param name="client">Клиент</param>
        /// <returns></returns>
        private async Task ReadClientInputAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                using (StreamReader sr = new StreamReader(stream, Encoding.ASCII))
                using (StreamWriter sw = new StreamWriter(stream, Encoding.ASCII))
                {
                    // Чтение запроса
                    string command = await sr.ReadLineAsync();
                    int startIndex = command.IndexOf('<');
                    int endIndex = command.IndexOf('>');
                    string prefix = command.Substring(startIndex + 1, endIndex - startIndex - 1);

                    // Обработка данных запроса
                    if (_autocomplete != null)
                    {
                        DictItem[] items = await Task.Run(() => _autocomplete.Items(prefix));
                        
                        // Запись ответа
                        foreach (DictItem item in items)
                        {
                            await sw.WriteLineAsync(item.Word);
                            await sw.FlushAsync();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Создаем объект, отвечающий за генерацию списка автодополнений
        /// </summary>
        private void CreateAutocomplete()
        {
            TrieLoader trieLoader = new TrieLoader();
            if (File.Exists(_fileName))
            {
                using (StreamReader reader = new StreamReader(_fileName))
                {
                    _trie = trieLoader.Load(reader);
                    _autocomplete = new Autocomplete.Library.Autocomplete(_trie, 1000);
                    Console.WriteLine("Dictionary load, enties: {0}", _trie.Count);
                }
            }
        }

        private int _port;
        private TcpListener _tcpListener;
        private IAutocopletable _autocomplete;
        private string _fileName;
        private Trie<DictItem> _trie;

        private static readonly int DEFAULT_PORT = 11000;
    }
}
