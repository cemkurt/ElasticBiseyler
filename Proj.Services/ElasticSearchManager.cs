using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Services
{
    public class ElasticSearchManager<T> : IElasticSearchManager<T> where T : class 
    {

        private readonly IElasticClient _elasticClient;
        public ElasticSearchManager(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<List<T>> SearchAsync(SearchDescriptor<T> searchDescriptor)
        {

            var indexName = typeof(T).Name.ToLower() + "_index";

            var models = await _elasticClient.SearchAsync<T>(searchDescriptor.Index(indexName));

            return models.Documents.ToList();
        }

        public async Task<T> AddOrUpdateAsync(T item)
        {

            var indexName = typeof(T).Name.ToLower() + "_index";

            var model = await _elasticClient.IndexAsync<T>(item, x => x.Index(indexName));

            return item;
        }

        public async Task<T> GetAsync(T item)
        {

            var indexName = typeof(T).Name.ToLower() + "_index";

            var model = await _elasticClient.GetAsync(DocumentPath<T>.Id(new Id(item)), x => x.Index(indexName));

            return model.Source;
        }

        public  async void DeleteIndexAsync()
        {
            var indexName = typeof(T).Name.ToLower() + "_index";

            await _elasticClient.Indices.DeleteAsync(indexName);

            
        }

        public async Task<bool> DeleteAsync(T item)
        {
            var indexName = typeof(T).Name.ToLower() + "_index";

            await _elasticClient.DeleteAsync(DocumentPath<T>.Id(new Id(item)), x => x.Index(indexName));

            return true;
        }

        public async Task<List<T>> LoadAsync(List<T> list, string indexName = null)
        {
            if (string.IsNullOrEmpty(indexName))
            {
                indexName = typeof(T).Name.ToLower() + "_index";
            }

            if (!_elasticClient.Indices.Exists(indexName).Exists)
            {
                _elasticClient.Indices.Create(indexName, x => x.Map<Category>(y => y.AutoMap()));
            }
            else
            {
                _elasticClient.Indices.Delete(indexName);

                _elasticClient.Indices.Create(indexName, x => x.Map<Category>(y => y.AutoMap()));
            }

            _elasticClient.Bulk(x => x.Index(indexName).IndexMany(list));


            return list;
        }
    }



    public interface IElasticSearchManager<T> where T : class
    {
        public Task<T> GetAsync(T item);
        public Task<bool> DeleteAsync(T item);
        public void  DeleteIndexAsync();
        public Task<T> AddOrUpdateAsync(T item);
        public Task<List<T>> SearchAsync(SearchDescriptor<T> searchDescriptor);
        public Task<List<T>> LoadAsync(List<T> list, string indexName = null);
    }
}

public class BaseModel : IBaseModel
{
    public string Name { get; set; }

}
public interface IBaseModel
{
    string Name { get; set; }
}