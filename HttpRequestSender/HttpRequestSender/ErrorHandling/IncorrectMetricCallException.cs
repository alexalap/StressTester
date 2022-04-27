using System;

namespace HttpRequestSender.ErrorHandling
{
    [Serializable]
    class IncorrectMetricCallException : Exception
    {
        public IncorrectMetricCallException (string methodName, string address)
            : base($"Invalid call of \"{methodName}\" in metrics of \"{address}\".")
        {
            
        }
    }
}
