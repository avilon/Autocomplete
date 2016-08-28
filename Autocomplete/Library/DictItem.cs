using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.Library
{
    /// <summary>
    /// Элемент частотного словаря
    /// </summary>
    public class DictItem
    {
        public DictItem(string word, int rating)
        {
            Word = word;
            Rating = rating;
        }

        /// <summary>
        /// Слово, содержащееся в словаре
        /// </summary>
        public string Word { get; private set; }

        /// <summary>
        /// Частота появления слова (частотный рейтинг)
        /// </summary>
        public int Rating { get; private set; }
    }
}
