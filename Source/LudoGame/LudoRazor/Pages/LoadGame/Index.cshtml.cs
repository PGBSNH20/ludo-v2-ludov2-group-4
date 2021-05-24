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

namespace LudoRazor.Pages.LoadGame
{
    public class IndexModel : PageModel
    {
        readonly LudoContext _dbContext;

        public IndexModel(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<GameBoard> GameBoard { get; set; }
            

        public IActionResult OnGet()
        {
            var client = new RestClient("https://localhost:44370/api/Game/get-gameboards");
            var request = new RestRequest(Method.GET);
            request.AddJsonBody(GameBoard);
            var response = client.Execute<List<GameBoard>>(request);

            GameBoard = response.Data.ToList();

            //Following code works, GETs GameBoards straight from Db: 
            //GameBoard =  _dbContext.GameBoards.ToList();

            return Page();
        }
    }
}
