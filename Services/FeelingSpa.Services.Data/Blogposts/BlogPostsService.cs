using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FeelingSpa.Data.Common.Repositories;
using FeelingSpa.Data.Models;
using FeelingSpa.Services.Mapping;
using FeelingSpa.Web.ViewModels.BlogPost;
using FeelingSpa.Web.ViewModels.Salons;
using Microsoft.EntityFrameworkCore;

namespace FeelingSpa.Services.Data.Blogposts
{
    public class BlogPostsService : IBlogPostsService
    {
        private readonly IDeletableEntityRepository<Blog> blogsRepository;
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };

        public BlogPostsService(IDeletableEntityRepository<Blog>blogsRepository)
        {
            this.blogsRepository = blogsRepository;
        }

        public async Task CreateAsync(BlogPostInputModel input, string imagePath)
        {
            var blogPost = new Blog()
            {
                Title = input.Title,
                Author = input.Author,
                Content = input.Content,
            };

            Directory.CreateDirectory($"{imagePath}/blogPosts/");
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
                blogPost.Images.Add(dbImage);

                var physicalPath = $"{imagePath}/blogPosts/{dbImage.Id}.{extension}";
                await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.blogsRepository.AddAsync(blogPost);
            await this.blogsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 6)
        {
            var blogPosts = this.blogsRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return blogPosts;
        }

        public int GetCount()
        {
            return this.blogsRepository.All().Count();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var blogPost =
                await this.blogsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .To<T>().FirstOrDefaultAsync();
            return blogPost;
        }

        public async Task<IEnumerable<T>> GetAllWithSingleAsync<T>(int? sortId)
        {
            IQueryable<Blog> query =
                this.blogsRepository
                    .AllAsNoTracking()
                    .OrderByDescending(x => x.CreatedOn);

            if (sortId != null)
            {
                query = query
                    .Where(x => x.Id == sortId);
            }

            return await query.To<T>().ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var blogPost = await this.blogsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            this.blogsRepository.Delete(blogPost);
            await this.blogsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, BlogPostInputModel input)
        {
            var blogPost = this.blogsRepository.All().FirstOrDefault(x => x.Id == id);
            blogPost.Content = input.Content;
            blogPost.Author = input.Author;
            blogPost.Title = input.Title;
            await this.blogsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllLatestAsync<T>(int? count = null)
        {
            IQueryable<Blog> query =
                this.blogsRepository
                    .All()
                    .OrderByDescending(x => x.CreatedOn);
            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return await query.To<T>().ToListAsync();

        }
    }
}
