using LudoAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Models;
using LudoAPI.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LudoAPI.Data.Repository
{
    public class GameBoardRepository : IGameBoard
    {
        private readonly LudoContext _dbContext;

        public GameBoardRepository(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }
            
        public async Task<GameBoard> AddNewGame(GameBoard gameBoard)
        {
            await _dbContext.GameBoards.AddAsync(gameBoard);
            await _dbContext.SaveChangesAsync();

            return gameBoard;
        }

        public GameBoard GetGameBoard(int id)
        {
            var result =  _dbContext.GameBoards.FirstOrDefault(g => g.Id == id);

            return result;
        }

        public List<GameBoard> GetGameBoards()
        {
            var result = _dbContext.GameBoards.ToList();

            return result;
        }
    }
}
