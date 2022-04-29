using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpRequestSender.ErrorHandling
{
    static class TextBoxValidator
    {
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
