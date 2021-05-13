using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Models;

namespace LudoAPI.Interfaces
{
    public interface IPlayer
    {
        Task<Player> AddPlayer(Player player);
        
    }
}
