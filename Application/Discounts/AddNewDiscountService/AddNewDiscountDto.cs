using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Discounts.AddNewDiscountService
{
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
