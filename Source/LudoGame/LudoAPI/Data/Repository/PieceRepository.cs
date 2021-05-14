using LudoAPI.Data.Interfaces;
using LudoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Data.Repository
{
    public class PieceRepository : IPiece
    {
        private readonly LudoContext _dbContext;

        public PieceRepository(LudoContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<Piece> MovePiece(Piece piece)
        {
            int die = Die.RollDie();

            piece.Position += die;

            

            _dbContext.Pieces.Update(piece);
            await _dbContext.SaveChangesAsync();

            return piece;
        }

        public async Task<List<Piece>> GetPlayerPieces(int id)
        {
            var result =  _dbContext.Players.Where(p => p.Id == id).FirstOrDefault();

            List<Piece> pieces =  _dbContext.Pieces.Where(p => p.PlayerId == result.Id).ToList();

            return pieces;
        }

    }
}
