using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public sealed class ValidationErrorException : Exception
    {
        private ValidationErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public ValidationErrorException(string message)
            : base(message) { }

        public override void GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
