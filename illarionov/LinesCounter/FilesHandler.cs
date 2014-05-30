using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinesCounter
{
    public class FilesHandler
    {
        private readonly List<string> _filesList;

        public FilesHandler(List<string> filesList)
        {
            _filesList = filesList;
        }

        public void CountUsefullLines()
        {
            foreach (var streamReader in _filesList.Select(currentFile => new StreamReader(currentFile)))
            {
                RemoveComments(streamReader);
                streamReader.Close();
            }
        }

        private void RemoveComments(StreamReader currentReader)
        {
            while (!currentReader.EndOfStream)
            {
                var currentString = currentReader.ReadLine();
                currentString = currentString.Trim();
                if (currentString.StartsWith("/*") && currentString.EndsWith("*/"))
                    continue;
                if (currentString.Equals(String.Empty) || currentString.StartsWith("//") && _isEntry == true)
                    continue;
                var start = currentString.IndexOf("/*", System.StringComparison.Ordinal);
                if (-1 != start)
                {
                    _isEntry = true;
                    continue;
                }
                
                if (_isEntry && currentString.EndsWith("*/"))
                {
                    _isEntry = false;
                    continue;
                }
                var end = currentString.IndexOf("*/", System.StringComparison.Ordinal);
                if (_isEntry && end != -1)
                {
                    _isEntry = false;
                    _lines++;
                    continue;
                }
                if (_isEntry)
                    continue;
                _lines++;
            }


            /*
            var text = new ArrayList();
            while (!currentReader.EndOfStream)
            {
                var currentString = currentReader.ReadLine();
                currentString = currentString.Trim();
                if (currentString.StartsWith("\n") || currentString.StartsWith("//"))
                    _lines--;
                text.Add(currentString);
            }
            foreach (string str in text)
            {
                var m = _re.Match(str);
                if (m.Success)
                    _lines--;
            }
            _lines += text.Count;
*/

        }

        private bool _isEntry = false;
        readonly Regex _re = new Regex(@"(/\*.*\*/)|(/\*.*)|(.*\*/)");
        private int _lines;
        public int Lines
        {
            get { return _lines; }
        }
    }
}
