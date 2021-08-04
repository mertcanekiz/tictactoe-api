using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TicTacToe.Application.Common.Interfaces;

namespace TicTacToe.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                Console.WriteLine("userId: " + userIdString);
                if (string.IsNullOrEmpty(userIdString))
                    return null;
                return Guid.Parse(userIdString);
            }
        }
    }
}