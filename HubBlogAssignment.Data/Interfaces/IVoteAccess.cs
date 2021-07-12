using HubBlogAssignment.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.DataAccess
{
    public interface IVoteAccess
    {
        Task CreateVoteForPost(int postId, Guid userObjectId);
        Task DeleteVoteForPost(int postId, Guid userObjectId);
        Task<IEnumerable<Vote>> GetVotesForPost(int postId);
        Task CreateVoteForComment(int commentId, Guid userObjectId);
        Task DeleteVoteForComment(int commentId, Guid userObjectId);
        Task<IEnumerable<Vote>> GetVotesForComment(int commentId);
    }
}
