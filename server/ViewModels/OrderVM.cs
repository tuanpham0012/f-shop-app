﻿using ShopAppApi.Data;

namespace ShopAppApi.ViewModels;

public partial class OrderVM
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public string OrderDate { get; set; } = string.Empty;

    public string? DeliveryDate { get; set; } = string.Empty;

    public long CustomerId { get; set; }

    public long? UserId { get; set; }

    public long PaymentMethodId { get; set; }

    public long ShippingUnitId { get; set; }

    public double TotalAmount { get; set; }

    public double DiscountAmount { get; set; }

    public double ShippingFee { get; set; }

    public string? ShippingAddress { get; set; }

    public string? ShippingPhone { get; set; }

    public string? ReceiverName { get; set; }

    public byte Status { get; set; }

    public double? TaxFee { get; set; }

    public double? TotalPrice { get; set; }

    public string? Note { get; set; }

    public virtual CustomerVM Customer { get; set; } = null!;

    public virtual ICollection<OrderDetailVM> OrderDetails { get; set; } = [];

    public virtual PaymentMethodVM PaymentMethod { get; set; } = null!;

    public virtual ShippingUnitVM ShippingUnit { get; set; } = null!;

    public virtual User? User { get; set; }

    public string TextStatus { get; set; } = string.Empty;

    public int ProductCount { get; set; }
    public List<OrderStatus> OrderStatusesChange { get; set; } = new List<OrderStatus>();
    public bool ShowChangeStatus { get; set; } = false;
}

public partial class OrderStatus
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
