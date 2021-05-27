using LudoAPI.Data.Interfaces;
using LudoAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
                    GameBoardId = player.GameBoardId,
                    PlayerId = player.Id,
                    IsActive = false,
                    IsDone = false

                };

                if (player.Color == "red")
                {
                    piece.Position = 1;
                }
                else if (player.Color == "green")
                {
                    piece.Position = 11;
                }
                else if (player.Color == "blue")
                {
                    piece.Position = 21;
                }
                else
                {
                    piece.Position = 31;
                }


                await _dbContext.Pieces.AddAsync(piece);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Piece> MovePiece(Piece piece)
        {
            
            _dbContext.Pieces.Update(piece);
            await _dbContext.SaveChangesAsync();

            return piece;
        }

        public async Task<List<Piece>> GetPlayerPieces(int id)
        {
            var result =  await _dbContext.Players.Where(p => p.Id == id).FirstOrDefaultAsync();

            List<Piece> pieces = await _dbContext.Pieces.Where(p => p.PlayerId == result.Id).ToListAsync();

            return pieces;
        }

        public async Task<Piece> GetPieceById(int id)
        {
            var result = await _dbContext.Pieces.Where(p => p.Id == id).FirstOrDefaultAsync();
            return result;
        }

        //public bool PossibleOptions(int die, Player currentPlayer)
        //{
        //    var playerPieces = _dbContext.Pieces.Where(p => p.PlayerId == currentPlayer.Id).ToList();
        //    int activatedPieces = playerPieces.Count(p => p.IsActive == true);

        //    if (activatedPieces == 0 && die < 6)
        //    {
        //        // NextPlayer(Player currentPlayer)
        //    }

        //    else if (activatedPieces == 0 && die == 6)
        //    {
        //        // MoveBasePiece(int die, Piece choosedPiece)
        //    }

        //    else if (activatedPieces == 4)
        //    {
        //        // MovePiece()
        //    }
        //}
        

    }
}
