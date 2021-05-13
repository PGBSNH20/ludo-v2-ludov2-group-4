﻿using LudoAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Models;
using LudoAPI.Data.Interfaces;

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
            await _dbContext.GameBoards.AddAsync(gameBoard);
            await _dbContext.SaveChangesAsync();

            return gameBoard;
        }       
    }
}
