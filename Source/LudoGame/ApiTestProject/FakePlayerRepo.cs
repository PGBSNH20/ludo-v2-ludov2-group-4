using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoAPI.Interfaces;
using LudoAPI.Models;

namespace ApiTestProject
{
    public class FakePlayerRepo : IPlayer
    {
        public Task<Player> AddPlayer(Player player)
        {
            return null;
        }

        public List<Player> GetPlayersByGameBoardId(int gameBoardId)
        {
            return null;
        }
    }
}
