using System;
using System.Collections.Generic;
using System.Text;

namespace CommonUnit.Encryption
{
    public static class PasswordEncryHelper
    {
        /// <summary>
        /// 密码加密
        /// </summary>
        /// <param name="password"></param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string PwdEncry(string password,string key)
        {
            return AesEncryptionHelper.AesEncrypt(MD5EncryptionHelper.MD5_32(password), key);
        }
    }
}
