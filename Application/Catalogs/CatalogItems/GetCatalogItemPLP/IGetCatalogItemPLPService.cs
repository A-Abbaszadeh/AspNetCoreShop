using Application.Catalogs.CatalogItems.UriComposer;
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
        PaginatedItemDto<CatalogPLPDto> Execute(int page, int pageSize);
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

        public PaginatedItemDto<CatalogPLPDto> Execute(int page, int pageSize)
        {
            
            int rowCount = 0;
            var data = _context.CatalogItems.Include(p => p.CatalogItemImages)
                .OrderByDescending(p => p.Id).PagedResult(page, pageSize, out rowCount)
                .Select(p => new CatalogPLPDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Rate = 4,
                    Image = _uriComposerService.ComposeImageUri(p.CatalogItemImages.FirstOrDefault().Src)
                }).ToList();

            return new PaginatedItemDto<CatalogPLPDto>(page, pageSize, rowCount, data);
        }
    }
    public class CatalogPLPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public byte Rate { get; set; }
    }
}
