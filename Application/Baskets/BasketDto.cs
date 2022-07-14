using System.Collections.Generic;
using System.Linq;

namespace Application.Baskets
{
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public int DiscountAmount { get; set; }
        public int TotalPrice()
        {
            if (Items.Any())
            {
                int total = Items.Sum(p => p.UnitPrice * p.Quantity);
                total -= DiscountAmount;
                return total;
            }
            return 0;
        }

        public int TotalPriceWithoutDiscount()
        {
            if (Items.Any())
            {
                int total = Items.Sum(p => p.UnitPrice * p.Quantity);
                return total;
            }
            return 0;
        }

    }
}
