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

        public PlayersController(IPlayer iPlayer)
        {
            _iPlayer = iPlayer;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> Post([FromForm] Player player)
        {
            var result = await _iPlayer.AddPlayer(player);

            if (result == null)
            {
                return BadRequest();
            }

            return StatusCode(StatusCodes.Status201Created, "You have created a user");
        }
    }
}
