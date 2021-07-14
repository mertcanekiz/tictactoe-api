using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using TicTacToe.Exceptions;

namespace TicTacToe.Domain.Game.States
{
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(WaitingForPlayerTwoState))]
    [BsonKnownTypes(typeof(PlayerOneState))]
    [BsonKnownTypes(typeof(PlayerTwoState))]
    [BsonKnownTypes(typeof(FinishedGameState))]
    public abstract class GameState
    {
        public abstract string Name { get; }
        public abstract GameState NextState { get; }
        [BsonIgnore]
        public abstract Player Player { get; }

        protected readonly Game Game;

        public static string PlayerOneStateName = "playerOne";
        public static string PlayerTwoStateName = "playerTwo";
        public static string WaitingStateName = "waiting";
        public static string FinishedStateName = "finished";

        public static GameState GetGameStateByName(Game game, string name)
        {
            if (name == PlayerOneStateName)
                return new PlayerOneState(game);
            if (name == PlayerTwoStateName)
                return new PlayerTwoState(game);
            if (name == WaitingStateName)
                return new WaitingForPlayerTwoState(game);
            if (name == FinishedStateName)
                return new FinishedGameState(game);
            return default;
        }

        public GameState(Game game)
        {
            Game = game;
        }
    }
}