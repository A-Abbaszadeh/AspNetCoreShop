using Application.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.Orders.CustormerOrderService
{
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
}
