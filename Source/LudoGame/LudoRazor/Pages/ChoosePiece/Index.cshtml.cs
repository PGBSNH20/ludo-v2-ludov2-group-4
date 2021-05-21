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

namespace LudoRazor.Pages.ChoosePiece
{
    public class IndexModel : PageModel
    {
        private readonly LudoAPI.Data.LudoContext _context;

        public IndexModel(LudoAPI.Data.LudoContext context)
        {
            _context = context;
        }

        public GameBoard CurrentGame { get;set; }
        public int die { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Player> Players { get; set; }
        public List<Piece> Pieces { get; set; }
        public string ValidatePlayer { get; set; } = "";


        public async Task OnGetAsync(int id)
        {
            CurrentGame = _context.GameBoards.FirstOrDefault(g => g.Id == id);
            
            Players = _context.Players.Where(p => p.GameBoardId == CurrentGame.Id).ToList();
            CurrentPlayer = Players[CurrentGame.CurrentPlayerId];
            Pieces = _context.Pieces.Where(p => p.PlayerId == CurrentPlayer.Id ).ToList();
            ValidatePlayer = $"{CurrentPlayer.Name}";

           die = Die.RollDie();


        }
    }
}
