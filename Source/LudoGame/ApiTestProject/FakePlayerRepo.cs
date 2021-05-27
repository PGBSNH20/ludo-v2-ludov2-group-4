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
        public async Task<Player> AddPlayer(Player player)
        {
            

            return player;
        }

        public List<Player> GetPlayersByGameBoardId(int gameBoardId)
        {
            List<Player> player = new List<Player>
            {
                new()
                {
                    Id = 2,
                    Name = "Calle",
                    Color = "red",
                    GameBoardId = 1
                }
            };

            return player;
        }
    }
}
