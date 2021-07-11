using System.Collections.Generic;

namespace TicTacToe.Models.Request
{
    public class CreateGameRequestModel
    {
        public List<string> WinConditionCheckers { get; set; }
    }
}