using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public static class CryptographyService
    {
        public static Stopwatch EncryptTime { get; } = new Stopwatch();
        public static Stopwatch DecryptTime { get; } = new Stopwatch();

        private static SymmetricAlgorithm _algorithm = DES.Create();

        public static void SetSymmetricAlgorithm(SymmetricAlgorithmEnum algorithm)
        {
            switch(algorithm)
            {
                case SymmetricAlgorithmEnum.DES:
                    _algorithm = DES.Create();
                    break;
                case SymmetricAlgorithmEnum.TripleDES:
                    _algorithm = TripleDES.Create();
                    break;
                case SymmetricAlgorithmEnum.Rijndael:
                    _algorithm = Rijndael.Create();
                    break;
            }
            InitKeyAndIV();


        }
        public static void SetSymmetricAlgorithm(string algorithm)
        {
            SymmetricAlgorithmEnum alg;
            if (!Enum.TryParse(algorithm, out alg))
                return;

            SetSymmetricAlgorithm(alg);
        }

        public static (byte[] key, byte[] iv) InitKeyAndIV()
        {
            _algorithm.GenerateIV();
            _algorithm.GenerateKey();

            return (_algorithm.Key, _algorithm.IV);
        }

        public static void SetKeyAndIV(byte[] key, byte[] iv)
        {
            _algorithm.Key = key;
            _algorithm.IV = iv;
        }

        public static byte[] Encrypt(byte[] mess)
        {
            EncryptTime.Reset();
            EncryptTime.Start();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, _algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(mess, 0, mess.Length);
            cs.Close();
            EncryptTime.Stop();
            var encrypted = ms.ToArray();
            return encrypted;
        }

        public static byte[] Decrypt(byte[] mess)
        {
            DecryptTime.Reset();
            DecryptTime.Start();

            var plaintext = new byte[mess.Length];
            MemoryStream ms = new MemoryStream(mess);
            CryptoStream cs = new CryptoStream(ms, _algorithm.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(plaintext, 0, mess.Length);
            cs.Close();
            DecryptTime.Stop();
            return plaintext;
        }
    }
}
