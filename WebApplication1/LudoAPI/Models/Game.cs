using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; }
        public Player Winner { get; set; }


    }
}
