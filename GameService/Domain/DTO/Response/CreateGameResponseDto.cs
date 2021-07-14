using System;

namespace TicTacToe.Domain.DTO.Response
{
    public class CreateGameResponseDto
    {
        public GameResponseDto Game { get; set; }
        public bool Success { get; set; }
    }
}