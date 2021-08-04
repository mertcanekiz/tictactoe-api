using System;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Exceptions
{
    public class TileNotEmptyException : Exception
    {
        public TileNotEmptyException(Position position)
            : base($"The tile at {position} is not empty")
        {
            
        }
    }
}