using System;
using System.ComponentModel;

namespace HttpRequestSender.ErrorHandling
{
    static class TextBoxValidator
    {
        /// <summary>
        /// Checks if the request per second value is valid.
        /// Tries to convert the string value into T.
        /// </summary>
        /// <typeparam name="T"> Generic type. (most likely numeric value) </typeparam>
        /// <param name="input"> Input in the text box. </param>
        /// <param name="result"> T value of the given string. Out parameter. </param>
        /// <param name="canBeEmpty"> Whether empty value is accepted or not. </param>
        /// <returns> Returns validity. </returns>
        public static bool Validate<T>(string input, out T result, bool canBeEmpty = false)
        {
            result = default;
            if (string.IsNullOrEmpty(input))
            {
                return canBeEmpty;
            }
            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                if(converter != null)
                {
                    // Converts the string input into an object and returns it if its not null.
                    result = (T)converter.ConvertFromString(input);
                    return result != null;
                }
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }
    }
}
