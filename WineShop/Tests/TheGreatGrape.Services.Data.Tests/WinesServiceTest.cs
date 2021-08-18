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
        public void GetCountResultShouldNotReturnNotApproved()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));

            var service = new WinesService(mockRepo.Object);

            list.Add(new Wine { Id = 1 });
            list.Add(new Wine { Id = 2 });

            Assert.Equal(0, service.GetCount());
        }

        [Fact]
        public void GetCountResultShouldReturnApprovedCount()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));

            var service = new WinesService(mockRepo.Object);

            list.Add(new Wine { Id = 1, IsApproved = true });
            list.Add(new Wine { Id = 2, IsApproved = true });

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

            Assert.True(mockRepo.Object.AllAsNoTracking().FirstOrDefault(x => x.Id == 1).IsApproved = true);
        }

        [Fact]
        public async Task ApproveOrRemoveShouldDeleteWine()
        {
            var list = new List<Wine>();
            var mockRepo = new Mock<IDeletableEntityRepository<Wine>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Wine>())).Callback((Wine wine) => list.Add(wine));

            var service = new WinesService(mockRepo.Object);

            list.Add(new Wine { Id = 1, IsApproved = false, IsDeleted = false, CreatedOn = DateTime.UtcNow,  });

            await service.ApproveOrRemove(1, "false");

            Assert.True(mockRepo.Object.AllAsNoTracking().FirstOrDefault().IsDeleted = true);
        }
    }
}
