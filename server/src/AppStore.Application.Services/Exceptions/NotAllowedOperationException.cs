using System;

namespace AppStore.Application.Services.Exceptions
{
    [Serializable]
    public class NotAllowedOperationException : Exception
    {
        public NotAllowedOperationException() : base() { }

        public NotAllowedOperationException(string message) : base(message) { }

        public NotAllowedOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
