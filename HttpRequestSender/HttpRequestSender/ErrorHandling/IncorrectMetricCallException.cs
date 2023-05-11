using System;

namespace HttpRequestSender.ErrorHandling
{
    [Serializable]
    class InvalidMethodCallException : Exception
    {
        /// <summary>
        /// Exception for when a method call is invalid.
        /// For example pausing an already paused metric or adding a new value to a closed metric.
        /// </summary>
        /// <param name="methodName"> Name of the method that has been called. </param>
        /// <param name="address"> Given url of the address in metrics. </param>
        public InvalidMethodCallException (string methodName, string address)
            : base($"Invalid call of \"{methodName}\" in metrics of \"{address}\".")
        {
            
        }
    }
}
