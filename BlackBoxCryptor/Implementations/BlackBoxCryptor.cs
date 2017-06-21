using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using BlackBoxCryptor.Interfaces;
using BlackBoxCryptor.ViewModels;

namespace BlackBoxCryptor.Implementations
{
    public class BlackBoxCryptor : IBlackBoxCryptor
    {
        #region Local Variables
        private readonly ICryptoKeyHandler _keyHandler;
        #endregion

        public BlackBoxCryptor()
        {
            _keyHandler = new CryptoKeyHandler();
        }

        public string Key => _keyHandler.GetKey();

        public byte[] CryptorKey => GenerateCryptorKey();

        private byte[] GenerateCryptorKey()
        {
            return Encoding.UTF8.GetBytes(Key);
        }

        public bool Initialize(string cryptographicKey)
        {
            bool a = _keyHandler.Initialize();

            if (!a)
                return a;
            else
            {
                //set new key
                string keyResult = _keyHandler.SetKey(cryptographicKey);

                if (!string.IsNullOrWhiteSpace(keyResult))
                    return true;
                else
                    return false;
            }

        }

        public string Encrypt(string plainText, EncryptionScheme scheme)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentNullException("plainText");


            byte[] initialBytes = Encoding.UTF8.GetBytes(plainText);
            string result = string.Empty;

            switch (scheme)
            {
                case EncryptionScheme.AES:
                    result = AESFunction(initialBytes, CryptorAction.Encrypt);
                    break;
                case EncryptionScheme.RSA:
                    result = RSAFunction(initialBytes, CryptorAction.Encrypt);
                    break;
                case EncryptionScheme.TripleDES:
                    result = TripleDESFunction(initialBytes, CryptorAction.Encrypt);
                    break;
                default:
                    break;
            }

            return result;
        }

       

        public string Decrypt(string cipherText, EncryptionScheme scheme)
        {
            if (string.IsNullOrWhiteSpace(cipherText))
                throw new ArgumentNullException("cipherText");


            //convert from base64
            byte[] initial = Convert.FromBase64String(cipherText);
            string result = string.Empty;


            //switch schemes
            switch (scheme)
            {
                case EncryptionScheme.AES:
                    break;
                case EncryptionScheme.RSA:
                    break;
                case EncryptionScheme.TripleDES:
                    result = TripleDESFunction(initial, CryptorAction.Decrypt);
                    break;
                default:
                    break;
            }

            return result;
        }






        #region Cryptography at work

        #region Algorithms i.e AES, RSA, TripleDES
        //AES
        private string AESFunction(byte[] initialBytes, CryptorAction action)
        {
            Aes aes = Aes.Create();

            //set key
            aes.Key = CryptorKey;
            //operation mode
            aes.Mode = CipherMode.ECB;
            //padding mode
            aes.Padding = PaddingMode.PKCS7;


            string result = string.Empty;

            switch (action)
            {
                case CryptorAction.Encrypt:
                    result = SymmetricEncryption(aes, initialBytes);
                    break;
                case CryptorAction.Decrypt:
                    result = SymmetricDecryption(aes, initialBytes);
                    break;
                default:
                    break;
            }

            return result;
        }
        //RSA
        private string RSAFunction(byte[] input, CryptorAction action)
        {
            throw new NotImplementedException();
        }
        //TripleDES
        private string TripleDESFunction(byte[] input, CryptorAction action)
        {
            TripleDES tDes = TripleDES.Create();

            //set secret key
            tDes.Key = CryptorKey;
            //mode of operation
            tDes.Mode = CipherMode.ECB;
            //padding mode
            tDes.Padding = PaddingMode.PKCS7;

            string result = string.Empty;

            switch (action)
            {
                case CryptorAction.Encrypt:
                    result = SymmetricEncryption(tDes, input);
                    break;
                case CryptorAction.Decrypt:
                    result = SymmetricDecryption(tDes, input);
                    break;
                default:
                    break;
            }

            return result;
        }
        #endregion

        #region Symmetric Algorithms Encryption & Decryption
        //encryption
        private string SymmetricEncryption(SymmetricAlgorithm algorithm, byte[] data)
        {
            byte[] encryptedBytes = TransformSymmetric(data, algorithm, CryptorAction.Encrypt);

            return Convert.ToBase64String(encryptedBytes);
        }
        //decryption
        private string SymmetricDecryption(SymmetricAlgorithm algorithm, byte[] data)
        {
            byte[] decryptedBytes = TransformSymmetric(data, algorithm, CryptorAction.Decrypt);

            return Encoding.UTF8.GetString(decryptedBytes);
        }
        //transform
        private byte[] TransformSymmetric(byte[] data, SymmetricAlgorithm algorithm, CryptorAction action)
        {
            ICryptoTransform _transform = null;


            switch (action)
            {
                case CryptorAction.Encrypt:
                    _transform = algorithm.CreateEncryptor();
                    break;
                case CryptorAction.Decrypt:
                    _transform = algorithm.CreateDecryptor();
                    break;
                default:
                    break;
            }


            byte[] transformResult = _transform.TransformFinalBlock(data, 0, data.Length);

            //release resources used by algorithm
            algorithm.Dispose();

            return transformResult;
        }

        #endregion


        #region Asymmetric Algorithms Encryption & Decryption
        //not yet implemented
        #endregion

        #endregion
    }
}
