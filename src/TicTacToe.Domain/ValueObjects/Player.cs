using System;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.ValueObjects
{
    public class Player
    {
        public Guid? Id { get; set; }
        public TileType Type { get; set; }
    }
}