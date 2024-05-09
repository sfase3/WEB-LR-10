using ProductWebAPI.Models;

namespace ProductWebAPI.Services
{
    public class ShopCartService : IShopCartService
    {
        private readonly List<ShopCard> _shopcards;

        public ShopCartService()
        {
            _shopcards = new List<ShopCard>
            {
                new ShopCard { ShopCardId = 1, ShopCardName = "Яйця", TotalAmount = 15.00 },
                new ShopCard { ShopCardId = 2, ShopCardName = "Ковбаса Звичайна", TotalAmount = 35.00 },
                new ShopCard { ShopCardId = 3, ShopCardName = "Щука", TotalAmount = 60.00 },
                new ShopCard { ShopCardId = 4, ShopCardName = "Коньяк 5 зірок", TotalAmount = 756.00 },

            };
        }
        public async Task<ResponseModel<ShopCard>> GetShopCard(int id)
        {
            var ShopCard = _shopcards.FirstOrDefault(p => p.ShopCardId == id);
            if (ShopCard == null)
            {
                return new ResponseModel<ShopCard>
                {
                    Data = null,
                    Success = false,
                    Message = $"ShopCard with id {id} not found."
                };
            }
            return new ResponseModel<ShopCard>
            {
                Data = ShopCard,
                Success = true,
                Message = $"Successfully retrieved ShopCard with id {id}."
            };
        }
        public async Task<ResponseModel<IEnumerable<ShopCard>>> GetAllShopCards()
        {
            return new ResponseModel<IEnumerable<ShopCard>>
            {
                Data = _shopcards,
                Success = true,
                Message = "Successfully retrieved all ShopCards."
            };
        }
        public async Task<ResponseModel<ShopCard>> AddShopCard(ShopCard ShopCard)
        {
            ShopCard.ShopCardId = _shopcards.Any() ? _shopcards.Max(p => p.ShopCardId) + 1 : 1;
            _shopcards.Add(ShopCard);
            return new ResponseModel<ShopCard>
            {
                Data = ShopCard,
                Success = true,
                Message = "ShopCard added successfully."
            };
        }

        public async Task<ResponseModel<ShopCard>> UpdateShopCard(int id, ShopCard ShopCard)
        {
            var existingShopCard = _shopcards.FirstOrDefault(p => p.ShopCardId == id);
            if (existingShopCard == null)
            {
                return new ResponseModel<ShopCard>
                {
                    Data = null,
                    Success = false,
                    Message = $"ShopCard with id {id} not found."
                };
            }
            existingShopCard.ShopCardName = ShopCard.ShopCardName;
            existingShopCard.TotalAmount= ShopCard.TotalAmount;
            return new ResponseModel<ShopCard>
            {
                Data = existingShopCard,
                Success = true,
                Message = $"ShopCard with id {id} updated successfully."
            };
        }

        public async Task<ResponseModel<ShopCard>> DeleteShopCard(int id)
        {
            var ShopCardToRemove = _shopcards.FirstOrDefault(p => p.ShopCardId == id);
            if (ShopCardToRemove == null)
            {
                return new ResponseModel<ShopCard>
                {
                    Data = null,
                    Success = false,
                    Message = $"ShopCard with id {id} not found."
                };
            }
            _shopcards.Remove(ShopCardToRemove);
            return new ResponseModel<ShopCard>
            {
                Data = ShopCardToRemove,
                Success = true,
                Message = $"ShopCard with id {id} deleted successfully."
            };
        }

    }
}
