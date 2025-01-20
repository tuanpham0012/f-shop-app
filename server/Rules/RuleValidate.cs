using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;

namespace ShopAppApi.Rules
{
    public class ModelExists(string tableName) : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success!;
            }
            var context = validationContext.GetService(typeof(ShopAppContext)) as ShopAppContext;
            if (context == null)
            {
                return new ValidationResult("Unable to retrieve ShopAppContext from validation context.");
            }
            // var checkUnique = TableName switch
            // {
            //     "Categories" => context.Categories.Any(a => a.Id == (long)value),
            //     "Suppliers" => context.Suppliers.Any(a => a.Id == (long)value),
            //     "Brands" => context.Brands.Any(a => a.Id == (long)value),
            //     "Taxes" => context.Taxes.Any(a => a.Id == (long)value),
            //     _ => false,
            // };

            string query = $"SELECT id FROM {tableName} WHERE id = @filterValue";
            var checkExists = context.Database.SqlQueryRaw<dynamic>(query, new SqlParameter("@filterValue", value.ToString())).Any();
            if (checkExists)
            {
                return ValidationResult.Success!;
            }
            return new ValidationResult($"{tableName} Id does not exists");
        }
    }

    public class ModelUnique(string tableName, string columnName, string? message) : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success!;
            }

            var httpContextAccessor = validationContext.GetRequiredService<IHttpContextAccessor>();

            var httpContext = httpContextAccessor.HttpContext;
            var getIdFromRouteValue = httpContext?.GetRouteValue("Id");

            var context = validationContext.GetService(typeof(ShopAppContext)) as ShopAppContext;
            if (context == null)
            {
                return new ValidationResult("Unable to retrieve ShopAppContext from validation context.");
            }

            string query = $"SELECT {columnName} FROM {tableName} WHERE {columnName} = @filterValue";
            if(getIdFromRouteValue != null)
            {
                query += $" AND id != {getIdFromRouteValue}";
            }
            var checkUnique = context.Database.SqlQueryRaw<dynamic>(query, new SqlParameter("@filterValue", value.ToString())).Any();

            // var checkUnique = TableName switch
            // {
            //     "OptionValues" => context.OptionValues.Any(a => a.Code == value.ToString()),
            //     "Options" => context.Options.Any(a => a.Code == value.ToString()),
            //     _ => false,
            // };
            if (!checkUnique)
            {
                return ValidationResult.Success!;
            }
            return new ValidationResult( message?.Replace("{value}", value.ToString()) ?? $"{columnName} đã được sử dụng.");
        }
    }
}