using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Application.Games.Commands.CreateGame;
using TicTacToe.Application.Games.Commands.JoinGame;
using TicTacToe.Application.Games.Commands.MakeMove;
using TicTacToe.Application.Games.Queries.GetGameById;
using TicTacToe.Application.Games.Queries.GetGamesWithPagination;

namespace TicTacToe.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public GamesController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames([FromQuery] GetGamesWithPaginationQuery query)
        {
            var games = await _mediator.Send(query);
            return Ok(games);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGameById([FromRoute] Guid id)
        {
            var game = await _mediator.Send(new GetGameByIdQuery {Id = id});
            return Ok(game);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody] CreateGameCommand command)
        {
            // var userId = _currentUserService.UserId;
            // return Ok(userId);
            var game = await _mediator.Send(command);
            return Ok(game);
        }

        [HttpPost("{id:guid}/join")]
        public async Task<IActionResult> JoinGame([FromRoute] Guid id)
        {
            await _mediator.Send(new JoinGameCommand {GameId = id});
            return Ok(new {Success = true});
        }

        [HttpPost("{id:guid}/makeMove")]
        public async Task<IActionResult> MakeMove(Guid id, [FromBody] MakeMoveCommand command)
        {
            if (id != command.GameId)
            {
                return BadRequest();
            }
            var game = await _mediator.Send(command);
            return Ok(game);
        }
    }
}