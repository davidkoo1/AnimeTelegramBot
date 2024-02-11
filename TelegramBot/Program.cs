using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

const string token = "6274520148:AAFkH45qrBP5cyc5jSWm5JFM1pIL1MTZbfM";

ITelegramBotClient bot = new TelegramBotClient(token);

Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result);

var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }, // receive all update types
};
bot.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken
);

Console.ReadLine();
async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is null && update.CallbackQuery is null)
        return;

    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
    {
        Console.WriteLine(update.Message.Chat.Id);
        var message = update.Message.Text.ToLower();

        if (message.StartsWith("/getrandomanime"))
        {
            await GetRandomAnime(update);
        }
    }
}

string EscapeMarkdownCharacters(string input)
{
    return input.Replace("_", "\\_")
                .Replace("*", "\\*")
                .Replace("[", "\\[")
                .Replace("]", "\\]")
                .Replace("(", "\\(")
                .Replace(")", "\\)")
                .Replace(".", "\\.")
                .Replace("!", "\\!")
                .Replace("-", "\\-")
                .Replace("`", "\\`")
                .Replace("~", "\\~")
                .Replace(">", "\\>")
                .Replace("#", "\\#")
                .Replace("+", "\\+");
}

async Task GetRandomAnime(Update update)
{
    try
    {
        await bot.SendTextMessageAsync(update.Message.Chat.Id, "Wait!!");
        bool animeConditionsMet = false;

        while (!animeConditionsMet)
        {
            // Make a request to the Jikan API to get random anime
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var apiUrl = "https://api.jikan.moe/v4/random/anime";
                    var response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        // Использовать JObject для работы с JSON-строкой
                        JObject json = JObject.Parse(data);

                        // Получить данные напрямую из JSON-строки
                        string animeTitle = json["data"]["title"]?.ToString() ?? "";
                        string animeSynopsis = json["data"]["synopsis"]?.ToString() ?? "";
                        string animeUrl = json["data"]["url"]?.ToString() ?? "";
                        var animeImageUrl = json["data"]["images"]["jpg"]["image_url"]?.ToString() ?? "";

                        string animeStatus = json["data"]["status"]?.ToString() ?? "";
                        int animeEpisodes = json["data"]["episodes"] is not null ? Convert.ToInt32(json["data"]["episodes"]) : 0;
                        string animeType = json["data"]["type"]?.ToString() ?? "";
                        int animeUserScore = !string.IsNullOrWhiteSpace(json["data"]["scored_by"]?.ToString()) ? Convert.ToInt32(json["data"]["scored_by"]) : 0;
                        int animeYear = !string.IsNullOrWhiteSpace(json["data"]["year"]?.ToString()) ? Convert.ToInt32(json["data"]["year"]) : 0;



                        // Check if conditions are met
                        animeConditionsMet = animeType.ToLower() == "tv"
                                            && animeEpisodes <= 50 && animeEpisodes > 20 && animeUserScore >= 10000
                                            && animeStatus.ToLower() == "finished airing";

                        if (animeConditionsMet)
                        {
                            // Send the received data as a message
                            var message = $"*Title:* {EscapeMarkdownCharacters(animeTitle)}\n\n" +
                                          $"*Synopsis:*\n{EscapeMarkdownCharacters(animeSynopsis)}\n\n" +
                                          $"*Episodes:*{animeEpisodes}\n\n" +
                                          $"*Photo:* {EscapeMarkdownCharacters(animeImageUrl)}\n\n";
                                          

                            var parseMode = Telegram.Bot.Types.Enums.ParseMode.MarkdownV2;
                            await bot.SendTextMessageAsync(update.Message.Chat.Id, message, parseMode: parseMode);
                        }
                        //await Task.Delay(1000);
                    }

                }
                catch (Exception ex)
                {
                    // Handle exceptions, e.g., log or notify the user
                    Console.WriteLine($"Error getting random anime: {ex.Message}");

                    //await bot.SendTextMessageAsync(update.Message.Chat.Id, "An error occurred while fetching random anime.");
                }
            }

            // Introduce a delay of 100 milliseconds before the next iteration

        }
    }
    catch (Exception ex)
    {
        // Handle exceptions, e.g., log or notify the user
        Console.WriteLine($"Error getting random anime: {ex.Message}");
        //await bot.SendTextMessageAsync(update.Message.Chat.Id, "An error occurred while fetching random anime.");
    }
}



async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{

}