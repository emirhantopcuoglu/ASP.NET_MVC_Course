using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void TestVerileriniDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if (context != null)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }

            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Text = "web programlama" },
                    new Tag { Text = "backend" },
                    new Tag { Text = "frontend" },
                    new Tag { Text = "fullstack" },
                    new Tag { Text = "php" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "RickGrimes" },
                    new User { UserName = "HarryPotter" }
                );
                context.SaveChanges();
            }

            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "ASP.NET",
                        Content = "ASP.NET Dersleri",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        Image = "img1.jpg",
                        UserId = 1
                    },
                    new Post
                    {
                        Title = "PHP",
                        Content = "PHP Dersleri",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(2).ToList(),
                        Image = "img3.jpg",
                        UserId = 1
                    },
                    new Post
                    {
                        Title = "Django",
                        Content = "Django Dersleri",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-30),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "img3.jpg",
                        UserId = 2
                    }
                );
                context.SaveChanges();
            }
        }
    }
}