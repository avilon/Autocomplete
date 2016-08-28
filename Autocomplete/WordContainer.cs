using Autocomplete.Library;

namespace Autocomplete
{
    /// <summary>
    /// Объединяет в одной структуре часотный словарь и набор слов для аводополнений
    /// </summary>
    public class WordContainer
    {
        public WordContainer()
        {
            Trie = new Trie<DictItem>();
            Words = new string[0];
        }
        public WordContainer(Trie<DictItem> trie, string[] words)
        {
            Trie = trie;
            Words = words;
        }
       
        public Trie<DictItem> Trie { get; set; }
        public string[] Words { get; set; }
    }
}
