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
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
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
            /// GET all users
            app.MapGet("/users", (IUserService service) =>
                service.GetAllUsers());
            /// GET all orders
            app.MapGet("/orders", (IOrderService service) =>
                service.GetAllOrders());
            /// GET game by ID
            app.MapGet("/games/{id}", (int id, IGameService service) =>
            {
                var game = service.GetGameById(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
            });
            /// GET user by ID
            app.MapGet("/user/{id}", (int id, IUserService service) =>
            {
                var User = service.GetUserById(id);
                return User is not null ? Results.Ok(User) : Results.NotFound();
            });
            // GET order by ID
            app.MapGet("/order/{id}", (int id, IOrderService service) =>
            {
                var Order = service.GetOrderById(id);
                return Order is not null ? Results.Ok(Order) : Results.NotFound();
            });
            /// CREATE new game
            app.MapPost("/games", (Game game, IGameService service) =>
            {
                service.AddGame(game);
                return Results.Created($"/games/{game.Id}", game);
            });
            /// CREATE new user
            app.MapPost("/users", (User user, IUserService service) =>
            {
                service.AddUser(user);
                return Results.Created($"/users/{user.Id}", user);
            });
            /// CREATE new order
            app.MapPost("/orders", (Order order, IOrderService service) =>
            {
                service.AddOrder(order);
                return Results.Created($"/users/{order.Id}", order);
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
            /// UPDATE existing user
            app.MapPut("/users/{id}", (int id, UserDto dto, IUserService service) =>
            {
                var existing = service.GetUserById(id);

                if (existing is null)
                    return Results.NotFound();

                service.UpdateUser(
                    id,
                    dto.UserName,
                    dto.Email
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
            /// DELETE user
            app.MapDelete("/users/{id}", (int id, IUserService service) =>
            {
                var existing = service.GetUserById(id);

                if (existing is null)
                    return Results.NotFound();

                service.DeleteUser(id);

                return Results.NoContent();
            });
            /// DELETE order
            app.MapDelete("/orders/{id}", (int id, IOrderService service) =>
            {
                var existing = service.GetOrderById(id);

                if (existing is null)
                    return Results.NotFound();

                service.DeleteOrder(id);

                return Results.NoContent();
            });
            // ----------------------------------------------------
            // 🚀 RUN APPLICATION
            // ----------------------------------------------------
            app.Run();
        }
    }
}