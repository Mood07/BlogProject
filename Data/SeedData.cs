using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlogProject.Models;
using BlogProject.Data;

namespace BlogProject.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (!context.BlogPosts.Any())
            {
                context.BlogPosts.AddRange(
                    new BlogPost { Title = "First Blog Post", Content = "This is your first post!" },
                    new BlogPost { Title = "Second Blog Post", Content = "Write something awesome!" }
                );
                context.SaveChanges();
            }
        }
    }
}
