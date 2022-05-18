namespace ChessBurgas64.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
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
        public void InitializeSearchedParameteresShouldAssignCategoriesToInputWhenUserSearchesByCategories()
        {
            var puzzleCategoryMateIn1 = "Мат в 1 ход";
            var puzzleCategoryMateIn2 = "Мат в 2 хода";
            var puzzleCategoryMateIn1Id = "1";
            var puzzleCategoryMateIn2Id = "2";

            IDictionary<string, string> parms = new Dictionary<string, string>
            {
                { puzzleCategoryMateIn1, puzzleCategoryMateIn1Id },
                { puzzleCategoryMateIn2, puzzleCategoryMateIn2Id },
            };

            var input = new SearchInputModel();
            var categoryIds = new List<int>();

            foreach (var id in parms.Values)
            {
                categoryIds.Add(int.Parse(id));
            }

            var categoriesService = this.InitializeService();
            categoriesService.InitializeSearchedParameters(input, parms);

            Assert.Equal(categoryIds, input.Categories);
        }

        [Fact]
        public void InitializeSearchedParameteresShouldAssignCategoriesAndSearchTextToInputWhenUserSearchesByBothTextAndCategories()
        {
            var puzzleCategoryMateIn1 = "Мат в 1 ход";
            var puzzleCategoryMateIn2 = "Мат в 2 хода";
            var puzzleCategoryMateIn1Id = "1";
            var puzzleCategoryMateIn2Id = "2";
            var someSearchTextKey = "SearchText";
            var someSearchTextValue = "Реми";

            IDictionary<string, string> parms = new Dictionary<string, string>
            {
                { puzzleCategoryMateIn1, puzzleCategoryMateIn1Id },
                { puzzleCategoryMateIn2, puzzleCategoryMateIn2Id },
                { someSearchTextKey, someSearchTextValue },
            };

            var input = new SearchInputModel();
            var categoryIds = new List<int>();

            foreach (var id in parms.Values.Where(x => !x.Equals("Реми")))
            {
                categoryIds.Add(int.Parse(id));
            }

            var categoriesService = this.InitializeService();
            categoriesService.InitializeSearchedParameters(input, parms);

            Assert.Equal(categoryIds, input.Categories);
            Assert.Equal(someSearchTextValue, input.SearchText);
        }

        [Fact]
        public void InitializeSearchedParameteresShouldAssignSearchTextToInputWhenUserSearchesByText()
        {
            IDictionary<string, string> parms = new Dictionary<string, string>
            {
                { "SearchText", "Реми" },
            };

            var input = new SearchInputModel();
            var categoriesService = this.InitializeService();

            categoriesService.InitializeSearchedParameters(input, parms);
            Assert.Equal(parms["SearchText"], input.SearchText);
        }

        [Fact]
        public void InitializeSearchedParameteresShouldNotAssignCategoriesWhenUserSearchesByTextOnly()
        {
            IDictionary<string, string> parms = new Dictionary<string, string>
            {
                { "SearchText", "Реми" },
            };

            var input = new SearchInputModel();
            var categoriesService = this.InitializeService();

            categoriesService.InitializeSearchedParameters(input, parms);
            Assert.Empty(input.Categories);
        }

        [Fact]
        public async Task GetCategoriesByIdShouldReturnEmptyCollectionWhenGivenNoIds()
        {
            var categoriesService = this.InitializeService();

            var model = new PuzzleCategoryViewModel();
            IEnumerable<int> givenIds = new int[0];

            var viewModel = new SearchViewModel()
            {
                Categories = await categoriesService.GetCategoriesByIdsAsync<PuzzleCategoryViewModel>(givenIds, nameof(Puzzle)),
            };

            Assert.Empty(viewModel.Categories);
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
