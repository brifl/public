namespace ConsoleRunner
{
    public static class GeneralExtensions
    {
        public static string ToLength(this string s, int length, bool addEllipsis = false)
        {
            var sLength = s.Length;

            var whiteSpaceRoom = length - sLength;

            for (var i = 0; i < whiteSpaceRoom; i++)
            {
                s += " ";
            }

            if (addEllipsis && sLength > length)
            {
                s = s.Insert(length - 3, "...");
            }

            s = s.Substring(0, length);

            return s;
        }
    }
}