using System;
using System.Text;
using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Entities;
using TicTacToe.Domain.Factories.BoardFactory;
using TicTacToe.Infrastructure.Config;
using TicTacToe.Infrastructure.Factories;
using TicTacToe.Infrastructure.Identity;
using TicTacToe.Infrastructure.Persistence;

namespace TicTacToe.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            var mongoSettings = configuration.GetSection("MongoSettings").Get<MongoSettings>();
            services.AddSingleton(mongoSettings);
            services.AddSingleton<IMongoClient, MongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));
            services.AddSingleton<IRepository<Game>, Repository<Game>>();
            // services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
          
            
            services.AddIdentityMongoDbProvider<ApplicationUser, MongoRole<Guid>, Guid>(identity =>
            {
                identity.Password.RequiredLength = 8;
            }, mongo =>
            {
                mongo.ConnectionString = mongoSettings.ConnectionString + "/tictactoe_ca";
            }).AddDefaultTokenProviders();
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // ValidAudience = configuration["JWT:ValidAudience"],
                        // ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                });
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddScoped<IBoardFactory, BoardFactory>();
            services.AddScoped<IGameFactory, GameFactory>();
            services.AddScoped<IPlayerFactory, PlayerFactory>();
            services.AddScoped<IGameStateFactory, GameStateFactory>();
            
            return services;
        }
    }
}