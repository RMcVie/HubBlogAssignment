using HubBlogAssignemnt.Shared;
using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignemnt.Data
{
    public class MockDataAccess : IDataAccess
    {
        private List<PostDb> posts;

        public MockDataAccess()
        {
            GenerateSampleData();
        }

        public Task CreateCategory(string category)
        {
            throw new NotImplementedException();
        }

        public async Task CreateComment(int postId, CommentDb comment)
        {
            comment.CreatedDateTimeUtc = DateTime.UtcNow;
            comment.CreatedUser = "Temp User";
            var post = posts.Single(p => p.PostId == postId);
            comment.CommentId = post.Comments.Max(c => c.CommentId) + 1;
            post.Comments.Add(comment);
            await Task.CompletedTask;
        }

        public async Task CreatePost(PostDb post)
        {
            post.CreatedDateTimeUtc = DateTime.UtcNow;
            post.PostId = posts.Max(p => p.PostId) + 1;
            posts.Add(post);
            await Task.CompletedTask;
        }

        public Task DeleteComment(CommentDb comment)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(PostDb post)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentDb>> GetComments(int postId, OrderBy orderBy)
        {
            return orderBy switch
            {
                OrderBy.New => await Task.FromResult(posts.Single(p => p.PostId == postId).Comments.OrderByDescending(c => c.CreatedDateTimeUtc)),
                OrderBy.Top => await Task.FromResult(posts.Single(p => p.PostId == postId).Comments.OrderByDescending(c => c.Score)),
                _ => throw new InvalidEnumArgumentException($"{orderBy} is not a valid OderBy enum value"),
            };
        }

        public async Task<PostDb> GetPost(int id)
        {
            return await Task.FromResult(posts.SingleOrDefault(p => p.PostId == id));
        }

        public async Task<IEnumerable<PostDb>> GetPosts()
        {
            return await Task.FromResult(posts);
        }

        private void GenerateSampleData()
        {
            posts = FakeDataGenerator.FakePosts().Generate(15);
            posts.ForEach(p => p.Comments = FakeDataGenerator.FakeComments(p.PostId, p.CreatedDateTimeUtc).Generate(10));
        }
    }
}