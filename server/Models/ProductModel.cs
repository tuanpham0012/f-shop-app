using System;
using System.Collections.Generic;
using ShopAppApi.Data;

namespace ShopAppApi.Models;

public partial class ProductVM
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public int Stock { get; set; }

    public List<string> Images { get; set; } = [];

    public string? Unit { get; set; }

    public string? Barcode { get; set; }

    public string? Description { get; set; }

    public string? Alias { get; set; }

    public bool? CanEdit { get; set; }

    public bool? CanDelete { get; set; }

    public long? SupplierId { get; set; }

    public long? CategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = [];

    public virtual Category? Category { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = [];

    public virtual ICollection<OptionValue> OptionValues { get; set; } = [];

    public virtual ICollection<Option> Options { get; set; } = [];

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = [];

    public virtual ICollection<ProductComment> ProductComments { get; set; } = [];

    public virtual ICollection<ProductStatistic> ProductStatistics { get; set; } = [];

    public virtual ICollection<Sku> Skus { get; set; } = [];

    public virtual Supplier? Supplier { get; set; }

    public virtual ICollection<Variant> Variants { get; set; } = [];
}
