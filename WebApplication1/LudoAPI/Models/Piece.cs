using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class Piece
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int PlayerId { get; set; }
        public string Color { get; set; }
        public bool Activated { get; set; }
        public bool Finished { get; set; }


    }
}
