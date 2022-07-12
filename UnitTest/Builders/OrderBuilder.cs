using Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Builders
{
    public class OrderBuilder
    {
        private Order _order;
        public string TestBuyerId => "58968";
        public int TestCatalogItemId => 234;
        public string TestProductName => "Test Product Name";
        public string TestPictureUri => "https://localhost:44310/Resources/Images/2022/2022%20-%206/e7c21365-8889-4d5d-8000-8834b0d5c78d_4.jpg";
        public int TestUnitPrice = 1000;
        public int TestUnits = 3;
        public OrderBuilder()
        {
            _order = CreateOrderWithDefaultValue();
        }
        private Order CreateOrderWithDefaultValue()
        {
            List<OrderItem> testOrderItems = new List<OrderItem>()
            {
                new OrderItem(TestCatalogItemId, TestProductName, TestPictureUri, TestUnitPrice, TestUnits)
            };
            _order = new Order(TestBuyerId, new AddressBuilder().Build(), PaymentMethod.OnlinePayment, testOrderItems, null);
            return _order;
        }
        public Order Build()
        {
            return _order;
        }
    }
}
