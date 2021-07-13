using FluentValidation;
using TicTacToe.Domain.DTO.Request;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.States;
using TicTacToe.Domain.Game.WinConditions;
using TicTacToe.Factory.Board;

namespace TicTacToe.Validators
{
    public class CreateGameValidator : AbstractValidator<CreateGameRequestDto>
    {
        public CreateGameValidator()
        {
            RuleFor(x => x.BoardFactory).Must(BoardFactory.Any)
                .WithMessage(((_, s) => $"Invalid board factory: {s}"));
            RuleFor(x => x.InitialState).Must(GameState.Any)
                .WithMessage((_, s) => $"Invalid initial state: {s}");
            RuleForEach(x => x.WinConditionCheckers).Must(WinConditionChecker.Any)
                .WithMessage((_, s) => $"Invalid win condition checker: {s}");
        }
    }
}