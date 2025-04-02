namespace ShopAppApi.Helpers.Interfaces
{
    public interface IStringHelper
    {
        public HashSalt EncryptPassword(string password);
        public bool VerifyPassword(string enteredPassword, string salt, string storedPassword);
        public string RandomString(int length);
    }
}