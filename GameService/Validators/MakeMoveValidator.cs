using FluentValidation;
using TicTacToe.Domain.DTO.Request;

namespace TicTacToe.Validators
{
    public class MakeMoveValidator : AbstractValidator<MakeMoveRequestDto>
    {
        public MakeMoveValidator()
        {
            RuleFor(x => x.X).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2)
                .WithMessage("X coordinate of move must be in range [0-2]");
            RuleFor(x => x.Y).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2)
                .WithMessage("Y coordinate of move must be in range [0-2]");
        }
    }
}