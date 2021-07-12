using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Mongo.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.User;
using TicTacToe.Exceptions;

namespace TicTacToe.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly string _jwtKey;

        public UserService(IRepository<User> repository, IConfiguration configuration)
        {
            _repository = repository;
            _jwtKey = configuration.GetSection("JwtKey").ToString();
        }

        public Guid CreateUser(RegisterRequestDto dto)
        {
            var existingUser = _repository.FindOne(x => x.Username == dto.Username);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(dto.Username);
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User()
            {
                Username = dto.Username,
                Password = hashedPassword
            };
            return _repository.InsertOne(user);
        }

        public string Authenticate(LoginRequestDto dto)
        {
            var user = _repository.FindOne(x => x.Username == dto.Username);
            if (user == null)
                return null;
            var passwordsMatch = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!passwordsMatch)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, dto.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public List<User> GetAllUsers()
        {
            return _repository.FindAll();
        }
    }
}