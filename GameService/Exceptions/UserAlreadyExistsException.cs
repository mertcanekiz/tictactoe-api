using System;
using Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace TicTacToe.Exceptions
{
    public sealed class UserAlreadyExistsException : BadRequestException
    {
        public UserAlreadyExistsException(string username)
        : base($"Username {username} already exists")
        {
        }
    }
}