using System.Collections.Generic;

namespace Application.Banners
{
    public interface IBannerService
    {
        void AddBanner(BannerDto banner);
        List<BannerDto> GetBanners();
    }
}
