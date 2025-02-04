using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class TaxRequest : BaseRequest
    {
        public Boolean? NotUse { get; set; } = null!;
    }
    public class StoreTaxRequest
    {
        [Required(ErrorMessage = "Name không được để trống")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Giá trị không được để trống")]
        public byte Value { get; set; }
        public bool? NotUse { get; set; }
    }

    public class UpdateTaxRequest
    {
        public string Name { get; set; } = null!;
        public byte Value { get; set; }
        public bool? NotUse { get; set; }
    }
}