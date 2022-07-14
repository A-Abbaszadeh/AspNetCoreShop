namespace Application.Catalogs.CatalogItems.CatalogItemServices
{
    public class FavoriteCatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public int AvailableStock { get; set; }
        public string Image { get; set; }
    }
}
