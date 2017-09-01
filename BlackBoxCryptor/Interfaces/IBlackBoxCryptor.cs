using BlackBoxCryptor.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackBoxCryptor.Interfaces
{
    public interface IBlackBoxCryptor
    {
        /// <summary>
        /// Current key in use as a string
        /// </summary>
        string Key { get; }
        /// <summary>
        /// Byte array of the current key
        /// </summary>
        byte[] CryptorKey { get; }
        /// <summary>
        /// Takes passed plaintext and passes it through algorithms specified by the
        /// encryption scheme and produces a ciphertext encrypted by the current
        /// private key in use for encryption & Decryption.
        /// 
        /// 
        /// 
        /// N.B Current version only supports Symmetric Encryption & Decryption, asymmetric
        /// support is comming soon.
        /// </summary>
        /// <param name="plainText">Plain word(s) from the user e.g Hello Bob</param>
        /// <param name="scheme">Algorithm to use in encrypting data e.g TipleDES, RSA</param>
        /// <returns>Resultant ciphertext in base64 format</returns>
        string Encrypt(string plainText, EncryptionScheme scheme);
        /// <summary>
        /// Re-constructs ciphertext to human readable plaintext. 
        /// 
        /// N.B //Remember to use the same scheme to decrypt data as used
        /// in encryption of the data otherwise decryption will fail.
        /// 
        /// 
        /// 
        /// N.B Current version only supports Symmetric Encryption & Decryption, asymmetric
        /// support is comming soon.
        /// <param name="cipherText">Non-human readable ciphertext in base64 format</param>
        /// <param name="scheme">Cryptographic algorithm used in encrypting 
        /// the initial data
        /// </param>
        /// <returns>Plaintext in human readable format.</returns>
        string Decrypt(string cipherText, EncryptionScheme scheme);
    }
}
