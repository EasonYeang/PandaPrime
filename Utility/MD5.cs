using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public class MD5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="pwStr">待加密字符串</param>
        /// <returns>MD5字符串</returns>
        public static string Encode(string pwStr)
        {
            MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider();
            byte[] bytes = csp.ComputeHash(Encoding.Default.GetBytes(pwStr));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
