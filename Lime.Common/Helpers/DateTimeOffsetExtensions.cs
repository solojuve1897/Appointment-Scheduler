using System;

namespace Lime.Common
{
    public static class DateTimeOffsetExtensions
    {
        public static string GetShortDate(this DateTimeOffset date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static string GetTime(this DateTimeOffset date)
        {
            return date.ToString("HH:mm");
        }
    }
}
