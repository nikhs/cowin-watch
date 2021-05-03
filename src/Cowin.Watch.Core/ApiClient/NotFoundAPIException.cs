using System;
using System.Runtime.Serialization;

namespace Cowin.Watch.Core
{
    [Serializable]
    internal class NotFoundAPIException : Exception
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
}