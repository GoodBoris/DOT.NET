using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Encrypter
{
    class ArgumentsParser : AlgorithmType
    {
        public ArgumentsParser(IEnumerable<string> args)
        {
            _argsList = new List<string>(args);
            CheckParameters();
        }

        private void CheckParameters()
        {

            
            if (0 == _argsList.Count)
                throw new Exception("No arguments was given");
            switch (_argsList[0])
            {
                case "encrypt":
                    if (4 != _argsList.Count)
                        throw new Exception("Uncorrect arguments!");
                    state = State.Encrypt;
                    
                    break;
                case "decrypt":
                    if (5 != _argsList.Count)
                        throw new Exception("Uncorrect arguments!");
                    state = State.Decrypt;
                    break;
                default:
                    throw new Exception("Uncorrect arguments!");
            }

            switch (_argsList[2])
            {
                case "aes":
                    base.algorithm = AlgorithmType.Algorithm.aes;
                    break;
                case "des":
                    base.algorithm = AlgorithmType.Algorithm.des;
                    break;
                case "rc2":
                    base.algorithm = AlgorithmType.Algorithm.rc2;
                    break;
                case "rijndael":
                    base.algorithm = AlgorithmType.Algorithm.rijndael;
                    break;
                default:
                    throw new Exception("Unknown algorithm!");
                    
            }

            switch (state)
            {
                case State.Encrypt:
                    var encrypt = new Encrypt(_argsList[1], _argsList[3], base.algorithm);
                    break;
                case State.Decrypt:
                    var decrypt = new Decrypt(_argsList[1], _argsList[4],  _argsList[3], base.algorithm );
                    break;
            }
        }

        private enum State
        {
            Encrypt, Decrypt
        }

        private State state;

        private readonly List<string> _argsList;
    }
}
