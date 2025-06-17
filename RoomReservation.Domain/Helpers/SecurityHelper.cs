using System.Security.Cryptography;
using System.Text;

namespace RoomReservation.Domain.Helpers
{
    public static class SecurityHelper
    {
        public static string HashString(string stringToHash)
        {
            using var hashAlgorithm = SHA256.Create();

            var data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (var i = 0; i < data.Length; i++) sBuilder.Append(data[i].ToString("x2"));

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}