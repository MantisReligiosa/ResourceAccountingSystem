using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public sealed class DatabaseException : Exception
    {
        private DatabaseException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public DatabaseException(string message)
            : base(message) { }

        public DatabaseException(string message,Exception ex)
            : base(message, ex) { }

        public override void GetObjectData(
            SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}