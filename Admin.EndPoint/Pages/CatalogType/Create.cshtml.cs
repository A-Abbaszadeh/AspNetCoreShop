using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class CreateModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly IMapper _mapper;
        public CreateModel(ICatalogTypeService catalogTypeService, IMapper mapper)
        {
            _catalogTypeService = catalogTypeService;
            _mapper = mapper;
        }

        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel { };
        public List<string> Messages { get; set; } = new List<string>();

        public void OnGet(int? parentId)
        {
            CatalogType.ParentCatalogTypeId = parentId;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var model = _mapper.Map<CatalogTypeDto>(CatalogType);
            var result = _catalogTypeService.Add(model);

            if (result.IsSuccess)
            {
                return RedirectToPage("index", new { parentId = model.ParentCatalogTypeId });
            }

            Messages = result.Messages;
            return Page();
        }
    }
}
