using System;
using System.Collections.Generic;
using System.IO;
using NLog;
 
namespace Autocomplete
{
    /// <summary>
    /// Запуск тестовой задачи на выполнение
    /// </summary>
    /// <remarks>
    /// пример командной строки: Autocomplete.exe test.in
    /// В качестве параметра указывается путь до файла с частотным словарем и набором слов.
    /// В случае запуска без параметров, выполняется поиск файла test.in в текущем каталоге.
    /// 
    /// Если файл не удается обнаружить, программа завершается с соответствующей записью в логе выполнения.
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "test.in";
            if (args.Length > 0)
            {
                fileName = args[0];
            }

            if (File.Exists(fileName))
            {
                    logger.Debug("=========================");
                    Runner runner = new Runner(fileName);
                    runner.Execute();
                    logger.Debug("=========================");
            }
            else
            {
                logger.Error("File {0} not found", fileName);
            }
        }

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    }
}
