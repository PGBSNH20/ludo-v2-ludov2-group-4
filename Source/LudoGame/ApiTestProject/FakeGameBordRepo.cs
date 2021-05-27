using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoAPI.Interfaces;
using LudoAPI.Models;

namespace ApiTestProject
{
    public class FakeGameBordRepo : IGameBoard
    {
        public async Task<GameBoard> AddNewGame(GameBoard gameBoard)
        {
            return gameBoard;
        }

        public GameBoard GetGameBoard(int id)
        {
            GameBoard g = new GameBoard
            {
                Id = 1,
                Name = "Signes Game"
            };
            return g;
        }

        public List<GameBoard> GetGameBoards()
        {
            var gameBoards = new List<GameBoard>
            {
                new()
                {
                    Id = 1,
                    Name = "Kevins game",
                    Colors = null,
                    Created = DateTime.Now,
                    CurrentPlayerIndex = 1,
                    
                }
            };

            return gameBoards;
        }
    }
}
