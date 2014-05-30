using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Autofac;
using Autofac.Core;

namespace Encrypter
{
    internal class Encrypt
    {
        public Encrypt(string inputName, string outputName, AlgorithmType.Algorithm algorithm)
        {
            _algorithm = algorithm;
            CheckAcsess(inputName, outputName);
        }

        private void CheckAcsess(string inputName, string outputName)
        {
            _reader = new FileStream(inputName, FileMode.Open, FileAccess.Read);
            _writer = new FileStream(outputName, FileMode.OpenOrCreate, FileAccess.Write);
            _writerKey = new FileStream("key.key", FileMode.OpenOrCreate, FileAccess.Write);
            MakeEncrypt();
        }

        private void MakeEncrypt()
        {
            var autofc = new Autofc();
            _container = autofc.Container;
            var currentAlgorithm = _container.ResolveKeyed<SymmetricAlgorithm>(_algorithm);
            //var currentAlgorithm = new AesCryptoServiceProvider();
            _key = currentAlgorithm.Key;
            _iv = currentAlgorithm.IV;
            WriteKey();
            var encryptor = currentAlgorithm.CreateEncryptor(_key, _iv);
            using (var cryptoStream = new CryptoStream(_writer, encryptor, CryptoStreamMode.Write))
            {
                _reader.CopyTo(cryptoStream);
            }
            _reader.Dispose();
            _writer.Dispose();

        }

        private void WriteKey()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(Convert.ToBase64String(_iv));
            stringBuilder.AppendLine(Convert.ToBase64String(_key));
            var keyPath = stringBuilder.ToString();
            using (var streamWriter = new StreamWriter(_writerKey))
            {
                streamWriter.Write(keyPath);
            }
            _writerKey.Dispose();

        }

        private byte[] _key;
        private byte[] _iv;
        private IContainer _container;
        private readonly AlgorithmType.Algorithm _algorithm;
        private Stream _reader = null;
        private Stream _writer, _writerKey = null;
    }


}

