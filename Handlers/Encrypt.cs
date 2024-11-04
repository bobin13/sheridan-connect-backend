using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace FindTeaBackEnd.Handlers
{
    public class Encrypt
    {
        public static string GenerateHash(string salt, string password)
        {
            string key = salt + password;

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(key);

            return hashedPassword;
        }

        public static Boolean CheckHash(string password, string hashedPassword)
        {

            Boolean isMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            return isMatch;
        }

        public static string GenerateSalt(int length)
        {

            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string salt = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return salt;
        }
    }
}