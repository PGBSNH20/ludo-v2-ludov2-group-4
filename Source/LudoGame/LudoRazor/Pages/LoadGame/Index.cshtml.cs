using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LudoAPI.Data;
using LudoAPI.Models;

namespace LudoRazor.Pages.LoadGame
{
    public class IndexModel : PageModel
    {
        public string ButtonMessage { get; set; }

        private readonly LudoAPI.Data.LudoContext _context;

        public IndexModel(LudoAPI.Data.LudoContext context)
        {
            _context = context;
        }

        public IList<GameBoard> GameBoard { get;set; }

        public async Task OnGetAsync()
        {
            GameBoard = await _context.GameBoards.ToListAsync();
        }
    }
}
