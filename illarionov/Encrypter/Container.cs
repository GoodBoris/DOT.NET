using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Encrypter
{
    class Autofc
    {
        public Autofc()
        {
            InitialContainer();
        }

        private void InitialContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AesCryptoServiceProvider>().Keyed<SymmetricAlgorithm>(AlgorithmType.Algorithm.aes);
            builder.RegisterType<RC2CryptoServiceProvider>().Keyed<SymmetricAlgorithm>(AlgorithmType.Algorithm.rc2);
            builder.RegisterType<RijndaelManaged>().Keyed<SymmetricAlgorithm>(AlgorithmType.Algorithm.rijndael);
            builder.RegisterType<DESCryptoServiceProvider>().Keyed<SymmetricAlgorithm>(AlgorithmType.Algorithm.des);
            var container = builder.Build();
            Container = container;
        }

        public IContainer Container { get; private set; }
    }
}
