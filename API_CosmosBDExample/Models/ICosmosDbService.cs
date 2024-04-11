namespace API_CosmosBDExample.Models
{
    public interface ICosmosDbService
    {

        Task<IEnumerable<Elementos>>GetMultipleAsync(string query);
        Task<Elementos> GetAsync(string id);
        Task AddAsync(Elementos item);
        Task UpdateAsync(string id,Elementos item);
        Task DeleteAsync(string id);



    }
}
