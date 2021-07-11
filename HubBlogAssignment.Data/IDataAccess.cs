using HubBlogAssignment.Data.Entities;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> CreateCategory(Category category);
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task<Post> CreatePost(Post post, Guid userObjectId);
        Task DeletePost(Post post);
        Task<Comment> CreateComment(int postId, Comment comment, Guid userObjectId);
        Task DeleteComment(Comment comment);
        Task<IEnumerable<Comment>> GetComments(int postId, OrderBy orderBy);
    }
}
