using Application.Interfaces.Contexts;
using Domain.Orders;
using Domain.Payments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payments
{
    public interface IPaymentService
    {
        PaymentOfOrderDto PayForOrder(int orderId);
        PaymentDto GetPayment(Guid id);
    }
    public class PaymentService : IPaymentService
    {
        private readonly IDatabaseContext _context;
        private readonly IIdentityDatabaseContext _identityContext;
        public PaymentService(IDatabaseContext context, IIdentityDatabaseContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public PaymentDto GetPayment(Guid id)
        {
            var payment = _context.Payments
                .Include(p => p.Order).ThenInclude(p => p.OrderItems)
                .SingleOrDefault(p => p.Id == id);

            var user = _identityContext.Users.SingleOrDefault(u => u.Id == payment.Order.UserId);

            string description = $"پرداخت سفارش شماره {payment.OrderId}" + Environment.NewLine;
            description += "محصولات :" + Environment.NewLine;
            foreach (var item in payment.Order.OrderItems.Select(oi => oi.ProductName))
            {
                description += $" -{item}";
            }

            return new PaymentDto
            {
                Id = payment.Id,
                PhoneNumber = user.PhoneNumber,
                Description = description,
                Email = user.Email,
                Amount = payment.Amount,
                UserId = user.Id
            };
        }

        public PaymentOfOrderDto PayForOrder(int orderId)
        {
            var order = _context.Orders.Include(o => o.OrderItems).SingleOrDefault(o => o.Id == orderId);
            if (order is null) throw new Exception("");

            var payment = _context.Payments.SingleOrDefault(p => p.OrderId == order.Id);
            if (payment is null)
            {
                payment = new Payment(order.TotalPrice(), order.Id);
                _context.Payments.Add(payment);
                _context.SaveChanges();
            }

            return new PaymentOfOrderDto
            {
                PaymentId = payment.Id,
                Amount = payment.Amount,
                PaymentMethod = order.PaymentMethod,
            };
        }
    }
    public class PaymentOfOrderDto
    {
        public Guid PaymentId { get; set; }
        public int Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public string UserId { get; set; }

    }
}
