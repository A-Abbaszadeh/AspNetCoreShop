using System.Text;
using System.Threading.Tasks;

namespace Application.Baskets
{
    public interface IBasketService
    {
        BasketDto GetOrCreateBasketForUser(string buyerId);
        void AddItemToBasket(int basketId, int catalogItemId, int quantity = 1);
        bool RemoveItemFromBasket(int itemId);
        bool SetQuantities(int itemId, int quantity);
        BasketDto GetBasketForUser(string userId);
        void TransferBasket(string anonymousId, string userId);
    }
}
