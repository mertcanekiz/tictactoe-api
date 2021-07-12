namespace TicTacToe.Domain.DTO.Request
{
    public class MakeMoveRequestDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Type { get; set; }
    }
}