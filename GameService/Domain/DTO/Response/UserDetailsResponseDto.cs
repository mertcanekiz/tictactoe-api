using System;

namespace TicTacToe.Domain.DTO.Response
{
    public class UserDetailsResponseDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}