namespace ShopAppApi.Services.TelegramBotRepository
{
    public interface ITelegramRepository
    {
        public Task SendMessage(string message);
       
    }
}