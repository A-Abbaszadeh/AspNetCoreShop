using System.Collections.Generic;

namespace Application.Catalogs.GetMenuItem
{
    public interface IGetMenuItemService
    {
        List<MenuItemDto> Execute();
    }
}
