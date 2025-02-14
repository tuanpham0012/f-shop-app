
using Telegram.Bot;

namespace ShopAppApi.Repositories.TelegramBotRepository
{
    public class TelegramMessage
    {
        public string Message { get; set; }
    }
    public class TelegramRepository : ITelegramRepository
    {
        // https://api.telegram.org/bot{token}/getUpdates
        private readonly string _token = "7882187432:AAENL8opQe2yhZYmFLUz8ixeFMQsPHhV018";
        private readonly string _chatId = "1645705288";
        private TelegramBotClient _client;
        public TelegramRepository()
        {
            _client = new TelegramBotClient(_token);
        }

        public async Task SendMessage(string message)
        {
            var result = await _client.SendMessage(_chatId, message);
            Console.WriteLine(result.ToString());
        }
       
    }
}