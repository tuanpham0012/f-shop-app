using System;
using System.Collections.Generic;

namespace ShopAppApi.ViewModels;

public partial class DeliveryAddressVM
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

    public string? DetailAddress { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
