using Core.Exceptions;

namespace TicTacToe.Exceptions
{
    public class TileNotEmptyException : BadRequestException
    {
        public TileNotEmptyException(int x, int y) : base($"The tile at position ({x}, {y}) is not empty.")
        {
        }
    }
}