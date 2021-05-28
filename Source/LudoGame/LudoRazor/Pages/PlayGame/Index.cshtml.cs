using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LudoAPI.Data;
using LudoAPI.Models;
using RestSharp;

namespace LudoRazor.Pages.PlayGame
{
    public class IndexModel : PageModel
    {
        
        public GameBoard CurrentGame { get; set; }
        public List<Piece> Pieces { get; set; }
        public List<Player> Players { get; set; }
        public string ShowNextPlayer { get; set; } = "";
        


        public IActionResult OnGet(int id)
        {
            var client = new RestClient("https://localhost:44370");
            var request = new RestRequest("api/game/gameboards/" + id, Method.GET);
            var queryResult = client.Execute<GameBoard>(request).Data;

            CurrentGame = queryResult;

            var client2 = new RestClient("https://localhost:44370");
            var request2 = new RestRequest("api/game/pieces-by/" + id , Method.GET);
            var queryResult2 = client2.Execute<List<Piece>>(request2).Data;

            Pieces = queryResult2;
            
            var client3 = new RestClient("https://localhost:44370");
            var request3 = new RestRequest("api/game/get-gameboard/players/" + CurrentGame.Id , Method.GET);
            var queryResult3 = client3.Execute<List<Player>>(request3).Data;

            Players = queryResult3;

            ShowNextPlayer = $"{Players[CurrentGame.CurrentPlayerIndex].Name}, it's your turn to roll the die!";


            return Page();
        }

        
    }
}
