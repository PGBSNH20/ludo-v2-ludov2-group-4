using LudoAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Models;

namespace LudoAPI.Data.Repository
{
    public class PlayerRepository : IPlayer
    {

        private readonly LudoContext _dbContext;

        public PlayerRepository(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Player PostPlayer(Player player)
        {


            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
            return player;
            //for (int i = 0; i < amountOfPlayers; i++)
            //{
            //    player.Name = 
            //}

        }
    }
}
