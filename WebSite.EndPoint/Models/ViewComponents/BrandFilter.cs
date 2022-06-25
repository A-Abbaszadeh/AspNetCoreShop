using Application.Catalogs.CatalogItems.CatalogItemServices;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Models.ViewComponents
{
    public class BrandFilter : ViewComponent
    {
        private readonly ICatalogItemService _catalogItemService;

        public BrandFilter(ICatalogItemService catalogItemService)
        {
            _catalogItemService = catalogItemService;
        }

        public IViewComponentResult Invoke()
        {
            var brands = _catalogItemService.GetCatalogBrand();
            return View(viewName: "BrandFilter", model: brands);
        }
    }
}
