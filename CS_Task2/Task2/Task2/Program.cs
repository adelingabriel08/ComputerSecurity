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
                    using (var encryptor = provider.CreateEncryptor(provider.Key, provider.IV))
                    {
                        outputFile.Write(provider.IV, 0, 16);
                        using (var cs = new CryptoStream(outputFile, encryptor, CryptoStreamMode.Write))
                        {
                            using (FileStream fsInput = new FileStream(@"../../../photo.jpg", FileMode.Open))
                            {
                                int data;
                                while ((data = fsInput.ReadByte()) != -1)
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
}
