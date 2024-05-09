using System.Collections.Generic;
using System.Threading.Tasks;
using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public interface IShopCartService
    {
        Task<ResponseModel<ShopCard>> GetShopCard(int id);
        Task<ResponseModel<IEnumerable<ShopCard>>> GetAllShopCards();
        Task<ResponseModel<ShopCard>> AddShopCard(ShopCard ShopCard);
        Task<ResponseModel<ShopCard>> UpdateShopCard(int id, ShopCard ShopCard);
        Task<ResponseModel<ShopCard>> DeleteShopCard(int id);
    }
}
