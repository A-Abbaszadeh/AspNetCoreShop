using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
using Domain.Catalogs;
using System.Collections.Generic;
using System.Linq;

namespace Application.Catalogs.CatalogTypes
{
    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        public CatalogTypeService(IDatabaseContext databaseContext, IMapper mapper)
        {
            _context = databaseContext;
            _mapper = mapper;
        }

        public BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalogType)
        {
            var model = _mapper.Map<CatalogType>(catalogType);

            _context.CatalogTypes.Add(model);
            _context.SaveChanges();

            return new BaseDto<CatalogTypeDto>(
                true,
                new List<string> { $"نوع {model.Type} با موفقیت ثبت گردید." },
                _mapper.Map<CatalogTypeDto>(model)
                );
        }

        public BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalogType)
        {
            var model = _context.CatalogTypes.SingleOrDefault(ct => ct.Id == catalogType.Id);

            _mapper.Map(catalogType, model);
            _context.SaveChanges();

            return new BaseDto<CatalogTypeDto>(
                true,
                new List<string> { $"نوع {model.Type} با موفقیت ویرایش شد." },
                _mapper.Map<CatalogTypeDto>(model)
                );
        }

        public BaseDto<CatalogTypeDto> FindById(int id)
        {
            var data = _context.CatalogTypes.Find(id);
            var result = _mapper.Map<CatalogTypeDto>(data);
            return new BaseDto<CatalogTypeDto>(true, null, result);
        }

        public PaginatedItemDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize)
        {
            int totalCount = 0;
            var model = _context.CatalogTypes
                .Where(ct => ct.ParentCatalogTypeId == parentId)
                .PagedResult(page,pageSize, out totalCount);

            var result = _mapper.ProjectTo<CatalogTypeListDto>(model).ToList();

            return new PaginatedItemDto<CatalogTypeListDto>(page, pageSize, totalCount, result);
        }

        public BaseDto Remove(int Id)
        {
            var catalogType = _context.CatalogTypes.Find(Id);

            _context.CatalogTypes.Remove(catalogType);
            _context.SaveChanges();

            return new BaseDto(true, new List<string> { "آیتم مورد نظر با موفقیت حذف شد" });
        }
    }
}
