using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class Piece
    {
        public int Id { get; set; }
        public int Color { get; set; }
        public int Position { get; set; }
        public int PlayerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
    }
}
