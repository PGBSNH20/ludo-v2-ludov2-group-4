using LudoAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Models;

namespace LudoAPI.Data.Repository
{
    public class GameRepository : IPlayer, IGameBoard
    {
        private readonly LudoContext _dbContext;

        public GameRepository(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Player> AddPlayer(Player player)
        {

             await _dbContext.Players.AddAsync(player); 
             await _dbContext.SaveChangesAsync();

            return player;
        }

        public async Task AddPieces(Player player)
        {
            for (int i = 0; i < 4; i++)
            {
                var piece = new Piece()
                {
                    Color = player.Color,
                    PlayerId = player.Id,
                    IsActive = false,
                    IsDone = false
                };


                await _dbContext.Pieces.AddAsync(piece);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<GameBoard> AddNewGame(GameBoard gameBoard)
        {
            await _dbContext.GameBoards.AddAsync(gameBoard);
            await _dbContext.SaveChangesAsync();

            return gameBoard;
        }
    }
}
