using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Dtos;
using Infrastructure.ExternalApi.ImageServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Admin.EndPoint.Pages.CatalogItem
{
    public class CreateModel : PageModel
    {
        private readonly IAddNewCatalogItemService _addNewCatalogItemService;
        private readonly ICatalogItemService _catalogItemService;
        private readonly IImageUploadService _imageUploadService;
        public CreateModel(
            IAddNewCatalogItemService addNewCatalogItemService, 
            ICatalogItemService catalogItemService, 
            IImageUploadService imageUploadService)
        {
            _addNewCatalogItemService = addNewCatalogItemService;
            _catalogItemService = catalogItemService;
            _imageUploadService = imageUploadService;
        }

        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }
        [BindProperty]
        public AddNewCatalogItemDto Data { get; set; }
        public List<IFormFile> Files { get; set; }
        public void OnGet()
        {
            Categories = new SelectList(_catalogItemService.GetCatalogType(), "Id", "Type");
            Brands = new SelectList(_catalogItemService.GetCatalogBrand(), "Id", "Brand");
        }

        public JsonResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new JsonResult(new BaseDto<int>(false, allErrors.Select(p => p.ErrorMessage).ToList(), 0));
            }

            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                Files.Add(file);
            }

            List<AddNewCatalogItemImageDto> images = new List<AddNewCatalogItemImageDto>();

            if (Files.Count > 0)
            {
                // Upload files
                var result = _imageUploadService.Upload(Files);
                foreach (var item in result)
                {
                    images.Add(new AddNewCatalogItemImageDto { Src = item });
                }
            }
            var x = Data.Features;
            Data.Images = images;
            var resultService = _addNewCatalogItemService.Execute(Data);

            return new JsonResult(resultService);
        }
    }
}
