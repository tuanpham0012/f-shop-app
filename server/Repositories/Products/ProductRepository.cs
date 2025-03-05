using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Models;
using ShopAppApi.Request;
using ShopAppApi.Response;
using Slugify;
using System.Collections;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace ShopAppApi.Repositories.Products
{
    public class ProductRepository(ShopAppContext context) : IProductRepository
    {
        private readonly ShopAppContext _context = context;
        public async Task Create(StoreProductRequest product)
        {
            using var transaction = _context.Database.BeginTransaction();

            foreach (var item in product.Images.Select((value, i) => (value, i)))
            {
                if (item.value != null)
                {
                    product.Images[item.i] = await FileHelper.SaveFile(item.value);
                }
            }

            var entry = new Product
            {
                Code = product.Code,
                Barcode = product.Barcode,
                Name = product.Name,
                Price = product.Price,
                UnitBuy = product.UnitBuy,
                UnitSell = product.UnitSell,
                Alias = new SlugHelper().GenerateSlug(product.Name),
                Description = product.Description,
                CategoryId = product.CategoryId ?? throw new ArgumentNullException(nameof(product.CategoryId)),
                BrandId = product.BrandId ?? throw new ArgumentNullException(nameof(product.BrandId)),
                TaxId = product.TaxId ?? throw new ArgumentNullException(nameof(product.TaxId)),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Images = JsonSerializer.Serialize(product.Images),
                IsNew = product.IsNew,
                NumberWarning = product.NumberWarning,
                HasVariants = product.HasVariants,
                IsFeatured = product.IsFeatured,
            };
            _context.Add(entry);
            await _context.SaveChangesAsync();
            double minPrice = Double.MaxValue;
            foreach (var option in product.Options)
            {
                option.ProductId = entry.Id;
                var _option = await this.CreateOrUpdateOption(option);

                foreach (var value in option.OptionValues)
                {
                    value.OptionId = _option.Id;
                    value.ProductId = entry.Id;
                    var _value = await this.CreateOrUpdateOptionValue(value);
                }
            }

            if (product.Skus.Count() > 0)
            {
                foreach (var sku in product.Skus)
                {
                    sku.ProductId = entry.Id;
                    var _sku = await this.CreateOrUpdateSku(sku);

                    minPrice = sku.Price < minPrice ? sku.Price : minPrice;

                    foreach (var variant in sku.Variants)
                    {
                        var optionValue = _context.OptionValues.AsNoTracking().FirstOrDefault(x => x.Code == variant.Code);
                        if (optionValue != null)
                        {
                            variant.ProductId = entry.Id;
                            variant.OptionValueId = optionValue.Id;
                            variant.OptionId = optionValue.OptionId;
                            variant.SkuId = _sku.Id;
                            var _variant = await this.CreateOrUpdateVariant(variant);
                        }
                    }
                }
            }
            else
            {
                var sku = new SkusRequest
                {
                    ProductId = entry.Id,
                    Name = entry.Name,
                    Price = entry.Price,
                    Barcode = entry.Code,
                };
                var _sku = await this.CreateOrUpdateSku(sku);
                minPrice = sku.Price;
            }

            entry.Price = minPrice;
            await _context.SaveChangesAsync();
            transaction.Commit();
            //return entry;
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Product? Find(long Id, List<String>? Includes = null!)
        {
            var query = _context.Products.AsQueryable();
            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }
            return query.AsNoTracking().SingleOrDefault(x => x.Id == Id);
        }

        public async Task<PaginatedList<Product>> GetAll(ProductRequest request, List<String>? includes = null!)
        {
            var query = _context.Products.AsQueryable();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (!String.IsNullOrEmpty(request.Search))
            {
                query = query.Where(q => q.Name.Contains(request.Search));
            }

            if (request.CategoryId != null)
            {
                query = query.Where(q => q.CategoryId == request.CategoryId);
            }
            if (request.BrandId != null)
            {
                query = query.Where(q => q.BrandId == request.BrandId);
            }

            switch (request.OrderBy)
            {
                case 1:
                    query = query.OrderBy(x => x.Price);
                    break;
                case 2:
                    query = query.OrderByDescending(x => x.Price);
                    break;
                default:
                    query = query.OrderByDescending(x => x.Id);
                    break;
            }

            return await PaginatedList<Product>.CreateAsync(query.AsSplitQuery().AsNoTracking(), request.Page, request.PageSize);
        }

        public async Task Update(long id, UpdateProductRequest product)
        {
            using var transaction = _context.Database.BeginTransaction();

            var _product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id) ?? throw new ArgumentException("Product does not exists!");
            string[] arrImg = [];

            var oldImages = _product.Images != null ? JsonSerializer.Deserialize<List<string>>(_product.Images) : [];

            var imgDeleted = oldImages?.Except(product.Images) ?? Enumerable.Empty<string>();

            foreach (var img in imgDeleted)
            {
                FileHelper.DeleteFile(img);
            }

            foreach (var item in product.Images.Select((value, i) => (value, i)))
            {
                product.Images[item.i] = await FileHelper.SaveFile(item.value);
            }

            _product.Name = product.Name ?? _product.Name;
            _product.Code = product.Code ?? _product.Code;
            _product.Barcode = product.Barcode ?? _product.Barcode;
            _product.Price = product.Price;
            _product.UnitBuy = product.UnitBuy ?? _product.UnitBuy;
            _product.UnitSell = product.UnitSell ?? _product.UnitSell;
            _product.Alias = new SlugHelper().GenerateSlug(product.Name);
            _product.Description = product.Description ?? _product.Description;
            _product.CategoryId = product.CategoryId ?? _product.CategoryId;
            _product.BrandId = product.BrandId ?? _product.BrandId;
            _product.TaxId = product.TaxId ?? _product.TaxId;
            _product.UpdatedAt = DateTime.UtcNow;
            _product.Images = JsonSerializer.Serialize(product.Images);
            _product.IsNew = product.IsNew;
            _product.NumberWarning = product.NumberWarning;
            _product.HasVariants = product.HasVariants;
            _product.IsFeatured = product.IsFeatured;

            double minPrice = Double.MaxValue;
            int maxStock = Int32.MinValue;
            foreach (var option in product.Options)
            {
                if (option.IsDeleted == true)
                {
                    await this.DeleteOption((long)option.Id);
                    continue;
                }
                option.ProductId = _product.Id;
                var _option = await this.CreateOrUpdateOption(option);

                foreach (var value in option.OptionValues)
                {
                    if (value.IsDeleted == true)
                    {
                        await this.DeleteOptionValue((long)value.Id);
                        continue;
                    }
                    value.OptionId = _option.Id;
                    value.ProductId = _product.Id;
                    await this.CreateOrUpdateOptionValue(value);
                }
            }

            if (product.NewSkus == true)
            {
                await this.DeleteSkus((long)product.Id);
            }

            if (product.Skus.Count() > 0)
            {
                foreach (var sku in product.Skus)
                {
                    sku.ProductId = _product.Id;
                    sku.Barcode = sku.Barcode ?? _product.Barcode;
                    var _sku = await this.CreateOrUpdateSku(sku);

                    minPrice = sku.Price < minPrice ? sku.Price : minPrice;

                    foreach (var variant in sku.Variants)
                    {
                        var optionValue = _context.OptionValues.AsNoTracking().FirstOrDefault(x => x.Code == variant.Code);
                        if (optionValue != null)
                        {
                            variant.ProductId = _product.Id;
                            variant.OptionValueId = optionValue.Id;
                            variant.OptionId = optionValue.OptionId;
                            variant.SkuId = _sku.Id;
                            await this.CreateOrUpdateVariant(variant);
                        }
                    }
                }
            }
            else
            {
                var sku = new SkusRequest
                {
                    ProductId = _product.Id,
                    Name = _product.Name,
                    Price = _product.Price,
                    Barcode = _product.Barcode
                };
                var _sku = await this.CreateOrUpdateSku(sku);
                minPrice = sku.Price;
                maxStock = sku.Stock;
            }

            _product.Price = minPrice;
            await _context.SaveChangesAsync();
            transaction.Commit();

        }

        public async Task<Sku> CreateOrUpdateSku(SkusRequest sku)
        {
            var _sku = new Sku
            {
                ProductId = (long)sku.ProductId,
                Name = sku.Name,
                Price = sku.Price,
                Barcode = sku.Barcode,
                Stock = sku.Stock,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            if (sku.Id != null)
            {
                _sku = _context.Skus.FirstOrDefault(sk => sk.Id == sku.Id) ?? _sku;
            }

            if (_sku.Id != 0)
            {
                _sku.ProductId = (long)sku.ProductId;
                _sku.Name = sku.Name;
                _sku.Price = sku.Price;
                _sku.Barcode = sku.Barcode;
                _sku.Stock = sku.Stock;
                _sku.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                _context.Add(_sku);
            }
            await _context.SaveChangesAsync();
            return _sku;
        }

        public async Task<Option> CreateOrUpdateOption(OptionsRequest option)
        {
            var _option = new Option
            {
                ProductId = (long)option.ProductId,
                Code = option.Code,
                Name = option.Name,
                Order = option.Order,
                Visual = option.Visual,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            if (option.Id != null)
            {
                _option = _context.Options.FirstOrDefault(op => op.Id == option.Id) ?? _option;
            }
            if (_option.Id != 0)
            {
                _option.ProductId = (long)option.ProductId;
                _option.Code = option.Code;
                _option.Name = option.Name;
                _option.Order = option.Order;
                _option.Visual = option.Visual;
                _option.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                _context.Add(_option);
            }
            await _context.SaveChangesAsync();
            return _option;
        }

        public async Task<OptionValue> CreateOrUpdateOptionValue(OptionValuesRequest optionValue)
        {
            var img = await FileHelper.SaveFile(optionValue.Image);
            var _optionValue = new OptionValue
            {
                OptionId = (long)optionValue.OptionId,
                ProductId = (long)optionValue.ProductId,
                Code = optionValue.Code,
                Image = img,
                Label = optionValue.Label,
                Value = optionValue.Value,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            if (optionValue.Id != null)
            {
                _optionValue = _context.OptionValues.FirstOrDefault(op => op.Id == optionValue.Id) ?? _optionValue;
            }

            if (_optionValue.Id != 0)
            {
                _optionValue.OptionId = (long)optionValue.OptionId;
                _optionValue.ProductId = (long)optionValue.ProductId;
                _optionValue.Code = optionValue.Code;
                _optionValue.Image = optionValue.Image;
                _optionValue.Label = optionValue.Label;
                _optionValue.Value = optionValue.Value;
                _optionValue.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                _context.Add(_optionValue);
            }
            await _context.SaveChangesAsync();
            return _optionValue;
        }

        public async Task<Variant> CreateOrUpdateVariant(VariantRequest variant)
        {
            var _variant = new Variant
            {
                OptionId = (long)variant.OptionId,
                OptionValueId = (long)variant.OptionValueId,
                ProductId = (long)variant.ProductId,
                SkuId = (long)variant.SkuId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            if (variant.Id != null)
            {
                _variant = _context.Variants.FirstOrDefault(x => x.Id == variant.Id) ?? _variant;
            }
            if (_variant.Id != 0)
            {
                _variant.OptionId = (long)variant.OptionId;
                _variant.OptionValueId = (long)variant.OptionValueId;
                _variant.ProductId = (long)variant.ProductId;
                _variant.SkuId = (long)variant.SkuId;
                _variant.UpdatedAt = DateTime.UtcNow;
            }
            else
            {
                _context.Add(_variant);
            }
            await _context.SaveChangesAsync();
            return _variant;
        }

        public async Task DeleteOption(long Id)
        {
            var option = await _context.Options.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
            if (option != null)
            {
                _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOptionValue(long? Id)
        {
            var optionValue = await _context.OptionValues.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
            if (optionValue != null)
            {
                FileHelper.DeleteFile(optionValue.Image);
                _context.OptionValues.Remove(optionValue);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteSkus(long? ProductId)
        {
            var skus = _context.Skus.AsNoTracking().Where(x => x.ProductId == ProductId);
            _context.Skus.RemoveRange(skus);
            await _context.SaveChangesAsync();

        }
    }
}
