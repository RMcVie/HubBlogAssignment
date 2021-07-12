using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public interface ICommentAccess
    {
        Task<IEnumerable<Comment>> GetComments(int postId, OrderBy orderBy);
        Task CreateComment(int postId, Comment comment, Guid userObjectId);
    }
}
