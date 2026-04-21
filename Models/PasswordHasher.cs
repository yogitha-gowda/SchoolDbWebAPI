using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace SchoolDBCoreWebAPI.Models
{
    public class PasswordHasher
    {
        //Hashes a plain tex into a Base64-encoded SHA-256 hash

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();//The memory is deleted after execution

            //Convert the input password string into a byte array using UTF-* encoding
            var bytes=Encoding.UTF8.GetBytes(password);

            //Computes the SHA-256 hash
            var hash=sha256.ComputeHash(bytes);

            //Convert the hashed byte ARRAY into a Base64-encoded string
            return Convert.ToBase64String(hash);
         }

        public static bool VerifyPasword(string hashedPassword,string providedPassword)
        {
            return hashedPassword==HashPassword(providedPassword);
        }
    }
}
