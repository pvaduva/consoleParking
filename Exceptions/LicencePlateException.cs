using System.Runtime.Serialization;

namespace parking
{
    [Serializable]
    internal class LicencePlateException : Exception
    {
        public LicencePlateException()
        {
        }

        public LicencePlateException(string? message) : base(message)
        {
        }

        public LicencePlateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected LicencePlateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}