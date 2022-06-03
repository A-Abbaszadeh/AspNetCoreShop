using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
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
        PaginatedItemDto<CatalogItemListDto> GetCatalogList(int page, int pageSize);
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

        public PaginatedItemDto<CatalogItemListDto> GetCatalogList(int page, int pageSize)
        {
            int rowCount = 0;
            var data = _context.CatalogItems
                .Include(p => p.CatalogType).Include(p => p.CatalogBrand)
                .ToPaged(page, pageSize, out rowCount).OrderByDescending(p => p.Id)
                .Select(p => new CatalogItemListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Type = p.CatalogType.Type,
                    Brand = p.CatalogBrand.Brand,
                    AvailableStock = p.AvailableStock,
                    RestockThreshold = p.RestockThreshold,
                    MaxStockThreshold = p.MaxStockThreshold
                }).ToList();

            return new PaginatedItemDto<CatalogItemListDto>(page, pageSize, rowCount, data);
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

    public class CatalogItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
    }
}
