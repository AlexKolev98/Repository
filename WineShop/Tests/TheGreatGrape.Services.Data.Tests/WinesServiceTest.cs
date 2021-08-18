using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatGrape.Data;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Services.Data.Tests.Models;
using TheGreatGrape.Web.ViewModels.Wines;
using Xunit;

namespace TheGreatGrape.Services.Data.Tests
{
    public class WinesServiceTest
    {
        [Fact]
        public void GetCountResultShouldBeCorrect()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));

            var service = new WinesService(mockRepo.Object);

            list.Add(new Wine { Id = 1 });
            list.Add(new Wine { Id = 2 });

            Assert.Equal(2, service.GetCount());
        }

        [Fact]
        public void GetCountResultShouldBeIncorrect()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));

            var service = new WinesService(mockRepo.Object);

            Assert.False(service.GetCount() == 1);
        }

        [Fact]
        public async Task ApproveOrRemoveShouldApproveWine()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));

            var service = new WinesService(mockRepo.Object);

            list.Add(new Wine { Id = 1, IsApproved = false });

            await service.ApproveOrRemove(1, "true");

            Assert.True(mockRepo.Object.AllAsNoTracking().FirstOrDefault().IsApproved = true);
        }

        [Fact]
        public async Task ApproveOrRemoveShouldDeleteWine()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));

            var service = new WinesService(mockRepo.Object);

            list.Add(new Wine { Id = 1, IsApproved = false, IsDeleted = false, DeletedOn = null });

            var wine = service.ApproveOrRemove(1, "false");

            Assert.True(mockRepo.Object.AllAsNoTracking().FirstOrDefault().IsDeleted);
        }

        [Fact]
        public async Task GetApprovedOnlyShouldReturnApprovedOnly()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));
            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());

            var service = new WinesService(mockRepo.Object);

            var mockMapper = new Mock<IMapper>();

            var wine1 = new Wine { Id = 1, IsApproved = false };
            var wine2 = new Wine { Id = 2, IsApproved = true };

            await mockRepo.Object.AddAsync(wine1);
            await mockRepo.Object.SaveChangesAsync();
            await mockRepo.Object.AddAsync(wine2);
            await mockRepo.Object.SaveChangesAsync();

            var count2 = service.GetAll<WineTestModel>(1, 12);

            Assert.Equal(2, count2.Count());
        }
    }
}
