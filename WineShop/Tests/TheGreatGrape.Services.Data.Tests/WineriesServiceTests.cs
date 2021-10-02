using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.WineShop;
using Xunit;

namespace TheGreatGrape.Services.Data.Tests
{
    public class WineriesServiceTests
    {
        [Fact]
        public void GetCountResultShouldBeCorrect()
        {
            var list = new List<Winery>();
            var mockRepo = new Mock<IDeletableEntityRepository<Winery>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Winery>())).Callback((Winery winery) => list.Add(winery));

            var service = new WineriesService(mockRepo.Object);

            list.Add(new Winery { Id = 1 });
            list.Add(new Winery { Id = 2 });

            Assert.Equal(2, service.GetCount());
        }

        [Fact]
        public void GetAllAsKeyValuePairsShouldReturnCorrectCountAsKeyValuePairs()
        {
            var list = new List<Winery>();
            var mockRepo = new Mock<IDeletableEntityRepository<Winery>>();

            mockRepo.Setup(x => x.AllAsNoTracking()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Winery>())).Callback((Winery winery) => list.Add(winery));

            var service = new WineriesService(mockRepo.Object);

            list.Add(new Winery { Id = 1, Name = "1" });
            list.Add(new Winery { Id = 2, Name = "2" });

            Assert.Equal(2, service.GetAllAsKeyValuePairs().Count());
            Assert.IsAssignableFrom<IEnumerable<KeyValuePair<string, string>>>(service.GetAllAsKeyValuePairs());
        }
    }
}
