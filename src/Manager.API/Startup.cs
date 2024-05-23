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
using System;
using AutoMapper;
using Manager.Domain.Entities;
using Manager.API.ViewModels;

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
            services.AddControllers();
			
			#region AutoMapper
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
                cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
            });

            IMapper mapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton(Configuration);

            services.AddDbContext<ManagerContext>(options =>
                options.UseSqlServer(Configuration["ConnectionString:USER_MANAGER"]), ServiceLifetime.Transient);

			services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserService,UserService>();
			#endregion	
            
            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Manager.API", Version = "v1" });
            });
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

            // Ativa o middleware para servir o Swagger gerado como um endpoint JSON
            app.UseSwagger();

            // Ativa o middleware para servir o UI do Swagger no navegador
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Manager.API V1");
                c.RoutePrefix = string.Empty; // define o Swagger UI na raiz (opcional)
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // Adicione outros endpoints aqui, se necessário.
            });
        }
    }