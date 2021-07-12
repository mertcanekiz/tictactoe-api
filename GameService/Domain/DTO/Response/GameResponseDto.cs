using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Game;

namespace TicTacToe.Domain.DTO.Response
{
    public class MoveResponseModel
    {
        public string[] Board { get; set; }
        public int MoveNumber { get; set; }
    }
    
    public class GameResponseDto : DocumentResponseDto
    {
        public List<MoveResponseModel> Moves { get; set; }
        public bool IsWon { get; set; }
        public PieceType? Winner { get; set; }
        public string State { get; set; }

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

        public GameResponseDto(Game.Game game)
        {
            Id = game.Id;
            CreatedAt = game.CreatedAt;
            UpdatedAt = game.UpdatedAt;
            DeletedAt = game.DeletedAt;
            Moves = game.Moves.Select(x => new MoveResponseModel()
            {
                Board = BoardLineRepresentation(x.Board.Tiles),
                MoveNumber = x.MoveNumber
            }).ToList();
            IsWon = game.IsWon;
            Winner = game.Winner;
            State = game.State.Name;
        }
    }
}