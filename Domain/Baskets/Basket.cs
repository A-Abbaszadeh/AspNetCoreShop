using Domain.Attributes;
using Domain.Catalogs;
using Domain.Discounts;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Baskets
{
    [Auditable]
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; private set; }
        private readonly List<BasketItem> _items = new List<BasketItem>();

        public int DiscountAmount { get; private set; }
        public Discount AppliedDiscount { get; private set; }
        public int? AppliedDiscountId { get; private set; }

        public ICollection<BasketItem> Items => _items.AsReadOnly();
        public Basket(string buyerId)
        {
            BuyerId = buyerId;
        }

        public void AddItem(int unitPrice, int quantity, int catalogItemId)
        {
            if (!Items.Any(i => i.CatalogItemId == catalogItemId))
            {
                _items.Add(new BasketItem(unitPrice, quantity, catalogItemId));
                return;
            }

            var existingItem = Items.FirstOrDefault(i => i.CatalogItemId == catalogItemId);
            existingItem.AddQuantity(quantity);
        }
        public int TotalPrice()
        {
            int totalPrice = _items.Sum(p => p.UnitPrice * p.Quantity);
            totalPrice -= AppliedDiscount.GetDiscountAmount(totalPrice);
            return totalPrice;
        }

        public int TotalPriceWithoutDiscount()
        {
            int totalPrice = _items.Sum(p => p.UnitPrice * p.Quantity);
            return totalPrice;
        }
        public void ApplyDiscountCode(Discount discount)
        {
            AppliedDiscount = discount;
            AppliedDiscountId = discount.Id;
            DiscountAmount = discount.GetDiscountAmount(TotalPriceWithoutDiscount());
        }

        public void RemoveDiscount()
        {
            AppliedDiscount = null;
            AppliedDiscountId = null;
            DiscountAmount = 0;
        }
    }

    [Auditable]
    public class BasketItem
    {
        public int Id { get; set; }
        public int UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public int CatalogItemId { get; private set; }
        public CatalogItem CatalogItem { get; private set; }
        public int BasketId { get; private set; }
        public Basket Basket { get; private set; }
        public BasketItem(int unitPrice, int quantity, int catalogItemId)
        {
            UnitPrice = unitPrice;
            SetQuantity(quantity); //  -> Quantity = quantity;
            CatalogItemId = catalogItemId;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
