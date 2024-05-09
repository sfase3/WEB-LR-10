using System.Collections.Generic;
using System.Threading.Tasks;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IProductService
    {
        Task<ResponseModel<Product>> GetProduct(int id);
        Task<ResponseModel<IEnumerable<Product>>> GetAllProducts();
        Task<ResponseModel<Product>> AddProduct(Product product);
        Task<ResponseModel<Product>> UpdateProduct(int id, Product product);
        Task<ResponseModel<Product>> DeleteProduct(int id);
    }
}
