using Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Builders;
using Xunit;

namespace UnitTest.Core.Domain.OrderTest
{
    public class OrderStatusTest
    {
        [Fact]
        public void When_order_is_delivered_OrderStatus_changes_to_Delivered()
        {
            var builder = new OrderBuilder();
            var order = builder.Build();
            order.OrderDeliverd();
            Assert.Equal(OrderStatus.Delivered, order.OrderStatus);
        }
    }
}
