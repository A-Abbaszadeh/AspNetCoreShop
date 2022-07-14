using System;
using System.Text;
using System.Threading.Tasks;

namespace Application.HomePages
{
    public interface IHomePageService
    {
        HomePageDto GetData();
    }
}
