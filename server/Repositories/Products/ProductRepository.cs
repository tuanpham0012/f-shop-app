using Microsoft.EntityFrameworkCore;
using Nest;
using ShopAppApi.Data;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.Services.Elasticsearch;
using ShopAppApi.ViewModels;
using Slugify;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopAppApi.Repositories.Products
{
    public class ProductRepository(ShopAppContext context, IFileHelper fileHelper, IConfiguration configuration, IElasticsearchService elasticsearch) : IProductRepository
    {
        private readonly ShopAppContext _context = context;
        private readonly string elsIndex = "product";
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
                ImageThumb = product.ImageThumb != null ? fileHelper.SaveFile(product.ImageThumb, "imgThumb") : "",
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
                    Path = image.Path != null ? fileHelper.SaveFile(image.Path, "images") : "",
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
                CreateOrUpdateOption(option);
            }

            if (product.Skus.Count > 0)
            {
                foreach (var sku in product.Skus)
                {
                    sku.ProductId = entry.Id;
                    await CreateOrUpdateSku(sku, entry);

                    minPrice = sku.Price < minPrice ? sku.Price : minPrice;
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
                await CreateOrUpdateSku(sku, entry);
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
                ConversionUnit = p.ConversionUnit,
                SoldOut = p.SoldOut,
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
            });
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
                ConversionUnit = p.ConversionUnit,
                SoldOut = p.SoldOut,
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

            foreach (var item in product.Images)
            {
                if (item.IsDeleted == true)
                {
                    var delImg = await _context.ProductImages.SingleOrDefaultAsync(x => x.Id == item.Id);
                    if (delImg != null)
                    {
                        fileHelper.DeleteFile(delImg.Path);
                        _context.ProductImages.Remove(delImg);
                    }
                }
                else if (item.Id == 0)
                {
                    var saveImg = fileHelper.SaveFile(item.Path, "images");
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
            _product.ConversionUnit = product.ConversionUnit;
            _product.SoldOut = product.SoldOut;

            // if(!string.IsNullOrEmpty(product.Description))
            // {
            //     fileHelper.SaveHtmlFile(_product.Description, $"{_product.Alias}.html");
            // }

            if (!string.IsNullOrEmpty(product.ImageThumb) && (_product.ImageThumb == null || !product.ImageThumb.Contains(_product.ImageThumb)))
            {
                fileHelper.DeleteFile(_product.ImageThumb);
                _product.ImageThumb = fileHelper.SaveFile(product.ImageThumb, "imgThumb");
            }
            _product.IsNew = product.IsNew;
            _product.NumberWarning = product.NumberWarning;
            _product.HasVariants = product.HasVariants;
            _product.IsFeatured = product.IsFeatured;

            double minPrice = Double.MaxValue;
            int maxStock = Int32.MinValue;
            foreach (var option in product.Options)
            {
                if (option.IsDeleted == true && option.Id != null)
                {
                    DeleteOption((long)option.Id);
                    continue;
                }
                option.ProductId = _product.Id;
                CreateOrUpdateOption(option);
            }

            if (product.NewSkus == true && product.Id != null)
            {
                DeleteSkus((long)product.Id);
            }

            if (product.Skus.Count > 0)
            {
                foreach (var sku in product.Skus)
                {
                    sku.ProductId = _product.Id;
                    sku.Barcode ??= _product.Barcode;
                    if (string.IsNullOrWhiteSpace(sku.ImagePath))
                    {
                        sku.ImagePath = product.ImageThumb;
                    }
                    await CreateOrUpdateSku(sku, _product);

                    minPrice = sku.Price < minPrice ? sku.Price : minPrice;
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
                await CreateOrUpdateSku(sku, _product);
                minPrice = sku.Price;
                maxStock = sku.Stock;
            }

            _product.Price = minPrice;
            await _context.SaveChangesAsync();
            transaction.Commit();

        }

        private async Task CreateOrUpdateSku(SkusRequest sku, Product product)
        {
            var _sku = new Sku();
            if (sku.Id != null && sku.IsEdited == true)
            {
                _sku = _context.Skus.SingleOrDefault(x => x.Id == sku.Id);
                if (_sku == null)
                {
                    return;
                }
                if (!string.IsNullOrEmpty(sku.ImagePath) && (_sku.ImagePath == null || !sku.ImagePath.Contains(_sku.ImagePath)))
                {
                    fileHelper.DeleteFile(_sku.ImagePath);
                    _sku.ImagePath = fileHelper.SaveFile(sku.ImagePath, "imgThumb");
                }
                _sku.ProductId = sku.ProductId ?? 0;
                _sku.Name = sku.Name ?? "";
                _sku.Price = sku.Price;
                _sku.Barcode = sku.Barcode ?? "";
                _sku.Stock = sku.Stock;
                _sku.UpdatedAt = DateTime.UtcNow;
                _sku.ImageCode = sku.ImageCode ?? "";
                _context.SaveChanges();

                await elasticsearch.UpdateDocument<SkuVM>(elsIndex, sku.Id.ToString() ?? "0", new SkuVM
                {
                    Id = _sku.Id,
                    ProductId = _sku.ProductId,
                    Barcode = _sku.Barcode,
                    Price = _sku.Price,
                    Name = _sku.Name,
                    ImagePath = fileHelper.GetLink(_sku.ImagePath),
                    ImageCode = _sku.ImageCode,
                    Stock = _sku.Stock,
                    ProductBarcode = product.Barcode,
                    ProductName = product.Name,
                    ProductCode = product.Code,
                });

            }
            else if (sku.Id == null)
            {
                _sku = new Sku
                {
                    ProductId = sku.ProductId ?? 0,
                    Name = sku.Name ?? "",
                    Price = sku.Price,
                    ImagePath = !string.IsNullOrWhiteSpace(sku.ImagePath) ? fileHelper.SaveFile(sku.ImagePath, "imgThumb") : "",
                    Barcode = sku.Barcode ?? "",
                    Stock = sku.Stock,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ImageCode = sku.ImageCode ?? "",
                };
                _context.Add(_sku);
                _context.SaveChanges();
                await elasticsearch.CreateDocument<SkuVM>(elsIndex, new SkuVM
                {
                    Id = _sku.Id,
                    ProductId = _sku.ProductId,
                    Barcode = _sku.Barcode,
                    Price = _sku.Price,
                    Name = _sku.Name,
                    ImagePath = fileHelper.GetLink(_sku.ImagePath),
                    ImageCode = _sku.ImageCode,
                    Stock = _sku.Stock,
                    ProductBarcode = product.Barcode,
                    ProductName = product.Name,
                    ProductCode = product.Code,
                });
                CreateOrUpdateVariant(sku.Variants, _sku);
            }
        }

        private void CreateOrUpdateOption(OptionsRequest option)
        {
            var _option = new Option { };

            if (option.Id != null && option.IsEdited == true)
            {
                _option = new Option
                {
                    Id = option.Id ?? 0,
                    ProductId = option.ProductId ?? 0,
                    Code = option.Code,
                    Name = option.Name ?? "",
                    Order = option.Order,
                    Visual = option.Visual,
                    UpdatedAt = DateTime.UtcNow,
                };
                _context.Options.Update(_option);
            }
            else if (option.Id == null)
            {
                _option = new Option
                {
                    ProductId = option.ProductId ?? 0,
                    Code = option.Code,
                    Name = option.Name ?? "",
                    Order = option.Order,
                    Visual = option.Visual,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                };
                _context.Add(_option);
            }
            _context.SaveChanges();
            CreateOrUpdateOptionValue(option.OptionValues, _option);
        }

        private void CreateOrUpdateOptionValue(ICollection<OptionValuesRequest> optionValues, Option option)
        {
            foreach (var optionValue in optionValues)
            {
                if (optionValue.IsDeleted == true && optionValue.Id != null)
                {
                    DeleteOptionValue((long)optionValue.Id);
                    continue;
                }
                optionValue.OptionId = option.Id;
                optionValue.ProductId = option.ProductId;
                CreateOrUpdateOptionValue(optionValue);
                _context.SaveChanges();

            }
        }

        private void CreateOrUpdateOptionValue(OptionValuesRequest optionValue)
        {
            if (optionValue.Id != null && optionValue.IsEdited == true)
            {
                var updatedOptionValue = new OptionValue
                {
                    Id = optionValue.Id ?? 0,
                    OptionId = optionValue.OptionId ?? 0,
                    ProductId = optionValue.ProductId ?? 0,
                    Code = optionValue.Code,
                    Label = optionValue.Label,
                    Value = optionValue.Value,
                    Image = optionValue.Image,
                };
                _context.OptionValues.Update(updatedOptionValue);
            }
            else if (optionValue.Id == null)
            {
                var _optionValue = new OptionValue
                {
                    OptionId = optionValue.OptionId != null ? (long)optionValue.OptionId : 0,
                    ProductId = optionValue.ProductId != null ? (long)optionValue.ProductId : 0,
                    Code = optionValue.Code,
                    Label = optionValue.Label,
                    Value = optionValue.Value,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                };

                _context.Add(_optionValue);
            }
            // _context.SaveChanges();
        }

        public void CreateOrUpdateVariant(ICollection<VariantRequest> variants, Sku sku)
        {
            foreach (var variant in variants)
            {
                if (variant.Id != null)
                {
                    continue;
                }
                var optionValue = _context.OptionValues.AsNoTracking().FirstOrDefault(x => x.Code == variant.Code);
                if (optionValue != null)
                {
                    variant.OptionValueId = optionValue.Id;
                    variant.OptionId = optionValue.OptionId;
                }
                variant.ProductId = sku.ProductId;
                variant.SkuId = sku.Id;
                CreateOrUpdateVariant(variant);
                _context.SaveChanges();

            }
        }

        public void CreateOrUpdateVariant(VariantRequest variant)
        {
            var _variant = new Variant
            {
                OptionId = variant.OptionId ?? 0,
                OptionValueId = variant.OptionValueId ?? 0,
                ProductId = variant.ProductId ?? 0,
                SkuId = variant.SkuId ?? 0,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            _context.Add(_variant);

        }

        public void DeleteOption(long Id)
        {
            var option = _context.Options.SingleOrDefault(x => x.Id == Id);
            if (option != null)
            {
                _context.Options.Remove(option);
                _context.SaveChanges();
            }
        }

        public void DeleteOptionValue(long Id)
        {
            var optionValue = _context.OptionValues.SingleOrDefault(x => x.Id == Id);
            if (optionValue != null)
            {
                fileHelper.DeleteFile(optionValue.Image);
                _context.OptionValues.Remove(optionValue);
                _context.SaveChanges();
            }

        }

        public void DeleteSkus(long ProductId)
        {
            // var skus = _context.Skus.Where(x => x.ProductId == ProductId);
            // _context.Skus.RemoveRange(skus);
            // _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("DELETE FROM Skus WHERE product_id = {0}", ProductId);
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
                Images = p.ProductImages.Select(i => new ProductImageVM
                {
                    Path = fileHelper.GetLink(i.Path),
                }).ToList(),
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
                ImagePath = fileHelper.GetLink(s.ImagePath),
                ImageCode = s.ImageCode,
                Stock = s.Stock,
                Variants = s.Variants.Select(v => new VariantVM
                {
                    Id = v.Id,
                    ProductId = v.ProductId,
                    SkuId = v.SkuId,
                    OptionId = v.OptionId,
                    OptionValueId = v.OptionValueId,
                    Code = v.OptionValue.Code ?? "",
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

        public async Task<List<SkuVM>> SearchProduct(string search)
        {
            var searchResponse = await elasticsearch.ElasticClient().SearchAsync<SkuVM>(s => s
            .Index(elsIndex)
            .Query(q => q
            .Match(mm => mm // Tìm kiếm trên nhiều fields
                .Query(search)
                .Field(p => p.Name)
                .Fuzziness(Fuzziness.Auto) // Cho phép lỗi chính tả nhỏ
            ) || q
            .Match(mm => mm // Tìm kiếm trên nhiều fields
                .Query(search)
                .Field(p => p.ProductName)
                .Fuzziness(Fuzziness.Auto) // Cho phép lỗi chính tả nhỏ
            ) ||q
            .Match(mm => mm // Tìm kiếm trên nhiều fields
                .Query(search)
                .Field(p => p.ProductBarcode)
                .Fuzziness(Fuzziness.Auto) // Cho phép lỗi chính tả nhỏ
            ) || q
            .Match(mm => mm // Tìm kiếm trên nhiều fields
                .Query(search)
                .Field(p => p.ProductCode)
                .Fuzziness(Fuzziness.Auto) // Cho phép lỗi chính tả nhỏ
            ))
            .From(0)
            .Size(10));

            if (searchResponse.IsValid)
            {
                return searchResponse.Documents.ToList();
            }
            else
            {
                Console.WriteLine($"Lỗi khi tìm kiếm sản phẩm: {searchResponse.DebugInformation}");
                if (searchResponse.OriginalException != null)
                {
                    Console.WriteLine($"Exception gốc: {searchResponse.OriginalException.Message}");
                }
                // Trả về danh sách rỗng trong trường hợp lỗi hoặc không tìm thấy
                return new List<SkuVM>();
            }

        }
    }
}



