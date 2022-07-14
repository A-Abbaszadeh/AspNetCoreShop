using Domain.Banners;
using System.ComponentModel.DataAnnotations;

namespace Application.Banners
{
    public class BannerDto
    {
        [Display(Name = "نام بنر")]
        public string Name { get; set; }
        [Display(Name = "تصویر بنر")]
        public string Image { get; set; }
        [Display(Name = "لینک")]
        public string Link { get; set; }
        [Display(Name = "فعال بودن")]
        public bool IsActive { get; set; }
        [Display(Name = "ترتیب نمایش")]
        public int Priority { get; set; }
        [Display(Name = "موقعیت نمایش")]
        public BannerPosition Position { get; set; }
    }
}
