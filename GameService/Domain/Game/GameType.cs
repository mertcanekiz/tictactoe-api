using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TicTacToe.Domain.Game.Factory.Game;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.Strategies;
using TicTacToe.Domain.Game.Strategies.Move;
using TicTacToe.Domain.Game.Strategies.Move.ComputerStrategies;

namespace TicTacToe.Domain.Game
{
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(AgainstHumanGameType))]
    [BsonKnownTypes(typeof(AgainstComputerGameType))]
    public abstract class GameType
    {
        public abstract string Name { get; set; }
        public abstract IMoveStrategy MoveStrategy { get; set; }
        public abstract IGameFactory GameFactory { get; set; }

        private static readonly Dictionary<string, GameType> GameTypes = new();
        public static readonly GameType Singleplayer = new SingleplayerGameType();
        public static readonly GameType AgainstHuman = new AgainstHumanGameType();
        public static readonly GameType AgainsComputer = new AgainstComputerGameType();

        static GameType()
        {
            GameTypes.Add(Singleplayer.Name, Singleplayer);
            GameTypes.Add(AgainstHuman.Name, AgainstHuman);
            GameTypes.Add(AgainsComputer.Name, AgainsComputer);
        }

        public static GameType GetGameTypeByName(string name)
        {
            GameTypes.TryGetValue(name, out var gameType);
            return gameType;
        }

        public static bool Any(string name)
        {
            return GameTypes.ContainsKey(name);
        }
    }
    
    public class SingleplayerGameType : GameType
    {
        public override string Name { get; set; } = "singleplayer";
        public override IMoveStrategy MoveStrategy { get; set; } = new SingleplayerMoveStrategy();
        public override IGameFactory GameFactory { get; set; } = new SingleplayerGameFactory();
    }

    public class AgainstHumanGameType : GameType
    {
        public override string Name { get; set; } = "human";
        public override IMoveStrategy MoveStrategy { get; set; }
        public override IGameFactory GameFactory { get; set; } = new AgainstHumanGameFactory();
    }

    public class AgainstComputerGameType : GameType
    {
        public override string Name { get; set; } = "computer";

        public override IMoveStrategy MoveStrategy { get; set; } =
            new AgainstComputerMoveStrategy(new RandomComputerStrategy());

        public override IGameFactory GameFactory { get; set; } = new AgainstComputerGameFactory();
    }
}