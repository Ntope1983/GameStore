using System.Text.Json;

namespace GameStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            string path = Path.Combine(AppContext.BaseDirectory, "Games.json");
            List<Game> gameList = LoadFromJson<Game>(path);

            IGameRepository repository = new InMemoryGameRepository();
            foreach (Game game in gameList)
            {
                repository.Add(game);
            }
            IGameService gameService = new GameService(repository);

            app.MapGet("/", () => "Hello Word!");

            // ΔΙΟΡΘΩΣΗ: Επιστρέφουμε τα ζωντανά δεδομένα από το Service, όχι τη στατική gameList
            app.MapGet("/games", () => gameService.GetAllGames());

            // ΔΙΟΡΘΩΣΗ: Επιστρέφουμε τα δεδομένα από το Service για να βρίσκει και τα νέα παιχνίδια
            app.MapGet("/games/{id}", (int id) =>
            {
                var game = gameService.GetGameById(id);
                return game is not null ? Results.Ok(game) : Results.NotFound();
            });

            app.MapPost("/games", (GameDto newGameDto) =>
            {
                // 1. Προσθήκη του παιχνιδιού στη μνήμη μέσω του Service
                gameService.AddGame(newGameDto.GameNameDto,
                                    newGameDto.GameCategoryDto,
                                    newGameDto.GamePriceDto,
                                    newGameDto.GameDateDto);

                // 2. ΜΟΝΙΜΗ ΑΠΟΘΗΚΕΥΣΗ: Παίρνουμε όλη την ενημερωμένη λίστα και τη γράφουμε στο JSON αρχείο
                var updatedGames = gameService.GetAllGames();
                SaveToJson(path, updatedGames);

                // 3. Επιστροφή σωστής HTTP απάντησης
                return Results.Created($"/games", newGameDto);
            });

            app.Run();
        }

        public static List<T> LoadFromJson<T>(string path)
        {
            if (!File.Exists(path))
                return new List<T>();

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public static void SaveToJson<T>(string path, IEnumerable<T> data) // Έγινε public για να καλείται παντού
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(path, json);
        }
    }
}
