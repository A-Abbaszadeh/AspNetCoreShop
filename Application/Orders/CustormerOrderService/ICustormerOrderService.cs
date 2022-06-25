using Application.Interfaces.Contexts;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.CustormerOrderService
{
    public interface ICustormerOrderService
    {
        List<MyOrderDto> GetMyOrder(string userId);
    }

    public class CustormerOrderService : ICustormerOrderService
    {
        private readonly IDatabaseContext _context;

        public CustormerOrderService(IDatabaseContext context)
        {
            _context = context;
        }
        public List<MyOrderDto> GetMyOrder(string userId)
        {
            var orders = _context.Orders.Include(o => o.OrderItems)
                .Where(o => o.UserId == userId).OrderByDescending(o => o.Id)
                .Select(o => new MyOrderDto
                {
                    Id = o.Id,
                    Date = _context.Entry(o).Property("InsertTime").CurrentValue.ToString(),
                    OrderStatus = o.OrderStatus,
                    PaymentStatus = o.PaymentStatus,
                    Price = o.TotalPrice()
                }).ToList();

            return orders;
        }
    }

    public class MyOrderDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
