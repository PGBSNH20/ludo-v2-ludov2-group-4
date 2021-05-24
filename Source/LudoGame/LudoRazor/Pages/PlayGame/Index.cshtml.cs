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
        private readonly LudoAPI.Data.LudoContext _context;

        public IndexModel(LudoAPI.Data.LudoContext context)
        {
            _context = context;
        }

        public GameBoard CurrentGame { get; set; }
        public List<Piece> Pieces { get; set; }
        public List<Player> Players { get; set; }
        public Die Die { get; set; }
        public string ShowNextPlayer { get; set; } = "";
        


        public IActionResult OnGet(int id)
        {
            CurrentGame = _context.GameBoards.FirstOrDefault(g => g.Id == id);
            Pieces = _context.Pieces.Where(p => p.GameBoardId == id).ToList();
            Players = _context.Players.Where(p => p.GameBoardId == CurrentGame.Id).ToList();
            ShowNextPlayer = $"{Players[CurrentGame.CurrentPlayerId].Name}, it's your turn to roll the die!";
            
            return Page();
        }

        [BindProperty]
        public GameBoard GameBoard { get; set; }

        [BindProperty]
        public Piece Piece { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = new RestClient("http://localhost:5000/api/Game/pieces");
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddJsonBody(Piece);
            IRestResponse response = client.Execute(request);

            return Page();
        }
    }
}
