using System;
using System.Collections.Generic;

namespace HttpRequestSender.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method.
        /// Swaps a list item with another item.
        /// </summary>
        /// <typeparam name="T"> Generic type. </typeparam>
        /// <param name="list"> Current list. </param>
        /// <param name="i"> Index of first list item. </param>
        /// <param name="j"> Index of second list item. </param>
        public static void Swap<T>(this List<T> list, int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        /// <summary>
        /// Extension method.
        /// Adds seconds to the current time to make the minute round also truncates the milliseconds of the value.
        /// </summary>
        /// <param name="time"> Current DateTime. </param>
        /// <returns> Returns the next round minute. </returns>
        public static DateTime NextMinute(this DateTime time)
        {
            DateTime datetime = time.AddSeconds(60 - time.Second);
            datetime.AddMilliseconds(-datetime.Millisecond);
            return datetime;
        }
    }
}
