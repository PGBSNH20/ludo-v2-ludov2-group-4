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
        public Task<GameBoard> AddNewGame(GameBoard gameBoard)
        {
            throw new NotImplementedException();
        }

        public GameBoard GetGameBoard(int id)
        {
            return null;
        }

        public List<GameBoard> GetGameBoards()
        {
            throw new NotImplementedException();
        }
    }
}
