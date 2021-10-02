namespace TheGreatGrape.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using TheGreatGrape.Data.Common.Repositories;
    using TheGreatGrape.Data.Models.TheGreatGrape.Models;
    using Xunit;

    public class VotesServiceTests
    {
        [Fact]
        public async Task AverageVoteFrom3UsersShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "2", 5);
            await service.SetVoteAsync(1, "3", 2);

            Assert.Equal(4, service.GetAverageVote(1));
        }

        [Fact]
        public async Task AverageVoteFrom3UsersAfterChangeInVoteShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "2", 5);
            await service.SetVoteAsync(1, "3", 2);
            await service.SetVoteAsync(1, "3", 5);
            await service.SetVoteAsync(1, "2", 1);
            await service.SetVoteAsync(1, "1", 3);

            Assert.Equal(3, service.GetAverageVote(1));
        }

        [Fact]
        public async Task DefaultVoteShouldBeZero()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 0);

            Assert.Equal(0, service.GetAverageVote(1));
        }

        [Fact]
        public async Task VotesCountShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "2", 5);
            await service.SetVoteAsync(1, "3", 2);

            Assert.Equal(3, mockRepo.Object.All().Count());
        }

        [Fact]
        public async Task VotesCountAfterChangeInVoteShouldBeCorrect()
        {
            var list = new List<Vote>();
            var mockRepo = new Mock<IRepository<Vote>>();

            mockRepo.Setup(x => x.All()).Returns(list.AsQueryable());
            mockRepo.Setup(x => x.AddAsync(It.IsAny<Vote>())).Callback((Vote vote) => list.Add(vote));

            var service = new VotesService(mockRepo.Object);

            await service.SetVoteAsync(1, "1", 5);
            await service.SetVoteAsync(1, "2", 5);
            await service.SetVoteAsync(1, "3", 2);
            await service.SetVoteAsync(1, "1", 2);
            await service.SetVoteAsync(1, "2", 2);
            await service.SetVoteAsync(1, "3", 5);

            Assert.Equal(3, mockRepo.Object.All().Count());
        }
    }
}
