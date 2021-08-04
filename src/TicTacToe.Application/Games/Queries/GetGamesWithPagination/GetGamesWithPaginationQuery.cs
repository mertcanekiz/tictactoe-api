using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Application.Common.Models;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Games.Queries.GetGamesWithPagination
{
    public class GetGamesWithPaginationQuery : IRequest<PaginatedList<Game>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
    
    public class GetGamesWithPaginationQueryHandler : IRequestHandler<GetGamesWithPaginationQuery, PaginatedList<Game>>
    {
        private IRepository<Game> _repository;

        public GetGamesWithPaginationQueryHandler(IRepository<Game> repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<Game>> Handle(GetGamesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var (games, count) = await _repository.GetPaginatedAsync(request.PageNumber, request.PageSize);
            return new PaginatedList<Game>(games, count, request.PageNumber, request.PageSize);
        }
    }
}