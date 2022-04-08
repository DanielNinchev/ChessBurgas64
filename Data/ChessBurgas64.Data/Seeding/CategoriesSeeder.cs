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
            if (dbContext.AnnouncementCategories.Any())
            {
                return;
            }

            await dbContext.AnnouncementCategories.AddAsync(new AnnouncementCategory { Name = GlobalConstants.NewsCategory });
            await dbContext.AnnouncementCategories.AddAsync(new AnnouncementCategory { Name = GlobalConstants.AnnouncementsCategory });
            await dbContext.AnnouncementCategories.AddAsync(new AnnouncementCategory { Name = GlobalConstants.ArticlesCategory });

            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.DrawIn1 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.DrawIn2 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.DrawIn3 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.FindBestContinuation });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.MateIn1 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.MateIn2 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.MateIn3 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.WinningMaterialIn1 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.WinningMaterialIn1 });
            await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.WinningMaterialIn1 });

            await dbContext.SaveChangesAsync();
        }
    }
}
