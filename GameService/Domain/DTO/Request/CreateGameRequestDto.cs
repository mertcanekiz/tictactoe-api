using System.Collections.Generic;

namespace TicTacToe.Domain.DTO.Request
{
    public class CreateGameRequestDto
    {
        public List<string> WinConditionCheckers { get; set; }
        public string BoardFactory { get; set; }
        public string InitialState { get; set; }
        public bool Tie { get; set; }
    }
}