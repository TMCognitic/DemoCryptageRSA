using System;
using System.IO;
using ToolBox.Cryptography;

namespace KeysGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ICryptoRSA crypto = new CryptoRSA(KeySize:KeySizes.RSA2048);

            File.WriteAllBytes("Keys.bin", crypto.BinaryBothKeys);
        }
    }
}
