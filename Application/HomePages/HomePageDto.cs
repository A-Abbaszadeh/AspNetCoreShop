using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using System.Collections.Generic;

namespace Application.HomePages
{
    public class HomePageDto
    {
        public List<BannerDto> Banners { get; set; }
        public List<CatalogPLPDto> MostPopular { get; set; }
        public List<CatalogPLPDto> BestSellers { get; set; }
    }
}
