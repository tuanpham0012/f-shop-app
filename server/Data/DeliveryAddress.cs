using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class DeliveryAddress
{
    public long Id { get; set; }

    public long CustomerId { get; set; }

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public long? ProvinceId { get; set; }

    public long? WardId { get; set; }

    public string? Lat { get; set; }

    public string? Lng { get; set; }

    public bool? Default { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Province? Province { get; set; }

    public virtual Ward? Ward { get; set; }
}
