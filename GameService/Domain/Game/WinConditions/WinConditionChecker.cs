using System.Collections.Generic;
using Core.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace TicTacToe.Domain.Game.WinConditions
{
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(HorizontalWinConditionChecker))]
    [BsonKnownTypes(typeof(VerticalWinConditionChecker))]
    [BsonKnownTypes(typeof(DiagonalWinConditionChecker))]
    [BsonKnownTypes(typeof(TieConditionChecker))]
    public abstract class WinConditionChecker
    {
        public static readonly WinConditionChecker Vertical = new VerticalWinConditionChecker();
        public static readonly WinConditionChecker Horizontal = new HorizontalWinConditionChecker();
        public static readonly WinConditionChecker Diagonal = new DiagonalWinConditionChecker();
        public static readonly WinConditionChecker Tie = new TieConditionChecker();

        private static readonly Dictionary<string, WinConditionChecker> WinConditions = new();

        public abstract string Name { get; }

        static WinConditionChecker()
        {
            WinConditions.Add(Vertical.Name, Vertical);
            WinConditions.Add(Horizontal.Name, Horizontal);
            WinConditions.Add(Diagonal.Name, Diagonal);
            WinConditions.Add(Tie.Name, Tie);
        }

        public static WinConditionChecker GetWinConditionCheckerByName(string name)
        {
            bool exists = WinConditions.TryGetValue(name, out var winConditionChecker);
            if (!exists)
                throw new BadRequestException($"Invalid win condition checker name: {name}");
            return winConditionChecker;
        }

        public static bool Any(string name)
        {
            return WinConditions.ContainsKey(name);
        }
        public abstract WinCondition Check(Board board);
    }
}