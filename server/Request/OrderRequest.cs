using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class OrderRequest : BaseRequest
    {
        public long? CustomerId { get; set; } = null;
        public long? UserId { get; set; } = null;
        public long? PaymentMethodId { get; set; } = null;
        public long? ShippingUnitId { get; set; } = null;
        public int? Status { get; set; } = null;
    }
}