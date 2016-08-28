using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete.Library
{
    /// <summary>
    /// Реализация поведения IAutocompletable
    /// </summary>
    public class Autocomplete : IAutocopletable
    {
        public DictItem[] Items(string prefix)
        {
            if (_cache.ContainsKey(prefix))
                return _cache[prefix];

            DictItem[] dictItems = _algorithm(prefix, Trie).ToArray();
            _cache.Add(prefix, dictItems);

            return dictItems;
        }

        public Trie<DictItem> Trie { get; private set; }
        public Autocomplete(Trie<DictItem> trie, int itemCount = 10)
        {

            _itemCount = itemCount;
            Trie = trie;
            _cache = new Dictionary<string, DictItem[]>();
            _algorithm =
                (key, tr) => tr.GetPrefixNode(key).OrderByDescending(di => di.Rating).ThenBy(di => di.Word).Take(_itemCount);
        }

        private readonly Dictionary<string, DictItem[]> _cache;
        private readonly Func<string, Trie<DictItem>, IEnumerable<DictItem>> _algorithm;
        private readonly int _itemCount;
    }
}
