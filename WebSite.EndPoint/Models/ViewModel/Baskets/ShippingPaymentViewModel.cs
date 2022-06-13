using Application.Baskets;
using Application.Users;
using System.Collections.Generic;

namespace WebSite.EndPoint.Models.ViewModel.Baskets
{
    public class ShippingPaymentViewModel
    {
        public BasketDto Basket { get; set; }
        public List<UserAddressDto> UserAddresses { get; set; }
    }
}
