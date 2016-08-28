using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.Library
{
    /// <summary>
    /// Префиксное дерево
    /// </summary>
    /// <typeparam name="T">Тип значения, хранящегося в узлах дерева</typeparam>
    /// <remarks>
    /// В данной реализации предусмотрен только метод вставки нового элемента и возврата всех конечных узлов.
    /// Такие действия, как редактирование/удаление узлов, поиск и выборка промежуточных элементов, на данном
    /// этапе можно пропустить.
    /// </remarks>
    public class Trie<T>
    {

        public Trie()
        {
            _root = new TrieNode<T>();
            _count = 0;
        }

        public int Count => _count;

        public void Insert(string key, T value)
        {
            _root.InsertNode(key, 0, value);
            _count++;
        }

        /// <summary>
        /// Возвращает значения всех конечных узлов, соответствующих префиксу
        /// </summary>
        /// <param name="prefix">Префикс, по которому выбираются значения</param>
        /// <returns></returns>
        public IEnumerable<T> GetPrefixNode(string prefix)
        {
            return _root.GetAllNodesByPrefix(prefix);
        }

        private readonly TrieNode<T> _root;
        private int _count;
    }

}
