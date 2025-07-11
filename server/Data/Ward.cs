using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class Ward
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public string WardCode { get; set; } = null!;

    public long? ProvinceId { get; set; }

    public string? GhnWardId { get; set; }

    public string? GhnWardCode { get; set; }

    public string? VtpWardId { get; set; }

    public string? VtpWardCode { get; set; }

    public string? VnpWardId { get; set; }

    public string? VnpWardCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; } = new List<DeliveryAddress>();

    public virtual Province? Province { get; set; }
}
