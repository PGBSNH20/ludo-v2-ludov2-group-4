using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LudoAPI.Data;
using LudoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoRazor.Pages.NextPlayer
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
        public int NextPlayerId { get; set; }
        public Player NextPlayer { get; set; }
        public List<Player> Players { get; set; }

        public IActionResult OnGet(int gameId)
        {
            GameBoard = _context.GameBoards.FirstOrDefault(m => m.Id == gameId);
            CurrentPlayerId = GameBoard.CurrentPlayerId;
            Players = _context.Players.Where(p => p.GameBoardId == GameBoard.Id).ToList();

            if (GameBoard.CurrentPlayerId == GameBoard.Players.Count())
            {
                GameBoard.CurrentPlayerId = 0;
                NextPlayerId = GameBoard.CurrentPlayerId;
            }
            else
            {
                GameBoard.CurrentPlayerId++;
                NextPlayerId = GameBoard.CurrentPlayerId;

            }

            
            NextPlayer = GameBoard.Players.FirstOrDefault(p => p.Id == NextPlayerId);
            _context.SaveChanges();
            return Page();
        }

        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    _context.GameBoards.Add(LudoAPI.Models.GameBoard);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}
