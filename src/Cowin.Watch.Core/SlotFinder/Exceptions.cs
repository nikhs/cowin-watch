using System;

namespace Cowin.Watch.Core.SlotFinder
{
    [Serializable]
    public class InvalidConstraintException : Exception
    {
        private readonly string constraintType;

        public InvalidConstraintException(string constraintType)
        {
            this.constraintType = constraintType;
        }
    }
}
