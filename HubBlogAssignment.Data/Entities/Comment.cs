using HubBlogAssignment.Data.Entities;
using System;

namespace HubBlogAssignment.Shared
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public int Score { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
    }
}
