using System;
using System.Collections.Generic;
using Core.Mongo;
using TicTacToe.Domain.Game.States;

namespace TicTacToe.Domain.Game
{
    public class Game : Document
    {
        public GameState State;
        public List<Move> Moves { get; set; }
        public Guid CreatedBy { get; set; }
        public List<WinConditionChecker> WinConditionCheckers { get; set; }
        public bool IsWon { get; set; }
        public PieceType? Winner { get; set; }
    }
}