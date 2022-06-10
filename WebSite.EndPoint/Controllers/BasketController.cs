﻿using Application.Baskets;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using WebSite.EndPoint.Utilities;

namespace WebSite.EndPoint.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly SignInManager<User> _signInManager;
        private string UserId = null;

        public BasketController(IBasketService basketService, SignInManager<User> signInManager)
        {
            _basketService = basketService;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var data = GetOrSetBasket();
            return View(data);
        }
        [HttpPost]
        public IActionResult Index(int catalogItemId, int quantity = 1)
        {
            var basket = GetOrSetBasket();
            _basketService.AddItemToBasket(basket.Id, catalogItemId, quantity);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveItemFromBasket(int itemId)
        {
            _basketService.RemoveItemFromBasket(itemId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult SetQuantity(int itemId, int quantity)
        {
            return Json(_basketService.SetQuantities(itemId, quantity));
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
