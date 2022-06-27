using Application.UriComposer;
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

namespace Application.Catalogs.CatalogItems.GetCatalogItemPLP
{
    public interface IGetCatalogItemPLPService
    {
        PaginatedItemDto<CatalogPLPDto> Execute(GetCatalogPLPRequestDto request);
    }
    public class GetCatalogItemPLPService : IGetCatalogItemPLPService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposerService;
        public GetCatalogItemPLPService(IDatabaseContext context, IUriComposerService uriComposerService)
        {
            _context = context;
            _uriComposerService = uriComposerService;
        }

        public PaginatedItemDto<CatalogPLPDto> Execute(GetCatalogPLPRequestDto request)
        {
            
            int rowCount = 0;
            var query = _context.CatalogItems
                .Include(ci => ci.Discounts).Include(ci => ci.CatalogItemImages)
                .OrderByDescending(ci => ci.Id).AsQueryable();

            if (request.BrandId is not null)
            {
                query = query.Where(ci => request.BrandId.Any(b => b == ci.CatalogBrandId));
            }

            if (request.CatalogTypeId is not null)
            {
                query = query.Where(ci => ci.CatalogTypeId == request.CatalogTypeId);
            }

            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(ci => ci.Name.Contains(request.SearchKey) || ci.Description.Contains(request.SearchKey));
            }

            if (request.AvailableStock)
            {
                query = query.Where(ci => ci.AvailableStock > 0);
            }

            switch (request.SortType)
            {
                case SortType.None:
                    break;
                case SortType.MostVisited:
                    query = query.OrderByDescending(ci => ci.VisitCount);
                    break;
                case SortType.BestSelling:
                    query = query.Include(ci => ci.OrderItems).OrderByDescending(ci => ci.OrderItems.Count());
                    break;
                case SortType.MostPopular:
                    query = query.Include(ci => ci.CatalogItemFavorites).OrderByDescending(ci => ci.CatalogItemFavorites.Count());
                    break;
                case SortType.Newest:
                    query = query.OrderByDescending(ci => ci.Id);
                    break;
                case SortType.Cheapest:
                    query = query.OrderBy(ci => ci.Price);
                    break;
                case SortType.MostExpensive:
                    query = query.OrderByDescending(ci => ci.Price);
                    break;
                default:
                    break;
            }

            var data = query.PagedResult(request.Page, request.PageSize, out rowCount).ToList()
                .Select(ci => new CatalogPLPDto
                {
                    Id = ci.Id,
                    Name = ci.Name,
                    Price = ci.Price,
                    Rate = 4,
                    AvailableStock = ci.AvailableStock,
                    Image = _uriComposerService.ComposeImageUri(ci.CatalogItemImages.FirstOrDefault().Src)
                }).ToList();

            return new PaginatedItemDto<CatalogPLPDto>(request.Page, request.PageSize, rowCount, data);
        }
    }

    public class GetCatalogPLPRequestDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? CatalogTypeId { get; set; }
        public int[] BrandId { get; set; }
        public bool AvailableStock { get; set; }
        public string SearchKey { get; set; }
        public SortType SortType { get; set; }
    }

    public enum SortType
    {
        /// <summary>
        /// بدون مرتب سازی
        /// </summary>
        None = 0,
        /// <summary>
        /// پربازدیدترین
        /// </summary>
        MostVisited = 1,
        /// <summary>
        /// پرفروشترین
        /// </summary>
        BestSelling = 2,
        /// <summary>
        /// محبوبترین
        /// </summary>
        MostPopular = 3,
        /// <summary>
        /// جدیدترین
        /// </summary>
        Newest = 4,
        /// <summary>
        /// ارزانترین
        /// </summary>
        Cheapest = 5,
        /// <summary>
        /// گرانترین
        /// </summary>
        MostExpensive = 6,
    }
    public class CatalogPLPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public byte Rate { get; set; }
        public int AvailableStock { get; set; }
    }
}
