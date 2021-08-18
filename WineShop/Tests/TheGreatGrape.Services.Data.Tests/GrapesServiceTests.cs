using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.WineShop;
using TheGreatGrape.Web.ViewModels.Grapes;
using Xunit;

namespace TheGreatGrape.Services.Data.Tests
{
    public class GrapesServiceTests
    {
        [Fact]
        public async Task GetAllShouldReturnCorrectCount()
        {
            var list = new List<Grape>();
            var mockGrapeRepo = new Mock<IDeletableEntityRepository<Grape>>();
            var mockWineRepo = new Mock<IDeletableEntityRepository<Wine>>();
            var winesService = new WinesService(mockWineRepo.Object);

            mockGrapeRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockGrapeRepo.Setup(x => x.AddAsync(It.IsAny<Grape>())).Callback((Grape grape) => list.Add(grape));

            var grapesService = new GrapesService(mockGrapeRepo.Object, winesService, mockWineRepo.Object);

            list.Add(new Grape { Id = 1 });
            list.Add(new Grape { Id = 2 });

            var getAll = grapesService.GetAll();

            Assert.True(getAll.Grapes.Count == 2);
        }

        [Fact]
        public void GetAllShouldReturnCorrectType()
        {
            var list = new List<Grape>();
            var mockGrapeRepo = new Mock<IDeletableEntityRepository<Grape>>();
            var mockWineRepo = new Mock<IDeletableEntityRepository<Wine>>();
            var winesService = new WinesService(mockWineRepo.Object);

            mockGrapeRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockGrapeRepo.Setup(x => x.AddAsync(It.IsAny<Grape>())).Callback((Grape grape) => list.Add(grape));

            var grapesService = new GrapesService(mockGrapeRepo.Object, winesService, mockWineRepo.Object);

            list.Add(new Grape { Id = 1 });
            list.Add(new Grape { Id = 2 });

            var getAll = grapesService.GetAll();

            Assert.IsType<GetGrapesViewModel>(getAll);
        }

        [Fact]
        public void GetCountShouldReturnCorrectCount()
        {
            var list = new List<Grape>();
            var mockGrapeRepo = new Mock<IDeletableEntityRepository<Grape>>();
            var mockWineRepo = new Mock<IDeletableEntityRepository<Wine>>();
            var winesService = new WinesService(mockWineRepo.Object);

            mockGrapeRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockGrapeRepo.Setup(x => x.AddAsync(It.IsAny<Grape>())).Callback((Grape grape) => list.Add(grape));

            var grapesService = new GrapesService(mockGrapeRepo.Object, winesService, mockWineRepo.Object);

            list.Add(new Grape { Id = 1 });
            list.Add(new Grape { Id = 2 });

            var getAll = grapesService.GetCount();

            Assert.Equal(2, getAll);
        }

        [Fact]
        public void GetWinesCountShouldReturnCorrectCount()
        {
            var grapesList = new List<Grape>();
            var winesList = new List<Wine>();
            var wineGrapesList = new List<WineGrape>();
            var mockGrapeRepo = new Mock<IDeletableEntityRepository<Grape>>();
            var mockWineRepo = new Mock<IDeletableEntityRepository<Wine>>();
            var winesService = new WinesService(mockWineRepo.Object);

            mockWineRepo.Setup(x => x.AllAsNoTracking()).Returns(winesList.AsQueryable());
            mockGrapeRepo.Setup(x => x.AllAsNoTracking()).Returns(grapesList.AsQueryable());
            mockGrapeRepo.Setup(x => x.AddAsync(It.IsAny<Grape>())).Callback((Grape grape) => grapesList.Add(grape));

            var grapesService = new GrapesService(mockGrapeRepo.Object, winesService, mockWineRepo.Object);

            wineGrapesList.Add(new WineGrape { GrapeId = 1, WineId = 1 });
            wineGrapesList.Add(new WineGrape { GrapeId = 1, WineId = 2 });

            winesList.Add(new Wine { Id = 2, IsApproved = true, Grapes = wineGrapesList });
            winesList.Add(new Wine { Id = 1, IsApproved = true, Grapes = wineGrapesList });

            var getAll = grapesService.GetWinesCount(1);

            Assert.Equal(2, getAll);
        }

        [Fact]
        public void GetWinesCountShouldIgnoreWineIfItIsNotApproved()
        {
            var grapesList = new List<Grape>();
            var winesList = new List<Wine>();
            var wineGrapesList = new List<WineGrape>();
            var mockGrapeRepo = new Mock<IDeletableEntityRepository<Grape>>();
            var mockWineRepo = new Mock<IDeletableEntityRepository<Wine>>();
            var winesService = new WinesService(mockWineRepo.Object);

            mockWineRepo.Setup(x => x.AllAsNoTracking()).Returns(winesList.AsQueryable());
            mockGrapeRepo.Setup(x => x.AllAsNoTracking()).Returns(grapesList.AsQueryable());
            mockGrapeRepo.Setup(x => x.AddAsync(It.IsAny<Grape>())).Callback((Grape grape) => grapesList.Add(grape));

            var grapesService = new GrapesService(mockGrapeRepo.Object, winesService, mockWineRepo.Object);

            wineGrapesList.Add(new WineGrape { GrapeId = 1, WineId = 1 });
            wineGrapesList.Add(new WineGrape { GrapeId = 1, WineId = 2 });

            winesList.Add(new Wine { Id = 2, IsApproved = true, Grapes = wineGrapesList });
            winesList.Add(new Wine { Id = 1, IsApproved = true, Grapes = wineGrapesList });
            winesList.Add(new Wine { Id = 3, IsApproved = false, Grapes = wineGrapesList });

            var getAll = grapesService.GetWinesCount(1);

            Assert.Equal(2, getAll);
        }
    }
}
