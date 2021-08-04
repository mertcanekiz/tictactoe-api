using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Application.Games.Commands.MakeMove
{
    public class MakeMoveCommandValidator : AbstractValidator<MakeMoveCommand>
    {
        private readonly IRepository<Game> _repository;
        
        public MakeMoveCommandValidator(IRepository<Game> repository)
        {
            _repository = repository;
            RuleFor(x => x.Position).NotNull().SetValidator(new PositionValidator());
            RuleFor(x => x.GameId).MustAsync(BeAValidGame);
        }

        private async Task<bool> BeAValidGame(Guid id, CancellationToken cancellationToken = default)
        {
            var game = await _repository.GetByIdAsync(id);
            return game != null;
        }

        class PositionValidator : AbstractValidator<Position>
        {
            public PositionValidator()
            {
                RuleFor(x => x.X).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);
                RuleFor(x => x.Y).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);
            }
        }
    }
}