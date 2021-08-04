using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Attributes;
using TicTacToe.Application.Common.Exceptions;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Factories;
using TicTacToe.Domain.ValueObjects;
using UnauthorizedAccessException = TicTacToe.Application.Common.Exceptions.UnauthorizedAccessException;

namespace TicTacToe.Application.Games.Commands.MakeMove
{
    [Authorize]
    public class MakeMoveCommand : IRequest<Game>
    {
        public Guid GameId { get; set; }
        public Position Position { get; set; }
    }

    public class MakeMoveCommandHandler : IRequestHandler<MakeMoveCommand, Game>
    {
        private readonly IRepository<Game> _repository;
        private readonly ICurrentUserService _currentUserService;

        public MakeMoveCommandHandler(IRepository<Game> repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<Game> Handle(MakeMoveCommand request, CancellationToken cancellationToken)
        {
            var game = await _repository.GetByIdAsync(request.GameId);
            if (game is null)
            {
                throw new NotFoundException(nameof(Game), request.GameId);
            }

            var userId = _currentUserService.UserId;

            if (userId != game.GetGameState().Player.Id)
            {
                throw new UnauthorizedAccessException();
            }

            var gameType = GameType.GetGameTypeByName(game.GameTypeName);
            var moveStrategy = gameType.MoveStrategy;
            
            moveStrategy.MakeMove(game, request.Position);

            await _repository.ReplaceAsync(game);

            return game;
        }
    }
}