using Core.Mongo;

namespace TicTacToe.Models.User
{
    public class User : Document
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}