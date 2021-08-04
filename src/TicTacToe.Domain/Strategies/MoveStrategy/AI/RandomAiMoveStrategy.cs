using System;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Strategies.MoveStrategy
{
    public class RandomAiMoveStrategy : IAiMoveStrategy
    {
        public void MakeMove(Game game, TileType tileType)
        {
            var emptyTiles = game.Board.GetEmptyTiles();

            if (emptyTiles.Count == 0) return;

            var selectedTile = emptyTiles[new Random().Next(emptyTiles.Count)];
            
            game.Board.SetTileAt(selectedTile.Position, tileType);
         
            Console.WriteLine($"Computer made move at {selectedTile.Position.X}, {selectedTile.Position.Y}, {tileType}");
        }
    }
}