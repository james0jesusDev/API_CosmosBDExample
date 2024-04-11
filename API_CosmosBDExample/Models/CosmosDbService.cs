
using Microsoft.Azure.Cosmos;
using System.ComponentModel;

namespace API_CosmosBDExample.Models
{
    public class CosmosDbService : ICosmosDbService
    {
        private Microsoft.Azure.Cosmos.Container _container;

        public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }





        public async Task AddAsync(Elementos item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Elementos>(id, new PartitionKey(id));
        }

        public async Task<Elementos> GetAsync(string id)
        {
            try
            {

                var response = await _container.ReadItemAsync<Elementos>(id, new PartitionKey(id));
                return response.Resource;
            }
            
            catch (CosmosException )
            {

                return null;
            }



        }

        public async Task<IEnumerable<Elementos>> GetMultipleAsync(string queryString)
        {
            var query = _container.GetItemQueryIterator<Elementos>(new QueryDefinition(queryString));
               var results =  new  List<Elementos>();
            
            while(query.HasMoreResults)
            {

                var response= await query.ReadNextAsync();
                results.AddRange(response.ToList());    
            }
            return results;
        }

        public async Task UpdateAsync( string id, Elementos item)
        {

            await _container.UpsertItemAsync(item, new PartitionKey(id));

        }
    }
}
