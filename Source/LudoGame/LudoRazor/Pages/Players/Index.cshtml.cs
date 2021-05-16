using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Interfaces;
using LudoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace LudoRazor.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly IPlayer _player;

        public IndexModel(IPlayer player)
        {
            _player = player;
        }

        public IList<Player> Player { get;set; }

        public IEnumerable<Player> OnGet(int id)
        {
            Player = ( _player.GetPlayersByGameBoardId(id)).ToList();
            return Player;
        }
    }
}
