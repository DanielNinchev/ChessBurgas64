namespace ChessBurgas64.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Services.Mapping;
    using ChessBurgas64.Web.ViewModels;
    using ChessBurgas64.Web.ViewModels.Categories;
    using Moq;
    using Xunit;

    public class CategoriesServiceTests
    {
        private readonly IMapper mapper;

        [Fact]
        public void WhenUserSearchesByCategoriesAllCheckedCategoriesShouldBeInitialized()
        {
            IDictionary<string, string> parms = new Dictionary<string, string>
            {
                { "Мат в 1 ход", "1" },
                { "Мат в 2 хода", "2" },
            };

            var input = new SearchInputModel();
            var categoryIds = new List<int>();

            foreach (var id in parms.Values)
            {
                categoryIds.Add(int.Parse(id));
            }

            var exception = Record.Exception(() => new Exception());
            var categoriesService = this.InitializeService();
            categoriesService.InitializeSearchedParameters(input, parms);

            Assert.Equal(categoryIds, input.Categories);
            Assert.Null(exception);
        }

        [Fact]
        public void WhenUserSearchesByTextAndCategoriesAllParametersShouldBeInitialized()
        {
            IDictionary<string, string> parms = new Dictionary<string, string>
            {
                { "Мат в 1 ход", "1" },
                { "Мат в 2 хода", "2" },
                { "SearchText", "Реми" },
            };

            var input = new SearchInputModel();
            var categoryIds = new List<int>();

            foreach (var id in parms.Values.Where(x => !x.Equals("Реми")))
            {
                categoryIds.Add(int.Parse(id));
            }

            var exception = Record.Exception(() => new Exception());
            var categoriesService = this.InitializeService();
            categoriesService.InitializeSearchedParameters(input, parms);

            Assert.Equal(categoryIds, input.Categories);
            Assert.Equal(parms["SearchText"], input.SearchText);
            Assert.Null(exception);
        }

        [Fact]
        public void WhenUserSearchesByTextItShouldBeInitializedAndCategoriesShouldBeEmpty()
        {
            IDictionary<string, string> parms = new Dictionary<string, string>
            {
                { "SearchText", "Реми" },
            };

            var input = new SearchInputModel();

            var exception = Record.Exception(() => new Exception());
            var categoriesService = this.InitializeService();
            categoriesService.InitializeSearchedParameters(input, parms);

            Assert.Equal(parms["SearchText"], input.SearchText);
            Assert.Empty(input.Categories);
            Assert.Null(exception);
        }

        [Fact]
        public void ShouldReturnEmptyCollectionIfGetCategoriesByIdIsGivenNoIds()
        {
            var exception = Record.Exception(() => new Exception());
            var categoriesService = this.InitializeService();

            var model = new PuzzleCategoryViewModel();
            IEnumerable<int> givenIds = new int[0];

            var viewModel = new SearchViewModel()
            {
                Categories = categoriesService.GetCategoriesByIds<PuzzleCategoryViewModel>(givenIds, nameof(Puzzle)),
            };

            Assert.Empty(viewModel.Categories);
            Assert.Null(exception);
        }

        private CategoriesService InitializeService()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            var announcementCategoriesMockRepo = new Mock<IDeletableEntityRepository<AnnouncementCategory>>();
            var puzzleCategoriesMockRepo = new Mock<IDeletableEntityRepository<PuzzleCategory>>();
            var videoCategoriesMockRepo = new Mock<IDeletableEntityRepository<VideoCategory>>();

            return new CategoriesService(
                announcementCategoriesMockRepo.Object,
                puzzleCategoriesMockRepo.Object,
                videoCategoriesMockRepo.Object);
        }
    }
}
