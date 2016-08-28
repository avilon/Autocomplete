using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autocomplete.Library;
using NLog;

namespace Autocomplete
{
    /// <summary>
    /// Выполнение тестового задания.
    /// </summary>
    public class Runner
    {
        public Runner(string fileName)
        {
            if (!File.Exists(fileName))
            {
                logger.Error("Test file {0} not found...", fileName);     
            }

            logger.Info("Runner start");

            logger.Debug("Create dictionary");
            _wordLoader = new WordLoader();
            _wordContainer = _wordLoader.Load(fileName);
            _outFile = Path.ChangeExtension(fileName, ".out");
        }

        public void Execute()
        {
            Library.Autocomplete a = new Library.Autocomplete(_wordContainer.Trie);
            logger.Debug("Start execution");
            using (StreamWriter sw = new StreamWriter(_outFile))
            {
                foreach (string word in _wordContainer.Words)
                {
                    DictItem[] items = a.Items(word);
                    foreach (DictItem item in items)
                    {
                        sw.WriteLine(item.Word);
                    }

                    sw.WriteLine();
                }
            }
            logger.Debug("End execution");
        }

        private WordLoader _wordLoader;
        private WordContainer _wordContainer;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string _outFile;
    }
}
