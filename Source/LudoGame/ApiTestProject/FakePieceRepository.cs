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

        public async Task<Piece> GetPieceById(int id)
        {
            Piece piece = new Piece
            {
                Id = 1,
                Color = "red",
                GameBoardId = 1,
                PlayerId = 1,
            };
            return piece;
        }
    }
}
