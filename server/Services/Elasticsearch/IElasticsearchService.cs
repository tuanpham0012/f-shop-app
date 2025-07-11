using Nest;

namespace ShopAppApi.Services.Elasticsearch
{
    public interface IElasticsearchService
    {
        Task<bool> IndexExists(string indexName);
        Task CreateIndex(string indexName);
        Task DeleteIndex(string indexName);
        Task UpdateDocument<T>(string indexName, string documentId, T document) where T : class;
        Task CreateDocument<T>(string indexName, T document) where T : class;
        Task CreateManyDocument<T>(string indexName, List<T> document) where T : class;
        Task DeleteDocument(string indexName, string documentId);
        Task<T> GetDocument<T>(string indexName, string documentId) where T : class;
        IElasticClient ElasticClient();
    }
}