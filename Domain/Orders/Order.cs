using Domain.Attributes;
using Domain.Catalogs;
using Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Orders
{
    [Auditable]
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; private set; }
        public DateTime OrderDate { get; private set; } = DateTime.Now;
        public Address Address { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        private readonly List<OrderItem> _orderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public decimal DiscountAmount { get; private set; }
        public Discount AppliedDiscount { get; private set; }
        public int? AppliedDiscountId { get;  private set; }

        public Order(string userId, Address address, PaymentMethod paymentMethod, List<OrderItem> orderItems, Discount discount)
        {
            UserId = userId;
            Address = address;
            PaymentMethod = paymentMethod;
            _orderItems = orderItems;
            if (discount is not null)
            {
                ApplyDiscountCode(discount);
            }
        }

        public Order()  // for EF Core
        {
        }

        /// <summary>
        /// مبلغ کل با تخفیف
        /// </summary>
        /// <returns></returns>
        public int TotalPrice()
        {
            int totalPrice = _orderItems.Sum(p => p.UnitPrice * p.Units);
            if (AppliedDiscount is not null)
            {
                totalPrice -= AppliedDiscount.GetDiscountAmount(totalPrice);
            }
            return totalPrice;
        }
        /// <summary>
        /// مبلغ کل بدون تخفیف
        /// </summary>
        /// <returns></returns>
        public int TotalPriceWithoutDiscount()
        {
            int totalPrice = _orderItems.Sum(p => p.UnitPrice * p.Units);
            return totalPrice;
        }

        public void ApplyDiscountCode(Discount discount)
        {
            AppliedDiscount = discount;
            AppliedDiscountId = discount.Id;
            DiscountAmount = discount.GetDiscountAmount(TotalPrice());
        }

        /// <summary>
        /// پرداخت با موفقیت انجام شد
        /// </summary>
        public void PaymentDone()
        {
            PaymentStatus = PaymentStatus.Paid;
        }

        /// <summary>
        /// سفارش تحویل داده شد
        /// </summary>
        public void OrderDeliverd()
        {
            OrderStatus = OrderStatus.Delivered;
        }

        /// <summary>
        /// سفارش مرجوع شد
        /// </summary>
        public void OrderReturned()
        {
            OrderStatus = OrderStatus.Returned;
        }

        /// <summary>
        /// سفارش لغو شد
        /// </summary>
        public void OrderCancelled()
        {
            OrderStatus = OrderStatus.Cancelled;
        }
    }

    [Auditable]
    public class OrderItem
    {
        public int Id { get; set; }
        public CatalogItem CatalogItem { get; set; }
        public int CatalogItemId { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
        public int UnitPrice { get; private set; }
        public int Units { get; private set; }
        public OrderItem(int catalogItemId, string productName, string pictureUri, int unitPrice, int units)
        {
            CatalogItemId = catalogItemId;
            ProductName = productName;
            PictureUri = pictureUri;
            UnitPrice = unitPrice;
            Units = units;
        }
        public OrderItem() // For EF Core
        {
        }
    }
    public class Address
    {
        public string State { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string PostalAddress { get; private set; }
        public string ReciverName { get; private set; }
        public Address(string state, string city, string zipCode, string postalAddress, string reciverName)
        {
            State = state;
            City = city;
            ZipCode = zipCode;
            PostalAddress = postalAddress;
            ReciverName = reciverName;
        }
    }

    public enum PaymentMethod
    {
        /// <summary>
        /// پرداخت آنلاین
        /// </summary>
        OnlinePayment = 0,
        /// <summary>
        /// پرداخت در محل
        /// </summary>
        PaymentOnTheSpot = 1,
    }

    public enum PaymentStatus
    {
        /// <summary>
        /// منتظر پرداخت
        /// </summary>
        WatingForPayment = 0,
        /// <summary>
        /// پرداخت انجام شد
        /// </summary>
        Paid = 1,
    }

    public enum OrderStatus
    {
        /// <summary>
        /// در حال پردازش
        /// </summary>
        Processing = 0,
        /// <summary>
        /// تحویل داده شده
        /// </summary>
        Delivered = 1,
        /// <summary>
        /// مرجوع شده
        /// </summary>
        Returned = 2,
        /// <summary>
        /// لغو شده
        /// </summary>
        Cancelled = 3,
    }
}
