using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Models
{
    public class Die
    {
        public static int RollDie()
        {
            var rnd = new Random();
            int roll = rnd.Next(1, 7);
            return roll;
        }
        
    }
}
