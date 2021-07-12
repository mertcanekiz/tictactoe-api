using MongoDB.Driver;

namespace Core.Mongo.Context
{
    public interface IContext
    {
        IMongoCollection<T> DbSet<T>(string collection) where T : Document;
    }
}