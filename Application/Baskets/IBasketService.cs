using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Baskets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
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
                .Include(b => b.Items)
                .ThenInclude(i => i.CatalogItem)
                .ThenInclude(ci => ci.CatalogItemImages)
                .FirstOrDefault(b => b.BuyerId == buyerId);

            if (basket is null)
            {
                return CreateBasketForUser(buyerId);
            }
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
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
    }
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

    }
    public class BasketItemDto
    {
        public int Id { get; set; }
        public int CatalogItemId { get; set; }
        public string CatalogName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}
