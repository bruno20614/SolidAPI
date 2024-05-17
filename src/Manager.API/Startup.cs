using Manager.Infra.Context;
using Manager.Infra.Interfaces;
using Manager.Infra.Repositories;
using Manager.Services.DTO;
using Manager.Services.Interfaces;
using Manager.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Manager.API;
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração dos serviços que seu aplicativo usará.
            services.AddControllers();
            // Adicione outros serviços aqui, como serviços de banco de dados, autenticação, etc.
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configuração do pipeline de middleware HTTP.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configuração de tratamento de erros em produção.
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Adicione outros endpoints aqui, se necessário.
            });
        }
    }