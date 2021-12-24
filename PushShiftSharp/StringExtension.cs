using System.Text;
using System.Text.RegularExpressions;

namespace PushShiftSharp
{
    public static class StringExtension
    {
        public static int GetNumbers(this string obj)
        {
            int some = 0;
            int.TryParse(new string(obj.Where(Char.IsDigit).ToArray()), out some);
            return some;
        }

        static string pattern = @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)";
        public static string RemoveURLs(this string obj)
        {
            obj = Regex.Replace(obj, pattern, string.Empty);
            return obj;
        }
        public static bool ItHasURL(this string obj)
        {
            var url_regex = new Regex(pattern);
            return url_regex.Match(obj).Success;
        }
        public static string AllTrim(this string obj)
        {
            obj.Replace("  ", " ");
            obj.Replace("   ", " ");
            return obj.Trim();
        }

        static char[] separatorsAfter = { '.', '!', '?', '\n' };
        public static string ToSentenceView(this string obj) //not finished yet
        {
            StringBuilder sb = new StringBuilder(obj.AllTrim());
            sb[0] = char.ToUpper(sb[0]);
            bool nextInUpper = false;
            for (int i = 1; i < sb.Length - 1; i++)
            {
                if (sb[i] != ' ' && nextInUpper)
                {
                    sb[i] = char.ToUpper(sb[i]);
                    nextInUpper = false;
                }
                if (separatorsAfter.Contains(sb[i]))
                {
                    sb[i + 1] = char.ToUpper(sb[i + 1]);
                    nextInUpper = true;
                }
                else sb[i + 1] = char.ToLower(sb[i + 1]);
            }
            return sb.ToString();
        }
    }
}
