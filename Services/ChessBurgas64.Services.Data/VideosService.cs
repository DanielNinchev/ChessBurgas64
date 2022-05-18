namespace ChessBurgas64.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Data.Contracts;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels.Videos;
    using Microsoft.EntityFrameworkCore;

    public class VideosService : IVideosService
    {
        private readonly IDeletableEntityRepository<Video> videosRepository;
        private readonly IMapper mapper;

        public VideosService(IDeletableEntityRepository<Video> videosRepository, IMapper mapper)
        {
            this.videosRepository = videosRepository;
            this.mapper = mapper;
        }

        public async Task CreateAsync(VideoInputModel input)
        {
            var video = this.mapper.Map<Video>(input);

            await this.videosRepository.AddAsync(video);
            await this.videosRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var video = await this.videosRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            this.videosRepository.Delete(video);
            await this.videosRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int page, int itemsPerPage)
        {
            var videos = await this.videosRepository
                .AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToListAsync();

            return videos;
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var video = await this.videosRepository
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return video;
        }

        public async Task<int> GetCountAsync()
        {
            return await this.videosRepository.AllAsNoTracking().CountAsync();
        }

        public async Task<IEnumerable<T>> GetSearchedAsync<T>(IEnumerable<int> categoryIds, string searchText)
        {
            var videos = this.videosRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .AsQueryable();

            if (categoryIds.Any() && searchText != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    videos = videos.Where(x => categoryIds.Any(id => id == x.CategoryId)
                                                        && (x.Title.ToLower().Contains(searchText.ToLower())
                                                            || x.Category.Name.ToLower().Contains(searchText.ToLower())
                                                            || x.Trainer.User.FirstName.ToLower().Contains(searchText.ToLower())
                                                            || x.Trainer.User.LastName.ToLower().Contains(searchText.ToLower())
                                                            || searchText.ToLower().Contains(x.Trainer.User.FirstName.ToLower())
                                                            || searchText.ToLower().Contains(x.Trainer.User.LastName.ToLower())
                                                            || x.Description.ToLower().Contains(searchText.ToLower())));
                }
            }
            else if (!categoryIds.Any() && searchText != null)
            {
                videos = videos.Where(x => x.Title.ToLower().Contains(searchText.ToLower())
                                                           || x.Category.Name.ToLower().Contains(searchText.ToLower())
                                                           || x.Trainer.User.FirstName.ToLower().Contains(searchText.ToLower())
                                                            || x.Trainer.User.LastName.ToLower().Contains(searchText.ToLower())
                                                            || searchText.ToLower().Contains(x.Trainer.User.FirstName.ToLower())
                                                            || searchText.ToLower().Contains(x.Trainer.User.LastName.ToLower())
                                                           || x.Description.ToLower().Contains(searchText.ToLower()));
            }
            else if (categoryIds.Any() && searchText == null)
            {
                foreach (var categoryId in categoryIds)
                {
                    videos = videos.Where(x => categoryIds.Any(id => id == x.CategoryId));
                }
            }
            else
            {
                return null;
            }

            return await videos.To<T>().ToListAsync();
        }

        public async Task UpdateAsync(int id, VideoInputModel input)
        {
            var video = await this.videosRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            video.Description = input.Description;
            video.CategoryId = input.CategoryId;
            video.Title = input.Title;
            video.TrainerId = input.TrainerId;
            video.Url = input.Url;

            await this.videosRepository.SaveChangesAsync();
        }
    }
}
