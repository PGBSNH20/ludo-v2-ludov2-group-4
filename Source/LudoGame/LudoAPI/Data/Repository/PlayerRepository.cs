using LudoAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoAPI.Data.Repository
{
    public class PlayerRepository : IPlayer
    {
        public void CreatePlayer(int amountOfPlayers)
        {
            for (int i = 0; i <= amountOfPlayers; i++)
            {
                Console.WriteLine("Enter a name: ");
                string name = Console.ReadLine();
            }

            throw new NotImplementedException();
        }
    }
}
