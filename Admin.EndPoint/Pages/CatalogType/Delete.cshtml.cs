using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Admin.EndPoint.Pages.CatalogType
{
    public class DeleteModel : PageModel
    {
        private readonly ICatalogTypeService _catalogTypeService;
        private readonly IMapper _mapper;
        public DeleteModel(ICatalogTypeService catalogTypeService, IMapper mapper)
        {
            _catalogTypeService = catalogTypeService;
            _mapper = mapper;
        }

        [BindProperty]
        public CatalogTypeViewModel CatalogType { get; set; } = new CatalogTypeViewModel { };
        public List<string> Messages { get; set; } = new List<string>();
        public void OnGet(int id)
        {
            var model = _catalogTypeService.FindById(id);
            if (model.IsSuccess) { CatalogType = _mapper.Map<CatalogTypeViewModel>(model.Data); }

            Messages = model.Messages;
        }

        public IActionResult OnPost()
        {
            var result = _catalogTypeService.Remove(CatalogType.Id);
            Messages = result.Messages;

            if (result.IsSuccess) { return RedirectToPage("index"); }

            return Page();
        }
    }
}
