using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomplete.Library;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Autocomplete;

namespace Autocomplete.Tests
{
    /// <summary>
    /// Тест загрузчика словаря
    /// </summary>
    /// <remarks>
    /// TODO: Путь до тестового файла перенести в конфиг приложения
    /// </remarks>
    [TestFixture]
    public class TestAutocomplete
    {
        [Test]
        public void TestReadWords()
        {
            string fileName = @"C:\AA\Frisby\test.in";
            TrieLoader trieLoader = new TrieLoader();
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    Trie<DictItem> trie = trieLoader.Load(reader);
                    Library.Autocomplete autocomplete = new Library.Autocomplete(trie);

                    DictItem[] items = autocomplete.Items("cbc");
                    Assert.AreEqual(10, items.Length);
                }
            }
        }
    }
}
