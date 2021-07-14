using System;
using System.Collections.Generic;
using System.Linq;
using Core.Mongo;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.WinConditions;

namespace TicTacToe.Domain.Game
{
    public class Game : Document
    {
        public string StateName { get; set; }
        [BsonIgnore]
        [JsonIgnore]
        public GameState State => GameState.GetGameStateByName(this, StateName);
        public List<Move> Moves { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public string GameTypeName { get; set; }
        [BsonIgnore]
        [JsonIgnore]
        public GameType GameType => GameType.GetGameTypeByName(GameTypeName);
        public List<WinConditionChecker> WinConditionCheckers { get; set; }
        public WinCondition WinCondition { get; set; }

        public void CheckWinConditions()
        {
            foreach (var checker in WinConditionCheckers)
            {
                var condition = checker.Check(Moves.Last().Board);
                if (condition.IsTied && (!WinCondition?.IsWon ?? true))
                {
                    WinCondition = condition;
                    StateName = new FinishedGameState(this).Name;  // TODO
                }
                if (condition.IsWon)
                {
                    WinCondition = condition;
                    StateName = new FinishedGameState(this).Name; // TODO
                }
            }
        }
    }
}