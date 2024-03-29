﻿using Application.Dtos;
using Domain.Users;
using System.Collections.Generic;

namespace Application.Discounts
{
    public interface IDiscountService
    {
        List<CatalogItemDto> GetCatalogItems(string searchKey);
        bool ApplyDiscountInBasket(string couponCode, int basketId);
        bool RemoveDiscountFromBasket(int basketId);
        BaseDto IsDiscountValid(string couponCode, User user);
    }
}
