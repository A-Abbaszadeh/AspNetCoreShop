using Application.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Baskets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Application.Baskets
{
    public class BasketService : IBasketService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposerService;
        public BasketService(IDatabaseContext context, IUriComposerService uriComposerService)
        {
            _context = context;
            _uriComposerService = uriComposerService;
        }

        public BasketDto GetOrCreateBasketForUser(string buyerId)
        {
            var basket = _context.Baskets
                .Include(b => b.Items).ThenInclude(i => i.CatalogItem).ThenInclude(ci => ci.CatalogItemImages)
                .Include(b => b.Items).ThenInclude(i => i.CatalogItem).ThenInclude(ci => ci.Discounts)
                .FirstOrDefault(b => b.BuyerId == buyerId);

            if (basket is null)
            {
                return CreateBasketForUser(buyerId);
            }
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                DiscountAmount = basket.DiscountAmount,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    Id = item.Id,
                    CatalogItemId = item.CatalogItemId,
                    CatalogName = item.CatalogItem.Name,
                    UnitPrice = item.CatalogItem.Price,
                    Quantity = item.Quantity,
                    ImageUrl = _uriComposerService.ComposeImageUri(item?.CatalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? "")
                }).ToList()
            };
        }

        private BasketDto CreateBasketForUser(string buyerId)
        {
            Basket basket = new Basket(buyerId);

            _context.Baskets.Add(basket);
            _context.SaveChanges();

            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId
            };
        }

        public void AddItemToBasket(int basketId, int catalogItemId, int quantity = 1)
        {
            var basket = _context.Baskets.Find(basketId);
            if (basket is null)
            {
                throw new Exception("");
            }
            var catalogItemPrice = _context.CatalogItems
                .Where(ci => ci.Id == catalogItemId)
                .Select(ci => ci.Price)
                .FirstOrDefault();

            basket.AddItem(catalogItemPrice, quantity, catalogItemId);
            _context.SaveChanges();
        }

        public bool RemoveItemFromBasket(int itemId)
        {
            var item = _context.BasketItems.Find(itemId);
            _context.BasketItems.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public bool SetQuantities(int itemId, int quantity)
        {
            var item = _context.BasketItems.Find(itemId);
            item.SetQuantity(quantity);
            _context.SaveChanges();
            return true;
        }

        public BasketDto GetBasketForUser(string userId)
        {
            var basket = _context.Baskets
                .Include(b => b.Items)
                .ThenInclude(i => i.CatalogItem)
                .ThenInclude(ci => ci.CatalogItemImages)
                .FirstOrDefault(b => b.BuyerId == userId);

            if (basket is null)
            {
                return null;
            }
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                DiscountAmount = basket.DiscountAmount,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    Id = item.Id,
                    CatalogItemId = item.CatalogItemId,
                    CatalogName = item.CatalogItem.Name,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    ImageUrl = _uriComposerService.ComposeImageUri(item?.CatalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? "")
                }).ToList()
            };
        }

        public void TransferBasket(string anonymousId, string userId)
        {
            var anonymousBasket = _context.Baskets
                .Include(b => b.Items).Include(b => b.AppliedDiscount).SingleOrDefault(b => b.BuyerId == anonymousId);
            if (anonymousBasket is null) return;

            var userBasket = _context.Baskets.SingleOrDefault(b => b.BuyerId == userId);
            if (userBasket is null)
            {
                userBasket = new Basket(userId);
                _context.Baskets.Add(userBasket);
            }

            foreach (var item in anonymousBasket.Items)
            {
                userBasket.AddItem(item.UnitPrice, item.Quantity, item.CatalogItemId);
            }

            if (anonymousBasket.AppliedDiscount is not null)
            {
                userBasket.ApplyDiscountCode(anonymousBasket.AppliedDiscount);
            }
            _context.Baskets.Remove(anonymousBasket);

            _context.SaveChanges();
        }
    }
}
