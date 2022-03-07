
using bnbAPI.Static;
using System;

namespace bnbAPI.Service
{
    public class PasswordHashService
    {
        public bool Verify(string plaintext, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(plaintext, hash);
        }

        public bool NeedsUpdate(string hashedPassword)
        {
             return Config.WorkFactor != BCrypt.Net.BCrypt.GetPasswordWorkFactor(hashedPassword);
        }

        public string Hash(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword, Config.WorkFactor);
        }
    }
}
