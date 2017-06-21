using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackBoxCryptor.ViewModels;
using BlackBoxCryptor.Interfaces;
using BlackBoxCryptor.Implementations;

namespace BlackBoxCryptor.Tests
{
    [TestClass]
    public class ConfigTester
    {
        private readonly ICryptoKeyHandler _keyHandler = new CryptoKeyHandler();

        [TestMethod]
        public void OpenConfigTest()
        {
            bool result = _keyHandler.Initialize();

            Assert.IsTrue(result);
        }
    }
}
