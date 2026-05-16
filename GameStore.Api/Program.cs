using Microsoft.EntityFrameworkCore;

namespace GameStore.Api
{
    /// <summary>
    /// Entry point of the application.
    /// Configures services, middleware, and HTTP endpoints.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method - application bootstrap.
        /// </summary>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ----------------------------------------------------
            // 🔧 SERVICE REGISTRATION (Dependency Injection)
            // ----------------------------------------------------

            /// Enables OpenAPI/Swagger documentation generation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            /// Registers DbContext for Entity Framework Core
            builder.Services.AddDbContext<GameStoreContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("GameStore")));

            /// Registers application service (business logic layer)
            builder.Services.AddScoped<IGameService, GameService>();

            // Build application pipeline
            var app = builder.Build();

            // ----------------------------------------------------
            // 🌐 MIDDLEWARE CONFIGURATION
            // ----------------------------------------------------

            if (app.Environment.IsDevelopment())
            {
                /// Enables Swagger UI only in development
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // ----------------------------------------------------
            // 📡 API ENDPOINTS
            // ----------------------------------------------------

            /// Root endpoint (health check)
            app.MapGet("/", () => "GameStore API is running!");

            /// GET all games
            app.MapGet("/games", (IGameService service) =>
                service.GetAllGames());

            /// GET game by ID
            app.MapGet("/games/{id}", (int id, IGameService service) =>
            {
                var game = service.GetGameById(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
            });

            /// CREATE new game
            app.MapPost("/games", (Game game, IGameService service) =>
            {
                service.AddGame(game);
                return Results.Created($"/games/{game.Id}", game);
            });

            /// UPDATE existing game
            app.MapPut("/games/{id}", (int id, GameDto dto, IGameService service) =>
            {
                var existing = service.GetGameById(id);

                if (existing is null)
                    return Results.NotFound();

                service.UpdateGame(
                    id,
                    dto.GameName,
                    dto.GameCategory,
                    dto.GamePrice,
                    dto.GameDate
                );

                return Results.NoContent();
            });

            /// DELETE game
            app.MapDelete("/games/{id}", (int id, IGameService service) =>
            {
                var existing = service.GetGameById(id);

                if (existing is null)
                    return Results.NotFound();

                service.DeleteGame(id);

                return Results.NoContent();
            });

            // ----------------------------------------------------
            // 🚀 RUN APPLICATION
            // ----------------------------------------------------
            app.Run();
        }
    }
}