﻿using System;
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
        readonly LudoContext _dbContext;

        public IndexModel(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<GameBoard> GameBoard { get; set; }
        public List<Player> players { get; set; }
        public List<Piece> pieces { get; set; }       

        public async Task OnGetAsync()
        {
            GameBoard = await _dbContext.GameBoards.ToListAsync();

            players = _dbContext.Players.Where(pl => pl.GameBoardId == 1).ToList();

            pieces = _dbContext.Pieces.Where(pi => pi.GameBoardId == 1).ToList();
        }

        ////function loadGame(input) {
        ////    document.getElementById("gameId").innerHTML = input;
        ////}
    }
}
