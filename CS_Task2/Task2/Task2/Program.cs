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
                using (FileStream outputFile = new FileStream("./output.jpg", FileMode.Create))
                {
                    using (var encryptor = provider.CreateEncryptor(provider.Key, provider.IV))
                    {
                        using (var ms = new MemoryStream())
                        {
                            ms.Write(provider.IV, 0, 16);
                            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsInput = new FileStream(@"C:\Users\Adelin.Chis\Documents\ComputerSecurity\CS_Task2\Task2\Task2\photo.jpg", FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsInput.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                            }
                            outputFile.Write(ms.ToArray());


                        }
                    }
                }
            }

        }
    }
}
