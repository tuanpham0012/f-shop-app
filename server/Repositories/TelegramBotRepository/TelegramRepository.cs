
using System.Drawing;
using Telegram.Bot;

namespace ShopAppApi.Repositories.TelegramBotRepository
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
        private readonly string _token = "7882187432:AAENL8opQe2yhZYmFLUz8ixeFMQsPHhV018";
        private readonly string _chatId = "1645705288";
        private TelegramBotClient _client;

        private IConfiguration _configuration1;
        public TelegramRepository(IConfiguration configuration)
        {
            _configuration1 = configuration;
            _client = new TelegramBotClient(_token);
            _token = _configuration1["TelegramBot:BotToken"];
            _chatId = _configuration1["TelegramBot:ChatId"];
        }

        public async Task SendMessage(string message)
        {
            var result = await _client.SendMessage(_chatId, message);
            Console.WriteLine(result.ToString());
        }
       
    }
}