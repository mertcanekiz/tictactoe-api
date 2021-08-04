using System;
using AspNetCore.Identity.Mongo.Model;

namespace TicTacToe.Infrastructure.Identity
{
    public class ApplicationUser : MongoUser<Guid>
    {
        
    }
}