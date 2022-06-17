using Domain.Attributes;
using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Discounts
{
    [Auditable]
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool UsePercentage { get; set; }
        public int DiscountPercentage { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool RequiredCouponCode { get; set; }
        public string CouponCode { get; set; }
        [NotMapped]
        public DiscountType DiscountType
        {
            get => (DiscountType)this.DiscountTypeId;
            set => this.DiscountTypeId = (int)value;
        }
        public int DiscountTypeId { get; set; }
        public ICollection<CatalogItem> CatalogItems { get; set; }
        public int LimitationTimes { get; set; }
        public DiscountLimitationType DiscountLimitation 
        {
            get => (DiscountLimitationType)this.DiscountLimitationId;
            set => this.DiscountLimitationId = (int)value;
        }
        public int DiscountLimitationId { get; set; }
    }

    public enum DiscountType
    {
        [Display(Name = "تخفیف برای محصولات")]
        AssignedToProduct = 1,
        //AssignedToCategories = 2,
        //AssignedToUser = 3,
        //AssignedToBrand = 4,
        // ...
    }

    public enum DiscountLimitationType
    {
        /// <summary>
        /// بدون محدودیت
        /// </summary>
        [Display(Name = "بدون محدودیت")]
        Unlimited = 0,
        /// <summary>
        /// N بار
        /// </summary>
        [Display(Name = "N بار")]
        NTimesOnly = 1,
        /// <summary>
        /// N بار به ازای هر مشتری
        /// </summary>
        [Display(Name = "N بار به ازای هر مشتری")]
        NTimesPerCustomer = 2,
    }
}
