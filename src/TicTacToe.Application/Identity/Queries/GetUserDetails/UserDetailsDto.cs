using System;

namespace TicTacToe.Application.Identity.Queries.GetUserDetails
{
    public class UserDetailsDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}