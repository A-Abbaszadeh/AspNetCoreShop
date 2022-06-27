using Application.Interfaces.Contexts;
using Domain.Banners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Banners
{
    public interface IBannerService
    {
        void AddBanner(BannerDto banner);
        List<BannerDto> GetBanners();
    }

    public class BannerService : IBannerService
    {
        private readonly IDatabaseContext _context;

        public BannerService(IDatabaseContext context)
        {
            _context = context;
        }
        public void AddBanner(BannerDto banner)
        {
            _context.Banners.Add(new Banner
            {
                Name = banner.Name,
                Image = banner.Image,
                Link = banner.Link,
                IsActive = banner.IsActive,
                Priority = banner.Priority,
                Position = banner.Position,
            });

            _context.SaveChanges();
        }

        public List<BannerDto> GetBanners()
        {
            var banners = _context.Banners.Select(b => new BannerDto
            {
                Name = b.Name,
                Image = b.Image,
                Link = b.Link,
                IsActive = b.IsActive,
                Priority = b.Priority,
                Position = b.Position,
            }).ToList();

            return banners;
        }
    }
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
