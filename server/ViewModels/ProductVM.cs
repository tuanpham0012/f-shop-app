using ShopAppApi.Data;

namespace ShopAppApi.ViewModels;
public class ProductVM
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public byte NumberWarning { get; set; }

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

    public virtual BrandVM Brand { get; set; } = null!;

    public virtual CategoryVM Category { get; set; } = null!;

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual ICollection<OptionValue> OptionValues { get; set; } = new List<OptionValue>();

    public virtual ICollection<OptionVM> Options { get; set; } = [];

    public virtual ICollection<ProductComment> ProductComments { get; set; } = new List<ProductComment>();

    public virtual ICollection<ProductImageVM> Images { get; set; } = new List<ProductImageVM>();

    public virtual ICollection<SkuVM> Skus { get; set; } = [];

    public virtual TaxVM Tax { get; set; } = null!;

    public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();
}