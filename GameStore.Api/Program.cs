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

            app.MapGamesEndpoints()
            // ----------------------------------------------------
            // 🚀 RUN APPLICATION
            // ----------------------------------------------------
            app.Run();
        }
    }
}