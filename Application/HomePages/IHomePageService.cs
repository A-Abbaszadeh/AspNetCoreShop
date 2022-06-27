using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Interfaces.Contexts;
using Application.UriComposer;
using Domain.Banners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HomePages
{
    public interface IHomePageService
    {
        HomePageDto GetData();
    }
    public class HomePageService : IHomePageService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposerService;
        private readonly IGetCatalogItemPLPService _getCatalogItemPLPService;

        public HomePageService(
            IDatabaseContext context,
            IUriComposerService uriComposerService,
            IGetCatalogItemPLPService getCatalogItemPLPService)
        {
            _context = context;
            _uriComposerService = uriComposerService;
            _getCatalogItemPLPService = getCatalogItemPLPService;
        }
        public HomePageDto GetData()
        {
            var banners = _context.Banners.Where(b => b.IsActive == true)
                .OrderBy(b => b.Priority).ThenByDescending(b => b.Id)
                .Select(b => new BannerDto
                {
                    Id = b.Id,
                    Image = _uriComposerService.ComposeImageUri(b.Image),
                    Link = b.Link,
                    Position = b.Position
                }).ToList();

            var bestSellers = _getCatalogItemPLPService.Execute(new GetCatalogPLPRequestDto
            {
                AvailableStock = true,
                Page = 1,
                PageSize = 20,
                SortType = SortType.BestSelling
            }).Data.ToList();

            var mostPopular = _getCatalogItemPLPService.Execute(new GetCatalogPLPRequestDto
            {
                AvailableStock = true,
                Page = 1,
                PageSize = 20,
                SortType = SortType.MostPopular
            }).Data.ToList();

            return new HomePageDto
            {
                Banners = banners,
                MostPopular = mostPopular,
                BestSellers = bestSellers
            };
        }
    }
    public class HomePageDto
    {
        public List<BannerDto> Banners { get; set; }
        public List<CatalogPLPDto> MostPopular { get; set; }
        public List<CatalogPLPDto> BestSellers { get; set; }
    }

    public class BannerDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public BannerPosition Position { get; set; }
    }
}
