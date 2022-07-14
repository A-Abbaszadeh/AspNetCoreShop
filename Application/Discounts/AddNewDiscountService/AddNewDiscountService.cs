using Application.Interfaces.Contexts;
using Domain.Discounts;
using System.Linq;

namespace Application.Discounts.AddNewDiscountService
{
    public class AddNewDiscountService : IAddNewDiscountService
    {
        private readonly IDatabaseContext _context;

        public AddNewDiscountService(IDatabaseContext context)
        {
            _context = context;
        }
        public void Execute(AddNewDiscountDto discount)
        {
            var newDiscount = new Discount
            {
                Name = discount.Name,
                UsePercentage = discount.UsePercentage,
                DiscountPercentage = discount.DiscountPercentage,
                DiscountAmount = discount.DiscountAmount,
                StartDate = discount.StartDate,
                EndDate = discount.EndDate,
                RequiredCouponCode = discount.RequiredCouponCode,
                CouponCode = discount.CouponCode,
                DiscountTypeId = discount.DiscountTypeId,
                LimitationTimes = discount.LimitationTimes,
                DiscountLimitationId = discount.DiscountLimitationId,
            };

            if (discount.AppliedToCatalogItem is not null)
            {
                var catalogItems = _context.CatalogItems.Where(ci => discount.AppliedToCatalogItem.Contains(ci.Id)).ToList();
                newDiscount.CatalogItems = catalogItems;
            }

            _context.Discounts.Add(newDiscount);
            _context.SaveChanges();
        }
    }
}
