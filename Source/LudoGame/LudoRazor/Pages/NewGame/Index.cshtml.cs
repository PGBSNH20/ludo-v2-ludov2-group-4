using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LudoAPI.Data;
using LudoAPI.Models;
using Newtonsoft.Json;
using RestSharp;

namespace LudoRazor.Pages.NewGame
{
    public class GameModel : PageModel
    {
        public string GameBoardMessage { get; set; }

        [BindProperty]
        public GameBoard GameBoard { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = new RestClient("https://localhost:44370/api/Game/GameBoards");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(GameBoard);
            IRestResponse response = client.Execute(request);

            GameBoardMessage = response.Content;

            return RedirectToPage("/Players/Index");
        }
    }
}
