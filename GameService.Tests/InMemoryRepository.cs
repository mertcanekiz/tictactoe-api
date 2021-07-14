using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Mongo;
using Core.Mongo.Repository;

namespace GameService.Tests
{
    public class InMemoryRepository<T> : IRepository<T> where T : Document
    {
        private readonly Dictionary<Guid, T> _documents = new();
        
        public List<T> FindAll()
        {
            return _documents.Values.ToList();
        }

        public T FindById(Guid id)
        {
            _documents.TryGetValue(id, out var result);
            return result;
        }

        public T FindOne(Expression<Func<T, bool>> expression)
        {
            var result = _documents.Values.AsQueryable().FirstOrDefault(expression);
            return result;
        }

        public Guid InsertOne(T document)
        {
            if (document.Id == Guid.Empty)
                document.Id = Guid.NewGuid();
            _documents[document.Id] = document;
            return document.Id;
        }

        public T UpdateOne(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties)
        {
            throw new NotImplementedException();
        }
    }
}