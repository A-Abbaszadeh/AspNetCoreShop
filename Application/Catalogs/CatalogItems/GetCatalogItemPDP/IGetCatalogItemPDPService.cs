using Application.UriComposer;
using Application.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.CatalogItems.GetCatalogItemPDP
{
    public interface IGetCatalogItemPDPService
    {
        CatalogItemPDPDto Execute(int id);
    }

    public class GetCatalogItemPDPService : IGetCatalogItemPDPService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposerService;
        public GetCatalogItemPDPService(IDatabaseContext context, IUriComposerService uriComposerService)
        {
            _context = context;
            _uriComposerService = uriComposerService;
        }

        public CatalogItemPDPDto Execute(int id)
        {
            var catalogItem = _context.CatalogItems
                .Include(p => p.CatalogType)
                .Include(p => p.CatalogBrand)
                .Include(p => p.Discounts)
                .Include(p => p.CatalogItemImages)
                .Include(p => p.CatalogItemFeatures)
                .SingleOrDefault(p => p.Id == id);

            var features = catalogItem.CatalogItemFeatures
                .Select(p => new PDPFeaturesDto
                {
                    Group = p.Group,
                    Key = p.Key,
                    Value = p.Value
                }).ToList().GroupBy(p => p.Group);

            var similarCatalogs = _context.CatalogItems
                .Include(p => p.CatalogItemImages)
                .Where(p => p.CatalogTypeId == catalogItem.CatalogTypeId)
                .Take(10)
                .Select(p => new SimilarCatalogItemDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Image = _uriComposerService.ComposeImageUri(p.CatalogItemImages.FirstOrDefault().Src),
                }).ToList();

            return new CatalogItemPDPDto
            {
                Id = catalogItem.Id,
                Name = catalogItem.Name,
                Type = catalogItem.CatalogType.Type,
                Brand = catalogItem.CatalogBrand.Brand,
                Price = catalogItem.Price,
                OldPrice = catalogItem.OldPrice,
                PercentDiscount = catalogItem.PercentDiscount,
                Description = catalogItem.Description,
                Images = catalogItem.CatalogItemImages.Select(p => _uriComposerService.ComposeImageUri(p.Src)).ToList(),
                Features = features,
                SimilarCatalogs = similarCatalogs
            };
        }
    }

    public class CatalogItemPDPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }

        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public int? PercentDiscount { get; set; }

        public string Description { get; set; }
        public List<string> Images { get; set; }
        public IEnumerable<IGrouping<string, PDPFeaturesDto>> Features { get; set; }
        public List<SimilarCatalogItemDto> SimilarCatalogs { get; set; }
    }

    public class PDPFeaturesDto
    {
        public string Group { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class SimilarCatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
    }
}
