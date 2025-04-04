﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using System.ComponentModel.DataAnnotations;
using ShopAppApi.Rules;

namespace ShopAppApi.Request
{
    public class ProductRequest : BaseRequest
    {
        public long? CategoryId { get; set; } = null!;
        public string? CategoryCode { get; set; } = null!;
        public string? BrandCode { get; set; } = null!;
        public long? BrandId { get; set; } = null!;
        public int? OrderBy { get; set; } = null!;
        public bool? IsNew { get; set; } = null!;
        public bool? IsFeatured { get; set; } = null!;
        public int? MinPrice { get; set; } = null!;
        public int? MaxPrice { get; set; } = null!;

    }

    public partial class StoreProductRequest
    {
        public long? Id { get; set; }
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        [ModelUnique("products", "code", "Mã sản phẩm {value} đã được sử dụng.")]
        public string Code { get; set; } = string.Empty;
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public string? ImageThumb { get; set; }
        [Required(ErrorMessage = "Mã vạch không được để trống")]
        [ModelUnique("products", "barcode", "Mã vạch {value} đã tồn tại.")]
        public string Barcode { get; set; } = string.Empty;
        public string UnitBuy { get; set; } = string.Empty;
        public string UnitSell { get; set; } = string.Empty;
        public bool? HasVariants { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsFeatured { get; set; }
        public short NumberWarning { get; set; }
        [Required(ErrorMessage = "Loại sản phẩm không được để trống")]
        [ModelExists("categories")]
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Thương hiệu sản phẩm không được để trống")]
        [ModelExists("brands")]
        public long BrandId { get; set; }
        [Required]
        [ModelExists("taxes")]
        public long TaxId { get; set; }
        public short ConversionUnit { get; set; }
        public bool? SoldOut { get; set; }
        public virtual ICollection<OptionsRequest> Options { get; set; } = [];
        public virtual ICollection<SkusRequest> Skus { get; set; } = [];
        public virtual ICollection<ProductImageRequest> Images { get; set; } = [];
    }

    public class OptionsRequest
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public long? ProductId { get; set; }
        public string? Name { get; set; }
        public byte Visual { get; set; }
        public byte Order { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsEdited { get; set; } = false;

        public virtual ICollection<OptionValuesRequest> OptionValues { get; set; } = [];
    }

    public class OptionValuesRequest
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public long? ProductId { get; set; }
        public long? OptionId { get; set; }
        public string? Value { get; set; }
        public string? Image { get; set; }
        public string? Label { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsEdited { get; set; } = false;

    }
    public class SkusRequest
    {
        public long? Id { get; set; }
        public long? ProductId { get; set; }
        public string? Barcode { get; set; } = null!;
        public string? ImageCode { get; set; }

        public string? ImagePath { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Name { get; set; } = null!;
        public int Stock { get; set; }
        public virtual ICollection<VariantRequest> Variants { get; set; } = [];
        public bool? IsEdited { get; set; } = false;
    }

    public class VariantRequest
    {
        public string? Code { get; set; }
        public long? Id { get; set; }
        public long? ProductId { get; set; }
        public long? OptionId { get; set; }
        public long? OptionValueId { get; set; }
        public long? SkuId { get; set; }
    }

    public partial class UpdateProductRequest
    {
        public long? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public double Price { get; set; }
        public string? ImageThumb { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string UnitBuy { get; set; } = string.Empty;
        public string UnitSell { get; set; } = string.Empty;
        public bool? HasVariants { get; set; }
        public bool? IsNew { get; set; }
        public bool? IsFeatured { get; set; }
        public byte NumberWarning { get; set; }
        public string Alias { get; set; } = string.Empty;
        [ModelExists("categories")]
        public long? CategoryId { get; set; }
        [ModelExists("brands")]
        public long? BrandId { get; set; }
        [ModelExists("taxes")]
        public long? TaxId { get; set; }
        public virtual ICollection<OptionsRequest> Options { get; set; } = [];
        public virtual ICollection<SkusRequest> Skus { get; set; } = [];
        public virtual ICollection<ProductImageRequest> Images { get; set; } = [];
        public bool? NewSkus { get; set; }
        public short ConversionUnit { get; set; }
        public bool? SoldOut { get; set; }
    }

    public partial class ProductImageRequest
    {
        public long? Id { get; set; }
        public string Code { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public string Extension { get; set; } = null!;
        public byte Type { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public partial class ProductDesRequest
    {
        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Description { get; set; } = string.Empty;
    }

}
