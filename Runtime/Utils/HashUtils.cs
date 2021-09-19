//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Security.Cryptography;
using System.Text;

namespace Lumpn.Matomo.Utils
{
    internal static class HashUtils
    {
        public static byte[] HashMD5(string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            return HashMD5(bytes);
        }

        public static byte[] HashMD5(byte[] bytes)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(bytes);
                return hash;
            }
        }
    }
}
