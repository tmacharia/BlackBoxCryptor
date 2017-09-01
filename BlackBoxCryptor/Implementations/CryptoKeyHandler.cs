using BlackBoxCryptor.Interfaces;
using BlackBoxCryptor.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlackBoxCryptor.Implementations
{
    public class CryptoKeyHandler : ICryptoKeyHandler
    {
        #region Local Variables
        private ConfigParser _config = new ConfigParser();
        #endregion


        public CryptoKeyHandler()
        {
            Initialize();
        }
      

        public bool Initialize()
        {
            bool readResults = _config.ReadConfig();

            return readResults;
        }

        public string GetKey()
        {
            bool x = CheckInitialization();

            if (x)
                return GetMd5Hash(_config.GetAppSetting("cryptographic_key"));
            else
                return x.ToString();
        }

        private bool CheckInitialization()
        {
            if (_config.AppSettings == null || _config.ConfigFileStream == null)
                throw new Exception("Please initialize first to prepare the library before continuing. \n\nSolution: Before getting or setting a value in settings, call the initialize method first.");
            else
                return true;
        }

        public string SetKey(string value)
        {
            bool x = CheckInitialization();

            if(x)
            {
                bool setResults = _config.ChangeSetting("cryptographic_key", value);

                if (!setResults)
                    throw new Exception("Error occured while changing your cryptographic key");



                return _config.GetAppSetting("cryptographic_key");

            }
            else
            {
                return x.ToString();
            }
        }

        private string GetMd5Hash(string key)
        {
            MD5 mD5 = MD5.Create();


            byte[] data = mD5.ComputeHash(Encoding.UTF8.GetBytes(key));

            return Convert.ToBase64String(data);
        }
    }
}
