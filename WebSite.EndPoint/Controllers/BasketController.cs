using Application.Baskets;
using Application.Orders;
using Application.Payments;
using Application.Users;
using Domain.Orders;
using Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using WebSite.EndPoint.Models.ViewModel.Baskets;
using WebSite.EndPoint.Utilities;

namespace WebSite.EndPoint.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserAddressService _userAddressService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private string UserId = null;

        public BasketController(
            IBasketService basketService, 
            SignInManager<User> signInManager, 
            IUserAddressService userAddressService,
            IOrderService orderService,
            IPaymentService paymentService)
        {
            _basketService = basketService;
            _signInManager = signInManager;
            _userAddressService = userAddressService;
            _orderService = orderService;
            _paymentService = paymentService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var data = GetOrSetBasket();
            return View(data);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(int catalogItemId, int quantity = 1)
        {
            var basket = GetOrSetBasket();
            _basketService.AddItemToBasket(basket.Id, catalogItemId, quantity);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RemoveItemFromBasket(int itemId)
        {
            _basketService.RemoveItemFromBasket(itemId);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SetQuantity(int itemId, int quantity)
        {
            return Json(_basketService.SetQuantities(itemId, quantity));
        }

        public IActionResult ShippingPayment()
        {
            ShippingPaymentViewModel model = new ShippingPaymentViewModel();
            var userId = ClaimUtility.GetUserId(User);
            model.Basket = _basketService.GetBasketForUser(userId);
            model.UserAddresses = _userAddressService.GetAddress(userId);

            return View(model);
        }
        
        [HttpPost]
        public IActionResult ShippingPayment(int Address, PaymentMethod PaymentMethod)
        {
            string userId = ClaimUtility.GetUserId(User);
            var basket = _basketService.GetBasketForUser(userId);
            int orderId = _orderService.CreateOrder(basket.Id, Address, PaymentMethod);
            if (PaymentMethod == PaymentMethod.OnlinePayment)
            {
                // ثیت درخواست
                var payment = _paymentService.PayForOrder(orderId);
                // ارسال به درگاه پرداخت
                return RedirectToAction("Index", "Pay", new { PaymentId = payment.PaymentId });
            }
            else
            {
                // برو به صفحه سفارشات

                return RedirectToAction("Index", "Orders", new { area = "customers" });
            }
        }

        private BasketDto GetOrSetBasket()
        {
            if (_signInManager.IsSignedIn(User))
            {
                UserId = ClaimUtility.GetUserId(User);
                return _basketService.GetOrCreateBasketForUser(UserId);
            }
            else
            {
                SetCookieForBasket();
                return _basketService.GetOrCreateBasketForUser(UserId);
            }
        }
        private void SetCookieForBasket()
        {
            string basketCookieName = "BasketId";
            if (Request.Cookies.ContainsKey(basketCookieName))
            {
                UserId = Request.Cookies[basketCookieName];
            }
            if (UserId is not null) return;

            UserId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                IsEssential = true,
                Expires = DateTime.Today.AddYears(1)
            };
            Response.Cookies.Append(basketCookieName, UserId, cookieOptions);
        }
    }
}
