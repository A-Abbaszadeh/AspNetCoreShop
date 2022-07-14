using Application.UriComposer;
using Application.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Catalogs.CatalogItems.GetCatalogItemPDP
{
    public class GetCatalogItemPDPService : IGetCatalogItemPDPService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposerService;
        public GetCatalogItemPDPService(IDatabaseContext context, IUriComposerService uriComposerService)
        {
            _context = context;
            _uriComposerService = uriComposerService;
        }

        public CatalogItemPDPDto Execute(string slug)
        {
            var catalogItem = _context.CatalogItems
                .Include(p => p.CatalogType)
                .Include(p => p.CatalogBrand)
                .Include(p => p.Discounts)
                .Include(p => p.CatalogItemImages)
                .Include(p => p.CatalogItemFeatures)
                .SingleOrDefault(p => p.Slug == slug);

            catalogItem.VisitCount++;
            _context.SaveChanges();

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
}
