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
            var video = this.videosRepository.All().FirstOrDefault(x => x.Id == id);
            this.videosRepository.Delete(video);
            await this.videosRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage)
        {
            var videos = this.videosRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>()
                .ToList();

            return videos;
        }

        public T GetById<T>(int id)
        {
            var video = this.videosRepository.AllAsNoTracking().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return video;
        }

        public int GetCount()
        {
            return this.videosRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<T> GetSearched<T>(int page, int itemsPerPage, IEnumerable<int> categoryIds, string searchText)
        {
            var videos = this.videosRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

            if (categoryIds != null && searchText != null)
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
            else if (categoryIds == null && searchText != null)
            {
                videos = videos.Where(x => x.Title.ToLower().Contains(searchText.ToLower())
                                                           || x.Category.Name.ToLower().Contains(searchText.ToLower())
                                                           || x.Trainer.User.FirstName.ToLower().Contains(searchText.ToLower())
                                                            || x.Trainer.User.LastName.ToLower().Contains(searchText.ToLower())
                                                            || searchText.ToLower().Contains(x.Trainer.User.FirstName.ToLower())
                                                            || searchText.ToLower().Contains(x.Trainer.User.LastName.ToLower())
                                                           || x.Description.ToLower().Contains(searchText.ToLower()));
            }
            else if (categoryIds != null && searchText == null)
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

            return videos.To<T>().ToList();
        }

        public async Task UpdateAsync(int id, VideoInputModel input)
        {
            var video = this.videosRepository.All().FirstOrDefault(x => x.Id == id);

            video.Description = input.Description;
            video.CategoryId = input.CategoryId;
            video.Title = input.Title;
            video.TrainerId = input.TrainerId;
            video.Url = input.Url;

            await this.videosRepository.SaveChangesAsync();
        }
    }
}
