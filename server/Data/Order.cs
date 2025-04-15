using System;
using System.Collections.Generic;

namespace ShopAppApi.Data;

public partial class Order
{
    public long Id { get; set; }

    public string Code { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public DateOnly? DeliveryDate { get; set; }

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

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual ShippingUnit ShippingUnit { get; set; } = null!;

    public virtual User? User { get; set; }
}
