using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopAppApi.Data;
using ShopAppApi.Rules;
using System.ComponentModel.DataAnnotations;

namespace ShopAppApi.Request
{
    public class CustomerRequest : BaseRequest
    {
        public int? Status { get; set; }
    }

    public class StoreCustomerRequest
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [ModelUnique("customers", "email", "Email {value} đã được sử dụng.")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; } = null!;

        // [StringLength(32, ErrorMessage = "Độ dài {0} từ {2} đến {1} kí tự.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$", ErrorMessage = "Mật khẩu từ 8 đến 15 kí tự bao gồm chữ thường, chữ in hoa, số và kí tự.")]
        public string Password { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public byte Status { get; set; } = 0;
        public byte Gender { get; set; } = 0;
    }

    public class UpdateCustomerRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        [ModelUnique("customers", "email", "Email {value} đã được sử dụng.")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng email")]
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public byte Status { get; set; } = 0;
        public byte Gender { get; set; } = 0;
    }

    public class DeliveryRequest
    {
        public long? CustomerId { get; set; }
        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public string Phone { get; set; } = null!;

        [Required]
        public long ProvinceId { get; set; }
        [Required]
        public long WardId { get; set; }
        public bool? Default { get; set; }
    }
}
