using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Interfaces;
using LudoAPI.Models;

namespace LudoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayer _iPlayer;
        private readonly IGameBoard _board;

        public PlayersController(IPlayer iPlayer, IGameBoard board)
        {
            _iPlayer = iPlayer;
            _board = board;
        }

       

        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer([FromForm] Player player)
        {
            var result = await _iPlayer.AddPlayer(player); 
            await _iPlayer.AddPieces(result);

            if (result == null)
            {
                return BadRequest();
            }

            return StatusCode(StatusCodes.Status201Created, "You have created a user");
        }

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

    }
}
