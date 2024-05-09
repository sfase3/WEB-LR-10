using System.Collections.Generic;
using System.Threading.Tasks;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Яйця", Price = 15.00 },
                new Product { Id = 2, Name = "Ковбаса Звичайна", Price = 35.00 },
                new Product { Id = 3, Name = "Щука", Price = 60.00 },
                new Product { Id = 4, Name = "Коньяк 5 зірок", Price = 756.00 },
                new Product { Id = 5, Name = "Банани", Price = 100.00 },
                new Product { Id = 6, Name = "Сухий завтрак", Price = 49.50 },
                new Product { Id = 7, Name = "Какао", Price = 40.99 },
                new Product { Id = 8, Name = "Кілька в томаті", Price = 10.20 },
                new Product { Id = 9, Name = "Хліб", Price = 18.00 },
                new Product { Id = 10, Name = "Кока Кола", Price = 20.00},

            };
        }
        public async Task<ResponseModel<Product>> GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new ResponseModel<Product>
                {
                    Data = null,
                    Success = false,
                    Message = $"Product with id {id} not found."
                };
            }
            return new ResponseModel<Product>
            {
                Data = product,
                Success = true,
                Message = $"Successfully retrieved product with id {id}."
            };
        }
        public async Task<ResponseModel<IEnumerable<Product>>> GetAllProducts()
        {
            return new ResponseModel<IEnumerable<Product>>
            {
                Data = _products,
                Success = true,
                Message = "Successfully retrieved all products."
            };
        }

        public async Task<ResponseModel<Product>> AddProduct(Product product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return new ResponseModel<Product>
            {
                Data = product,
                Success = true,
                Message = "Product added successfully."
            };
        }
        public async Task<ResponseModel<Product>> UpdateProduct(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return new ResponseModel<Product>
                {
                    Data = null,
                    Success = false,
                    Message = $"Product with id {id} not found."
                };
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return new ResponseModel<Product>
            {
                Data = existingProduct,
                Success = true,
                Message = $"Product with id {id} updated successfully."
            };
        }

        public async Task<ResponseModel<Product>> DeleteProduct(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.Id == id);
            if (productToRemove == null)
            {
                return new ResponseModel<Product>
                {
                    Data = null,
                    Success = false,
                    Message = $"Product with id {id} not found."
                };
            }
            _products.Remove(productToRemove);
            return new ResponseModel<Product>
            {
                Data = productToRemove,
                Success = true,
                Message = $"Product with id {id} deleted successfully."
            };
        }
    }
}
