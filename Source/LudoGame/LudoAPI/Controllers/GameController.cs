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
using Microsoft.EntityFrameworkCore;

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
        public List<Player> PlayersList { get; set; }
        
        
        public GameController(IPlayer player, IGameBoard board, IPiece piece, LudoContext dbcontext)
        {
            _player = player;
            _board = board;
            _piece = piece;
            this.dbcontext = dbcontext;
        }


        [Route("get-die/{gameBoardId}")]
        [HttpGet]
        public int PostDieByGameBoardId(int gameBoardId)
        {
            var gameBoard = dbcontext.GameBoards.FirstOrDefault(g => g.Id == gameBoardId);

            int die = Die.RollDie();
            gameBoard.Die = die;
            

            dbcontext.GameBoards.Update(gameBoard);
            dbcontext.SaveChanges();
            return die;
        }

        [Route("update-piece-position/{pieceId}")]
        [HttpGet]
        public int UpdatePiecePosition(int pieceId)
        {
           
            var piece = dbcontext.Pieces.FirstOrDefault(p => p.Id == pieceId);
            var gameBoard = dbcontext.GameBoards.FirstOrDefault(g => g.Id == piece.GameBoardId);
            piece.Position += (int)gameBoard.Die;

            dbcontext.Pieces.Update(piece);
            dbcontext.SaveChanges();

            return piece.Position;
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
            PlayersList = result;

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

        [Route("nextplayer/{CurrentGameBoardId}")]
        [HttpGet]
        public async Task<GameBoard> GetNextPlayer(int CurrentGameBoardId)
        {

            

            //var currentPlayer = dbcontext.Players.FirstOrDefault(p => p.Id == currentPlayerIndex);

            var gameBoard = await dbcontext.GameBoards.Include(g => g.Players).FirstOrDefaultAsync(g => g.Id == CurrentGameBoardId );
            //var gameborad = _board.GetGameBoardWithPlayers(CurrentGameBoardId)
             
                

            /*_board.GetGameBoard(currentPlayer.Id);*/

            if (gameBoard.CurrentPlayerIndex == gameBoard.Players.Count - 1)
            {
                gameBoard.CurrentPlayerIndex = 0;


            }
            else
            {
                gameBoard.CurrentPlayerIndex++;

            }

            //currentPlayer.Id = gameBoard.CurrentPlayerId;

            //currentPlayer = gameBoard.Players.FirstOrDefault(p => p.Id == gameBoard.CurrentPlayerId);
            dbcontext.GameBoards.Update(gameBoard);
            await dbcontext.SaveChangesAsync();
            //var currentPlayerIndex = dbcontext.GameBoards.FirstOrDefault(p => p.Id == gameBoard.CurrentPlayerIndex);
            return gameBoard;
        }

        [Route("pieces-by/{gameId}")]
        [HttpGet]
        public List<Piece> GetPiecesByGameId(int gameId)
        {
            var pieces =  dbcontext.Pieces.Where(p => p.GameBoardId == gameId).ToList();
            return pieces;

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
        [HttpGet]
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