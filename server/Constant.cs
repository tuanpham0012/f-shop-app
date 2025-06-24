namespace ShopAppApi
{
    public static class Constants
    {
        public const string AdminMenuCache = "AdminMenuCache";
        public const string UserMenuCache = "UserMenuCache";
        public const string CategoryCache = "CategoryCache";
        public const string BrandCache = "BrandCache";

        public enum OrderStatus : byte
        {
            Pending = 0,
            Processing = 1,
            Shipped = 2,
            Cancelled = 3,
        }
    }
}