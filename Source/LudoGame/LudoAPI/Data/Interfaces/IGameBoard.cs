using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Models;

namespace LudoAPI.Interfaces
{
    public interface IGameBoard
    {
       Task<GameBoard> AddNewGame(GameBoard gameBoard);
       GameBoard GetGameBoard(int id);
       List<GameBoard> GetGameBoards();

    }
}
