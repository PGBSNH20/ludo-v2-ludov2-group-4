using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LudoAPI.Data;
using LudoAPI.Models;
using RestSharp;

namespace LudoRazor.Pages.MovePiece
{
    public class IndexModel : PageModel
    {
        private readonly LudoAPI.Data.LudoContext _context;

        public IndexModel(LudoAPI.Data.LudoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GameBoard GameBoard { get; set; }
        public int CurrentPlayerId { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Piece> Pieces { get; set; }
        public List<Player> Players { get; set; }
        public Piece ChoosedPiece { get; set; }
        public int Die { get; set; }
        public int NextPlayerId { get; private set; }
        public Player NextPlayer { get; private set; }
        public string Winner { get; set; }

        public async Task<IActionResult> OnGetAsync(int? gameId, int dieValue, int pieceId)
        {
            Die = dieValue;

            if (gameId == null)
            {
                return NotFound();
            }


            var client = new RestClient("https://localhost:44370");
            var request = new RestRequest("api/game/gameboards/" + gameId, Method.GET);
            var queryResult = client.Execute<GameBoard>(request).Data;

            GameBoard = queryResult;

            var client3 = new RestClient("https://localhost:44370");
            var request3 = new RestRequest("api/game/get-gameboard/players/" + gameId, Method.GET);
            var queryResult3 = client3.Execute<List<Player>>(request3).Data;

            Players = queryResult3;
            
            
            CurrentPlayer = Players[GameBoard.CurrentPlayerIndex];
            GameBoard.Players = Players;
            CurrentPlayerId = GameBoard.CurrentPlayerIndex;
            

            var client2 = new RestClient("https://localhost:44370");
            var request2 = new RestRequest("api/game/get-piece/" + pieceId, Method.GET);
            var queryResult2 = client2.Execute<Piece>(request2).Data;

           ChoosedPiece = queryResult2;


            var client4 = new RestClient("https://localhost:44370");
            var request4 = new RestRequest("api/game/pieces/" + CurrentPlayer.Id, Method.GET);
            var queryResult4 = client4.Execute<List<Piece>>(request4).Data;

            Pieces = queryResult4;

            var client5 = new RestClient("https://localhost:44370");
            var request5 = new RestRequest("api/game/nextplayer/" + GameBoard.Id, Method.GET);
            var nextPlayer = client5.Execute<GameBoard>(request5).Data;
            

            var client6 = new RestClient("https://localhost:44370");
            var request6 = new RestRequest("api/game/update-piece-position/" + ChoosedPiece.Id, Method.GET);
            var updatedPiecePosition = client6.Execute<int>(request6).Data;

            // Moving the choosed piece by adding the die.
            ChoosedPiece.Position = updatedPiecePosition;

            if (GameBoard.Winner != null)
            {
                Winner = GameBoard.Winner;
            }

            return Page();
        }

    }
}
