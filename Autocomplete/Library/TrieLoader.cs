using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.Library
{
    /// <summary>
    /// Загружает префиксное дерево данными из файла частотого словаря.
    /// </summary>
    /// <remarks>
    /// Процесс загрузки дерева выненен в отдельный класс, 
    /// потом можно будет использовать в клиент-серверной версии приложения
    /// </remarks>
    public class TrieLoader
    {
        public Trie<DictItem> Load(StreamReader reader)
        {
            Trie<DictItem> trie = new Trie<DictItem>();

            string line = reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
                throw new Exception("First file line must contain number of dictionary words");

            int n = int.Parse(line);
            for (int i = 0; i < n; i++)
            {
                // В первой строке д.б. записано общее количество строк словаря
                line = reader.ReadLine();
                if (string.IsNullOrEmpty(line))
                    throw new Exception($"Bad file format: empty string in position {i}");

                string[] lineSplit = line.Split(' ');

                string word = lineSplit[0];

                // Длина слова должна находится в разумных пределах.................
                if (word.Length < MIN_WORD_LEN || word.Length > MAX_WORD_LEN)
                    throw new Exception("Bad word length");

                int rating = int.Parse(lineSplit[1]);
                // Частота слова попалает в заданные границы ?
                if (rating < MIN_RATING || rating > MAX_RATING)
                    throw new Exception("Suspicious rating... (WADA???)");

                DictItem dictItem = new DictItem(word, rating);
                trie.Insert(dictItem.Word, dictItem);
            }

            return trie;
        }

        // граничные условия на длину и рейтинг
        private static readonly int MIN_WORD_LEN = 1;
        private static readonly int MAX_WORD_LEN = 15;
        private static readonly int MIN_RATING = 1;
        private static readonly int MAX_RATING = 100000;
    }
}
