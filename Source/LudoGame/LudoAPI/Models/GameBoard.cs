using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class GameBoard
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public bool Done { get; set; }
        public string? Winner { get; set; }
        public int? CurrentPlayerId { get; set; }

        [NotMapped]
        public List<Square> Squares { get; set; }
        
        public List<Player> Players { get; set; }

        [NotMapped] public List<string> Colors { get; set; } = new() {"red", "blue", "yellow", "green"};
    }
}
