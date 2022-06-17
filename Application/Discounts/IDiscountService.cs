using Application.Interfaces.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Discounts
{
    public interface IDiscountService
    {
        List<CatalogItemDto> GetCatalogItems(string searchKey);
    }

    public class DiscountService : IDiscountService
    {
        private readonly IDatabaseContext _context;

        public DiscountService(IDatabaseContext context)
        {
            _context = context;
        }
        public List<CatalogItemDto> GetCatalogItems(string searchKey)
        {
            if (!String.IsNullOrEmpty(searchKey))
            {
                var data = _context.CatalogItems
                    .Where(ci => ci.Name.Contains(searchKey))
                    .Select(ci => new CatalogItemDto
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                    }).ToList();

                return data;
            }
            else
            {
                var data = _context.CatalogItems
                    .OrderByDescending(ci => ci.Id)
                    .Take(10)
                    .Select(ci => new CatalogItemDto
                    {
                        Id = ci.Id,
                        Name = ci.Name,
                    }).ToList();
                return data;
            }
        }
    }

    public class CatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
