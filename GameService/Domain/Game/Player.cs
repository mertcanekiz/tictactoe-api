﻿using System;
using Core.Mongo;

namespace TicTacToe.Domain.Game
{
    public class Player : Document
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public PieceType Type { get; set; }
    }
}