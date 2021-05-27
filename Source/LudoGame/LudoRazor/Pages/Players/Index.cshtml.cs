using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LudoAPI.Data;
using LudoAPI.Models;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace LudoRazor.Pages.Players
{
    public class IndexModel : PageModel
    {
        
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            

            return Page();
        }

        

        [BindProperty]
        public Player Player { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = new RestClient("https://localhost:44370/api/Game/players");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(Player);
            IRestResponse response = client.Execute(request);

            Message = response.Content;

            return Page();

        }
    }
}
