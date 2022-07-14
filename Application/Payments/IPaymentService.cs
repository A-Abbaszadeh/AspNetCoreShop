using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payments
{
    public interface IPaymentService
    {
        PaymentOfOrderDto PayForOrder(int orderId);
        PaymentDto GetPayment(Guid id);
        bool VerifyPayment(Guid id, string Authority, long RefId);
    }
}
