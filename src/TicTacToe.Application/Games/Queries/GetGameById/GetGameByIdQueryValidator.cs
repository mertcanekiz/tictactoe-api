using FluentValidation;

namespace TicTacToe.Application.Games.Queries.GetGameById
{
    public class GetGameByIdQueryValidator : AbstractValidator<GetGameByIdQuery>
    {
        public GetGameByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}