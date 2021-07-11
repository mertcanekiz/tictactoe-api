using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TicTacToe.Models;

namespace TicTacToe.Repository
{
    public interface IRepository<T> where T : Document
    {
        List<T> FindAll();
        T FindById(Guid id);
        Guid InsertOne(T document);
        T UpdateOne(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties);
    }
}