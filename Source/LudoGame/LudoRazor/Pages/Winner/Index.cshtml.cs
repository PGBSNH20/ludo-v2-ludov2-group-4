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

namespace LudoRazor.Pages.Winner
{
    public class IndexModel : PageModel
    {

        public GameBoard CurrentGame { get; set; }
        

        public IActionResult OnGet(int id)
        {
            var client = new RestClient("https://localhost:44370");
            var request = new RestRequest("api/game/gameboards/" + id, Method.GET);
            var queryResult = client.Execute<GameBoard>(request).Data;

            CurrentGame = queryResult;

            

            return Page();
        }

        [BindProperty]
        public GameBoard GameBoard { get; set; }

     
    }
}
