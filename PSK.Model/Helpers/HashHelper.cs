using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PSK.Model.Helpers
{
    public static class HashHelper
    {
        public static string Hash(this string str)
        {
            var crypto = new RNGCryptoServiceProvider();
            var salt = new byte[16];
            crypto.GetBytes(salt);

            var pbkdf2 = new Rfc2898DeriveBytes(str, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
