using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGetCatalogItemPDPService _getCatalogItemPDPService;
        private readonly IGetCatalogItemPLPService _getCatalogItemPLPService;
        public ProductController(
            IGetCatalogItemPLPService getCatalogItemPLPService,
            IGetCatalogItemPDPService getCatalogItemPِDPService)
        {
            _getCatalogItemPLPService = getCatalogItemPLPService;
            _getCatalogItemPDPService = getCatalogItemPِDPService;
        }

        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            var data = _getCatalogItemPLPService.Execute(page, pageSize);
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var data = _getCatalogItemPDPService.Execute(id);
            return View(data);
        }
    }
}
