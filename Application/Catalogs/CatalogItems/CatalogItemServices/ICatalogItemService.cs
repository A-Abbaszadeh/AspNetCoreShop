using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
