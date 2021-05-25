using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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
        [BindProperty(SupportsGet = true)]
        public IList<GameBoard> GameBoard { get; set; }
            

        public async Task OnGetAsync()
        {
            //var client = new RestClient("https://localhost:44370/api/Game/get-gameboards");
            //var request = new RestRequest(Method.GET);
            
            //var response = client.Execute<List<GameBoard>>(request);
            var client = new RestClient("https://localhost:44370");
            var request = new RestRequest("api/game/get-gameboards/", Method.GET);
            var queryResult = client.Execute<IList<GameBoard>>(request).Data;

            GameBoard = queryResult;
            
            //GameBoard = response.Data;

            //Following code works, GETs GameBoards straight from Db:
            /*GameBoard = _dbContext.GameBoards.ToList();*/

            


        }
    }
    
}
