using Application.Interfaces.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.CatalogItems.CatalogItemServices
{
    public interface ICatalogItemService
    {
        List<CatalogBrandDto> GetCatalogBrand();
        List<ListCatalogTypeDto> GetCatalogType();
    }

    public class CatalogItemService : ICatalogItemService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        public CatalogItemService(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CatalogBrandDto> GetCatalogBrand()
        {
            var brands = _context.CatalogBrands.OrderBy(cb => cb.Brand).Take(500).ToList();
            var data = _mapper.Map<List<CatalogBrandDto>>(brands);
            return data;
        }

        public List<ListCatalogTypeDto> GetCatalogType()
        {
            var types = _context.CatalogTypes
                .Include(ct => ct.ParentCatalogType)
                .Include(ct => ct.ParentCatalogType)
                .ThenInclude(ct => ct.ParentCatalogType.ParentCatalogType)
                .Include(ct => ct.SubType)
                .Where(ct => ct.ParentCatalogTypeId != null)
                .Where(ct => ct.SubType.Count() == 0)
                .Select(ct => new { ct.Id, ct.Type, ct.ParentCatalogType, ct.SubType })
                .ToList()
                .Select(ct => new ListCatalogTypeDto
                {
                    Id = ct.Id,
                    Type = $"{ct?.Type ?? ""} - {ct?.ParentCatalogType?.Type ?? ""} - {ct?.ParentCatalogType?.ParentCatalogType?.Type ?? ""}"
                }).ToList();

            return types;
        }
    }

    public class CatalogBrandDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
    public class ListCatalogTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
