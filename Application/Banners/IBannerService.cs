using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Banners
{
    public interface IBannerService
    {
        void AddBanner(BannerDto banner);
        List<BannerDto> GetBanners();
    }
}
