using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication.Components
{
    /// <summary>
    /// Cryptography class.
    /// </summary>
    public static class Cryptography
    {
        private const string BASE_64_HASH = "m04y0vcgjty";

        /// <summary>
        /// This method converts a string to MD5.
        /// </summary>
        /// <param name="source">Source value</param>
        /// <returns>Source converted</returns>
        public static string ConvertToMD5(string source)
        {
            string hash = "";

            using (MD5 MD5Hash = MD5.Create())
            {
                hash = GetMD5Hash(MD5Hash, source);
            }

            return hash;
        }

        /// <summary>
        /// Method compare two MD5 strings.
        /// </summary>
        /// <param name="source">Source value</param>
        /// <param name="hash">Hash value</param>
        /// <returns>Boolean status (True or False)</returns>
        public static bool CompareMD5(string source, string hash)
        {
            using (MD5 MD5Hash = MD5.Create())
            {
                string hashOfInput = GetMD5Hash(MD5Hash, source);

                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Method converts to base 64.
        /// </summary>
        /// <param name="source">Source value</param>
        /// <returns>string</returns>
        public static string ConvertToBase64(string source)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}{1}", source, BASE_64_HASH)));
        }

        /// <summary>
        /// Method convert from base 64.
        /// </summary>
        /// <param name="hash">Base64 value</param>
        /// <returns></returns>
        public static string ConvertFromBase64(string hash)
        {
            return Encoding.Default.GetString(Convert.FromBase64String(hash)).Replace(BASE_64_HASH, "");
        }

        private static string GetMD5Hash(MD5 MD5Hash, string input)
        {
            byte[] data = MD5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}