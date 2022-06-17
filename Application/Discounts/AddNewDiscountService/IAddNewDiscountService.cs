using Application.Interfaces.Contexts;
using Domain.Discounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Discounts.AddNewDiscountService
{
    public interface IAddNewDiscountService
    {
        void Execute(AddNewDiscountDto discount);
    }

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
    public class AddNewDiscountDto
    {
        [Display(Name = "نام تخفیف")]
        public string Name { get; set; }
        [Display(Name = "استفاده از درصد در تخفیف؟")]
        public bool UsePercentage { get; set; }
        [Display(Name = "درصد تخفیف")]
        public int DiscountPercentage { get; set; }
        [Display(Name = "مبلغ تخفیف")]
        public int DiscountAmount { get; set; }
        [Display(Name = "تاریخ شروع")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "تاریخ پایان")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "استفاده ار کوپن")]
        public bool RequiredCouponCode { get; set; }
        [Display(Name = "کد کوپن")]
        public string CouponCode { get; set; }
        [Display(Name = "نوع تخفیف")]
        public int DiscountTypeId { get; set; }
        [Display(Name = "تعداد دفعات استفاده")]
        public int LimitationTimes { get; set; }
        [Display(Name = "محدودیت تخفیف")]
        public int DiscountLimitationId { get; set; } = 0;
        [Display(Name = "اعمال برای محصولات")]
        public List<int> AppliedToCatalogItem { get; set; }
    }
}
