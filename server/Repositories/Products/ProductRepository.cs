﻿using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;
using Slugify;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopAppApi.Repositories.Products
{
    public class ProductRepository(ShopAppContext context, IFileHelper fileHelper, IConfiguration configuration) : IProductRepository
    {
        private readonly ShopAppContext _context = context;
        private readonly string driver = configuration.GetSection("FileStorage:Driver").Value!;
        public async Task Create(StoreProductRequest product)
        {
            using var transaction = _context.Database.BeginTransaction();

            var entry = new Product
            {
                Code = product.Code,
                Barcode = product.Barcode,
                Name = product.Name,
                Price = product.Price,
                UnitBuy = product.UnitBuy,
                UnitSell = product.UnitSell,
                Alias = new SlugHelper().GenerateSlug(product.Name),
                // Description = product.Description,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                TaxId = product.TaxId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ImageThumb = product.ImageThumb != null ? await fileHelper.SaveFile(product.ImageThumb, "imgThumb") : "",
                IsNew = product.IsNew,
                NumberWarning = product.NumberWarning,
                HasVariants = product.HasVariants,
                IsFeatured = product.IsFeatured,
                ConversionUnit = product.ConversionUnit,
                SoldOut = product.SoldOut,
            };
            _context.Add(entry);
            await _context.SaveChangesAsync();

            // lưu hình ảnh
            foreach (var image in product.Images)
            {
                var productImage = new ProductImage
                {
                    ProductId = entry.Id,
                    Code = image.Code,
                    Path = image.Path != null ? await fileHelper.SaveFile(image.Path, "images") : "",
                    Type = image.Type,
                    Driver = driver,
                    Extension = image.Extension,
                    FileName = image.FileName,
                    IsDeleted = false,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                };
                _context.Add(productImage);
            }
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

            if (product.Skus.Count > 0)
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

        public async Task<ProductVM> Show(long Id)
        {
            var query = _context.Products.AsQueryable().Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Barcode = p.Barcode,
                Code = p.Code,
                Price = p.Price,
                NumberWarning = p.NumberWarning,
                ImageThumb = !string.IsNullOrEmpty(p.ImageThumb) ? fileHelper.GetLink(p.ImageThumb) : null,
                UnitSell = p.UnitSell,
                UnitBuy = p.UnitBuy,
                // Description = p.Description,
                Alias = p.Alias,
                CanEdit = p.CanEdit,
                CanDelete = p.CanDelete,
                HasVariants = p.HasVariants,
                IsNew = p.IsNew,
                IsFeatured = p.IsFeatured,
                IsSale = p.IsSale,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                TaxId = p.TaxId,

                // Options = p.Options.Select(o => new OptionVM
                // {
                //     Id = o.Id,
                //     Code = o.Code,
                //     ProductId = o.ProductId,
                //     Name = o.Name,
                //     Visual = o.Visual,
                //     Order = o.Order,
                //     OptionValues = o.OptionValues.Select(v => new OptionValueVM
                //     {
                //         Id = v.Id,
                //         Code = v.Code,
                //         ProductId = v.ProductId,
                //         OptionId = v.OptionId,
                //         Value = v.Value,
                //         Label = v.Label
                //     }).ToList(),
                // }).ToList(),
                Images = p.ProductImages.Select(i => new ProductImageVM
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Code = i.Code,
                    Path = fileHelper.GetLink(i.Path),
                    Type = i.Type,
                    Driver = i.Driver,
                    IsDeleted = i.IsDeleted,
                    Extension = i.Extension,
                    FileName = i.FileName
                }).ToList(),
                // Skus = p.Skus.Select(s => new SkuVM
                // {
                //     Id = s.Id,
                //     ProductId = s.ProductId,
                //     Barcode = s.Barcode,
                //     Price = s.Price,
                //     Name = s.Name,
                //     Stock = s.Stock,
                //     Variants = s.Variants.Select(v => new VariantVM
                //     {
                //         Id = v.Id,
                //         ProductId = v.ProductId,
                //         SkuId = v.SkuId,
                //         OptionId = v.OptionId,
                //         OptionValueId = v.OptionValueId,
                //         OptionValue = new OptionValueVM
                //         {
                //             Id = v.OptionValue.Id,
                //             Code = v.OptionValue.Code,
                //             Value = v.OptionValue.Value,
                //             Label = v.OptionValue.Label,
                //         }
                //     }).ToList(),
                // }).ToList()
            });
            // if (Includes != null)
            // {
            //     foreach (var include in Includes)
            //     {
            //         query = query.Include(include);
            //     }
            // }
            return await query.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id) ?? throw new ArgumentException("Product does not exists!");
        }

        public async Task<PaginatedList<ProductVM>> GetAll(ProductRequest request, List<string>? includes = null!)
        {
            IQueryable<ProductVM> query = _context.Products.AsQueryable().Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Barcode = p.Barcode,
                Code = p.Code,
                Price = p.Price,
                NumberWarning = p.NumberWarning,
                ImageThumb = fileHelper.GetLink(p.ImageThumb),
                UnitSell = p.UnitSell,
                UnitBuy = p.UnitBuy,
                // Description = p.Description,
                Alias = p.Alias,
                CanEdit = p.CanEdit,
                CanDelete = p.CanDelete,
                HasVariants = p.HasVariants,
                IsNew = p.IsNew,
                IsFeatured = p.IsFeatured,
                IsSale = p.IsSale,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                TaxId = p.TaxId,
                Brand = new BrandVM
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name,
                    Code = p.Brand.Code
                },
                Category = new CategoryVM
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Code = p.Category.Code,
                },
                Tax = new TaxVM
                {
                    Id = p.Tax.Id,
                    Name = p.Tax.Name,
                    Value = p.Tax.Value
                },
                SkuCount = p.Skus.Count,
            });
            // if (includes != null)
            // {
            //     foreach (var include in includes)
            //     {
            //         query = query.Include(include);
            //     }
            // }

            if (!string.IsNullOrWhiteSpace(request.Search))
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

            if (request.IsNew != null)
            {
                query = query.Where(q => q.IsNew == request.IsNew);
            }

            if (request.IsFeatured != null)
            {
                query = query.Where(q => q.IsFeatured == request.IsFeatured);
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

            var Data = await PaginatedList<ProductVM>.CreateAsync(query.AsSingleQuery().AsNoTracking(), request.Page, request.PageSize);

            return Data;
        }

        public async Task Update(long id, UpdateProductRequest product)
        {
            using var transaction = _context.Database.BeginTransaction();

            var _product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id) ?? throw new ArgumentException("Product does not exists!");

            //string[] arrImg = [];


            foreach (var item in product.Images)
            {
                if (item.IsDeleted == true)
                {
                    var delImg = await _context.ProductImages.SingleOrDefaultAsync(x => x.Id == item.Id);
                    fileHelper.DeleteFile(delImg.Path);
                    _context.ProductImages.Remove(delImg);
                }
                else if (item.Id == 0)
                {
                    var saveImg = await fileHelper.SaveFile(item.Path);
                    var newImg = new ProductImage
                    {
                        ProductId = _product.Id,
                        Path = saveImg,
                        Code = item.Code,
                        Type = item.Type,
                        Driver = driver,
                        Extension = item.Extension,
                        FileName = saveImg,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                    };
                    _context.Add(newImg);
                }
                else
                {
                    continue;
                }
                await _context.SaveChangesAsync();
            }

            _product.Name = product.Name ?? _product.Name;
            _product.Code = product.Code ?? _product.Code;
            _product.Barcode = product.Barcode ?? _product.Barcode;
            _product.Price = product.Price;
            _product.UnitBuy = product.UnitBuy ?? _product.UnitBuy;
            _product.UnitSell = product.UnitSell ?? _product.UnitSell;
            _product.Alias = new SlugHelper().GenerateSlug(product.Name);
            // _product.Description = product.Description ?? _product.Description;
            _product.CategoryId = product.CategoryId ?? _product.CategoryId;
            _product.BrandId = product.BrandId ?? _product.BrandId;
            _product.TaxId = product.TaxId ?? _product.TaxId;
            _product.UpdatedAt = DateTime.UtcNow;

            // if(!string.IsNullOrEmpty(product.Description))
            // {
            //     fileHelper.SaveHtmlFile(_product.Description, $"{_product.Alias}.html");
            // }

            if (!string.IsNullOrEmpty(product.ImageThumb) && (_product.ImageThumb == null || !product.ImageThumb.Contains(_product.ImageThumb)))
            {
                fileHelper.DeleteFile(_product.ImageThumb);
                _product.ImageThumb = await fileHelper.SaveFile(product.ImageThumb);
            }
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
                    await DeleteOption((long)option.Id);
                    continue;
                }
                option.ProductId = _product.Id;
                var _option = await CreateOrUpdateOption(option);

                foreach (var value in option.OptionValues)
                {
                    if (value.IsDeleted == true)
                    {
                        await DeleteOptionValue((long)value.Id);
                        continue;
                    }
                    value.OptionId = _option.Id;
                    value.ProductId = _product.Id;
                    await CreateOrUpdateOptionValue(value);
                }
            }

            if (product.NewSkus == true)
            {
                await DeleteSkus((long)product.Id);
            }

            if (product.Skus.Count() > 0)
            {
                foreach (var sku in product.Skus)
                {
                    sku.ProductId = _product.Id;
                    sku.Barcode = sku.Barcode ?? _product.Barcode;
                    var _sku = await CreateOrUpdateSku(sku);

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
                            await CreateOrUpdateVariant(variant);
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
                var _sku = await CreateOrUpdateSku(sku);
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
            var img = await fileHelper.SaveFile(optionValue.Image);
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
                fileHelper.DeleteFile(optionValue.Image);
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

        public async Task<PaginatedList<ProductVM>> GetFeaturedProduct(ProductRequest request)
        {
            var query = _context.Products.AsQueryable().Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                NumberWarning = p.NumberWarning,
                ImageThumb = fileHelper.GetLink(p.ImageThumb),
                UnitSell = p.UnitSell,
                UnitBuy = p.UnitBuy,
                Alias = p.Alias,
                HasVariants = p.HasVariants,
                IsNew = p.IsNew,
                IsFeatured = p.IsFeatured,
                IsSale = p.IsSale,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                TaxId = p.TaxId,
                Brand = new BrandVM
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name,
                    Code = p.Brand.Code
                },
                Category = new CategoryVM
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Code = p.Category.Code,
                },
            }).Where(q => q.IsFeatured == true || q.IsNew == true);

            // if (includes != null)
            // {
            //     foreach (var include in includes)
            //     {
            //         query = query.Include(include);
            //     }
            // }

            if (!string.IsNullOrWhiteSpace(request.Search))
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

            return await PaginatedList<ProductVM>.CreateAsync(query.AsSplitQuery().AsNoTracking(), request.Page, request.PageSize);
        }

        public async Task<PaginatedList<ProductVM>> GetProductByCategory(string categoryCode, ProductRequest request)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(x => x.Code == categoryCode);
            if (category == null || category.NotUse == true)
            {
                throw new ArgumentException("Category does not exists!");
            }
            var query = _context.Products.AsQueryable().Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                NumberWarning = p.NumberWarning,
                ImageThumb = fileHelper.GetLink(p.ImageThumb),
                UnitSell = p.UnitSell,
                UnitBuy = p.UnitBuy,
                Alias = p.Alias,
                HasVariants = p.HasVariants,
                IsNew = p.IsNew,
                IsFeatured = p.IsFeatured,
                IsSale = p.IsSale,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                TaxId = p.TaxId,
                Brand = new BrandVM
                {
                    Id = p.Brand.Id,
                    Name = p.Brand.Name,
                    Code = p.Brand.Code
                },
                Category = new CategoryVM
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Code = p.Category.Code,
                },
            }).Where(q => q.CategoryId == category.Id);

            if (request.MinPrice != null)
            {
                query = query.Where(q => q.Price >= request.MinPrice);
            }
            if (request.MaxPrice != null)
            {
                query = query.Where(q => q.Price <= request.MaxPrice);
            }

            if (request.BrandId != null)
            {
                query = query.Where(q => q.BrandId == request.BrandId);
            }

            if (!string.IsNullOrEmpty(request.BrandCode))
            {
                query = query.Where(q => q.Brand.Code == request.BrandCode);
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

            return await PaginatedList<ProductVM>.CreateAsync(query.AsSplitQuery().AsNoTracking(), request.Page, request.PageSize);
        }

        public async Task<ProductVM> FindProductByAlias(string Alias)
        {
            var query = await _context.Products.AsQueryable().Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                NumberWarning = p.NumberWarning,
                ImageThumb = fileHelper.GetLink(p.ImageThumb),
                UnitSell = p.UnitSell,
                UnitBuy = p.UnitBuy,
                Alias = p.Alias,
                HasVariants = p.HasVariants,
                IsNew = p.IsNew,
                IsFeatured = p.IsFeatured,
                IsSale = p.IsSale,
                // Description = p.Description,
                Brand = new BrandVM
                {
                    Name = p.Brand.Name,
                    Code = p.Brand.Code
                },
                Category = new CategoryVM
                {
                    Name = p.Category.Name,
                    Code = p.Category.Code,
                },
                // Options = p.Options.Select(o => new OptionVM
                // {
                //     Id = o.Id,
                //     Code = o.Code,
                //     Name = o.Name,
                //     Visual = o.Visual,
                //     Order = o.Order,
                //     OptionValues = o.OptionValues.Select(v => new OptionValueVM
                //     {
                //         Id = v.Id,
                //         Code = v.Code,
                //         OptionId = v.OptionId,
                //         Value = v.Value,
                //         Label = v.Label
                //     }).ToList(),
                // }).ToList(),
                Images = p.ProductImages.Select(i => new ProductImageVM
                {
                    Path = fileHelper.GetLink(i.Path),
                }).ToList(),
                // Skus = p.Skus.Select(s => new SkuVM
                // {
                //     Id = s.Id,
                //     Barcode = s.Barcode,
                //     Price = s.Price,
                //     Name = s.Name,
                //     Stock = s.Stock,
                //     Variants = s.Variants.Select(v => new VariantVM
                //     {
                //         Id = v.Id,
                //         SkuId = v.SkuId,
                //         OptionId = v.OptionId,
                //         OptionValueId = v.OptionValueId,
                //     }).ToList(),
                // }).ToList()
            }).SingleOrDefaultAsync(x => x.Alias.Equals(Alias)) ?? throw new ArgumentException("Product does not exists!");

            return query;
        }
        public async Task<string?> GetDescriptionProduct(string Alias)
        {
            var entry = await _context.Products.AsQueryable().Select(p => new { Description = p.Description, Alias = p.Alias }).SingleOrDefaultAsync(p => p.Alias == Alias);
            return entry?.Description ?? "";
        }
        public async Task<string?> GetDescriptionProduct(long Id)
        {
            var entry = await _context.Products.AsQueryable().Select(p => new { Id = p.Id, Description = p.Description }).SingleOrDefaultAsync(p => p.Id == Id);
            return entry?.Description ?? "";
        }

        public async Task<ProductVM> GetSkuProduct(long Id)
        {
            var options = await _context.Options.Where(o => o.ProductId == Id).Select(o => new OptionVM
                {
                    Id = o.Id,
                    Code = o.Code,
                    ProductId = o.ProductId,
                    Name = o.Name,
                    Visual = o.Visual,
                    Order = o.Order,
                    OptionValues = o.OptionValues.Select(v => new OptionValueVM
                    {
                        Id = v.Id,
                        Code = v.Code,
                        ProductId = v.ProductId,
                        OptionId = v.OptionId,
                        Value = v.Value,
                        Label = v.Label
                    }).ToList(),
                }).ToListAsync();

            var skus = await _context.Skus.Where(o => o.ProductId == Id).Select(s => new SkuVM
            {
                Id = s.Id,
                ProductId = s.ProductId,
                Barcode = s.Barcode,
                Price = s.Price,
                Name = s.Name,
                Stock = s.Stock,
                Variants = s.Variants.Select(v => new VariantVM
                {
                    Id = v.Id,
                    ProductId = v.ProductId,
                    SkuId = v.SkuId,
                    OptionId = v.OptionId,
                    OptionValueId = v.OptionValueId,
                    OptionValue = new OptionValueVM
                    {
                        Id = v.OptionValue.Id,
                        Code = v.OptionValue.Code,
                        Value = v.OptionValue.Value,
                        Label = v.OptionValue.Label,
                    }
                }).ToList()
            }).ToListAsync();

            return new ProductVM
            {
                Options = options,
                Skus = skus,
            };
        }
        public void UpdateDesctionProduct(long Id, ProductDesRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            var entry = _context.Products.SingleOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Product does not exists!");
            entry.Description = request.Description;
            _context.SaveChanges();
            transaction.Commit();
        }

    }
}



