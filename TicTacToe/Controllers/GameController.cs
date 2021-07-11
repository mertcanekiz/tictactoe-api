using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Models;
using TicTacToe.Models.Request;
using TicTacToe.Models.Response;
using TicTacToe.Repository;
using TicTacToe.Services;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IRepository<Game> _repository;
        private readonly IGameService _service;

        public GameController(IRepository<Game> repository, IGameService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_repository.FindAll().Select(x => new GameResponseModel(x)));
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateGameRequestModel model)
        {
            var createdId = _service.CreateGame(model);
            return Ok(new {
                Id = createdId,
                Success = true
            });
        }

        [HttpGet("{id}")]
        public IActionResult Show([FromRoute] Guid id)
        {
            var game = _repository.FindById(id);
            if (game == null)
                return NotFound(new {Id = id, Success = false});
            return Ok(new GameResponseModel(game));
        }

        [HttpPost("{id:guid}/makeMove")]
        public IActionResult MakeMove([FromRoute] Guid id, [FromBody] MakeMoveRequestModel move)
        {
            var newState = _service.MakeMove(id, move.X, move.Y,
                move.Type == "x" ? PieceType.X : PieceType.O);
            return Ok(newState);
        }
    }
}