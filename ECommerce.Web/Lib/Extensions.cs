using System.Text.RegularExpressions;

namespace ECommerce.Web.Lib
{
    public static class Extensions
    {
        public static string UrlFriendlyString(this string phrase, int maxLength = 50)
        {
            string str = phrase.ToLower();

            char[] Char = new char[] { 'ö', 'Ö', 'ü', 'Ü', 'ç', 'Ç', 'İ', 'ı', 'Ğ', 'ğ', 'Ş', 'ş' };
            char[] newChar = new char[] { 'o', 'O', 'u', 'U', 'c', 'C', 'I', 'i', 'G', 'g', 'S', 's' };
            for (int i = 0; i < Char.Length; i++)
            {
                str = str.Replace(Char[i], newChar[i]);
            }

            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }
    }
}