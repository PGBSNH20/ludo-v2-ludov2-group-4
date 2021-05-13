using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        List<Piece> Pieces { get; set; }
        //[ForeignKey("GameBoard")]
        public int GameBoardId { get; set; }
        //public GameBoard GameBoard { get; set; }
    }
}
