using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.CatalogItem
{
    public class IndexModel : PageModel
    {
        private readonly ICatalogItemService _catalogItemService;
        public IndexModel(ICatalogItemService catalogItemService)
        {
            _catalogItemService = catalogItemService;
        }
        public PaginatedItemDto<CatalogItemListDto> CatalogItems { get; set; }
        public void OnGet(int page = 1, int pageSize = 100)
        {
            CatalogItems = _catalogItemService.GetCatalogList(page, pageSize);
        }
    }
}
