using System;
using System.Collections.Generic;
using LudoAPI.Data;
using LudoAPI.Data.Repository;
using LudoAPI.Interfaces;
using LudoAPI.Models;
using Xunit;

namespace ApiTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
           
            // arrange 
            IPlayer p = new PlayerRepository();

            GameBoard g = new GameBoard()
            {
                Id = 1,
                Name = "Calles game",
                CurrentPlayerId = 0,
                Done = false,
                Winner = null

            };

           List<Player> players = new List<Player>
               {
                   new()
                   {
                       Id = 1,
                       Name = "Calle",
                       Color = "blue",
                       GameBoardId = 1
                   },

                   new()
                   {
                       Id = 2,
                       Name = "Kevin",
                       Color = "red",
                       GameBoardId = 1
                   }

               } ;



           players = p.GetPlayersByGameBoardId(g.Id);

            Assert.True(players.Count == 2);

        }
    }
}
