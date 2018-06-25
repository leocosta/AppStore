using AppStore.Application.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;

namespace AppStore.API.Helpers
{
    public static class ExceptionConversor
    {
        public static HttpStatusCode ToHttpStatusCode(this Exception exception)
        {
            Dictionary<Type, HttpStatusCode> dict = GetExceptionDictionary();
            if (dict.ContainsKey(exception.GetType()))
            {
                return dict[exception.GetType()];
            }
            return dict[typeof(Exception)];
        }

        private static Dictionary<Type, HttpStatusCode> GetExceptionDictionary()
        {
            Dictionary<Type, HttpStatusCode> dict = new Dictionary<Type, HttpStatusCode>();
            dict[typeof(ResourceNotFoundException)] = HttpStatusCode.NotFound;
            dict[typeof(InvalidOperationException)] = (HttpStatusCode)422; //Unprocessable Entity
            dict[typeof(ArgumentException)] = HttpStatusCode.BadRequest;
            dict[typeof(Exception)] = HttpStatusCode.InternalServerError;
            return dict;
        }
    }
}