using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace jCtrl.Services.Core.Utils
{
    public static class Helpers
    {

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static class CompressionHelper
        {
            public static byte[] GZipByte(byte[] str)
            {
                if (str == null)
                {
                    return null;
                }

                using (var output = new MemoryStream())
                {
                    using (
                        var compressor = new Ionic.Zlib.GZipStream(
                        output, Ionic.Zlib.CompressionMode.Compress,
                        Ionic.Zlib.CompressionLevel.BestSpeed))
                    {
                        compressor.Write(str, 0, str.Length);
                    }

                    return output.ToArray();
                }
            }
        }
    }
}