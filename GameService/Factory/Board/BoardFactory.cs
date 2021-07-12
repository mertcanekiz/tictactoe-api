using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using TicTacToe.Models.Game;

namespace TicTacToe.Factory.Board
{
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(EmptyBoardFactory))]
    [BsonKnownTypes(typeof(RandomBoardFactory))]
    public abstract class BoardFactory
    {
        public abstract Models.Game.Board CreateBoard();
        public abstract string Name { get; set; }

        private static Dictionary<string, BoardFactory> _boardFactories = new();
        public static BoardFactory Empty = new EmptyBoardFactory();
        public static BoardFactory Random = new RandomBoardFactory();

        static BoardFactory()
        {
            _boardFactories.Add(Empty.Name, Empty);
            _boardFactories.Add(Random.Name, Random);
        }

        public static BoardFactory GetBoardFactoryByName(string name)
        {
            _boardFactories.TryGetValue(name, out var boardFactory);
            return boardFactory;
        }
    }
}