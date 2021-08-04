using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Domain.Common;

namespace TicTacToe.Application.Common.Interfaces
{
    public interface IRepository<T> where T : Document
    {
        Task<T> CreateAsync(T document);
        Task<T> GetByIdAsync(Guid id);
        Task<T> ReplaceAsync(T document);
        Task<(List<T> items, long count)> GetPaginatedAsync(int pageNumber, int pageSize);
    }
}