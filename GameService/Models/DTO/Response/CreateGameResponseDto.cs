using System;

namespace TicTacToe.Models.DTO.Response
{
    public class CreateGameResponseDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
    }
}