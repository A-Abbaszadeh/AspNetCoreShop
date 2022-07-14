using Domain.Orders;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders
{
    public interface IOrderService
    {
        int CreateOrder(int basketId, int userAddressId, PaymentMethod paymentMethod);
    }
}
