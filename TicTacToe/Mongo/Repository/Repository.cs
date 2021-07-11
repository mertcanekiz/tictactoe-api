using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using TicTacToe.Models;
using TicTacToe.Mongo.Context;

namespace TicTacToe.Repository
{
    public class Repository<T> : IRepository<T> where T : Document
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IContext context, string collectionName = "")
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;
            }

            collectionName = collectionName.First().ToString().ToLower() + collectionName.Substring(1);
            _collection = context.DbSet<T>(collectionName);
        }

        public List<T> FindAll()
        {
            return _collection.AsQueryable().ToList();
        }

        public T FindById(Guid id)
        {
            return _collection.AsQueryable().FirstOrDefault(x => x.Id.Equals(id));
        }

        public Guid InsertOne(T document)
        {
            if (document.Id == Guid.Empty)
            {
                document.Id = Guid.NewGuid();
            }
            document.CreatedAt = DateTime.Now;
            document.UpdatedAt = DateTime.Now;
            _collection.InsertOne(document);
            return document.Id;
        }

        public T UpdateOne(Expression<Func<T, bool>> expression,
            params (Expression<Func<T, object>>, object)[] updatedProperties)
        {
            var update = Builders<T>.Update.Set(x => x.UpdatedAt, DateTime.Now);
            foreach (var (key, value) in updatedProperties)
            {
                update = update.Set(key, value);
            }

            var result = _collection.FindOneAndUpdate(expression, update, new FindOneAndUpdateOptions<T>()
            {
                ReturnDocument = ReturnDocument.After
            });
            return result;
        }
    }
}