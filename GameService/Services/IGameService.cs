﻿using System;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game;

namespace TicTacToe.Services
{
    public interface IGameService
    {
        Guid CreateGame(CreateGameRequestDto dto, Guid userId);
        Move MakeMove(Guid gameId, int x, int y, PieceType type);
    }
}