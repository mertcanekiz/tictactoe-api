using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using TicTacToe.Exceptions;

namespace TicTacToe.Domain.Game.States
{
    [BsonDiscriminator(Required = true)]
    [BsonKnownTypes(typeof(XPlayerTurnState))]
    [BsonKnownTypes(typeof(OPlayerTurnState))]
    public abstract class GameState
    {
        public static readonly GameState XPlayerTurnState = new XPlayerTurnState();
        public static readonly GameState OPlayerTurnState = new OPlayerTurnState();
        public static readonly GameState FinishedGameState = new FinishedGameState();

        private static readonly Dictionary<string, GameState> States = new();

        static GameState()
        {
            States.Add(XPlayerTurnState.Name, XPlayerTurnState);
            States.Add(OPlayerTurnState.Name, OPlayerTurnState);
            States.Add(FinishedGameState.Name, FinishedGameState);
        }

        public static GameState GetGameStateByName(string name)
        {
            States.TryGetValue(name, out var state);
            return state;
        }

        public static bool Any(string name)
        {
            return States.ContainsKey(name);
        }

        public abstract string Name { get; }
        protected abstract GameState NextState { get; }
        protected abstract PieceType PieceType { get; }

        public virtual void MakeMove(Game game, int x, int y)
        {
            var previousMove = game.Moves.Last();
            
            var board = new Board()
            {
                Tiles = previousMove.Board.Tiles.Select(a => a.ToArray()).ToArray()
            };
            
            if (board.Tiles[y][x] != PieceType.Empty)
                throw new TileNotEmptyException(x, y);
            
            board.Tiles[y][x] = PieceType;
            
            game.Moves.Add(new Move()
            {
                Board = board,
                MoveNumber = previousMove.MoveNumber + 1
            });
            
            game.CheckWinConditions();

            game.State = NextState;
        }
    }
}