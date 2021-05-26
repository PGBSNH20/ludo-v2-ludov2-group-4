using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LudoAPI.Data;
using LudoAPI.Models;
using RestSharp;

namespace LudoRazor.Pages.MovePiece
{
    public class IndexModel : PageModel
    {
        private readonly LudoAPI.Data.LudoContext _context;

        public IndexModel(LudoAPI.Data.LudoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GameBoard GameBoard { get; set; }
        public int CurrentPlayerId { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Piece> Pieces { get; set; }
        public List<Player> Players { get; set; }
        public Piece ChoosedPiece { get; set; }
        public int Die { get; set; }
        public int NextPlayerId { get; private set; }
        public Player NextPlayer { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? gameId, int dieValue, int pieceId)
        {
            Die = dieValue;

            if (gameId == null)
            {
                return NotFound();
            }

            //GameBoard = await _context.GameBoards.FirstOrDefaultAsync(m => m.Id == gameId);
            //Players = _context.Players.Where(p => p.GameBoardId == GameBoard.Id).ToList();
            //CurrentPlayer = Players[GameBoard.CurrentPlayerId];
            //CurrentPlayerId = GameBoard.CurrentPlayerId;
            //Pieces = _context.Pieces.Where(p => p.PlayerId == CurrentPlayer.Id).ToList();
            //ChoosedPiece = _context.Pieces.FirstOrDefault(p => p.Id == pieceId);


            var client = new RestClient("https://localhost:44370");
            var request = new RestRequest("api/game/gameboards/" + gameId, Method.GET);
            var queryResult = client.Execute<GameBoard>(request).Data;

            GameBoard = queryResult;

            var client3 = new RestClient("https://localhost:44370");
            var request3 = new RestRequest("api/game/get-gameboard/players/" + gameId, Method.GET);
            var queryResult3 = client3.Execute<List<Player>>(request3).Data;

            Players = queryResult3;
            
            
            CurrentPlayer = Players[GameBoard.CurrentPlayerIndex];
            GameBoard.Players = Players;
            CurrentPlayerId = GameBoard.CurrentPlayerIndex;
            

            var client2 = new RestClient("https://localhost:44370");
            var request2 = new RestRequest("api/game/get-piece/" + pieceId, Method.GET);
            var queryResult2 = client2.Execute<Piece>(request2).Data;

           ChoosedPiece = queryResult2;


            var client4 = new RestClient("https://localhost:44370");
            var request4 = new RestRequest("api/game/pieces/" + CurrentPlayer.Id, Method.GET);
            var queryResult4 = client4.Execute<List<Piece>>(request4).Data;

            Pieces = queryResult4;



            var client5 = new RestClient("https://localhost:44370");
            var request5 = new RestRequest("api/game/nextplayer/" + GameBoard.Id, Method.GET);
            var updatedGameBoard = client5.Execute<GameBoard>(request5).Data;

            //NextPlayer = updatedGameBoard.Players.FirstOrDefault(p => p.);
            //NextPlayer = queryResult5;
            //var next = queryResult5;

            // Moving the choosed piece by adding the die.
            ChoosedPiece.Position += dieValue;



            //Deciding next player.If last player, it's the first player again:
            //if (CurrentPlayerId == Players.Count() - 1)
            //{
            //    GameBoard.CurrentPlayerId = 0;
            //    NextPlayerId = GameBoard.CurrentPlayerId;
            //}
            //else
            //{
            //    GameBoard.CurrentPlayerId++;
            //    NextPlayerId = GameBoard.CurrentPlayerId;

            //}

            //NextPlayer = GameBoard.Players.FirstOrDefault(p => p.Id == NextPlayerId);
            //_context.GameBoards.Update(GameBoard);
            //_context.Pieces.Update(ChoosedPiece);
            ////_context.SaveChanges();
            //await _context.SaveChangesAsync();



            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GameBoard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameBoardExists(GameBoard.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool GameBoardExists(int id)
        {
            return _context.GameBoards.Any(e => e.Id == id);
        }
    }
}
