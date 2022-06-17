using Application.Discounts.AddNewDiscountService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Admin.EndPoint.Pages.Discounts
{
    public class CreateModel : PageModel
    {
        private readonly IAddNewDiscountService _addNewDiscountService;

        public CreateModel(IAddNewDiscountService addNewDiscountService)
        {
            _addNewDiscountService = addNewDiscountService;
        }
        public AddNewDiscountDto Model { get; set; }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            _addNewDiscountService.Execute(Model);
        }
    }
}
