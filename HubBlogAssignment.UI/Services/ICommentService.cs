using HubBlogAssignment.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using HubBlogAssignment.Shared.DML;
using HubBlogAssignment.Shared.Read;

namespace HubBlogAssignment.UI.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentReadDto>> GetComments(int postId, OrderBy orderBy);
        Task CreateComment(int postId, CommentDmlDto comment);
    }
}
