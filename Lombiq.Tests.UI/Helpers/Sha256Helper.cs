using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Lombiq.Tests.UI.Helpers
{
    public static class Sha256Helper
    {
        public static string ComputeHash(string text)
        {
            using var sha256 = new SHA256Managed();
            var hashedIdBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

            var stringBuilder = new StringBuilder();

            for (int i = 0; i < hashedIdBytes.Length; i++)
            {
                stringBuilder.Append(hashedIdBytes[i].ToString("x2", CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }
    }
}
