using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Application.Identity.Commands.Login;

namespace TicTacToe.Application.Identity.Commands.Register
{
    public class RegisterCommand : IRequest<LoginResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoginResult>
    {
        private readonly IIdentityService _identityService;
        private IMediator _mediator;

        public RegisterCommandHandler(IIdentityService identityService, IMediator mediator)
        {
            _identityService = identityService;
            _mediator = mediator;
        }

        public async Task<LoginResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var (result, userId) = await _identityService.CreateUserAsync(request.Username, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception(); // TODO
            }

            var loginResult = await _mediator.Send(new LoginCommand
            {
                Username = request.Username,
                Password = request.Password
            }, cancellationToken);

            return loginResult;
        }
    }
}