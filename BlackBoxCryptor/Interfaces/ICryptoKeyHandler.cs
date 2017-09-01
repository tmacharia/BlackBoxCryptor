using System;
using System.Collections.Generic;
using System.Text;

namespace BlackBoxCryptor.Interfaces
{
    public interface ICryptoKeyHandler
    {
        /// <summary>
        /// Read all cypto handler settings and initialize required variables, Sets up
        /// enviroment with all required settings
        /// </summary>
        bool Initialize();
        /// <summary>
        /// Gets your private key to use during encryption and decryption from config file
        /// </summary>
        /// <returns>Encryption & Decryption Key</returns>
        string GetKey();
        /// <summary>
        /// Replaces default or old key with a newly provided key
        /// </summary>
        /// <param name="value">Value of the new key</param>
        /// <returns>New key as read from file</returns>
        string SetKey(string value);
    }
}
