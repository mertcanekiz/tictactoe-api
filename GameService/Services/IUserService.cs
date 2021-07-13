using System;
using System.Collections.Generic;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.User;

namespace TicTacToe.Services
{
    public interface IUserService
    {
        Guid CreateUser(RegisterRequestDto dto);
        string Authenticate(LoginRequestDto dto);
        List<User> GetAllUsers();
        User GetUserById(Guid id);
    }
}