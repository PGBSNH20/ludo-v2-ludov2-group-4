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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
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
        

        [Test]
        public async Task PostPlayer_AddNewPlayer_ExpectUserCreated()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            var playerToPost = new Player()
            {
                Id = 1,
                Name = "Signe",
                Color = "blue",
                GameBoardId = 1,
            };

            // Act
            var result = await sut.PostPlayer(playerToPost);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            Assert.AreEqual("You have created a user", ((ObjectResult)result.Result).Value.ToString());
         
        }

        [Test]
        public async Task PostPlayer_AddPlayerWithAOccupiedColor_ExpectColorIsOccupied()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            var playerToPost = new Player()
            {
                Id = 1,
                Name = "Kevin",
                Color = "red",
                GameBoardId = 1,
            };

            // Act
            var result = await sut.PostPlayer(playerToPost);

            // assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            Assert.AreEqual("Color is occupied", ((ObjectResult)result.Result).Value.ToString());
        }

        [Test]
        public async Task PostPlayer_AddPlayerWithAExistingName_ExpectNameAlreadyExists()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            var playerToPost = new Player()
            {
                Id = 1,
                Name = "Calle",
                Color = "green",
                GameBoardId = 1,
            };

            // Act
            var result = await sut.PostPlayer(playerToPost);


            // assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            Assert.AreEqual("The name already exists", ((ObjectResult)result.Result).Value.ToString());
        }

        [Test]
        public async Task PostPlayer_ChooseWrongColor_ExpectColorIsWrong()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            var playerToPost = new Player()
            {
                Id = 1,
                Name = "Calle",
                Color = "purple",
                GameBoardId = 1,
            };

            // Act
            var result = await sut.PostPlayer(playerToPost);


            // assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            Assert.AreEqual("Invalid Color", ((ObjectResult)result.Result).Value.ToString());

        }

        [Test]
        public async Task PostPlayer_AddNewPlayer_WhenGameBoardAlreadyContainsMaxPlayers_ExpectBadRequest()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            var playerToPost = new Player()
            {
                Id = 7,
                Name = "Calle",
                Color = "red",
                GameBoardId = 2,
            };

            // Act
            var result = await sut.PostPlayer(playerToPost);


            // assert
            Assert.IsInstanceOf<ObjectResult>(result.Result);
            Assert.AreEqual("A ludo game can only include 2-4 players", ((ObjectResult)result.Result).Value.ToString());

        }

        [Test]
        public async Task PostGameBoard_AddNewGameBoard_ExpectGameBoardCreated()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            var gameBoardToPost = new GameBoard()
            {
                
                Name = "Calles game",
                
            };

            // Act
            var result = await sut.PostGameBoard(gameBoardToPost);
            
            // assert
            Assert.IsInstanceOf<ActionResult<LudoAPI.Models.GameBoard>>(result);
            Assert.AreEqual("You have created a gameboard", ((ObjectResult)result.Result).Value.ToString());       
        }

        [Test]
        public void GetAllGameBoards_Expect1GameBoard()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            // Act
            var result = sut.GetGameBoards();
            
            Assert.AreEqual(1, result.Result.Count);
        }


        [Test]
        public void GetGameBoard_ExpectGameBoardById1()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            // Act
            var result = sut.GetGameBoardById(1);

            //Assert.IsInstanceOf<ObjectResult>(result);
            Assert.AreEqual(1, result.Value.Id);
        }

        [Test]
        public void GetPieceById_ExpectPieceId1()
        {
            // Arrange
            IPlayer fakePlayerRepo = new FakePlayerRepo();
            IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
            IPiece fakePieceRepo = new FakePieceRepository();
            var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

            // Act
            var result = sut.GetPieceById(1);

            Assert.AreEqual(1, result.Result.Value.Id);
        }
    }
}