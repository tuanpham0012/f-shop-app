namespace ShopAppApi.Repositories.Metrics
{
    public interface ICoreMonitoringData
    {
        //
        // Summary:
        //     ghi các thông số dữ liệu cần monitor
        //
        // Parameters:
        //   metrics:
        //     Tên metrics
        //
        //   action:
        //     Hàm xử lý
        //
        //   time:
        //     Thời gian
        void WriteTimeMetric(string metrics, string action, long time);

        void WriteData(string metrics, string action, double time);

        //
        // Summary:
        //     Ghi dữ liệu
        //
        // Parameters:
        //   data:
        //     Dữ liệu dạng string cần build
        //
        //     Vd: $"{metrics},action={action} process_time={time}"
        void Write(string data);

        //
        // Summary:
        //     Thực hiện query data theo cấu query được chuyển vào
        //
        // Parameters:
        //   query:
        //     Cấu query
        //
        // Type parameters:
        //   T:
        //     Kiểu dữ liệu
        //
        // Returns:
        //     List T
        Task<List<T>> QueryAsync<T>(string query);
    }
}
