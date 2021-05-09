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
    public class NotFoundAPIException : Exception
    {
        public NotFoundAPIException()
        {
        }

        public NotFoundAPIException(string message) : base(message)
        {
        }

        public NotFoundAPIException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundAPIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class UnauthorizedAPIAccessException : Exception
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