using System;
using System.Linq;
using System.Reflection;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.States
{
    public abstract class GameState
    {
        public abstract string NextStateName { get; }

        protected readonly Game Game;

        public abstract Player Player { get; }

        protected GameState(Game game)
        {
            Game = game;
        }
        
        public static GameState GetGameStateByName(Game game, string name)
        {
            var type = typeof(GameState);
            var gameStates = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => type.IsAssignableFrom(x) && !x.IsAbstract);
            var gameStateType =
                gameStates.FirstOrDefault(x => x.Name.ToLowerInvariant().StartsWith(name.ToLowerInvariant()));

            return (GameState)Activator.CreateInstance(gameStateType ?? throw new InvalidOperationException(), game);
        }
    }
}