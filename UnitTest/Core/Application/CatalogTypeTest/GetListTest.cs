using Application.Catalogs.CatalogTypes;
using AutoMapper;
using Infrastructure.MappingProfile;
using UnitTest.Builders;
using Xunit;

namespace UnitTest.Core.Application.CatalogTypeTest
{
    public class GetListTest
    {
        [Fact]
        public void List_should_return_list_of_categorytypes()
        {
            // Arrange
            var databaseBulder = new DatabaseBuilder();
            var dbContext = databaseBulder.GetDbContext();

            var mockMapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new CatalogMappingProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var service = new CatalogTypeService(dbContext, mapper);

            service.Add(new CatalogTypeDto { Id = 1, Type = "T1" });
            service.Add(new CatalogTypeDto { Id = 2, Type = "T2" });

            var result = service.GetList(null, 1, 20);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
