using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public sealed class AlreadyExistsException : Exception
    {
        private AlreadyExistsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public AlreadyExistsException(string message)
            : base(message) { }

        public override void GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
