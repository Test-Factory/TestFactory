using System;
using System.Text;
using System.Security.Cryptography;

namespace TestFactory.Business.Components
{
    class HashDecoder
    {
        private static Random random = new Random();
        private const int MinSaltSize = 4;
        private const int MaxSaltSize = 8;

        public static string ComputeHash(string plainText, string saltValue)
        {
            //Guard.ThrowIfEmptyString(saltValue, "Salt value specified cannot be null or empty. You may call GenerateSaltString method prior to calling ComputeHash method in order to generate a salt value.", "saltValue");

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes = Encoding.Unicode.GetBytes(String.Concat(plainText, saltValue));

            MD5 hash = new MD5CryptoServiceProvider();

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
            string hashValue = Convert.ToBase64String(hashBytes);

            // Return the result.
            return hashValue;
        }

        public static bool VerifyHash(string plainText, string hashValue, string saltValue)
        {
            //Guard.ThrowIfEmptyString(saltValue, "Salt value specified cannot be null or empty. You may call GenerateSaltString method prior to calling ComputeHash method in order to generate a salt value.", "saltValue");

            // Compute a new hash string.
            string expectedHashString = ComputeHash(plainText, saltValue);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }

        public static string GenerateHashString()
        {
            byte[] saltBytes = null;

            // Generate a random number for the size of the salt.
            int saltSize = random.Next(MinSaltSize, MaxSaltSize);

            // Allocate a byte array, which will hold the salt.
            saltBytes = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            return ComputeHash(salt, salt);
        }

        public static string GenarateSalt()
        {
            var random = new RNGCryptoServiceProvider();
            int max_length = 32;
            byte[] salt = new byte[max_length];
            random.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }
    }
}
