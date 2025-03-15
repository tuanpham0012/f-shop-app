using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class Product
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public short NumberWarning { get; set; }

    public string? ImageThumb { get; set; }

    public string UnitSell { get; set; } = null!;

    public string UnitBuy { get; set; } = null!;

    public string? Description { get; set; }

    public string Alias { get; set; } = null!;

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }

    public bool? HasVariants { get; set; }

    public bool? IsNew { get; set; }

    public bool? IsFeatured { get; set; }

    public bool? IsSale { get; set; }

    public long BrandId { get; set; }

    public long CategoryId { get; set; }

    public long TaxId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<OptionValue> OptionValues { get; set; } = new List<OptionValue>();

    public virtual ICollection<Option> Options { get; set; } = new List<Option>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductComment> ProductComments { get; set; } = new List<ProductComment>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductStatistic> ProductStatistics { get; set; } = new List<ProductStatistic>();

    public virtual ICollection<Sku> Skus { get; set; } = new List<Sku>();

    public virtual Tax Tax { get; set; } = null!;

    public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();
}
