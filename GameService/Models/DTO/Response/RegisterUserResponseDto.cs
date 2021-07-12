﻿using System;

namespace TicTacToe.Models.DTO.Response
{
    public class RegisterUserResponseDto
    {
        public RegisterUserResponseDto(Guid createdUserId, bool success = true)
        {
            Id = createdUserId;
            Success = success;
        }

        public Guid Id { get; set; }
        public bool Success { get; set; }
    }
}