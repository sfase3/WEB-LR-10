using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface ICatalogService
    {
        Task<ResponseModel<Catalog>> GetCatalog(int id);
        Task<ResponseModel<IEnumerable<Catalog>>> GetAllCatalogs();
        Task<ResponseModel<Catalog>> AddCatalog(Catalog Catalog);
        Task<ResponseModel<Catalog>> UpdateCatalog(int id, Catalog Catalog);
        Task<ResponseModel<Catalog>> DeleteCatalog(int id);
    }
}
