using System;
using System.Runtime.Serialization;

namespace Cowin.Watch.Core
{
    [Serializable]
    internal class UnauthorizedAPIAccessException : Exception
    {
        public UnauthorizedAPIAccessException()
        {
        }

        public UnauthorizedAPIAccessException(string message) : base(message)
        {
        }

        public UnauthorizedAPIAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedAPIAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}