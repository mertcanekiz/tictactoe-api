using System;
using Microsoft.AspNetCore.Http;

namespace Core.Exceptions
{
    public class HttpResponseException : Exception
    {
        public virtual int Status { get; set; } = StatusCodes.Status500InternalServerError;
        public object Value { get; set; }
    }
}