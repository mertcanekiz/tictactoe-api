using System;
using System.Collections.Generic;
using TicTacToe.Models.DTO.Request;
using TicTacToe.Models.User;

namespace TicTacToe.Services
{
    public interface IUserService
    {
        Guid CreateUser(RegisterRequestDto dto);
        string Authenticate(LoginRequestDto dto);
        List<User> GetAllUsers();
    }
}