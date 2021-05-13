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
    public class GameBoardController : ControllerBase
    {
        private readonly IGameBoard _gameBoard;
        

        public GameBoardController(IGameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

      
    }
}
