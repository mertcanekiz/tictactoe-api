using MongoDB.Driver;
using TicTacToe.Models;

namespace TicTacToe.Mongo.Context
{
    public class Context : IContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public Context(MongoClient client, string databaseName)
        {
            _mongoDatabase = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> DbSet<T>(string collection) where T : Document
        {
            return _mongoDatabase.GetCollection<T>(collection);
        }
    }
}