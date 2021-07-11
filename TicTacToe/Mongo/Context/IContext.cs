using MongoDB.Driver;
using TicTacToe.Models;

namespace TicTacToe.Mongo.Context
{
    public interface IContext
    {
        IMongoCollection<T> DbSet<T>(string collection) where T : Document;
    }
}