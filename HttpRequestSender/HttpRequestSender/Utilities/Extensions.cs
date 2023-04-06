using System;
using System.Collections.Generic;

namespace HttpRequestSender.Utilities
{
    public static class Extensions
    {
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static DateTime NextMinute(this DateTime time)
        {
            DateTime datetime = time.AddSeconds(60 - time.Second);
            datetime.AddMilliseconds(-datetime.Millisecond);
            return datetime;
        }
    }
}
