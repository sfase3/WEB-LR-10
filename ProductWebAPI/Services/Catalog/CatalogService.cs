using System.Collections.Generic;
using System.Threading.Tasks;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly List<Catalog> catalogs;

        public CatalogService()
        {
            catalogs = new List<Catalog>
            {
                new Catalog { CatalogId = 1, CatalogName = "Овочі"},
                new Catalog { CatalogId = 2, CatalogName = "Фрукти"},
                new Catalog { CatalogId = 3, CatalogName = "Консерви"},
                new Catalog { CatalogId = 4, CatalogName = "М'ясо та деликатеси"},
                new Catalog { CatalogId = 5, CatalogName = "Риба та морепродукти"},
                new Catalog { CatalogId = 6, CatalogName = "Молочні продукти"},
                new Catalog { CatalogId = 7, CatalogName = "Хлібобулочні вироби"},
                new Catalog { CatalogId = 8, CatalogName = "Напої та безалкогольні напої"},
                new Catalog { CatalogId = 9, CatalogName = "Алкогольні напої"},
                new Catalog { CatalogId = 10, CatalogName = "Заморожені продукти"},

            };
        }
        public async Task<ResponseModel<Catalog>> GetCatalog(int id)
        {
            var Catalog = catalogs.FirstOrDefault(p => p.CatalogId == id);
            if (Catalog == null)
            {
                return new ResponseModel<Catalog>
                {
                    Data = null,
                    Success = false,
                    Message = $"Catalog with id {id} not found."
                };
            }
            return new ResponseModel<Catalog>
            {
                Data = Catalog,
                Success = true,
                Message = $"Successfully retrieved Catalog with id {id}."
            };
        }

        public async Task<ResponseModel<IEnumerable<Catalog>>> GetAllCatalogs()
        {
            return new ResponseModel<IEnumerable<Catalog>>
            {
                Data = catalogs,
                Success = true,
                Message = "Successfully retrieved all Catalogs."
            };
        }

        public async Task<ResponseModel<Catalog>> AddCatalog(Catalog Catalog)
        {
            Catalog.CatalogId = catalogs.Any() ? catalogs.Max(p => p.CatalogId) + 1 : 1;
            catalogs.Add(Catalog);
            return new ResponseModel<Catalog>
            {
                Data = Catalog,
                Success = true,
                Message = "Catalog added successfully."
            };
        }
        public async Task<ResponseModel<Catalog>> UpdateCatalog(int id, Catalog Catalog)
        {
            var existingCatalog = catalogs.FirstOrDefault(p => p.CatalogId == id);
            if (existingCatalog == null)
            {
                return new ResponseModel<Catalog>
                {
                    Data = null,
                    Success = false,
                    Message = $"Catalog with id {id} not found."
                };
            }
            existingCatalog.CatalogName = Catalog.CatalogName;
            return new ResponseModel<Catalog>
            {
                Data = existingCatalog,
                Success = true,
                Message = $"Catalog with id {id} updated successfully."
            };
        }
        public async Task<ResponseModel<Catalog>> DeleteCatalog(int id)
        {
            var CatalogToRemove = catalogs.FirstOrDefault(p => p.CatalogId == id);
            if (CatalogToRemove == null)
            {
                return new ResponseModel<Catalog>
                {
                    Data = null,
                    Success = false,
                    Message = $"Catalog with id {id} not found."
                };
            }
            catalogs.Remove(CatalogToRemove);
            return new ResponseModel<Catalog>
            {
                Data = CatalogToRemove,
                Success = true,
                Message = $"Catalog with id {id} deleted successfully."
            };
        }

        

        
    }
}
