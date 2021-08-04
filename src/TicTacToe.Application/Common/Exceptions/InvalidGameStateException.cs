using System;

namespace TicTacToe.Application.Common.Exceptions
{
    public class InvalidGameStateException : Exception
    {
        public InvalidGameStateException(string message)
            : base(message)
        {
            
        }
    }

    public class GameNotWaitingForPlayerTwoException : InvalidGameStateException
    {
        public GameNotWaitingForPlayerTwoException()
            : base("Game is not waiting for player two")
        {
        }
    }
}