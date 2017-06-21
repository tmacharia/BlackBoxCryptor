namespace BlackBoxCryptor.ViewModels
{
    public enum EncryptionScheme
    {
        /// <summary>
        /// Advanced Encyption Standard of 1998 by Vincent Rijmen and Joan Daemen
        /// </summary>
        AES,
        /// <summary>
        /// Ron Rivest, Adi Shamir & Leonard Adleman algorithm of 1977
        /// </summary>
        RSA,
        /// <summary>
        /// Triple Data Encryption Standard of 1998 by IBM
        /// </summary>
        TripleDES
    }
}
