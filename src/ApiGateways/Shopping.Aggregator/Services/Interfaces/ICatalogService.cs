using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
        Task<CatalogModel> GetCatalogById(string id);
    }
}
