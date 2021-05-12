using System;
using System.Collections.Generic;
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
        public List<Player> Players { get; set; }
    }
}
