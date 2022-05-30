using FluentValidation;
using System.Collections.Generic;

namespace Application.Catalogs.CatalogItems.AddNewCatalogItem
{
    public class AddNewCatalogItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public List<AddNewCatalogItemFeatureDto> Features { get; set; }
        public List<AddNewCatalogItemImageDto> Images { get; set; }
    }

    public class AddNewCatalogItemDtoValidator:AbstractValidator<AddNewCatalogItemDto>
    {
        public AddNewCatalogItemDtoValidator()
        {
            RuleFor(p => p.Name).NotNull().WithMessage("نام کاتالوگ اجباری است");
            RuleFor(p => p.Name).Length(2, 100);
            RuleFor(p => p.Description).NotNull().WithMessage("توضیحات اجباری است");

        }
    }
}
