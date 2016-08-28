using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomplete.Library;
using NLog;

namespace Autocomplete
{
    /// <summary>
    /// Загрузка частотного словаря в объект Trie и списка тестовых строк в массив.
    /// </summary>
    /// <remarks>
    /// Загруженные данные упаковываются в общую структуру - WordContainer - 
    /// и возвращаются вызывающему коду
    /// </remarks>
    public class WordLoader
    {
        public WordContainer Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                logger.Error("File {0} not found", fileName);
                return new WordContainer();
            }

            using (StreamReader sr = new StreamReader(fileName))
            {
                Trie<DictItem> trie = GenerateTrie(sr);
                string[] words = GenerateWords(sr);
                return new WordContainer(trie, words);
            }
        }
        private Trie<DictItem> GenerateTrie(StreamReader reader)
        {
            TrieLoader trieLoader = new TrieLoader();
            return trieLoader.Load(reader);
        }

        private string[] GenerateWords(StreamReader reader)
        {
            string line = reader.ReadLine();
            // в первой строке д.б. общее количество строк
            if (string.IsNullOrEmpty(line))
                throw new Exception("Bad file format - not found line with number of checking words");

            int m = int.Parse(line);
            string[] words = new string[m];

            for (int i = 0; i < m; i++)
            {
                line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    throw new Exception("Неправильный формат ввода данных. Не найдена строка слова для автодополнения.");

                words[i] = line;
            }
            return words;
        }

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
