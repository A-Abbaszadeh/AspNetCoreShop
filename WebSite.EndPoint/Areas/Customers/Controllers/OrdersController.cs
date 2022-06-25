using Application.Orders.CustormerOrderService;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Areas.Customers.Controllers
{
    [Authorize]
    [Area("Customers")]
    public class OrdersController : Controller
    {
        private readonly ICustormerOrderService _custormerOrderService;
        private readonly UserManager<User> _userManager;

        public OrdersController(ICustormerOrderService custormerOrderService, UserManager<User> userManager)
        {
            _custormerOrderService = custormerOrderService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var data = _custormerOrderService.GetMyOrder(user.Id);
            return View(data);
        }
    }
}
