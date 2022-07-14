using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.CustormerOrderService
{
    public interface ICustormerOrderService
    {
        List<MyOrderDto> GetMyOrder(string userId);
    }
}
