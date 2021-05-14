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
    public class GameRepository : IGameBoard
    {
        private readonly LudoContext _dbContext;

        public GameRepository(LudoContext dbContext)
        {
            _dbContext = dbContext;
        }
            
        public async Task<GameBoard> AddNewGame(GameBoard gameBoard)
        {
            List<Square> squares = new List<Square>();

            for (int i = 0; i < 60; i++)
            {
                Square square = new Square
                {
                    Id = i
                };
                squares.Add(square);
                
            }

            await _dbContext.GameBoards.AddAsync(gameBoard);
            await _dbContext.SaveChangesAsync();

            return gameBoard;
        }

        public GameBoard GetGameBoard(int id)
        {
            var result =  _dbContext.GameBoards.FirstOrDefault(g => g.Id == id);

            return result;
        }
    }
}
