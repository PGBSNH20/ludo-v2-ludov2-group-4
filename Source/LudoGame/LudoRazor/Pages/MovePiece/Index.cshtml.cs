using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LudoAPI.Data;
using LudoAPI.Models;

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
        public Player CurrentPlayer { get; set; }
        public List<Piece> Pieces { get; set; }
        public List<Player> Players { get; set; }
        public Piece ChoosedPiece { get; set; }
        public int Die { get; set; }

        public async Task<IActionResult> OnGetAsync(int? gameId, int dieValue, int pieceId)
        {
            Die = dieValue;

            if (gameId == null)
            {
                return NotFound();
            }

            GameBoard = await _context.GameBoards.FirstOrDefaultAsync(m => m.Id == gameId);
            Players = _context.Players.Where(p => p.GameBoardId == GameBoard.Id).ToList();
            CurrentPlayer = Players[GameBoard.CurrentPlayerId];
            Pieces = _context.Pieces.Where(p => p.PlayerId == CurrentPlayer.Id).ToList();
            ChoosedPiece = _context.Pieces.FirstOrDefault(p => p.Id == pieceId);

            ChoosedPiece.Position += dieValue;
            
           await _context.SaveChangesAsync();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GameBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameBoardExists(GameBoard.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GameBoardExists(int id)
        {
            return _context.GameBoards.Any(e => e.Id == id);
        }
    }
}
