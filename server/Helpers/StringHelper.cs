using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using ShopAppApi.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace ShopAppApi.Helpers
{
    public class StringHelper : IStringHelper
    {
        private readonly Random random = new();

        public HashSalt EncryptPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return new HashSalt
            {
                Hash = encryptedPassw,
                Salt = Convert.ToBase64String(salt)
            };
        }

        public bool VerifyPassword(string enteredPassword, string salt, string storedPassword)
        {
            string encryptedPassw = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));
            return encryptedPassw == storedPassword;
        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class HashSalt
    {
        public string Hash { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty;
    }
}
