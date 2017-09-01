using BlackBoxCryptor.Implementations;
using BlackBoxCryptor.Interfaces;
using BlackBoxCryptor.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxCryptor.Tests
{
    [TestClass]
    public class BlackBoxTester
    {
        private readonly ICryptoKeyHandler _keyHandler = new CryptoKeyHandler();
        private readonly IBlackBoxCryptor _blackBoxCryptor = new BlackBox();
        [TestMethod]
        public void InitializeKeyHandlerTest()
        {
            bool result = _keyHandler.Initialize();

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GetKeyForEncryptionTest()
        {
            string key = _keyHandler.GetKey();

            Assert.IsNotNull(key);
        }
        
        [TestMethod]
        public void EncryptionAndDecryptionTest()
        {
            string plainText = "Hello World!";
            string cipherText = _blackBoxCryptor.Encrypt(plainText, EncryptionScheme.AES);
        }
        [TestMethod]
        public void EncryptDecryptTest()
        {
            IBlackBoxCryptor cryptor = new BlackBox();

            string plainText = "The Future of Tech!";

            string cipherText = cryptor.Encrypt(plainText, ViewModels.EncryptionScheme.AES);

            //check ciphertext is not null
            Assert.IsNotNull(cipherText);

            string decryptedText = cryptor.Decrypt(cipherText, ViewModels.EncryptionScheme.AES);

            //compare if ciphertext is equal to original plainText
            Assert.AreEqual(plainText, decryptedText);
        }
    }
}
