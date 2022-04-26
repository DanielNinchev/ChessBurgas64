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
            if (!dbContext.AnnouncementCategories.Any())
            {
                await dbContext.AnnouncementCategories.AddAsync(new AnnouncementCategory { Name = GlobalConstants.NewsCategory });
                await dbContext.AnnouncementCategories.AddAsync(new AnnouncementCategory { Name = GlobalConstants.AnnouncementsCategory });
                await dbContext.AnnouncementCategories.AddAsync(new AnnouncementCategory { Name = GlobalConstants.ArticlesCategory });
            }

            if (!dbContext.PuzzleCategories.Any())
            {
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.DrawIn1 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.DrawIn2 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.DrawIn3 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.FindBestContinuation });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.MateIn1 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.MateIn2 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.MateIn3 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.WinningMaterialIn1 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.WinningMaterialIn2 });
                await dbContext.PuzzleCategories.AddAsync(new PuzzleCategory { Name = GlobalConstants.WinningMaterialIn3 });
            }

            if (!dbContext.VideoCategories.Any())
            {
                await dbContext.VideoCategories.AddAsync(new VideoCategory { Name = GlobalConstants.ChessGames });
                await dbContext.VideoCategories.AddAsync(new VideoCategory { Name = GlobalConstants.ChessEndgames });
                await dbContext.VideoCategories.AddAsync(new VideoCategory { Name = GlobalConstants.ChessMiddlegames });
                await dbContext.VideoCategories.AddAsync(new VideoCategory { Name = GlobalConstants.ChessOpenings });
                await dbContext.VideoCategories.AddAsync(new VideoCategory { Name = GlobalConstants.ChessStreamings });
                await dbContext.VideoCategories.AddAsync(new VideoCategory { Name = GlobalConstants.ForBeginners });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
