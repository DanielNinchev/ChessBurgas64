namespace ChessBurgas64.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = GlobalConstants.NewsCategory });
            await dbContext.Categories.AddAsync(new Category { Name = GlobalConstants.AnnouncementsCategory });
            await dbContext.Categories.AddAsync(new Category { Name = GlobalConstants.ArticlesCategory });

            await dbContext.SaveChangesAsync();
        }
    }
}
