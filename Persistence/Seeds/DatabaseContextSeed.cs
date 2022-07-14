using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Persistence.Seeds
{
    public class DatabaseContextSeed
    {
        public static void CatalogSeed(ModelBuilder modelBuilder)
        {
            foreach (var catalogType in GetCatalogTypesList())
            {
                modelBuilder.Entity<CatalogType>().HasData(catalogType);
            }

            foreach (var catalogBrand in GetCatalogBrandsList())
            {
                modelBuilder.Entity<CatalogBrand>().HasData(catalogBrand);
            }
        }

        private static IEnumerable<CatalogType> GetCatalogTypesList()
        {
            return new List<CatalogType>()
            {
                new CatalogType() {  Id = 1,  Type = "کالای دیجیتال" },
                new CatalogType() {  Id = 2,  Type = "لوازم جانبی گوشی" , ParentCatalogTypeId = 1 },
                new CatalogType() {  Id = 3,  Type = "پایه نگهدارنده گوشی" , ParentCatalogTypeId = 2 },
                new CatalogType() {  Id = 4,  Type = "پاور بانک (شارژر همراه)", ParentCatalogTypeId = 2 },
                new CatalogType() {  Id = 5,  Type = "کیف و کاور گوشی", ParentCatalogTypeId = 2 },
            };
        }

        private static IEnumerable<CatalogBrand> GetCatalogBrandsList()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand() { Id = 1, Brand = "سامسونگ" },
                new CatalogBrand() { Id = 2, Brand = "شیائومی " },
                new CatalogBrand() { Id = 3, Brand = "اپل" },
                new CatalogBrand() { Id = 4, Brand = "هوآوی" },
                new CatalogBrand() { Id = 5, Brand = "نوکیا " },
                new CatalogBrand() { Id = 6, Brand = "ال جی" }
            };
        }
    }
}
