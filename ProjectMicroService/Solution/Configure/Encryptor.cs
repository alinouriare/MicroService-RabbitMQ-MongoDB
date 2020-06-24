﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Configure
{
    public class Encryptor : IEncryptor
    {
        private static readonly int saltSize = 40;
        private static readonly int iterationsCount = 10000;
        public string GetHash(string value, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), iterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(saltSize));
        }

        public string GetSalt(string value)
        {
            var saltBytes = new byte[saltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }


        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length + sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
