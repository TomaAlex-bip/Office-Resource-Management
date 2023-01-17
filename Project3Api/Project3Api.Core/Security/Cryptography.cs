using System.Security.Cryptography;
using System.Text;

namespace Project3Api.Core.Security
{
    public class Cryptography
    {
        public static string HashString(string inputString)
        {
            byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(inputString));

            StringBuilder sb = new();

            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}
