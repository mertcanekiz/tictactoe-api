using System.Collections.Generic;
using TicTacToe.Domain.Common;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.ValueObjects
{
    public class WinCondition : ValueObject
    {
        public TileType? Winner { get; set; }
        public bool IsWon { get; set; }
        public bool IsTied { get; set; }
        public  List<Tile> Tiles { get; set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Winner;
            ///
        }
    }
}