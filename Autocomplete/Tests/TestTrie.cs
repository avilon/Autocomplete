using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomplete.Library;
using NUnit.Framework;

namespace Autocomplete.Tests
{
    /// <summary>
    /// Экспресс-тест дерева
    /// </summary>
    /// <remarks>
    /// TODO: Путь до тестового файла перенести в конфиг приложения
    /// </remarks>
    [TestFixture]
    public class TestTrie
    {
        [Test]
        public void TestSearch()
        {
            TrieLoader tl = new TrieLoader();
            using (StreamReader sr = new StreamReader(@"C:\AA\\Frisby\test.in"))
            {
                Trie<DictItem> trie = tl.Load(sr);
                Assert.IsTrue(trie.Count > 0);
            }
        }
    }
}
