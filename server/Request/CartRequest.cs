namespace ShopAppApi.Request
{
    public class AddCartRequest
    {
        public long? CustomerId { get; set; }
        public long SkuId { get; set; }
        public int Quantity { get; set; }

    }
}