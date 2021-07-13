using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;

namespace HubBlogAssignment.Data.Interfaces
{
    public interface ICommentAccess
    {
        Task<IEnumerable<Comment>> GetComments(int postId, OrderBy orderBy);
        Task CreateComment(int postId, Comment comment, Guid userObjectId);
    }
}
