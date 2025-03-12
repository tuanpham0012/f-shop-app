namespace ShopAppApi.ViewModels
{
    public partial class TaxVM
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public byte Value { get; set; }

        public bool? NotUse { get; set; }
    }
}