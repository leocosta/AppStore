using System;

namespace AppStore.Application.Services.Exceptions
{
    public class InvalidResourceOperationException : Exception
    {
        public InvalidResourceOperationException(string message)
            : base(message)
        { }

        public InvalidResourceOperationException()
            : base("It was not possible to complete the request.")
        { }
    }
}
