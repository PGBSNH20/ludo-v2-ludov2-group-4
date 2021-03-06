using LudoAPI.Interfaces;
using LudoAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Data.Repository
{
    public class PlayerRepository : IPlayer
    {
        private readonly LudoContext _dbContext;

        public PlayerRepository(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PlayerRepository()
        {

        }

        public async Task<Player> AddPlayer(Player player)
        {
            await _dbContext.Players.AddAsync(player);
            

            var gameboard = _dbContext.GameBoards.FirstOrDefault(g => g.Id == player.GameBoardId);
            gameboard.Players.Add(player);

            _dbContext.GameBoards.Update(gameboard);
            await _dbContext.SaveChangesAsync();

            return player;
        }

        public List<Player> GetPlayersByGameBoardId(int gameBoardId)
        {
            var result = _dbContext.Players.Where(p => p.GameBoardId == gameBoardId).ToList();

            return result;
        }
    }
}
