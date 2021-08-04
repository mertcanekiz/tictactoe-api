using System;

namespace TicTacToe.Application.Identity.Commands.Login
{
    public class LoginResult
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}