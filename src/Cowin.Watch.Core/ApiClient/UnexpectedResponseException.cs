using System;
using System.Runtime.Serialization;

namespace Cowin.Watch.Core
{
    [Serializable]
    internal class UnexpectedResponseException : Exception
    {
        public UnexpectedResponseException()
        {
        }

        public UnexpectedResponseException(string message) : base(message)
        {
        }

        public UnexpectedResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnexpectedResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}