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
using System.Text;
using AutoMapper;
using Manager.API.Token;
using Manager.Domain.Entities;
using Manager.API.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Manager.API.Token;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<ITokenGenerator, TokenGenerator>();
			#endregion

            #region Jwt

            var secretKey = Configuration["Jwt:Key"];
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            #endregion

            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Manager API",
                    Version = "V1",
                    Description = "A construção de uma API",
                    TermsOfService  = new Uri("https://localhost5028.com.br")
                });

                // Adicionando a definição de segurança
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Por favor, utilize o Bearer <Token>",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Adicionando o requisito de segurança
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new string []{ }
                    }
                });
            });

            #endregion

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

            app.UseAuthentication();
            
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