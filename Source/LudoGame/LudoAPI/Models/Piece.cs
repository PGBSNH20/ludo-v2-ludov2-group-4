using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class Piece
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Position { get; set; }
        public int Steps { get; set; }
        public bool IsDone { get; set; }
        public int GameBoardId { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
