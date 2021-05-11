using System;
using System.Runtime.Serialization;

namespace Cowin.Watch.Core.SlotFinder
{
    [Serializable]
    public class InvalidConstraintException : Exception
    {
        public InvalidConstraintException()
        {
        }

        public InvalidConstraintException(string message) : base(message)
        {
        }

        public InvalidConstraintException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
