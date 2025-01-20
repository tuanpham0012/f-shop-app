using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class BrandRequest : BaseRequest
    {
        public bool? NotUse { get; set; }

    }
    public class StoreBrandRequest
    {
        [Required]
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; } = null!;

        public string? Image { get; set; }

        public bool? NotUse { get; set; }
    }

    public class UpdateBrandRequest
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Image { get; set; }

        public bool? NotUse { get; set; }
    }
}