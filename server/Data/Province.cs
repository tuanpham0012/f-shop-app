using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class Province
{
    public long Id { get; set; }

    public string? CountryCode { get; set; }

    public string? ProvinceCode { get; set; }

    public string? PlaceType { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }

    public string? Lat { get; set; }

    public string? Lng { get; set; }

    public string? Zipcode { get; set; }

    public string? Code { get; set; }

    public string? GhnProvinceId { get; set; }

    public string? GhnProvinceCode { get; set; }

    public string? VtpProvinceId { get; set; }

    public string? VtpProvinceCode { get; set; }

    public string? VnpProvinceId { get; set; }

    public string? VnpProvinceCode { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; } = new List<DeliveryAddress>();

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
