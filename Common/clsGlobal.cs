using System.Security.Cryptography;
using System.Text;

namespace ProjectPlanner.Common
{
    public class clsGlobal
    {
        //private static readonly string Key = "MySuperSecretKey123"; // 16, 24 or 32 chars for AES
        //private static readonly string IV = "MySecretIV1234567";    // 16 chars for AES

        //public string Encrypt(string plainText)
        //{
        //    using Aes aes = Aes.Create();
        //    aes.Key = GenerateRandomBytes(32);
        //    aes.IV = GenerateRandomBytes(16);

        //    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        //    using MemoryStream ms = new();
        //    using CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
        //    using (StreamWriter sw = new(cs))
        //    {
        //        sw.Write(plainText);
        //    }

        //    return Convert.ToBase64String(ms.ToArray());
        //}

        //public string Decrypt(string cipherText)
        //{
        //    using Aes aes = Aes.Create();
        //    aes.Key = GenerateRandomBytes(32);
        //    aes.IV = GenerateRandomBytes(16);

        //    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        //    byte[] buffer = Convert.FromBase64String(cipherText);

        //    using MemoryStream ms = new(buffer);
        //    using CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read);
        //    using StreamReader sr = new(cs);
        //    return sr.ReadToEnd();
        //}


        //public static byte[] GenerateRandomBytes(int length)
        //{
        //    byte[] randomBytes = new byte[length];
        //    using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        //    rng.GetBytes(randomBytes);
        //    return randomBytes;
        //}


        public string Encrypt(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public string Decrypt(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            var key = "b14ca5898a4e4133bbce2ea2315a1916";

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
