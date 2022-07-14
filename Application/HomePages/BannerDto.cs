using Domain.Banners;

namespace Application.HomePages
{
    public class BannerDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public BannerPosition Position { get; set; }
    }
}
