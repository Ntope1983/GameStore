// GamesEndpoints.cs
public static class GamesEndpoints
{
    public static void MapGamesEndpoints(this WebApplication app)
    {
        /// Root endpoint (health check)
        app.MapGet("/", () => "GameStore API is running!");

        /// GET all games
        app.MapGet("/games", async (IGameService service) =>
           await service.GetAllGames());


        /// GET game by ID
        app.MapGet("/games/{id}", async (int id, IGameService service) =>
        {
            var game = await service.GetGameById(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        });
        /// CREATE new game
        app.MapPost("/games", async (GameDtoCreate game, IGameService service) =>
        {
            int idNewGame = await service.AddGame(game);
            return Results.Created($"/games/{idNewGame}", game);
        });
        /// UPDATE existing game
        app.MapPut("/games/{id}", async (int id, GameDtoCreate dto, IGameService service) =>
        {
            var existing = await service.GetGameById(id);

            if (existing is null)
                return Results.NotFound();

            await service.UpdateGame(
                id,
                dto.Name,
                dto.GameCategory,
                dto.GamePrice,
                dto.GameDate
            );

            return Results.NoContent();
        });
        /// DELETE game
        app.MapDelete("/games/{id}", async (int id, IGameService service) =>
        {
            var existing = await service.GetGameById(id);

            if (existing is null)
                return Results.NotFound();

            await service.DeleteGame(id);

            return Results.NoContent();
        });
    }

    public static void MapUsersEndpoints(this WebApplication app)
    {
        /// GET all users
        app.MapGet("/users", async (IUserService service) =>
            await service.GetAllUsers());

        /// GET user by ID
        app.MapGet("/users/{id}", async (int id, IUserService service) =>
        {
            var User = await service.GetUserById(id);
            return User is not null ? Results.Ok(User) : Results.NotFound();
        });
        /// CREATE new user
        app.MapPost("/users", async (UserDtoCreate user, IUserService service) =>
        {
            int id = await service.AddUser(user);
            return Results.Created($"/users/{id}", user);
        });
        /// UPDATE existing user
        app.MapPut("/users/{id}", async (int id, UserDtoCreate dto, IUserService service) =>
        {
            var existing = await service.GetUserById(id);

            if (existing is null)
                return Results.NotFound();

            await service.UpdateUser(
                id,
                dto.Username,
                dto.Email
            );

            return Results.NoContent();
        });
        /// DELETE user
        app.MapDelete("/users/{id}", async (int id, IUserService service) =>
        {
            var existing = await service.GetUserById(id);

            if (existing is null)
                return Results.NotFound();

            await service.DeleteUser(id);

            return Results.NoContent();
        });


    }

    public static void MapOrdersEndpoints(this WebApplication app)
    {
        /// GET all orders
        app.MapGet("/orders", async (IOrderService service) =>
            await service.GetAllOrders());

        // GET order by ID
        app.MapGet("/order/{id}", async (int id, IOrderService service) =>
        {
            var Order = await service.GetOrderById(id);
            return Order is not null ? Results.Ok(Order) : Results.NotFound();
        });

        /// CREATE new order
        app.MapPost("/orders", async (OrderDtoCreate order, IOrderService service) =>
        {
            int newOrderId = await service.AddOrder(order);
            return Results.Created($"/order/{newOrderId}", order);
        });

        /// DELETE order
        app.MapDelete("/orders/{id}", async (int id, IOrderService service) =>
        {
            var existing = await service.GetOrderById(id);

            if (existing is null)
                return Results.NotFound();

            await service.DeleteOrder(id);

            return Results.NoContent();
        });
    }


}