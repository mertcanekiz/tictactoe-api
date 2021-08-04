using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Exceptions;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Application.Common.Models;

namespace TicTacToe.Application.Identity.Commands.Login
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IIdentityService _identityService;

        public LoginCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var (result, loginResult) = await _identityService.LoginAsync(request.Username, request.Password);
            
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }

            return loginResult;
        }
    }
}