using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Autocomplete.Tests
{
    [TestFixture]
    public class TestWordLoader
    {
        /// <summary>
        /// проверим заполнение дерева на тестовом файле ( 100000 строк )
        /// </summary>
        [Test]
        public void TestLoadTrie()
        {
            WordLoader wl = new WordLoader();            
            WordContainer wc = wl.Load(@"C:\AA\\Frisby\test.in");
            Assert.AreEqual(100000, wc.Trie.Count);
        }
    }
}
