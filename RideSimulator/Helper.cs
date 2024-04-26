using System.Text;
using System;

namespace RideSimulator
{
    public static class Helper
    {
        private static Random random = new Random();

        public static string GenerateOtp()
        {
            const string chars = "0123456789";
            StringBuilder otp = new StringBuilder(6);

            for (int i = 0; i < 6; i++)
            {
                otp.Append(chars[random.Next(chars.Length)]);
            }

            return otp.ToString();
        }
    }
}
