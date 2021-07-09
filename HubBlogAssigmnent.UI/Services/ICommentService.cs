using HubBlogAssignemnt.Shared;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HubBlogAssigmnent.UI.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentReadDto>> GetComments(int postId, OrderBy orderBy);
        Task CreateComment(int postId, CommentDmlDto comment);
    }
}
