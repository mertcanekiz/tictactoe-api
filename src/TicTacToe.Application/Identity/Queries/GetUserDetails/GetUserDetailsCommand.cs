using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TicTacToe.Application.Common.Attributes;
using TicTacToe.Application.Common.Interfaces;

namespace TicTacToe.Application.Identity.Queries.GetUserDetails
{
    [Authorize]
    public class GetUserDetailsCommand : IRequest<UserDetailsDto>
    {
        public Guid UserId { get; set; }
    }
    
    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsCommand, UserDetailsDto>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public GetUserDetailsQueryHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<UserDetailsDto> Handle(GetUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId ?? Guid.Empty;
            var username = await _identityService.GetUsernameAsync(userId);
            return new UserDetailsDto
            {
                UserId = userId,
                Username = username
            };
        }
    }
}