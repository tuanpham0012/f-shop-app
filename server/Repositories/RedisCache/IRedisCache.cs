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
    TItem? GetByKey<TItem>(string key);

    //
    // Summary:
    //     Lấy đối tượng từ khóa cache nếu chưa có cache sẽ gọi hàm lấy đối tượng, hàm bất
    //     đồng bộ
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
    TItem? GetOrCreate<TItem>(string key, Func<TItem> factory);

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
    //     Thời gian tồn tại cache tính bắng giây
    //
    // Type parameters:
    //   TItem:
    //     Loại đối tượng
    //
    // Returns:
    //     TItem object
    TItem? GetOrCreate<TItem>(string key, Func<TItem> factory, int time);

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
    //     Ghi một giá trị vào cache
    //
    // Parameters:
    //   key:
    //     Khóa cache
    //
    //   value:
    //     Giá trị
    //
    // Type parameters:
    //   TItem:
    //     Loại đối tượng cần set
    //
    // Returns:
    //     TItem
    TItem SetValue<TItem>(string key, TItem value);

    //
    // Summary:
    //     Ghi một giá trị vào cache
    //
    // Parameters:
    //   key:
    //     Khóa cache
    //
    //   value:
    //     Giá trị
    //
    //   time:
    //     Thời gian lưu trong cache tính bằng giây
    //
    // Type parameters:
    //   TItem:
    //     Loại đối tượng cần set
    //
    // Returns:
    //     Trả lại đối tượng TItem
    TItem SetValue<TItem>(string key, TItem value, int time);

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
    void RemoveByPrefix(string pattern);

    //
    // Summary:
    //     Xóa hết dữ liệu cache
    void Clear();
    }
}