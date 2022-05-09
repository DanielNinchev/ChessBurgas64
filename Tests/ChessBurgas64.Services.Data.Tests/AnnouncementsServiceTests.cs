namespace ChessBurgas64.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using ChessBurgas64.Common;
    using ChessBurgas64.Data.Common.Repositories;
    using ChessBurgas64.Data.Models;
    using ChessBurgas64.Web.ViewModels.Announcements;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class AnnouncementsServiceTests
    {
        private readonly IMapper mapper;

        [Fact]
        public async Task ShouldThrowExceptionWhenImageWithInvalidExtensionIsAddedToTheAnnouncement()
        {
            var announcementsList = new List<Announcement>();
            var imagesList = new List<Image>();
            var notableMembersList = new List<NotableMember>();
            var puzzlesList = new List<Puzzle>();
            var announcementsMockRepo = new Mock<IDeletableEntityRepository<Announcement>>();
            var imagesMockRepo = new Mock<IDeletableEntityRepository<Image>>();
            var notableMembersMockRepo = new Mock<IDeletableEntityRepository<NotableMember>>();
            var puzzlesMockRepo = new Mock<IDeletableEntityRepository<Puzzle>>();
            var mockInvalidMainImage = new Mock<IFormFile>();
            var mockAdditionalImages = new Mock<List<IFormFile>>();

            var content = "Hello World from a Fake File";
            var invalidImageFileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            mockInvalidMainImage.Setup(x => x.OpenReadStream()).Returns(ms);
            mockInvalidMainImage.Setup(x => x.FileName).Returns(invalidImageFileName);
            mockInvalidMainImage.Setup(x => x.Length).Returns(ms.Length);

            announcementsMockRepo.Setup(x => x.All()).Returns(announcementsList.AsQueryable());
            announcementsMockRepo.Setup(x => x.AddAsync(It.IsAny<Announcement>())).Callback(
                (Announcement announcement) => announcementsList.Add(announcement));

            imagesMockRepo.Setup(x => x.All()).Returns(imagesList.AsQueryable());
            imagesMockRepo.Setup(x => x.AddAsync(It.IsAny<Image>())).Callback(
                (Image image) => imagesList.Add(image));

            var imagesService = new ImagesService(notableMembersMockRepo.Object, imagesMockRepo.Object, puzzlesMockRepo.Object);
            var announcementsService = new AnnouncementsService(announcementsMockRepo.Object, imagesMockRepo.Object, imagesService, this.mapper);

            var input = new AnnouncementInputModel()
            {
                Title = "Random title",
                Text = "Random text",
                Description = "Random description",
                Date = DateTime.Now,
                MainImage = mockInvalidMainImage.Object,
                AdditionalImages = mockAdditionalImages.Object.ToArray(),
            };

            var expectedImagesCount = mockAdditionalImages.Object.Count + 1;
            var mockAnnouncement = new Mock<Announcement>();

            await Assert.ThrowsAsync<InvalidDataException>(async () => await announcementsService.InitializeAnnouncementImages(input, mockAnnouncement.Object, GlobalConstants.AnnouncementImagesPath));
        }

        [Fact]
        public async Task ShouldAddInputAnnouncementImagesToDatabase()
        {
            var announcementsList = new List<Announcement>();
            var imagesList = new List<Image>();
            var notableMembersList = new List<NotableMember>();
            var puzzlesList = new List<Puzzle>();
            var announcementsMockRepo = new Mock<IDeletableEntityRepository<Announcement>>();
            var imagesMockRepo = new Mock<IDeletableEntityRepository<Image>>();
            var notableMembersMockRepo = new Mock<IDeletableEntityRepository<NotableMember>>();
            var puzzlesMockRepo = new Mock<IDeletableEntityRepository<Puzzle>>();
            var mockValidMainImage = new Mock<IFormFile>();
            var mockAdditionalImages = new Mock<List<IFormFile>>();

            var content = "Hello World from a Fake File";
            var validImageFileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            mockValidMainImage.Setup(x => x.OpenReadStream()).Returns(ms);
            mockValidMainImage.Setup(x => x.FileName).Returns(validImageFileName);
            mockValidMainImage.Setup(x => x.Length).Returns(ms.Length);

            announcementsMockRepo.Setup(x => x.All()).Returns(announcementsList.AsQueryable());
            announcementsMockRepo.Setup(x => x.AddAsync(It.IsAny<Announcement>())).Callback(
                (Announcement announcement) => announcementsList.Add(announcement));

            imagesMockRepo.Setup(x => x.All()).Returns(imagesList.AsQueryable());
            imagesMockRepo.Setup(x => x.AddAsync(It.IsAny<Image>())).Callback(
                (Image image) => imagesList.Add(image));

            var imagesService = new ImagesService(notableMembersMockRepo.Object, imagesMockRepo.Object, puzzlesMockRepo.Object);
            var announcementsService = new AnnouncementsService(announcementsMockRepo.Object, imagesMockRepo.Object, imagesService, this.mapper);

            mockAdditionalImages.Object.Add(mockValidMainImage.Object);
            mockAdditionalImages.Object.Add(mockValidMainImage.Object);
            mockAdditionalImages.Object.Add(mockValidMainImage.Object);
            mockAdditionalImages.Object.Add(mockValidMainImage.Object);

            var input = new AnnouncementInputModel()
            {
                Title = "Random title",
                Text = "Random text",
                Description = "Random description",
                Date = DateTime.Now,
                MainImage = mockValidMainImage.Object,
                AdditionalImages = mockAdditionalImages.Object.ToArray(),
            };

            var expectedImagesCount = mockAdditionalImages.Object.Count + 1;
            var mockAnnouncement = new Mock<Announcement>();
            mockAnnouncement.Setup(x => x.Images).Returns(imagesList.ToHashSet());

            await announcementsService.InitializeAnnouncementImages(input, mockAnnouncement.Object, GlobalConstants.AnnouncementImagesPath);

            Assert.Equal(expectedImagesCount, imagesList.Count);
        }
    }
}
