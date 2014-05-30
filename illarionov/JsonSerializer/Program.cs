using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var testClass = new TestClass()
            {
                i = 25, s = "Hello World", arrayMember = new int[] {1, 2, 3, 4, 5}, ignore = "Ignore"
            };
             
            using (var fs = new FileStream("SerializedObject.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    var serializer = new Serializer(testClass, sw);
                    serializer.Serialize();
                }
                fs.Dispose();
            }

        }
    }
}
