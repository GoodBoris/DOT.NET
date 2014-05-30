using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonSerializer
{
    [Serializable]
    class TestClass
    {
        public int i;
        public string s;

        [NonSerialized]
        public string ignore;

        public int[] arrayMember;

    }


}
