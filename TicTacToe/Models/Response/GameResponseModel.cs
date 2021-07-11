using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Models.Response
{
    public class GameStateResponseModel
    {
        public string[] Board { get; set; }
        public int MoveNumber { get; set; }
    }
    
    public class GameResponseModel : DocumentResponseModel
    {
        public List<GameStateResponseModel> States { get; set; }
        public bool IsWon { get; set; }
        public PieceType? Winner { get; set; }

        private string[] BoardLineRepresentation(PieceType[][] tiles)
        {
            var result = new string[3];
            for (int i = 0; i < tiles.Length; i++)
            {
                string line = "";
                for (int j = 0; j < tiles[i].Length; j++)
                {
                    string current = " ";
                    if (tiles[i][j] == PieceType.X)
                        current = "X";
                    if (tiles[i][j] == PieceType.O)
                        current = "O";
                    line += current;
                }

                result[i] = line;
            }

            return result;
        }

        public GameResponseModel(Game game)
        {
            Id = game.Id;
            CreatedAt = game.CreatedAt;
            UpdatedAt = game.UpdatedAt;
            DeletedAt = game.DeletedAt;
            States = game.States.Select(x => new GameStateResponseModel()
            {
                Board = BoardLineRepresentation(x.Board.Tiles),
                MoveNumber = x.MoveNumber
            }).ToList();
            IsWon = game.IsWon;
            Winner = game.Winner;
        }
    }
}