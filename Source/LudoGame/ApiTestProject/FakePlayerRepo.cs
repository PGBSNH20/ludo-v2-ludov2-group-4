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

                },

              
            };

            List<Player> players = new List<Player>
            {
                new()
                {
                    Id = 3,
                    Name = "Signe",
                    Color = "red",
                    GameBoardId = 2

                },

                new()
                {
                    Id = 4,
                    Name = "Sully",
                    Color = "blue",
                    GameBoardId = 2

                },

                new()
                {
                    Id = 5,
                    Name = "Jossan",
                    Color = "green",
                    GameBoardId = 2

                },

                new()
                {
                    Id = 6,
                    Name = "Frida",
                    Color = "yellow",
                    GameBoardId = 2

                },
            };

            if (gameBoardId == 1)
            {
                return player;
            }

            else
            {
                return players;
            }
        }
    }
}
