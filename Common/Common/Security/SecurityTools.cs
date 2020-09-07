using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    public static class SecurityTools
    {
        public static string Encriptar(this string _cadenaAencriptar)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(_cadenaAencriptar);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
        public static string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key, 0, 30);
        }
        public static string EncryptText(string text)
        {
            return Encrypt(text, char.ConvertFromUtf32(2) + "$?+???n");
        }
        public static string DecryptText(string text)
        {
            return Decrypt(text, char.ConvertFromUtf32(2) + "$?+???n");
        }
        public static string Encrypt(string text, string encryptText)
        {
            byte[] byKey;
            byte[] dv = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(encryptText.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                byte[] inputArray = System.Text.Encoding.UTF8.GetBytes(text);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);

                cryptoStream.Write(inputArray, 0, inputArray.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string Decrypt(string text, string encryptText)
        {
            byte[] bKey;
            byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            try
            {
                bKey = System.Text.Encoding.UTF8.GetBytes(encryptText.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = new byte[2000];
                InlineAssignHelper(ref inputByteArray, Convert.FromBase64String(text));

                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(bKey, IV), CryptoStreamMode.Write);

                cryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                cryptoStream.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;

                return encoding.GetString(memoryStream.ToArray(), 0, Convert.ToInt16(memoryStream.Length));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static T InlineAssignHelper<T>(ref T target, T value)
        {
            target = value;
            return value;
        }
    }
}
