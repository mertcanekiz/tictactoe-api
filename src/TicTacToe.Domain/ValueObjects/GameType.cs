using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Strategies.MoveStrategy;
using TicTacToe.Domain.Strategies.WinCondition;

namespace TicTacToe.Domain.ValueObjects
{
    public abstract class GameType
    {
        public abstract IMoveStrategy MoveStrategy { get; }

        private static readonly Dictionary<string, GameType> GameTypes = new();

        static GameType()
        {
            var type = typeof(GameType);
            var gameTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => type.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            foreach (var gameType in gameTypes)
            {
                var instance = (GameType) Activator.CreateInstance(gameType);
                var name = gameType.Name[..gameType.Name.IndexOf(nameof(GameType), StringComparison.Ordinal)]; 
                GameTypes.Add(name, instance);
            }
        }

        public static GameType GetGameTypeByName(string name)
        {
            return GameTypes[name];
        }

        public static bool Any(string name)
        {
            return GameTypes.ContainsKey(name);
        }
    }

    public abstract class AgainstComputerGameType : GameType
    {
        public class RandomAIGameType : GameType
        {
            public override IMoveStrategy MoveStrategy =>
                new AgainstComputerMoveStrategy(new RandomAiMoveStrategy());
        }

        public class HardAIGameType : GameType
        {
            public override IMoveStrategy MoveStrategy =>
                new AgainstComputerMoveStrategy(new MinimaxAiMoveStrategy());
        }
    }


    public class AgainstHumanGameType : GameType
    {
        public override IMoveStrategy MoveStrategy => new AgainstHumanMoveStrategy();
    }

    public class SinglePlayerGameType : GameType
    {
        public override IMoveStrategy MoveStrategy => new SinglePlayerMoveStrategy();
    }
}