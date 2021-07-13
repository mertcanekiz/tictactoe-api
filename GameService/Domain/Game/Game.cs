using System;
using System.Collections.Generic;
using System.Linq;
using Core.Mongo;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.WinConditions;

namespace TicTacToe.Domain.Game
{
    public class Game : Document
    {
        public GameState State;
        public List<Move> Moves { get; set; }
        public Guid CreatedBy { get; set; }
        public List<WinConditionChecker> WinConditionCheckers { get; set; }
        public WinCondition WinCondition { get; set; }

        public void CheckWinConditions()
        {
            WinCondition condition = null;
            foreach (var checker in WinConditionCheckers)
            {
                condition = checker.Check(Moves.Last().Board);
                Console.WriteLine($"{checker.Name}: IsTied={condition.IsTied}, IsWon={condition.IsWon}, Winner={condition.Winner}");
                if (condition.IsTied && (!WinCondition?.IsWon ?? true))
                {
                    WinCondition = condition;
                    State = GameState.FinishedGameState;
                }
                if (condition.IsWon)
                {
                    WinCondition = condition;
                    State = GameState.FinishedGameState;
                }
            }
        }
    }
}