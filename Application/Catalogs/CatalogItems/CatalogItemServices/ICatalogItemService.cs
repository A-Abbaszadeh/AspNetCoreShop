using Application.Dtos;
using System.Collections.Generic;

namespace Application.Catalogs.CatalogItems.CatalogItemServices
{
    public interface ICatalogItemService
    {
        List<CatalogBrandDto> GetCatalogBrand();
        List<ListCatalogTypeDto> GetCatalogType();
        PaginatedItemDto<CatalogItemListDto> GetCatalogList(int page, int pageSize);
        void AddToMyFavorite(string userId, int catalogItemId);
        PaginatedItemDto<FavoriteCatalogItemDto> GetMyFavorite(string userId, int page = 1, int pageSize = 20);
    }
}
