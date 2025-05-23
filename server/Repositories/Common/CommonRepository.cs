

using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Services.RedisCache;

namespace ShopAppApi.Repositories.Common
{
    public class CommonRepository(ShopAppContext _context, IRedisCache _cache) : ICommonRepository
    {
        private readonly ShopAppContext context = _context;
        private readonly IRedisCache cache = _cache;
        public async Task<List<PaymentMethod>> PaymentMethods()
        {
            string cacheKey = "PaymentMethods";
            var cachedData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                var paymentMethods = await context.PaymentMethods.ToListAsync();
                return paymentMethods;
            });
            return cachedData ?? [];
        }

        public async Task<List<ShippingUnit>> ShippingUnits()
        {
            string cacheKey = "ShippingUnits";
            var cachedData = await cache.GetOrCreateAsync(cacheKey, async () =>
            {
                var shippingUnits = await context.ShippingUnits.ToListAsync();
                return shippingUnits;
            });
            return cachedData ?? [];
        }

        public async Task<string> GetServerName()
        {
            var connection = context.Database.GetDbConnection();
            string serverName = "";
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT @@SERVERNAME";
                    object result = await command.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        serverName = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy tên server: {ex.Message}");
                // Xử lý lỗi nếu cần
            }
            finally
            {
                // Không đóng kết nối ở đây nếu nó được quản lý bởi DbContext
                // EF Core sẽ quản lý vòng đời của kết nối.
                // Nếu bạn tự mở, bạn có thể cần tự đóng, nhưng thường thì không cần với GetDbConnection().
            }
            return serverName;
        }
    }
}