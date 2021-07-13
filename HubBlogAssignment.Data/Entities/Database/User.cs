using System;
using System.Collections.Generic;

namespace HubBlogAssignment.Data.Entities.Database
{
    public class User: BaseEntity
    {
        public User()
        {
            Comments = new HashSet<CommentDb>();
            Posts = new HashSet<PostDb>();
        }

        public string DisplayName { get; set; }
        public Guid AADObjectId { get; set; }
        public ICollection<CommentDb> Comments { get; set; }
        public ICollection<PostDb> Posts { get; set; }
    }
}