using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Controllers;
using LudoAPI.Data;
using LudoAPI.Data.Interfaces;
using LudoAPI.Data.Repository;
using LudoAPI.Interfaces;
using LudoAPI.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ApiTestProject
{
    public class Tests
    {
        private static DbContextOptions<LudoContext> options = new DbContextOptionsBuilder<LudoContext>()
            .UseInMemoryDatabase(databaseName: "TestData")
            .Options;

        private static LudoContext _dbContext;
        private GameController gameController;
        private IPlayer _player;
        private IGameBoard _board;
        private IPiece _piece;

        [OneTimeSetUp]
        public async Task Setup()
        {
            _dbContext = new LudoContext(options);
            _dbContext.Database.EnsureCreated();

            await SeedDataBase();

            gameController = new GameController(_player, _board, _piece, _dbContext);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _dbContext.Database.EnsureDeleted();
        }


        private async Task SeedDataBase()
        {



            var playersList = new Player
            {
                
                
                    Id = 1,
                    Name = "Calle",
                    Color = "yellow",
                    GameBoardId = 1
                
               


            };

            var gameBoard = new GameBoard()
            {
                Id = 1,
                Name = "Calles game",
                Squares = null,
                Colors = { "yellow", "green", "blue", "red"},
                Created = DateTime.Now,
                Done = false,
                Winner = null,
                CurrentPlayerIndex = 0,
                Players = null
            };

          await  _board.AddNewGame(gameBoard);
           var result = await _player.AddPlayer(playersList);
           await _piece.AddPieces(result);
        }
        
        [Test]
        public void Test1()
        {
            

            //var player = _dbContext.Players.FirstOrDefault(p => p.Id == 1);
            var gameBoard = _board.GetGameBoard(1);

            Assert.AreEqual("Calles game", gameBoard.Name);
            //Assert.AreEqual("Calle", player.Name);
        }
    }
}