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

        private Player p1 = new() {Id = 1, Name = "Kevin", Color = "red", GameBoardId = 1};

        //[Test]
        //public async Task Test1()
        //{
        //    var controller = new GameController(_player, _board, _piece, _dbContext);

        //    var result = await controller.PostPlayer(p1);
            
            
        //    Assert.IsInstanceOf<BadRequestObjectResult>(result);
        //}

        //[OneTimeSetUp]
        //public async Task Setup()
        //{
        //    _dbContext = new LudoContext(options);
        //    _dbContext.Database.EnsureCreated();

        //    //await SeedDataBase();

        //    gameController = new GameController(_player, _board, _piece, _dbContext);
        //}

        //[OneTimeTearDown]
        //public void CleanUp()
        //{
        //    _dbContext.Database.EnsureDeleted();
        //}


        //private async Task SeedDataBase()
        //{



        //    var player = new Player
        //    {


        //            Id = 1,
        //            Name = "Calle",
        //            Color = "yellow",
        //            GameBoardId = 1




        //    };

        //    var gameBoard = new GameBoard()
        //    {
        //        Id = 1,
        //        Name = "Calles game",
        //        Squares = null,
        //        Colors = { "yellow", "green", "blue", "red"},
        //        Created = DateTime.Now,
        //        Done = false,
        //        Winner = null,
        //        CurrentPlayerIndex = 0,
        //        Players = null
        //    };

        //  await  _board.AddNewGame(gameBoard);
        //   var result = await _player.AddPlayer(player);
        //   await _piece.AddPieces(result);
        //}



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
                Name = "Calle",
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
        public async Task PostPlayer_WhenPlayersIsAlreadyMaximum_Expect()
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

        //[Test]
        //public async Task PostPlayer_ChooseWrongColor_ExpectColorIsWrsd()
        //{
        //    // Arrange
        //    IPlayer fakePlayerRepo = new FakePlayerRepo();
        //    IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
        //    IPiece fakePieceRepo = new FakePieceRepository();
        //    var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

        //    var playerToPost = new Player();
        //    //{
        //    //    Id = 1 ,
        //    //    Name = "Kevin",
        //    //    Color = "green",
        //    //    GameBoardId = 0
        //    //};

        //    // Act
        //    var result = await sut.PostPlayer(playerToPost);


        //    // assert
        //    Assert.IsInstanceOf<ObjectResult>(result.Result);
        //    Assert.AreEqual("You must enter a gameboard id", ((ObjectResult)result.Result).Value.ToString());



        //}


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



            //Assert.IsInstanceOf<ObjectResult>(result);
            Assert.AreEqual(1, result.Result.Value.Id);


        }

        //[Test]
        //public async Task GetPieceById_PieceDoNotExists_ExpectNotFound()
        //{
        //    // Arrange
        //    IPlayer fakePlayerRepo = new FakePlayerRepo();
        //    IGameBoard fakeGameBoardRepo = new FakeGameBordRepo();
        //    IPiece fakePieceRepo = new FakePieceRepository();
        //    var sut = new GameController(fakePlayerRepo, fakeGameBoardRepo, fakePieceRepo, null);

        //    // Act
        //    var result = await sut.GetPieceById(5);




        //    //Assert.IsInstanceOf<ObjectResult>(result);
        //    //Assert.IsInstanceOf<ObjectResult>(result);
        //    Assert.IsInstanceOf<ActionResult<LudoAPI.Models.Piece>>(result);
        //    Assert.AreEqual("A piece with that Id doesn't exist", ((ObjectResult)result.Result).Value.ToString());

        //    //Assert.AreEqual(1, result.Value.Id);



        //}






    }
}