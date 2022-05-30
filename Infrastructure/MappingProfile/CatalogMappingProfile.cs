using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Catalogs.CatalogTypes;
using Application.Catalogs.GetMenuItem;
using AutoMapper;
using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MappingProfile
{
    public class CatalogMappingProfile:Profile
    {
        public CatalogMappingProfile()
        {
            #region CatalogTypeMappingProfile
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();

            CreateMap<CatalogType, CatalogTypeListDto>()
                .ForMember(
                dest => dest.SubTypeCount,
                option => option.MapFrom(src => src.SubType.Count)
                );

            CreateMap<CatalogType, MenuItemDto>()
                .ForMember(
                dest => dest.Name,
                option => option.MapFrom(src => src.Type))
                .ForMember(
                dest => dest.ParentId,
                option => option.MapFrom(src => src.ParentCatalogTypeId))
                .ForMember(
                dest => dest.SubMenu,
                option => option.MapFrom(src => src.SubType));
            #endregion

            #region CatalogItemMappingProfile
            CreateMap<CatalogItemFeature, AddNewCatalogItemFeatureDto>().ReverseMap();
            CreateMap<CatalogItemImage, AddNewCatalogItemImageDto>().ReverseMap();
            CreateMap<CatalogItem, AddNewCatalogItemDto>()
                .ForMember(
                dest => dest.Features,
                option => option.MapFrom(src => src.CatalogItemFeatures))
                .ForMember(
                dest => dest.Images,
                option => option.MapFrom(src => src.CatalogItemImages)).ReverseMap();
            #endregion

            CreateMap<CatalogBrand, CatalogBrandDto>().ReverseMap();


        }
    }
}
