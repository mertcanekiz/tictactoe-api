using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Game.Factory.Board;

namespace TicTacToe.Domain.Game.Strategies.Move.ComputerStrategies
{
    public class RandomComputerStrategy : IComputerStrategy
    {
        public void MakeMove(Game game, PieceType pieceType)
        {
            Console.WriteLine("Making random move as the computer");
            var board = game.Moves.Last().Board.Clone();
            var emptyTiles = (from tileRow in board.Tiles from tile in tileRow where tile.PieceType == PieceType.Empty select tile).ToList();
            if (emptyTiles.Count == 0) return;
            var selectedTile = emptyTiles[new Random().Next(emptyTiles.Count)];
            Console.WriteLine($"Computer makes a move: {pieceType} at ({selectedTile.Position.X}, {selectedTile.Position.Y})");
            board.Tiles[selectedTile.Position.Y][selectedTile.Position.X].PieceType = pieceType;
        }
    }
}