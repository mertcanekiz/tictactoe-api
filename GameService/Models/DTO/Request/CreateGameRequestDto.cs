using System.Collections.Generic;

namespace TicTacToe.Models.DTO.Request
{
    public class CreateGameRequestDto
    {
        public List<string> WinConditionCheckers { get; set; }
        public string BoardFactory { get; set; }
    }
}