using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace jCtrl.Services.Core.Utils
{
    internal static class Encryptor
    {
        public static string Decrypt(string cipherText, string key, string salt)
        {

            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, GetSaltBytes(salt));

            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Encoding.Unicode.GetString(decryptedData);
        }
        public static string Encrypt(string clearText, string key, string salt)
        {

            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);

            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, GetSaltBytes(salt));

            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);
        }


        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;

            try
            {
                Rijndael alg = Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;

                cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.FlushFinalBlock();

                return ms.ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Debug.WriteLine("INNER EXCEPTION");
                    Debug.WriteLine(ex.Message);
                }

                return null;
            }
            finally
            {
                if ((cs != null))
                {
                    cs.Close();
                }
            }
        }

        private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = null;

            try
            {
                Rijndael alg = Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;

                cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearData, 0, clearData.Length);
                cs.FlushFinalBlock();

                return ms.ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    Debug.WriteLine("INNER EXCEPTION");
                    Debug.WriteLine(ex.Message);
                }

                return null;
            }
            finally
            {
                if ((cs != null))
                {
                    cs.Close();
                }
            }
        }

        private static byte[] GetSaltBytes(string salt)
        {
            return Encoding.ASCII.GetBytes(salt);
        }

    }
}
