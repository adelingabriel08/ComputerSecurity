using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var provider = new AesCryptoServiceProvider())
            {
                provider.Mode = CipherMode.ECB;
                provider.GenerateKey();
                provider.GenerateIV();
                using (FileStream outputFile = new FileStream("./output", FileMode.Create))
                {
                    using (var cs = new CryptoStream(outputFile, provider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream inputFile = new FileStream(@"../../../file", FileMode.Open))
                        {
                            int data;
                            while ((data = inputFile.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }
    }
}
