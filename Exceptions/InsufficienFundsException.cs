using System.Runtime.Serialization;

namespace parking
{
    [Serializable]
    internal class InsufficienFundsException : Exception
    {
        public InsufficienFundsException()
        {
        }

        public InsufficienFundsException(string? message) : base(message)
        {
        }

        public InsufficienFundsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsufficienFundsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}