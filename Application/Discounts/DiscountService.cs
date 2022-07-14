using Application.Dtos;
using Application.Interfaces.Contexts;
using Domain.Discounts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Discounts
{
    public class DiscountService : IDiscountService
    {
        private readonly IDatabaseContext _context;
        private readonly IDiscountHistoryService _discountHistoryService;

        public DiscountService(IDatabaseContext context, IDiscountHistoryService discountHistoryService)
        {
            _context = context;
            _discountHistoryService = discountHistoryService;
        }

        public List<CatalogItemDto> GetCatalogItems(string searchKey)
        {
            if (!String.IsNullOrEmpty(searchKey))
            {
                var data = _context.CatalogItems
                    .Where(ci => ci.Name.Contains(searchKey))
                    .Select(ci => new CatalogItemDto
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                    }).ToList();

                return data;
            }
            else
            {
                var data = _context.CatalogItems
                    .OrderByDescending(ci => ci.Id)
                    .Take(10)
                    .Select(ci => new CatalogItemDto
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                    }).ToList();
                return data;
            }
        }

        public bool ApplyDiscountInBasket(string couponCode, int basketId)
        {
            var basket = _context.Baskets.Include(b => b.Items).Include(b => b.AppliedDiscount)
                .FirstOrDefault(b => b.Id == basketId);

            var discount = _context.Discounts.Where(b => b.CouponCode.Equals(couponCode)).FirstOrDefault();

            basket.ApplyDiscountCode(discount);
            _context.SaveChanges();
            return true;
        }
        public bool RemoveDiscountFromBasket(int basketId)
        {
            var basket = _context.Baskets.Find(basketId);
            basket.RemoveDiscount();
            _context.SaveChanges();
            return true;
        }

        public BaseDto IsDiscountValid(string couponCode, User user)
        {
            var discount = _context.Discounts.Where(d => d.CouponCode.Equals(couponCode)).FirstOrDefault();

            if (discount is null) return new BaseDto(false, new List<string> { "کد تخفیف معتبر نیست" });

            var now = DateTime.UtcNow;
            if (discount.StartDate.HasValue)
            {
                var startDate = DateTime.SpecifyKind(discount.StartDate.Value, DateTimeKind.Utc);

                if (startDate.CompareTo(now) > 0)
                    return new BaseDto(false, new List<string> { "زمان استفاده از این تخفیف هنوز فرا نرسیده است" });
            }

            if (discount.EndDate.HasValue)
            {
                var endDate = DateTime.SpecifyKind(discount.EndDate.Value, DateTimeKind.Utc);

                if (endDate.CompareTo(now) < 0)
                    return new BaseDto(false, new List<string> { "کد تخفیف منقضی شده است" });

            }

            var checkLimit = CheckDiscountLimitations(discount, user);

            if (checkLimit.IsSuccess) return checkLimit;

            return new BaseDto(true, null);
        }

        private BaseDto CheckDiscountLimitations(Discount discount, User user)
        {
            switch (discount.DiscountLimitation)
            {
                case DiscountLimitationType.Unlimited:
                    return new BaseDto(true, null);
                case DiscountLimitationType.NTimesOnly:
                    {
                        var totalUsage = _discountHistoryService.GetAllDiscountUsageHistory(discount.Id, null, 0, 1).Data.Count();
                        if (totalUsage < discount.LimitationTimes)
                        {
                            return new BaseDto(true, null);
                        }
                        else
                        {
                            return new BaseDto(false, new List<string> { "ظرفیت استفاده از این کد تخفیف تکمیل شده است" });
                        }
                    }
                case DiscountLimitationType.NTimesPerCustomer:
                    {
                        if (user is not null)
                        {
                            var totalUsage = _discountHistoryService.GetAllDiscountUsageHistory(discount.Id, user.Id, 0, 1).Data.Count();
                            if (totalUsage < discount.LimitationTimes)
                            {
                                return new BaseDto(true, null);
                            }
                            else
                            {
                                return new BaseDto(false, new List<string> { "ظرفیت استفاده از این کد تخفیف برای شما تکمیل شده است" });
                            }
                        }
                        else
                        {
                            return new BaseDto(true, null);
                        }
                    }
            }
            return new BaseDto(true, null);
        }
    }
}
