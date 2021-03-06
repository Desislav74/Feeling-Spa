﻿using FeelingSpa.Web.ViewModels.Categories;

namespace FeelingSpa.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FeelingSpa.Web.ViewModels.Category;

    public interface ICategoriesService
    {
        Task CreateAsync(CreateCategoryInputModel input, string imagePath);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6);

        IEnumerable<T> GetAllSearch<T>();

        Task UpdateAsync(int id, CreateEditCategoryInputModel input);

        int GetCount();

        T GetById<T>(int id);

        Task DeleteAsync(int id);
    }
}
