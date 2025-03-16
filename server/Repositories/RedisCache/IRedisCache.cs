namespace ShopAppApi.Repositories.RedisCache
{
    public interface IRedisCache
    {
        //
        // Summary:
        //     Lấy đối tượng từ khóa cache và trả về với kiểu được chỉ định
        //
        // Parameters:
        //   key:
        //     Khóa
        //
        // Type parameters:
        //   T:
        //     Kiểu đối tượng cần nhận
        //
        // Returns:
        //     T object
        Task<T?> GetByKeyAsync<T>(string key);

        //
        // Summary:
        //     Lấy đối tượng từ khóa cache nếu chưa có cache sẽ gọi hàm lấy đối tượng
        //
        // Parameters:
        //   key:
        //     Khóa cache
        //
        //   factory:
        //     Hàm lấy đối tượng nếu cache null
        //
        //   time:
        //     Thời gian cache tính bằng giây
        //
        // Type parameters:
        //   T:
        //     Loại đối tượng
        //
        // Returns:
        //     T object
        Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int? time = null);

        //
        // Summary:
        //     Gỡ bỏ cache theo khóa cache bất đồng bộ
        //
        // Parameters:
        //   key:
        //     Khóa cache
        void Remove(string key);

        //
        // Summary:
        //     Gõ bỏ cache theo tiền tố
        //
        // Parameters:
        //   pattern:
        //     Mẫu khóa cache

        //
        // Summary:
        //     Xóa hết dữ liệu cache
        void Clear();

        bool TryGetValue<T>(string key, out T? value);
        void RemoveByPrefix(string pattern);

        string ReplaceString(string request);
    }


}