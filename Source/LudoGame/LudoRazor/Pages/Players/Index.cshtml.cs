using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LudoAPI.Data;
using LudoAPI.Models;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Json;

namespace LudoRazor.Pages.Players
{
    public class IndexModel : PageModel
    {
        private readonly LudoAPI.Data.LudoContext _context;

        public IndexModel(LudoAPI.Data.LudoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Player Player { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Player player)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var client = new RestClient("https://localhost:5001/api/Game/players");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(Player);
            IRestResponse response = client.Execute(request);

            

            return RedirectToPage("/Index");

            //using (var client = new ResClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:5001/api/Game/players");
            //    var postPlayer = client.PostAsJsonAsync<Player>("Player", player);
            //    postPlayer.Wait();
            //    var postResult = postPlayer.Result;

            //    if(postResult.IsSuccessStatusCode)
            //    {
            //        return RedirectToPage("./Index");
            //    }
            //    else 
            //    {
            //        return NotFound();
            //    }
            //}

            //_context.Players.Add(Player);
            //await _context.SaveChangesAsync();

            //return RedirectToPage("./Index");
        }
    }
}
