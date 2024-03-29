﻿using Application.Discounts;
using Application.Exceptions;
using Application.Interfaces.Contexts;
using Application.UriComposer;
using AutoMapper;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Application.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUriComposerService _uriComposerService;
        private readonly IDiscountHistoryService _discountHistoryService;

        public OrderService(
            IDatabaseContext context, 
            IMapper mapper, 
            IUriComposerService uriComposerService,
            IDiscountHistoryService discountHistoryService)
        {
            _context = context;
            _mapper = mapper;
            _uriComposerService = uriComposerService;
            _discountHistoryService = discountHistoryService;
        }

        public int CreateOrder(int basketId, int userAddressId, PaymentMethod paymentMethod)
        {
            var basket = _context.Baskets.Include(b => b.Items).Include(b => b.AppliedDiscount).SingleOrDefault(b => b.Id == basketId);

            if (basket is null) throw new NotFoundException(nameof(basket), basketId);

            int[] Ids = basket.Items.Select(b => b.CatalogItemId).ToArray();

            var catalogItems = _context.CatalogItems.Include(ci => ci.CatalogItemImages).Where(ci => Ids.Contains(ci.Id));

            var orderItems = basket.Items.Select(basketItem =>
            {
                var catalogItem = catalogItems.First(ci => ci.Id == basketItem.CatalogItemId);
                var pictureUri = _uriComposerService.ComposeImageUri(catalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? "");
                var orderItem = new OrderItem(catalogItem.Id, catalogItem.Name, pictureUri, catalogItem.Price, basketItem.Quantity);

                return orderItem;
            }).ToList();

            var userAddress = _context.UserAddresses.SingleOrDefault(ua => ua.Id == userAddressId);

            var address = _mapper.Map<Address>(userAddress);

            var Order = new Order(basket.BuyerId, address, paymentMethod, orderItems, basket.AppliedDiscount);

            _context.Orders.Add(Order);
            _context.Baskets.Remove(basket);
            _context.SaveChanges();

            if (basket.AppliedDiscount is not null)
            {
                _discountHistoryService.InsertDiscountHistoryUsageHistory(basket.AppliedDiscount.Id, Order.Id);
            }
            return Order.Id;
        }
    }
}
