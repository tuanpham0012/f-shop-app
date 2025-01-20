using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Data;

public partial class Tax
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    
    public byte Value { get; set; }

    public bool? NotUse { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
