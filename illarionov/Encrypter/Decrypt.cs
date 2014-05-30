using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Encrypter
{
    class Decrypt
    {
        public Decrypt(string inputName, string outputName, string keyName, AlgorithmType.Algorithm algorithm)
        {
            _algorithm = algorithm;
            CheckAcsess(inputName, outputName, keyName);

        }

        private void CheckAcsess(string inputName, string outputName, string keyName)
        {
            _reader = new FileStream(inputName, FileMode.Open, FileAccess.Read);
            _readerKey = new FileStream(keyName, FileMode.Open, FileAccess.Read);
            _writer = new FileStream(outputName, FileMode.OpenOrCreate, FileAccess.Write);
            
            MakeDycrypt();
        }

        private void MakeDycrypt()
        {

            var autofc = new Autofc();
            _container = autofc.Container;
            var currentAlgorithm = _container.ResolveKeyed<SymmetricAlgorithm>(_algorithm);
            currentAlgorithm.Padding = PaddingMode.None;
            //var currentAlgorithm = new AesCryptoServiceProvider {Padding = PaddingMode.None};
            ReadKey();
            var decryptor = currentAlgorithm.CreateDecryptor(_key, _iv);
            using (var cryptoStream = new CryptoStream(_reader, decryptor, CryptoStreamMode.Read))
            {
                cryptoStream.CopyTo(_writer);
            }
            _reader.Dispose();
            _writer.Dispose();
        }

        private void ReadKey()
        {
            {
                string ivLine;
                string keyLine;


                using (var streamReader = new StreamReader(_readerKey))
                {
                    ivLine = streamReader.ReadLine();
                    keyLine = streamReader.ReadLine();
                }


                Debug.Assert(ivLine != null, "ivLine != null");
                _iv = Convert.FromBase64String(ivLine.TrimEnd(new Char[] { ' ' }));
                Debug.Assert(keyLine != null, "keyLine != null");
                _key = Convert.FromBase64String(keyLine.TrimEnd(new Char[] { ' ' }));
            }
            _readerKey.Dispose();
    
        }

        private byte[] _key;
        private byte[] _iv;
        private Stream _reader = null;
        private Stream _writer = null;
        private Stream _readerKey = null;
        private IContainer _container;
        private AlgorithmType.Algorithm _algorithm;
    }
}
