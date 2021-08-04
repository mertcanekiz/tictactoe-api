using FluentValidation;

namespace TicTacToe.Application.Games.Commands.JoinGame
{
    public class JoinGameCommandValidator : AbstractValidator<JoinGameCommand>
    {
        public JoinGameCommandValidator()
        {
            RuleFor(x => x.GameId).NotNull().NotEmpty();
        }
    }
}