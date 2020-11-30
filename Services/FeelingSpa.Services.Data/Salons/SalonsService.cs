﻿using System.Collections.Generic;
using FeelingSpa.Services.Mapping;

namespace FeelingSpa.Services.Data.Salons
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Common.Repositories;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Web.ViewModels.City;
    using FeelingSpa.Web.ViewModels.Salons;

    public class SalonsService : ISalonsService
    {
        private readonly IDeletableEntityRepository<Salon> salonsRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public SalonsService(IDeletableEntityRepository<Salon> salonsRepository)
        {
            this.salonsRepository = salonsRepository;
        }

        public async Task CreateAsync(CreateSalonInputModel input, string imagePath)
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
    }
}