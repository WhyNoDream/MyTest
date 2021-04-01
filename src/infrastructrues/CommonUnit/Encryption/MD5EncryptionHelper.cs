using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CommonUnit.Encryption
{

    public static class MD5EncryptionHelper
    {
        public static string MD5_32(string encryStr)
        {
            //32位大写
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(encryStr));
                var strResult = BitConverter.ToString(result);
                string result3 = strResult.Replace("-", "");
                return result3;
            }
        }
    }
}
