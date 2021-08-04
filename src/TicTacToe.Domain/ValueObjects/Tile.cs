using System.Collections.Generic;
using TicTacToe.Domain.Common;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.ValueObjects
{
    public class Tile : ValueObject
    {
        public TileType Type { get; set; }
        public Position Position { get; set; }

        public bool IsEmpty => Type == TileType.None;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Position;
        }
    }
}