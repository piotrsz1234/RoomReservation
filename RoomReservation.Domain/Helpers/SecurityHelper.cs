using System.Security.Cryptography;
using System.Text;

namespace RoomReservation.Domain.Helpers {
    public static class SecurityHelper
    {
        public static string HashString(string stringToHash)
        {
            var hash = SHA256.Create();

            var data = hash.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

            return Encoding.UTF8.GetString(data);
        }
    }
}