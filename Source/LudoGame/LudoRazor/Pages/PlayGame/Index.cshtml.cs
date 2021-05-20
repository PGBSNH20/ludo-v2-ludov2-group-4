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
        


        public IActionResult OnGet()
        {
            CurrentGame = _context.GameBoards.FirstOrDefault(g => g.Id == 1);
            Pieces = _context.Pieces.Where(p => p.GameBoardId == 1).ToList();
            Players = _context.Players.Where(p => p.GameBoardId == CurrentGame.Id).ToList();
           
            return Page();
        }

        [BindProperty]
        public GameBoard GameBoard { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.GameBoards.Add(GameBoard);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
