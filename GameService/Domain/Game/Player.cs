using System;

namespace TicTacToe.Domain.Game
{
    public class Player
    {
        public Guid? Id { get; set; }
        public PieceType PieceType { get; set; }
    }
}