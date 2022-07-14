using Application.Dtos;
using Application.Interfaces.Contexts;
using Application.UriComposer;
using AutoMapper;
using Common;
using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.Catalogs.CatalogItems.CatalogItemServices
{
    public class CatalogItemService : ICatalogItemService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUriComposerService _uriComposerService;

        public CatalogItemService(IDatabaseContext context, IMapper mapper, IUriComposerService uriComposerService)
        {
            _context = context;
            _mapper = mapper;
            _uriComposerService = uriComposerService;
        }

        public void AddToMyFavorite(string userId, int catalogItemId)
        {
            var catalogItem = _context.CatalogItems.Find(catalogItemId);
            CatalogItemFavorite catalogItemFavorite = new CatalogItemFavorite()
            {
                CatalogItem = catalogItem,
                UserId = userId,
            };
            _context.CatalogItemFavorites.Add(catalogItemFavorite);
            _context.SaveChanges();
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

        public PaginatedItemDto<FavoriteCatalogItemDto> GetMyFavorite(string userId, int page = 1, int pageSize = 20)
        {
            var model = _context.CatalogItems
                .Include(ci => ci.CatalogItemFavorites).Include(ci => ci.Discounts).Include(ci => ci.CatalogItemImages)
                .Where(ci => ci.CatalogItemFavorites.Any(f => f.UserId == userId))
                .OrderBy(ci => ci.Id).AsQueryable();

            int rowCount = 0;
            var data = model.PagedResult(page, pageSize, out rowCount).ToList()
                .Select(f => new FavoriteCatalogItemDto
                {
                    Id = f.Id,
                    Name = f.Name,
                    Price = f.Price,
                    Rate = 4,
                    AvailableStock = f.AvailableStock,
                    Image = _uriComposerService.ComposeImageUri(f?.CatalogItemImages?.FirstOrDefault()?.Src ?? "")
                }).ToList();

            return new PaginatedItemDto<FavoriteCatalogItemDto>(page, pageSize, rowCount, data);
        }
    }
}
