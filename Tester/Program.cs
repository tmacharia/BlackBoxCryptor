using BlackBoxCryptor.Implementations;
using BlackBoxCryptor.Interfaces;
using System;
using System.Diagnostics;

namespace Tester
{
    class Program
    {
        private static IBlackBoxCryptor _blackBox = new BlackBox();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BlackBox Cryptor!");

            //initialize
            Initialize();

            Console.ReadKey();
        }

        private static void Initialize()
        {
            string plainTxt = "The Future of Tech";

            Console.WriteLine("PlainText -- > {0}",plainTxt);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string cipherTxt = _blackBox.Encrypt(plainTxt, BlackBoxCryptor.ViewModels.EncryptionScheme.AES);
            stopwatch.Stop();

            Console.WriteLine("CipherText -- > {0} \n=========\nEncryption in {1}\n========", cipherTxt,stopwatch.Elapsed.ToString());
        }
    }
}