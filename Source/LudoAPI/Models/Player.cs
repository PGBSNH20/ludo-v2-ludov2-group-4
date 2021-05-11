using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public int ActivatedPieces { get; set; }
        public int FinishedPieces { get; set; }

    }
}
