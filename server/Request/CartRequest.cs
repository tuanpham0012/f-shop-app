namespace ShopAppApi.Request
{
    public class AddCartRequest
    {
        public long? CustomerId { get; set; }
        public long SkuId { get; set; }
        public int Quantity { get; set; }

    }
    public class UpdateCartRequest
    {
        public long? CustomerId { get; set; }
        public long? SkuId { get; set; }
        public int Quantity { get; set; }
        public int Type { get; set; } // 0:cập nhật theo số lượng 1: plus, 2: minus
    }
}