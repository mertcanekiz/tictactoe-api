using System;

namespace TicTacToe.Domain.DTO.Response
{
    public class RegisterUserResponseDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}