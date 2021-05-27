using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LudoAPI.Data.Interfaces;
using LudoAPI.Models;

namespace ApiTestProject
{
    class FakePieceRepository : IPiece
    {
        public async Task AddPieces(Player player)
        {
           
        }

        public Task<Piece> MovePiece(Piece piece)
        {
            throw new NotImplementedException();
        }

        public Task<List<Piece>> GetPlayerPieces(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Piece> GetPieceById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
