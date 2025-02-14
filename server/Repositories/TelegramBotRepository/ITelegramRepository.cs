namespace ShopAppApi.Repositories.TelegramBotRepository
{
    public interface ITelegramRepository
    {
        public Task SendMessage(string message);
       
    }
}