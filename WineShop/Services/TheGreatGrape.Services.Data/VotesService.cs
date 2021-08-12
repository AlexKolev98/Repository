using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGreatGrape.Data.Common.Repositories;
using TheGreatGrape.Data.Models.TheGreatGrape.Models;

namespace TheGreatGrape.Services.Data
{
    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVote(int wineId)
        {
            var averageVotes = this.votesRepository.All().Where(x => x.WineId == wineId)
                .Average(x => x.Value);

            return averageVotes;
        }

        public async Task SetVoteAsync(int wineId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.WineId == wineId && x.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    WineId = wineId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}
