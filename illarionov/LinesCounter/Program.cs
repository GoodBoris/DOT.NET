using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinesCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            var argumentsParser = new ArgumentsParser(args);
            argumentsParser.GetNecessaryFiles();
            var filesHandler = new FilesHandler(argumentsParser.NecessaryFiles);
            filesHandler.CountUsefullLines();
            Console.WriteLine(filesHandler.Lines);
        }
    }
}
