using Core.Exceptions;

namespace TicTacToe.Exceptions
{
    public class GameFinishedException : BadRequestException
    {
        public GameFinishedException() : base("Game is finished. No more moves are allowed.")
        {
        }
    }
}