namespace TheGreatGrape.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheGreatGrape.Services.Data;
    using TheGreatGrape.Web.ViewModels.Votes;

    [ApiController]
    [Route("/api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteViewModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.WineId, userId, input.Value);
            var averageVotes = this.votesService.GetAverageVote(input.WineId);
            return new PostVoteViewModel
            {
                AverageVote = averageVotes,
            };
        }
    }
}
