﻿using Application.Interfaces.Contexts;
using Domain.Payments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Application.Payments
{
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
                .Include(p => p.Order).ThenInclude(p => p.AppliedDiscount)
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
            var order = _context.Orders
                .Include(o => o.OrderItems).Include(o => o.AppliedDiscount).SingleOrDefault(o => o.Id == orderId);

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

        public bool VerifyPayment(Guid id, string Authority, long RefId)
        {
            var payment = _context.Payments.Include(p => p.Order).SingleOrDefault(p => p.Id == id);
            if (payment is null) throw new Exception("Payment Not Found");

            payment.Order.PaymentDone();
            payment.PaymentIsDone(Authority, RefId);

            _context.SaveChanges();
            return true;
        }
    }
}
