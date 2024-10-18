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
                    new Tag { Text = "web programlama", Url = "web-programlama" },
                    new Tag { Text = "backend",  Url = "backend"},
                    new Tag { Text = "frontend",  Url = "frontend"},
                    new Tag { Text = "fullstack",  Url = "fullstack"},
                    new Tag { Text = "php",  Url = "php"}
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
                        Url = "aspnet-core",
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
                        Url = "php",
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
                        Url = "django",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-30),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "img3.jpg",
                        UserId = 2
                    },
                    new Post
                    {
                        Title = "React Dersleri",
                        Content = "React Dersleri",
                        Url = "react",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-40),
                        Tags = context.Tags.Take(4).ToList(),
                        Image = "img3.jpg",
                        UserId = 2
                    },
                    new Post
                    {
                        Title = "Angular Dersleri",
                        Content = "Angular Dersleri",
                        Url = "angular",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-50),
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