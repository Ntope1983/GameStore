// GamesEndpoints.cs
public static class GamesEndpoints
{
    public static void MapGamesEndpoints(this WebApplication app)
    {
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
        app.MapPost("/games", (GameDtoCreate game, IGameService service) =>
        {
            int idNewGame = service.AddGame(game);
            return Results.Created($"/games/{idNewGame}", game);
        });
        /// UPDATE existing game
        app.MapPut("/games/{id}", (int id, GameDtoCreate dto, IGameService service) =>
        {
            var existing = service.GetGameById(id);

            if (existing is null)
                return Results.NotFound();

            service.UpdateGame(
                id,
                dto.Name,
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
    }

    public static void MapUsersEndpoints(this WebApplication app)
    {
        /// GET all users
        app.MapGet("/users", (IUserService service) =>
            service.GetAllUsers());

        /// GET user by ID
        app.MapGet("/user/{id}", (int id, IUserService service) =>
        {
            var User = service.GetUserById(id);
            return User is not null ? Results.Ok(User) : Results.NotFound();
        });
        /// CREATE new user
        app.MapPost("/users", (UserDtoCreate user, IUserService service) =>
        {
            int id = service.AddUser(user);
            return Results.Created($"/users/{id}", user);
        });
        /// UPDATE existing user
        app.MapPut("/users/{id}", (int id, UserDtoCreate dto, IUserService service) =>
        {
            var existing = service.GetUserById(id);

            if (existing is null)
                return Results.NotFound();

            service.UpdateUser(
                id,
                dto.Username,
                dto.Email
            );

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


    }

    public static void MapOrdersEndpoints(this WebApplication app)
    {
        /// GET all orders
        app.MapGet("/orders", (IOrderService service) =>
            service.GetAllOrders());

        // GET order by ID
        app.MapGet("/order/{id}", (int id, IOrderService service) =>
        {
            var Order = service.GetOrderById(id);
            return Order is not null ? Results.Ok(Order) : Results.NotFound();
        });

        /// CREATE new order
        app.MapPost("/orders", (OrderDtoCreate order, IOrderService service) =>
        {
            int newOrderId = service.AddOrder(order);
            return Results.Created($"/order/{newOrderId}", order);
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
    }


}