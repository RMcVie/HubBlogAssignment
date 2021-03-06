using System.Threading.Tasks;
using HubBlogAssignment.Api.ExtensionMethods;
using HubBlogAssignment.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace HubBlogAssignment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class VotesController : ControllerBase
    {
        private readonly IVoteAccess dataAccess;

        public VotesController(IVoteAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpDelete("Posts/{postId:int}")]
        public async Task<IActionResult> DeletePostVote(int postId)
        {
            await dataAccess.DeleteVoteForPost(postId, HttpContext.User.GetAadObjectId());
            return NoContent();
        }

        [HttpDelete("Comments/{commentId:int}")]
        public async Task<IActionResult> DeleteCommentVote(int commentId)
        {
            await dataAccess.DeleteVoteForComment(commentId, HttpContext.User.GetAadObjectId());
            return NoContent();
        }

        [HttpPost("Posts/{postId:int}")]
        public async Task<IActionResult> CreatePostVote(int postId)
        {
            await dataAccess.CreateVoteForPost(postId, HttpContext.User.GetAadObjectId());
            return NoContent();
        }

        [HttpPost("Comments/{commentId:int}")]
        public async Task<IActionResult> CreateCommentVote(int commentId)
        {
            await dataAccess.CreateVoteForComment(commentId, HttpContext.User.GetAadObjectId());
            return NoContent();
        }
    }
}
