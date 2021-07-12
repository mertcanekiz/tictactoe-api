using System.Collections.Generic;

namespace TicTacToe.Models.DTO.Request
{
    public class CreateGameRequestDto
    {
        public string GameType { get; set; }
        public List<string> WinConditionCheckers { get; set; }
    }
}