using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.WinConditions;

namespace TicTacToe.Domain.DTO.Response
{
    public class MoveResponseModel
    {
        public string[] Board { get; set; }
        public int MoveNumber { get; set; }
    }
    
    public class GameResponseDto : DocumentResponseDto
    {
        public List<Move> Moves { get; set; }
        public WinCondition WinCondition { get; set; }
        public string State { get; set; }

        public GameResponseDto(Game.Game game)
        {
            Id = game.Id;
            CreatedAt = game.CreatedAt;
            UpdatedAt = game.UpdatedAt;
            DeletedAt = game.DeletedAt;
            Moves = game.Moves;
            WinCondition = game.WinCondition;
            State = game.State.Name;
        }
    }
}