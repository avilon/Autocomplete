using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autocomplete.Library
{
    /// <summary>
    /// Интерфейс объекта "Автоподстановка"
    /// </summary>
    public interface IAutocopletable
    {

        /// <summary>
        /// Возвращает массив с вариантами дополнений 
        /// </summary>
        /// <param name="prefix">Начальные буквы слова</param>
        /// <returns>DictItem[]</returns>
        DictItem[] Items(string prefix);
    }
}
