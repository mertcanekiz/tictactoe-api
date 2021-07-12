using System;

namespace TicTacToe.Domain.DTO.Response
{
    public class CreateGameResponseDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
    }
}