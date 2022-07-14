using Application.Interfaces.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Application.Catalogs.GetMenuItem
{
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
}
