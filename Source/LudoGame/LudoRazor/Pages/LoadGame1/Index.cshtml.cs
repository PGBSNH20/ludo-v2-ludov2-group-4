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

namespace LudoRazor.Pages.LoadGame1
{
    public class IndexModel1 : PageModel
    {
        readonly LudoContext _dbContext;

        public IndexModel1(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string PlayerAndPieces { get; set; }

        [BindProperty]
        public GameBoard GameBoard { get; set; }
        public Player player { get; set; }
        public List<Player> players { get; set; }
        public List<Piece> pieces { get; set; }


        public IActionResult OnGet()
        {
            players = _dbContext.Players.Where(pl => pl.GameBoardId == 1).ToList();

            pieces = _dbContext.Pieces.Where(pi => pi.GameBoardId == 1).ToList();
            
            return Page();
        }  
    }
}
