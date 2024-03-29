﻿using System.ComponentModel.DataAnnotations;

namespace Admin.EndPoint.ViewModels.Catalogs
{
    public class CatalogTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر 100 کاراکتر باید باشد")]
        public string Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }
    }
}
