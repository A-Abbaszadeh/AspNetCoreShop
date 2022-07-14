using Application.Discounts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Admin.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountApiController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountApiController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        [Route("SearchCatalogItem")]
        public async Task<IActionResult> SearchCatalogItem(string term)
        {
            return Ok(_discountService.GetCatalogItems(term));
        }
    }
}
