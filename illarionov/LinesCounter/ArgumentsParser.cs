using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LinesCounter
{
    class ArgumentsParser
    {
        public ArgumentsParser(ICollection<string> args)
        {
            _parameters = new List<string>(args);
            if (0 == args.Count)
            {
                _parameters.Add("*.cs");
            }
            
        }

        public void GetNecessaryFiles()
        {
            foreach (var parameter in _parameters)
            {
                _necessaryFiles.AddRange(Directory.GetFiles(_currentDirectory.FullName, parameter, SearchOption.AllDirectories));    
            }

            foreach (var necessaryFile in _necessaryFiles)
            {
                Console.WriteLine(necessaryFile);
            }
        }

        private readonly DirectoryInfo _currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
        private readonly List<string> _parameters;
        private readonly List<string> _necessaryFiles = new List<string>();

        public List<string> NecessaryFiles
        {
            get { return _necessaryFiles; }
        }
    }
}
