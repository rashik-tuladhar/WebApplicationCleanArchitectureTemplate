using System;
using System.IO;
using System.Text;

namespace Infrastructure.Shared.Helpers
{
    public static class GeneralUtility
    {
        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        // Generate a random password of a given length (optional)
        public static string RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            builder.Append(RandomString(3, true));
            builder.Append(RandomString(2, false));
            builder.Append("@");
            builder.Append(random.Next(10, 99));
            return builder.ToString();
        }
        // Generate a random string with a given size and case.
        // If second parameter is true, the return string is lowercase
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
