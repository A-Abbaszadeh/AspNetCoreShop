using Application.Interfaces.Contexts;
using Domain.Banners;
using System.Collections.Generic;
using System.Linq;

namespace Application.Banners
{
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
}
