using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace CS_Week5
{
    class Program
    {
        public static Stopwatch timer = new Stopwatch();
        static void Main(string[] args)
        {
            //key creation
            Console.WriteLine("Enter the number of bits for the key:");
            var bits = Console.ReadLine();

            Generate(int.Parse(bits));
        }



        public static void Generate(int bits)
        {
            RSACryptoServiceProvider myrsa = new RSACryptoServiceProvider(1600);
            int size;
            int count = 100;
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                myrsa = new RSACryptoServiceProvider(1024);
                size = myrsa.KeySize;
            }
            timer.Stop();
            Console.WriteLine($"({bits})Key creation: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");


            byte[] plain = new byte[20];
            byte[] ciphertext = myrsa.Encrypt(plain, true);
            timer.Reset();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                ciphertext = myrsa.Encrypt(plain, true);
            }
            timer.Stop();
            Console.WriteLine($"({bits})Encryption: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");


            timer.Reset();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                plain = myrsa.Decrypt(ciphertext, true);
            }
            timer.Stop();
            Console.WriteLine($"({bits})Decryption: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");


            SHA256Managed myHash = new SHA256Managed();
            string some_text = "this is a message to be signed";
            //sign the message
            byte[] signature = { };
            timer.Reset();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                signature = myrsa.SignData(Encoding.ASCII.GetBytes(some_text), myHash);
            }
            timer.Stop();
            Console.WriteLine($"({bits})Signing: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");



            timer.Reset();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                myrsa.VerifyData(Encoding.ASCII.GetBytes(some_text), myHash, signature);
            }
            timer.Stop();
            Console.WriteLine($"({bits})Verifying signature: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");

        }
    }
}
