
using System.Drawing;
using Telegram.Bot;

namespace ShopAppApi.Services.TelegramBotRepository
{
    public class TelegramMessage
    {
        public string? _message { get; set; }
        public string? host { get; set; }
        public string? _check_id { get; set; }
        public string? _check_name { get; set; }
        public string? _level { get; set; }
    }
    public class TelegramRepository : ITelegramRepository
    {
        // https://api.telegram.org/bot{token}/getUpdates
        private readonly string _token = "";
        private readonly string _chatId = "";
        private TelegramBotClient _client;

        public TelegramRepository(IConfiguration configuration)
        {
            _token = configuration["TelegtamConfig:BotToken"] ?? throw new("TelegtamConfig:BotToken is null.");
            _chatId = configuration["TelegtamConfig:ChatId"] ?? throw new("TelegtamConfig:ChatId is null."); 

            _client = new TelegramBotClient(_token);
        }

        public async Task SendMessage(string message)
        {
            var result = await _client.SendMessage(_chatId, message);
            Console.WriteLine(result.ToString());
        }
       
    }
}