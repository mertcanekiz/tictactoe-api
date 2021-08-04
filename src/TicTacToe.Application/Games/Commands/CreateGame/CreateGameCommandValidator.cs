using System;
using System.Linq;
using System.Reflection;
using FluentValidation;
using TicTacToe.Domain.Enums;
using TicTacToe.Domain.Strategies.WinCondition;
using TicTacToe.Domain.ValueObjects;

namespace TicTacToe.Application.Games.Commands.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.GameTypeName)
                .Must(GameType.Any)
                .WithMessage((command, x) => $"Invalid game type: {x}");

            RuleForEach(x => x.WinConditions)
                .Must(BeAValidWinChecker)
                .WithMessage((_, x) => $"Invalid win checker: {x}");
        }

        private static bool BeAValidWinChecker(string name)
        {
            var type = typeof(IWinChecker);
            var winCheckers = type.Assembly.GetTypes()
                .Where(x => type.IsAssignableFrom(x) && !x.IsInterface);
            var winCheckerType =
                winCheckers.FirstOrDefault(x => x.Name.ToLowerInvariant().StartsWith(name.ToLowerInvariant()));
            return winCheckerType != null;
        }
    }
}