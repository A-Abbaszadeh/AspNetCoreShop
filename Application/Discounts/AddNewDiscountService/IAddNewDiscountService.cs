using System.Text;
using System.Threading.Tasks;

namespace Application.Discounts.AddNewDiscountService
{
    public interface IAddNewDiscountService
    {
        void Execute(AddNewDiscountDto discount);
    }
}
