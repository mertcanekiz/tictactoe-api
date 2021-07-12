using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace TicTacToe.Factory.Board
{
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(EmptyBoardFactory))]
    [BsonKnownTypes(typeof(RandomBoardFactory))]
    public abstract class BoardFactory
    {
        public abstract Domain.Game.Board CreateBoard();
        protected abstract string Name { get; }

        private static readonly Dictionary<string, BoardFactory> BoardFactories = new();
        public static readonly BoardFactory Empty = new EmptyBoardFactory();
        public static readonly BoardFactory Random = new RandomBoardFactory();

        static BoardFactory()
        {
            BoardFactories.Add(Empty.Name, Empty);
            BoardFactories.Add(Random.Name, Random);
        }

        public static BoardFactory GetBoardFactoryByName(string name)
        {
            BoardFactories.TryGetValue(name, out var boardFactory);
            return boardFactory;
        }
    }
}