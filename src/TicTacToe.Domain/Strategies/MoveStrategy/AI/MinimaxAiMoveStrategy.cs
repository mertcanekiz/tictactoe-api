using System;
using System.Collections.Generic;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Domain.Strategies.MoveStrategy
{
    public class MinimaxAiMoveStrategy : IAiMoveStrategy
    {
        
        public void MakeMove(Game game, TileType tileType)
        {
            var aiTileType = tileType;
            var humanTileType = tileType == TileType.X ? TileType.O : TileType.X;
            var scores = new Dictionary<TileType, int>
            {
                {aiTileType, 1},
                {humanTileType, -1},
                {TileType.None, 0}
            };
            var bestScore = int.MinValue;
            Position selectedPosition = null;
            var board = game.Board;
            var emptyTiles = board.GetEmptyTiles();
            foreach (var tile in emptyTiles)
            {
                board.SetTileAt(tile.Position, tileType);
                var score = Minimax(game, 0, false, scores, aiTileType, humanTileType);
                board.SetTileAt(tile.Position, TileType.None);
                if (score > bestScore)
                {
                    bestScore = score;
                    selectedPosition = tile.Position;
                }
            }

            if (selectedPosition != null)
            {
                board.SetTileAt(selectedPosition, tileType);
            }
        }

        private int Minimax(Game game, int depth, bool isMaximizing, IReadOnlyDictionary<TileType, int> scores, TileType aiTileType, TileType humanTileType)
        {
            var result = game.GetFirstWinChecker().Check(game);
            if (result != null)
            {
                return scores[result.Winner ?? TileType.None];
            }

            if (isMaximizing)
            {
                var bestScore = int.MinValue;
                var emptyTiles = game.Board.GetEmptyTiles();
                foreach (var tile in emptyTiles)
                {
                    game.Board.SetTileAt(tile.Position, aiTileType);
                    var score = Minimax(game, depth + 1, false, scores, aiTileType, humanTileType);
                    game.Board.SetTileAt(tile.Position, TileType.None);
                    bestScore = Math.Max(score, bestScore);
                }

                return bestScore;
            }
            else
            {
                var bestScore = int.MaxValue;
                var emptyTiles = game.Board.GetEmptyTiles();
                foreach (var tile in emptyTiles)
                {
                    game.Board.SetTileAt(tile.Position, humanTileType);
                    var score = Minimax(game, depth + 1, true, scores, aiTileType, humanTileType);
                    game.Board.SetTileAt(tile.Position, TileType.None);
                    bestScore = Math.Min(score, bestScore);
                }

                return bestScore;
            }
        }
    }
}