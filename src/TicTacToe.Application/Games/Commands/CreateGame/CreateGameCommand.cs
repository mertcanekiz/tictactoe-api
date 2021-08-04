using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;

namespace TicTacToe.Application.Games.Commands.CreateGame
{
    public class CreateGameCommand : IRequest<Game>
    {
        public List<string> WinConditions { get; set; }
        public string GameTypeName { get; set; }
        public string FirstPlayerType { get; set; }
    }

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly IRepository<Game> _repository;
        private readonly IGameFactory _gameFactory;

        public CreateGameCommandHandler(IRepository<Game> repository, IGameFactory gameFactory)
        {
            _repository = repository;
            _gameFactory = gameFactory;
        }

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = _gameFactory.CreateGame(
                request.WinConditions,
                request.GameTypeName,
                request.FirstPlayerType);
            return await _repository.CreateAsync(game);
        }
    }
}