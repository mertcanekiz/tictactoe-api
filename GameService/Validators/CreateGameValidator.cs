using System;
using FluentValidation;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.Factory.Board;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.WinConditions;

namespace TicTacToe.Validators
{
    public class CreateGameValidator : AbstractValidator<CreateGameRequestDto>
    {
        public CreateGameValidator()
        {
            RuleFor(x => x.BoardFactory).Must(BoardFactory.Any)
                .WithMessage(((_, s) => $"Invalid board factory: {s}"));
            RuleFor(x => x.PlayerOnePieceType).Must(x => Enum.TryParse<PieceType>(x, true, out _))
                .WithMessage((_, s) => $"Invalid piece type: {s}");
            RuleForEach(x => x.WinConditionCheckers).Must(WinConditionChecker.Any)
                .WithMessage((_, s) => $"Invalid win condition checker: {s}");
            RuleFor(x => x.GameType).Must(GameType.Any)
                .WithMessage((_, s) => $"Invalid game type: {s}");
        }
    }
}