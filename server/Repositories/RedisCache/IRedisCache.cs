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
    //   TItem:
    //     Kiểu đối tượng cần nhận
    //
    // Returns:
    //     TItem object
    Task<TItem?> GetByKeyAsync<TItem>(string key);

    //
    // Summary:
    //     Lấy đôi từ khóa cache, hàm bất đồng bộ
    //
    // Parameters:
    //   key:
    //     Khóa cache
    //
    //   factory:
    //     Hàm lấy đối tượng nếu cache null
    //
    // Type parameters:
    //   TItem:
    //     Loại đối tượng
    //
    // Returns:
    //     TItem object
    Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory);

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
    //   TItem:
    //     Loại đối tượng
    //
    // Returns:
    //     TItem object
    Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory, int time);

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
    }
}