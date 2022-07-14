namespace Application.Catalogs.CatalogItems.GetCatalogItemPLP
{
    public class CatalogPLPDto
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public byte Rate { get; set; }
        public int AvailableStock { get; set; }
    }
}
