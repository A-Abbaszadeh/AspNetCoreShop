using Application.Catalogs.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Models.ViewComponents
{
    public class GetMenuCategories:ViewComponent
    {
        private readonly IGetMenuItemService _getMenuService;
        public GetMenuCategories(IGetMenuItemService getMenuService)
        {
            _getMenuService = getMenuService;
        }
        public IViewComponentResult Invoke()
        {
            var data = _getMenuService.Execute();
            return View(viewName: "GetMenuCategories",model:data);
        }
    }
}
