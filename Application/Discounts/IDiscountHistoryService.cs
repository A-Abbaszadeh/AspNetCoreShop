using Application.Dtos;
using Application.Interfaces.Contexts;
using Common;
using Domain.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Discounts
{
    public interface IDiscountHistoryService
    {
        void InsertDiscountHistoryUsageHistory(int discountId, int orderId);
        DiscountUsageHistory GetDiscountUsageHistoryById(int discountUsageHistoryId);
        PaginatedItemDto<DiscountUsageHistory> GetAllDiscountUsageHistory
            (int? discountId, string userId, int pageIndex, int pageSize);
    }

    public class DiscountHistoryService : IDiscountHistoryService
    {
        private readonly IDatabaseContext _context;

        public DiscountHistoryService(IDatabaseContext context)
        {
            _context = context;
        }

        public PaginatedItemDto<DiscountUsageHistory> GetAllDiscountUsageHistory(int? discountId, string userId, int pageIndex, int pageSize)
        {
            var query = _context.DiscountUsageHistories.AsQueryable();

            if (discountId.HasValue && discountId.Value > 0)
                query = query.Where(duh => duh.DiscountId == discountId);

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(duh => duh.Order != null && duh.Order.UserId == userId);

            query = query.OrderByDescending(duh => duh.CreatedOn);

            var pageItem = query.PagedResult(pageIndex, pageSize, out int rowCount);
            return new PaginatedItemDto<DiscountUsageHistory>(pageIndex, pageSize, rowCount, query.ToList());
        }

        public DiscountUsageHistory GetDiscountUsageHistoryById(int discountUsageHistoryId)
        {
            if (discountUsageHistoryId == 0) return null;

            var discountUsageHistory = _context.DiscountUsageHistories.Find(discountUsageHistoryId);

            return discountUsageHistory;
        }

        public void InsertDiscountHistoryUsageHistory(int discountId, int orderId)
        {
            var discount = _context.Discounts.Find(discountId);
            var order = _context.Orders.Find(orderId);

            DiscountUsageHistory discountUsageHistory = new DiscountUsageHistory()
            {
                CreatedOn = DateTime.Now,
                Discount = discount,
                Order = order,
            };
            _context.DiscountUsageHistories.Add(discountUsageHistory);
            _context.SaveChanges();
        }
    }
}
