using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public sealed class NotFoundException : Exception
    {
        private NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public NotFoundException(string message)
            : base(message) { }

        public override void GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
