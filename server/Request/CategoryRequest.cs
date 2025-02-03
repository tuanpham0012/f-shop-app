using System.ComponentModel.DataAnnotations;
using ShopAppApi.Rules;

namespace ShopAppApi.Request
{
    public class CategoryRequest : BaseRequest
    {
        public bool? NotUse { get; set; }

    }
    public class StoreCategoryRequest
    {
        [Required]
        [ModelUnique("categories", "code", "Mã đã tồn tại.")]
        public string Code { get; set; } = null!;
        [Required(ErrorMessage = "Tên không được để trống")]
        public string Name { get; set; } = null!;
        [ModelExists("categories")]
        public int? ParentId { get; set; }

        public bool? NotUse { get; set; }
    }

    public class UpdateCategoryRequest
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        [ModelExists("categories")]
        public int? ParentId { get; set; }

        public bool? NotUse { get; set; }
    }
}