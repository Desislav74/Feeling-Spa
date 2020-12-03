using FeelingSpa.Services.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FeelingSpa.Services.Data.Categories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Common.Repositories;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Web.ViewModels.Category;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(CreateCategoryInputModel input, string imagePath)
        {
            var category = new Category
            {
                Name = input.Name,
                Description = input.Description,
            };

            Directory.CreateDirectory($"{imagePath}/categories/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.'); 
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                   throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    Extension = extension,
                };
                category.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/categories/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categoriesRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            var categories = this.categoriesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return categories;
        }

        public int GetCount()
        {
            return this.categoriesRepository.All().Count();
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return category;
        }

        public async Task DeleteAsync(int id)
        {
            var category =
                await this.categoriesRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
        }
    }
}
