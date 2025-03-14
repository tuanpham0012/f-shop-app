﻿using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryVM>> GetAll(CategoryRequest request);
        List<CategoryTreeVM> BuildTree(List<CategoryVM> categories);
        Task<Category> Create(StoreCategoryRequest request);
        Task Update(long Id, UpdateCategoryRequest request);
        Task<Category> Show(long Id);
        Task Delete(long Id);

        Task<List<Category>> GetPopularCategory();

        Task<List<Category>> GetTopCategoryWithProduct();
        Task<List<Category>> GetCategoryHasFeaturedProduct();
    }
}
