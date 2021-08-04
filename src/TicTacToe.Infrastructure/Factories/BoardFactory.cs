using System.Collections.Generic;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Factories.BoardFactory;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Infrastructure.Factories
{
    public class BoardFactory : IBoardFactory
    {
        public Board CreateEmpty()
        {
            return new Board
            {
                Tiles = new List<Tile>
                {
                    new Tile {Position = new Position {X = 0, Y = 0}, Type = TileType.None},
                    new Tile {Position = new Position {X = 1, Y = 0}, Type = TileType.None},
                    new Tile {Position = new Position {X = 2, Y = 0}, Type = TileType.None},

                    new Tile {Position = new Position {X = 0, Y = 1}, Type = TileType.None},
                    new Tile {Position = new Position {X = 1, Y = 1}, Type = TileType.None},
                    new Tile {Position = new Position {X = 2, Y = 1}, Type = TileType.None},

                    new Tile {Position = new Position {X = 0, Y = 2}, Type = TileType.None},
                    new Tile {Position = new Position {X = 1, Y = 2}, Type = TileType.None},
                    new Tile {Position = new Position {X = 2, Y = 2}, Type = TileType.None},
                }
            };
        }
    }
}