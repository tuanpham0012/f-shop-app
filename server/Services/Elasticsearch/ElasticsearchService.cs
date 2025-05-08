using Nest;

namespace ShopAppApi.Services.Elasticsearch
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<bool> IndexExists(string indexName)
        {
            var response = await _elasticClient.Indices.ExistsAsync(indexName);
            return response.Exists;
        }

        public async Task CreateIndex(string indexName)
        {
            var createIndexResponse = await _elasticClient.Indices.CreateAsync(indexName, c => c
                .Map(m => m
                    .AutoMap()
                )
            );
        }

        public async Task UpdateDocument<T>(string indexName, string documentId, T document) where T : class
        {
            if(IndexExists(indexName).Result == false)
            {
                await CreateIndex(indexName);
            }
            if (string.IsNullOrEmpty(documentId))
            {
                throw new ArgumentException("Document ID cannot be null or empty.", nameof(documentId));
            }
            if( GetDocument<T>(indexName, documentId).Result == null)
            {
                await CreateDocument(indexName, document);
                return;
            }
            var response = await _elasticClient.UpdateAsync<T>(documentId, u => u.Index(indexName).Doc(document));
            if (response.IsValid)
            {
                Console.WriteLine("Cập nhật dữ liệu thành công!");
            }
            else
            {
                Console.WriteLine($"Lỗi: {response.OriginalException.Message}");
            }
        }
        public async Task CreateDocument<T>(string indexName, T document) where T : class
        {
            if(IndexExists(indexName).Result == false)
            {
                await CreateIndex(indexName);
            }
            var response = await _elasticClient.IndexAsync(document, idx => idx.Index(indexName));
            if (response.IsValid)
            {
                Console.WriteLine("Thêm dữ liệu thành công!");
            }
            else
            {
                Console.WriteLine($"Lỗi: {response.OriginalException.Message}");
            }
        }

        public async Task CreateManyDocument<T>(string indexName, List<T> document) where T : class
        {
            if(IndexExists(indexName).Result == false)
            {
                await CreateIndex(indexName);
            }
            var response = await _elasticClient.BulkAsync(idx => idx.Index(indexName).IndexMany(document));
            if (response.IsValid)
            {
                Console.WriteLine("Thêm dữ liệu thành công!");
            }
            else
            {
                Console.WriteLine($"Lỗi: {response.OriginalException.Message}");
            }
        }

        public async Task DeleteIndex(string indexName)
        {
            if(!IndexExists(indexName).Result)
            {
                return;
            }
            await _elasticClient.Indices.DeleteAsync(indexName);
        }

        public async Task DeleteDocument(string indexName, string documentId)
        {
            await _elasticClient.DeleteAsync<object>(documentId, d => d.Index(indexName));
        }

        public async Task<T> GetDocument<T>(string indexName, string documentId) where T : class
        {
            var response = await _elasticClient.GetAsync<T>(documentId, g => g.Index(indexName));
            return response.Source;
        }

        public Task<ISearchResponse<T>> SearchDocuments<T>(string indexName) where T : class
        {
            return _elasticClient.SearchAsync<T>(s => s
                .Index(indexName));
        }
    }
}