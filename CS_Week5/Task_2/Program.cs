using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Task_2
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
            DSACryptoServiceProvider myrsa = new DSACryptoServiceProvider(512);
            int size;
            int count = 100;
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                myrsa = new DSACryptoServiceProvider(1024);
                size = myrsa.KeySize;
            }
            timer.Stop();
            Console.WriteLine($"({bits})Key creation: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");



            SHA256Managed myHash = new SHA256Managed();
            string some_text = "this is a message to be signed";
            //sign the message
            byte[] signature = { };
            timer.Reset();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                signature = myrsa.SignData(Encoding.ASCII.GetBytes(some_text));
            }
            timer.Stop();
            Console.WriteLine($"({bits})Signing: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");



            timer.Reset();
            timer.Start();
            for (int i = 0; i < count; i++)
            {
                myrsa.VerifyData(Encoding.ASCII.GetBytes(some_text), signature);
            }
            timer.Stop();
            Console.WriteLine($"({bits})Verifying signature: " + (timer.ElapsedTicks / (10 * count)).ToString() + " ms");

        }
    }
}
