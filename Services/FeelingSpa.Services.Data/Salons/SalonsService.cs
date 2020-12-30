using System.Collections;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FeelingSpa.Services.Data.Salons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Common.Repositories;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;
    using FeelingSpa.Web.ViewModels.City;
    using FeelingSpa.Web.ViewModels.Salons;

    using Microsoft.EntityFrameworkCore;

    public class SalonsService : ISalonsService
    {
        private readonly IDeletableEntityRepository<Salon> salonsRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public SalonsService(IDeletableEntityRepository<Salon> salonsRepository)
        {
            this.salonsRepository = salonsRepository;
        }

        public async Task<string> CreateAsync(CreateSalonInputModel input, string imagePath)
        {
            var salon = new Salon
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = input.CategoryId,
                Name = input.Name,
                Address = input.Address,
                CityId = input.CityId,
            };

            Directory.CreateDirectory($"{imagePath}/salons/");
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
                salon.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/salons/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.salonsRepository.AddAsync(salon);
            await this.salonsRepository.SaveChangesAsync();
            return salon.Id;
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            var salons = this.salonsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return salons;
        }

        public int GetCount()
        {
            return this.salonsRepository.All().Count();
        }

        public T GetById<T>(string id)
        {
            var salon = this.salonsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return salon;
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var salon =
                await this.salonsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .To<T>().FirstOrDefaultAsync();
            return salon;
        }

        public async Task DeleteAsync(string id)
        {
            var salon =
                await this.salonsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            this.salonsRepository.Delete(salon);
            await this.salonsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, CreateEditInputViewModel input)
        {
            var salons = this.salonsRepository.All().FirstOrDefault(x => x.Id == id);
            salons.Name = input.Name;
            salons.Address = input.Address;
            salons.CategoryId = input.CategoryId;
            salons.CityId = input.CityId;
            await this.salonsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetByCategories<T>(IEnumerable<int> categoriesIds)
        {
            var query = this.salonsRepository.All().AsQueryable();
            foreach (var categoryId in categoriesIds)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            return query.To<T>().ToList();
        }

        public async Task RateSalonAsync(string id, int rateValue)
        {
            var salon =
                await this.salonsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

            var oldRating = salon.Rating;
            var oldRatersCount = salon.RatingCount;

            var newRatersCount = oldRatersCount + 1;
            var newRating = (oldRating + rateValue) / newRatersCount;

            salon.Rating = newRating;
            salon.RatingCount = newRatersCount;

            await this.salonsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var salons =
                await this.salonsRepository
                    .All()
                    .OrderBy(x => x.Name)
                    .To<T>().ToListAsync();
            return salons;
        }
    }
}
