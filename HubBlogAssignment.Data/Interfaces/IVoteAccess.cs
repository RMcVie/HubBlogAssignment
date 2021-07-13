using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities.Database;

namespace HubBlogAssignment.Data.Interfaces
{
    public interface IVoteAccess
    {
        Task CreateVoteForPost(int postId, Guid userObjectId);
        Task DeleteVoteForPost(int postId, Guid userObjectId);
        Task CreateVoteForComment(int commentId, Guid userObjectId);
        Task DeleteVoteForComment(int commentId, Guid userObjectId);
    }
}
