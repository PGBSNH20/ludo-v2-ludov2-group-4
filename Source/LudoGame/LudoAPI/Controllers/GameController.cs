using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Data;
using LudoAPI.Data.Interfaces;
using LudoAPI.Interfaces;
using LudoAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LudoAPI.Controllers
{
    //[EnableCors("MyAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IPiece _piece;
        private readonly LudoContext dbcontext;
        private readonly IPlayer _player;
        private readonly IGameBoard _board;
        
        public GameController(IPlayer player, IGameBoard board, IPiece piece, LudoContext dbcontext)
        {
            _player = player;
            _board = board;
            _piece = piece;
            this.dbcontext = dbcontext;
        }

        [Route("players")]
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer([FromBody] Player player)
        {           
            var gameBoard = _board.GetGameBoard(player.GameBoardId);
            var opponents = _player.GetPlayersByGameBoardId(player.GameBoardId);
            var trueColor = gameBoard.Colors.Any(x => x == player.Color);
            var colorNotAvailable = opponents.Any(o => o.Color.ToLower() == player.Color.ToLower());
            var nameNotAvailable = opponents.Any(o => o.Name.ToLower() == player.Name.ToLower());
            var amountOfPlayer = _player.GetPlayersByGameBoardId(player.GameBoardId).Count();           

            if (amountOfPlayer == 4) return BadRequest("A ludo game can only include 2-4 players");
            
            if (!trueColor) return BadRequest("Invalid Color");

            if (colorNotAvailable) return BadRequest("Color is occupied");

            if (nameNotAvailable) return BadRequest("The name already exists");

            var result = await _player.AddPlayer(player);
            await _piece.AddPieces(result);

            if (result == null) return BadRequest();

            return StatusCode(StatusCodes.Status201Created, "You have created a user");
        }


        [Route("get-gameboards")]
        [HttpGet]
        public async Task<ActionResult<List<GameBoard>>> GetGameBoards()
        {
            var result = _board.GetGameBoards();

            if (result == null) return NotFound("That player id doesn't exist");

            return Ok(result);
        }

        [Route("get-gameboard/players/{id}")]
        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetPlayersByGameBoard(int id)
        {
            var result = _player.GetPlayersByGameBoardId(id);

            if (result == null) return NotFound();

            return Ok(result);
        }



        [Route("gameboards")]
        [HttpPost]
        public async Task<IActionResult> PostGameBoard([FromBody] GameBoard gameBoard)
        {
            var result = await _board.AddNewGame(gameBoard);

            if (result == null) return NotFound();

            return StatusCode(StatusCodes.Status201Created, "You have created a gameboard");
        }

        [Route("gameboards/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetGameBoardById(int id)
        {
            var gameBoard = _board.GetGameBoard(id);

            if (gameBoard == null) return NotFound("That gameboard id doesn't exist");

            return Ok(gameBoard);
        }

        [Route("nextplayer/{id}")]
        [HttpGet]
        public async Task GetNextPlayer(int id)
        {
            var currentPlayer = dbcontext.Players.FirstOrDefault(p => p.Id == id);

            var gameBoard = dbcontext.GameBoards.FirstOrDefault(g => g.Id == currentPlayer.GameBoardId);      /*_board.GetGameBoard(currentPlayer.Id);*/

            if (gameBoard.CurrentPlayerId == gameBoard.Players.Count())
            {
                gameBoard.CurrentPlayerId = 0;

            }
            else
            {
                gameBoard.CurrentPlayerId++;

            }
            
            
            currentPlayer = gameBoard.Players.FirstOrDefault(p => p.Id == gameBoard.CurrentPlayerId);
            dbcontext.GameBoards.Update(gameBoard);
            await dbcontext.SaveChangesAsync();
            //return currentPlayer;
        }

        [Route("pieces/{playerId}")]
        [HttpGet]
        public async Task<ActionResult<List<Piece>>> GetPiecesByPlayerId(int playerId)
        {
            var result =  await _piece.GetPlayerPieces(playerId);

            if (result == null) return NotFound("That player id doesn't exist");

            return Ok(result);
        }

        [Route("get-piece/{id}")]
        public async Task<Piece> GetPieceById(int id)
        {
            var result = await _piece.GetPieceById(id);

            
            return result;
        }

        [Route("pieces")]
        [HttpPut]
        public async Task<IActionResult> PutPiece([FromBody] Piece piece)
        {
            var result = await _piece.MovePiece(piece);

            if (result.Position == 0) return BadRequest();

            return StatusCode(StatusCodes.Status200OK, $"You have moved a piece with id {piece.Id} ");
        }
    }
}