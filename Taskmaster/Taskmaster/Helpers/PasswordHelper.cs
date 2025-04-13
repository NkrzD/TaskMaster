// Helpers/PasswordHelper.cs
using System.Security.Cryptography;
using System.Text;

namespace TaskMaster.Helpers
{
    public static class PasswordHelper
    {
        public static string Hash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
