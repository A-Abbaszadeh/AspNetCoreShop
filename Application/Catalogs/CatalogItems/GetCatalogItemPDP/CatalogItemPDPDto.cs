﻿using System.Collections.Generic;
using System.Linq;

namespace Application.Catalogs.CatalogItems.GetCatalogItemPDP
{
    public class CatalogItemPDPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }

        public int Price { get; set; }
        public int? OldPrice { get; set; }
        public int? PercentDiscount { get; set; }

        public string Description { get; set; }
        public List<string> Images { get; set; }
        public IEnumerable<IGrouping<string, PDPFeaturesDto>> Features { get; set; }
        public List<SimilarCatalogItemDto> SimilarCatalogs { get; set; }
    }
}
