
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using Newtonsoft.Json.Linq;

namespace ShopAppApi.Repositories.Metrics
{
    public class InfluxData : ICoreMonitoringData
    {
        private readonly IConfiguration _configuration;
        private readonly InfluxDBClient _client;
        private readonly WriteApi _writeApi;
        private readonly QueryApi _queryApi;

        public InfluxData(IConfiguration configuration) {
            _configuration = configuration;
            _client = new InfluxDBClient(_configuration["InfluxSettings:Host"], _configuration["InfluxSettings:Token"]);
            _writeApi = _client.GetWriteApi();
            _queryApi = _client.GetQueryApi();
        }
        public virtual async Task<List<T>> QueryAsync<T>(string query)
        {
            return await _queryApi.QueryAsync<T>(query, _configuration["InfluxSettings:Org"]);
        }

        public virtual void Write(string data)
        {
            
            _writeApi.WriteRecord(data, WritePrecision.Ns, _configuration["InfluxSettings:Bucket"], _configuration["InfluxSettings:Org"]);
        }
        public virtual void WriteData(string metrics, string action, double value)
        {
            var point = PointData.Measurement(metrics)
                    .Tag("action", action)
                    .Field("value", value)
                    .Timestamp(DateTime.UtcNow, WritePrecision.Ns);
            _writeApi.WritePoint(point, _configuration["InfluxSettings:Bucket"], _configuration["InfluxSettings:Org"]);
        }

        public virtual void WriteTimeMetric(string metrics, string action, long time)
        {
            string record = $"{metrics},action={action} process_time={time}";
            _writeApi.WriteRecord(record, WritePrecision.Ns, _configuration["InfluxSettings:Bucket"], _configuration["InfluxSettings:Org"]);
        }
    }
}
