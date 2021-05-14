using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Interfaces;
using LudoAPI.Models;
using LudoAPI.Data.Interfaces;

namespace LudoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IPiece _piece;
        private readonly IPlayer _player;
        private readonly IGameBoard _board;

        public GameController(IPlayer player, IGameBoard board, IPiece piece)
        {
            _player = player;
            _board = board;
            _piece = piece;
        }

        [Route("players")]
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer([FromForm] Player player)
        {
            var result = await _player.AddPlayer(player);
            await _piece.AddPieces(result);

            if (result == null)
            {
                    return BadRequest();
            }
                

            return StatusCode(StatusCodes.Status201Created, "You have created a user");
        }

        [Route("gameboards")]
        [HttpPost]
        public async Task<IActionResult> PostGameBoard([FromForm] GameBoard gameBoard)
        {
            var result = await _board.AddNewGame(gameBoard);

            if (result == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status201Created, "You have created a gameboard");
        }

        [Route("pieces")]
        [HttpPut]
        public async Task<IActionResult> PutPiece([FromForm] Piece piece)
        {
            var result = await _piece.MovePiece(piece);

            if (result.Position == 0)
            {
                return BadRequest();
            }

            return StatusCode(StatusCodes.Status200OK, $"You have moved a piece with id {piece.Id} ");
        }
    }
}
