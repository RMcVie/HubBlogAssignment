using HubBlogAssignemnt.Shared;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubBlogAssignemnt.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<string>> GetCategories();
        Task CreateCategory(string category);
        Task<IEnumerable<PostDb>> GetPosts();
        Task<PostDb> GetPost(int id);
        Task CreatePost(PostDb post);
        Task DeletePost(PostDb post);
        Task CreateComment(int postId, CommentDb comment);
        Task DeleteComment(CommentDb comment);
        Task<IEnumerable<CommentDb>> GetComments(int postId, OrderBy orderBy);
    }
}
