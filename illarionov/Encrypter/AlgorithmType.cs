using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encrypter
{
    abstract class AlgorithmType
    {
        internal enum Algorithm
        {
            aes, des, rc2, rijndael
        }

        public Algorithm algorithm;
       
    }
}
