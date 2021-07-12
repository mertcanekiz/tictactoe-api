using System;
using System.Collections.Generic;
using Core.Mongo;

namespace TicTacToe.Models.Game
{
    public class Game : Document
    {
        public List<GameState> States { get; set; }
        public Guid CreatedBy { get; set; }
        public List<WinConditionChecker> WinConditionCheckers { get; set; }
        public bool IsWon { get; set; }
        public PieceType? Winner { get; set; }
    }
}