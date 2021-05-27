using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LudoAPI.Data;
using LudoAPI.Models;
using RestSharp;

namespace LudoRazor.Pages.ChoosePiece
{
    public class IndexModel : PageModel
    {
        private readonly LudoAPI.Data.LudoContext _context;

        public IndexModel(LudoAPI.Data.LudoContext context)
        {
            _context = context;
        }

        public GameBoard CurrentGame { get;set; }
        public int Die { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Player> Players { get; set; }
        public List<Piece> CurrentPlayerPieces { get; set; }
        public string ValidatePlayer { get; set; } = "";
       


        public async Task OnGetAsync(int gameId)
        {
            //CurrentGame = _context.GameBoards.FirstOrDefault(g => g.Id == id);

            /*_context.Players.Where(p => p.GameBoardId == CurrentGame.Id).ToList();*/
            
            //Pieces = _context.Pieces.Where(p => p.PlayerId == CurrentPlayer.Id ).ToList();
            

            


            var client4 = new RestClient("https://localhost:44370");
            var request4 = new RestRequest("api/game/get-die/" + gameId, Method.GET);
            var queryResult4 = client4.Execute<int>(request4).Data;

            Die = queryResult4;

            var client = new RestClient("https://localhost:44370");
            var request = new RestRequest("api/game/gameboards/" + gameId, Method.GET);
            var queryResult = client.Execute<GameBoard>(request).Data;

            CurrentGame = queryResult;

            var client3 = new RestClient("https://localhost:44370");
            var request3 = new RestRequest("api/game/get-gameboard/players/" + CurrentGame.Id, Method.GET);
            var queryResult3 = client3.Execute<List<Player>>(request3).Data;

            Players = queryResult3;



            //Players = queryResult.Players;
            CurrentPlayer = Players[queryResult.CurrentPlayerIndex];
            ValidatePlayer = $"{CurrentPlayer.Name}";


            var client2 = new RestClient("https://localhost:44370");
            var request2 = new RestRequest("api/game/pieces/" + CurrentPlayer.Id, Method.GET);
            var queryResult2 = client2.Execute<List<Piece>>(request2).Data;

            CurrentPlayerPieces = queryResult2;


           

        }
    }
}
