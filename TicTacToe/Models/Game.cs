using System;
using System.Collections.Generic;

namespace TicTacToe.Models
{
    public class Game : Document
    {
        public List<GameState> States { get; set; }
        // public Guid? PlayerOneId { get; set; }
        // public Guid? PlayerTwoId { get; set; }
        public List<WinConditionChecker> WinConditionCheckers { get; set; }
        public bool IsWon { get; set; }
        public PieceType? Winner { get; set; }
    }
}