using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Common;
using TicTacToe.Domain.Factories;
using TicTacToe.Domain.States;
using TicTacToe.Domain.Strategies.WinCondition;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Entities
{
    public class Game : Document
    {
        public Board Board { get; set; }
        public string GameTypeName { get; set; }
        public List<string> WinCheckerNames { get; set; }
        public string GameStateName { get; set; }
        public WinCondition WinCondition { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public List<IWinChecker> GetWinCheckers()
        {
            return WinCheckerNames.Select(WinCheckerFactory.CreateWinChecker).ToList();
        }

        public IWinChecker GetFirstWinChecker()
        {
            var winCheckerOrder = new List<Type>
            {
                typeof(HorizontalWinChecker),
                typeof(VerticalWinChecker),
                typeof(DiagonalWinChecker),
                typeof(TieChecker)
            };

            var winCheckers = GetWinCheckers();

            return winCheckerOrder.Select(type => winCheckers.FirstOrDefault(x => x.GetType() == type)).FirstOrDefault(winChecker => winChecker != null);
        }

        public GameState GetGameState()
        {
            return GameState.GetGameStateByName(this, GameStateName);
        }
    }
}