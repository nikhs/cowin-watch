using System;
using System.Runtime.Serialization;

namespace Cowin.Watch.Core
{

    [Serializable]
    public class UnexpectedResponseException : Exception
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

    [Serializable]
    public class NotFoundApiException : Exception
    {
        public NotFoundApiException()
        {
        }

        public NotFoundApiException(string message) : base(message)
        {
        }

        public NotFoundApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class UnauthorizedApiAccessException : Exception
    {
        public UnauthorizedApiAccessException()
        {
        }

        public UnauthorizedApiAccessException(string message) : base(message)
        {
        }

        public UnauthorizedApiAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedApiAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}