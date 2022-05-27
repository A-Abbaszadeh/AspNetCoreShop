using Application.Interfaces.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.GetMenuItem
{
    public interface IGetMenuItemService
    {
        List<MenuItemDto> Execute();
    }
    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        public GetMenuItemService(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<MenuItemDto> Execute()
        {
            var catalogType = _context.CatalogTypes.Include(ct => ct.ParentCatalogType).ToList();
            var data = _mapper.Map<List<MenuItemDto>>(catalogType);
            return data;
        }
    }
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<MenuItemDto> SubMenu { get; set; }
    }
}
