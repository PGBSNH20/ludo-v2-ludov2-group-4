using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LudoAPI.Data
{
    public class LudoContext : DbContext
    {
        public LudoContext(DbContextOptions<LudoContext> options) : base(options)
        {
            
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<GameBoard> GameBoards { get; set; }
        public DbSet<Piece> Pieces { get; set; }
        
    }
}
