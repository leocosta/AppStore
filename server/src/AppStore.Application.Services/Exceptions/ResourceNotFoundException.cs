using System;

namespace AppStore.Application.Services.Exceptions
{
    public class ResourceNotFoundException : Exception
	{
		public ResourceNotFoundException(string message)
			: base(message)
		{}

		public ResourceNotFoundException()
			: base("The requested resource was not found.")
		{}
	}
}
