namespace TheGreatGrape.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task SetVoteAsync(int wineId, string userId, byte value);

        double GetAverageVote(int wineId);
    }
}
