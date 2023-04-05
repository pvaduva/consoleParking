using System.Runtime.Serialization;

namespace parking
{
    [Serializable]
    internal class ParkingLotFullException : Exception
    {
        public ParkingLotFullException()
        {
        }

        public ParkingLotFullException(string? message) : base(message)
        {
        }

        public ParkingLotFullException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ParkingLotFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}