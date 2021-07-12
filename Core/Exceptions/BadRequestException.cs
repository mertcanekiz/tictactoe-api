using Microsoft.AspNetCore.Http;

namespace Core.Exceptions
{
    public class BadRequestException : HttpResponseException
    {
        public override int Status { get; set; } = StatusCodes.Status400BadRequest;

        public BadRequestException(string message)
        {
            Value = new
            {
                Message = message
            };
        }
    }
}