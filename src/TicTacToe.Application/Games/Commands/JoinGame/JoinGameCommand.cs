using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Exceptions;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.States;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Application.Games.Commands.JoinGame
{
    public class JoinGameCommand : IRequest
    {
        public Guid GameId { get; set; }
    }

    public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand>
    {
        private ICurrentUserService _currentUserService;
        private IRepository<Game> _repository;

        public JoinGameCommandHandler(ICurrentUserService currentUserService, IRepository<Game> repository)
        {
            _currentUserService = currentUserService;
            _repository = repository;
        }

        public async Task<Unit> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _repository.GetByIdAsync(request.GameId);

            if (game == null)
            {
                throw new NotFoundException(nameof(Game), request.GameId);
            }

            if (game.GameStateName != nameof(WaitingForPlayerTwoState))
            {
                throw new GameNotWaitingForPlayerTwoException();
            }

            var userId = _currentUserService.UserId;

            game.PlayerTwo = new Player
            {
                Id = userId,
                Type = game.PlayerOne.Type == TileType.X ? TileType.O : TileType.X
            };

            game.GameStateName = game.GetGameState().NextStateName;

            await _repository.ReplaceAsync(game);

            return Unit.Value;
        }
    }
}