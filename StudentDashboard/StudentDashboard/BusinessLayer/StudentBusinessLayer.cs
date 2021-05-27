using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace StudentDashboard.BusinessLayer
{
    public class StudentBusinessLayer
    {
        public string GenerateOtp()
        {
            return RandomNumber(100000, 999999).ToString();
        }
        public string GeneratePasswordVeryficationToken()
        {
            string token;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                token = Convert.ToBase64String(tokenData);
            }
            return token;
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}