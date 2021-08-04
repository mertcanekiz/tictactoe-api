using System.Linq;
using Microsoft.AspNetCore.Identity;
using TicTacToe.Application.Common.Models;

namespace TicTacToe.Infrastructure.Identity
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(x => x.Description));
        }
    }
}