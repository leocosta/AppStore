using System;

namespace AppStore.Application.Services.Exceptions
{
    [Serializable]
    public class UnauthorizedOperationException : Exception
    {
        public UnauthorizedOperationException() : base() { }

        public UnauthorizedOperationException(string message) : base(message) { }

        public UnauthorizedOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
