using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializer
{
    class Serializer
    {
        public Serializer(object obj, StreamWriter streamWriter)
        {
            _obj = obj;
            _streamWriter = streamWriter;
        }

        public void Serialize()
        {
            if (!_obj.GetType().IsClass)
            {
                Console.WriteLine("It's not a class!");
                throw new Exception("This object isn't serializeble!");
                return;
            }

            if (!_obj.GetType().IsSerializable)
            {
                Console.WriteLine("I can't serialize this class!");
                throw new Exception("This class isn't serializable!");
                return;    
            }
            _streamWriter.WriteLine(_obj.ToString());
            Console.WriteLine(_obj.ToString());
            _streamWriter.WriteLine("{");
            Console.WriteLine("{");
            foreach (var field in _obj.GetType().GetFields())
            {
                if (field.IsNotSerialized)
                    continue;
                if (field.FieldType.IsArray)
                {
                    SortOutTheArray(field);
                    continue;
                }
                var row = new StringBuilder();
                row.Append(field.FieldType.ToString()+ " ");
                row.Append(field.Name + " = ");
                row.AppendLine(field.GetValue(_obj).ToString());
                _streamWriter.WriteLine(row);
                Console.WriteLine(row);
            }

            _streamWriter.Write("}");
            Console.Write("}");
            

        }

        private void SortOutTheArray(FieldInfo obj_array)
        {

            var array =  obj_array.GetValue(_obj) as Array;
            var row = new StringBuilder();
            row.Append(obj_array.FieldType.ToString() + " ");
            row.Append(obj_array.Name + " = ");
            for (int i = 0; i < array.Length; ++i)
            {
                row.Append("[" + array.GetValue(i) + "]" + " ");
            }
            //row.AppendLine(obj_array.GetValue(_obj).ToString());
            row.AppendLine(String.Empty);
            _streamWriter.WriteLine(row);
            Console.WriteLine(row);
        }

        private readonly Object _obj;
        private readonly StreamWriter _streamWriter;

        
    }
}
