using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;

namespace Admin.EndPoint.MappingProfiles
{
    public class CatalogVMMappingProgile : Profile
    {
        public CatalogVMMappingProgile()
        {
            CreateMap<CatalogTypeDto, CatalogTypeViewModel>().ReverseMap();
        }
    }
}
