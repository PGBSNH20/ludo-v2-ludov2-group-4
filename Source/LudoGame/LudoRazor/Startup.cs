using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoAPI.Data;
using LudoAPI.Data.Interfaces;
using LudoAPI.Data.Repository;
using LudoAPI.Interfaces;
using LudoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LudoRazor
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.AddDbContext<LudoContext>(opt => opt.UseSqlServer(@"Server = localhost, 41433; Database = LudoGameDb; User ID = sa; Password = secretpassword123!"));

            services.AddHttpClient<IGameBoard,
                GameBoardRepository>(client =>
                client.BaseAddress = new Uri(Configuration.GetSection("http://localhost:5000").Value));

            services.AddHttpClient<IPlayer,
                PlayerRepository>(client =>
                client.BaseAddress = new Uri(Configuration.GetSection("http://localhost:5000").Value));

            services.AddHttpClient<IPiece,
                PieceRepository>(client =>
                client.BaseAddress = new Uri(Configuration.GetSection("http://localhost:5000").Value));

       


        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

            });
        }
    }
}
