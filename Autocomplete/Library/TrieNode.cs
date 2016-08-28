using System;
using System.Collections;
using System.Collections.Generic;

namespace Autocomplete.Library
{
    /// <summary>
    /// Узел префиксного дерева Trie
    /// </summary>
    public class TrieNode<T> : IEnumerable<T>
    {
        public TrieNode()
        {
            _childs = new Dictionary<char, TrieNode<T>>();
        }

        /// <summary>
        /// Флаг, сигналиирующий о том, что узел является конечным
        /// </summary>
        public bool IsComplete { get; private set; }

        /// <summary>
        /// Добавляет ключ по заданному узлу
        /// </summary>
        /// <param name="key">Знчение ключа, соответствующее узлу</param>
        /// <param name="position">Текущая позиция</param>
        /// <param name="value">Значение узла</param>
        public void InsertNode(string key, int position, T value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key is null");
            }

            if (position == key.Length)
            {
                _value = value;
                IsComplete = true;
                return;
            }

            char label = key[position];
            if (_childs.ContainsKey(label))
                _childs[label].InsertNode(key, ++position, value);
            else
            {
                var child = new TrieNode<T>();
                _childs.Add(label, child);
                child.InsertNode(key, ++position, value);
            }
        }

        public IEnumerable<T> GetAllNodesByPrefix(string prefix)
        {
            if (prefix == null)
                throw new ArgumentNullException("prefix");

            TrieNode<T> prefixKeyNode = GetNodeByPrefix(prefix, 0);

            if (prefixKeyNode != null)
                foreach (var value in prefixKeyNode)
                    yield return value;
        }

        private TrieNode<T> GetNodeByPrefix(string key, int position)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (position == key.Length)
                return this;

            char label = key[position];
            if (_childs.ContainsKey(label))
                return _childs[label].GetNodeByPrefix(key, ++position);

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsComplete)
                yield return _value;

            foreach (var childPair in _childs)
                foreach (var child in childPair.Value)
                    yield return child;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private T _value;
        private readonly IDictionary<char, TrieNode<T>> _childs;

    }
}
