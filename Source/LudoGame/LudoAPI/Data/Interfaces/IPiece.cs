using LudoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Data.Interfaces
{
   public interface IPiece
    {
        Task AddPieces(Player player);
        Task<Piece> MovePiece(Piece piece);
    }
}
