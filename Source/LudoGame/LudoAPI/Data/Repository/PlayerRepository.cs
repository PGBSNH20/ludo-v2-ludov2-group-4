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

        public async Task<Player> AddPlayer(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                Piece piece = new Piece
                {
                    Color = player.Color
                };
               await _dbContext.Pieces.AddAsync(piece);
               await _dbContext.Players.AddAsync(player);
               await _dbContext.SaveChangesAsync();
            }

            

            return player;
        }
    }
}
