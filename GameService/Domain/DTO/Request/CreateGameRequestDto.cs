using System.Collections.Generic;

namespace TicTacToe.Domain.DTO.Request
{
    public class CreateGameRequestDto
    {
        public List<string> WinConditionCheckers { get; set; }
        public string BoardFactory { get; set; }
        public string PlayerOnePieceType { get; set; }
        public string GameType { get; set; }
    }
}