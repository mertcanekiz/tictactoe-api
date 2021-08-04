using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Exceptions;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Games.Queries.GetGameById
{
    public class GetGameByIdQuery : IRequest<Game>
    {
        public Guid Id { get; set; }
    }
    
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Game>
    {
        private readonly IRepository<Game> _repository;

        public GetGameByIdQueryHandler(IRepository<Game> repository)
        {
            _repository = repository;
        }

        public async Task<Game> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await _repository.GetByIdAsync(request.Id);

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.Id);
            }

            return game;
        }
    }
}