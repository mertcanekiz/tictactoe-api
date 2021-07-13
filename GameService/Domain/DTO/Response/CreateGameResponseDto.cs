using System;

namespace TicTacToe.Domain.DTO.Response
{
    public class CreateGameResponseDto
    {
        public Game.Game Game { get; set; }
        public bool Success { get; set; }
    }
}