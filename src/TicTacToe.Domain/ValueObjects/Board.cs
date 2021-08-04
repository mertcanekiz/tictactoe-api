using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.ValueObjects
{
    public class Board
    {
        public List<Tile> Tiles { get; set; }

        public List<List<Tile>> GetAllRows()
        {
            return Tiles.GroupBy(x => x.Position.Y, x => x, (key, g) => g.ToList()).ToList();
        }
        
        public List<List<Tile>> GetAllColumns()
        {
            return Tiles.GroupBy(x => x.Position.X, x => x, (key, g) => g.ToList()).ToList();
        }

        public List<List<Tile>> GetDiagonals()
        {
            var result = new List<List<Tile>>();
            result.Add(Tiles.Where(x => x.Position.X == x.Position.Y).ToList());
            result.Add(Tiles.Where(x => x.Position.X == 2 - x.Position.Y).ToList());
            return result;
        }

        public List<Tile> GetRowContainingTile(Tile tile)
        {
            return Tiles.Where(x => x.Position.Y == tile.Position.Y).ToList();
        }

        public List<Tile> GetColumnContainingTile(Tile tile)
        {
            return Tiles.Where(x => x.Position.X == tile.Position.X).ToList();
        }

        public List<Tile> GetEmptyTiles()
        {
            return Tiles.Where(x => x.IsEmpty).ToList();
        }

        public Tile GetTileAt(Position position)
        {
            return Tiles.FirstOrDefault(x => x.Position.Equals(position));
        }

        public void SetTileAt(Position position, TileType type)
        {
            var tile = Tiles.FirstOrDefault(x => Equals(x.Position, position));
            
            if (tile == null)
            {
                throw new Exception();
            }

            tile.Type = type;
        }

        public void Clear()
        {
            Tiles.ForEach(x => x.Type = TileType.None);
        }

        public Board Clone()
        {
            return new Board
            {
                Tiles = new List<Tile>(Tiles)
            };
        }
    }
}