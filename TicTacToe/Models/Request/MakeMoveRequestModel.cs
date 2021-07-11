namespace TicTacToe.Models.Request
{
    public class MakeMoveRequestModel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Type { get; set; }
    }
}