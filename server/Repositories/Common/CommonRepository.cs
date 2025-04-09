

using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Repositories.RedisCache;

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
    }
}