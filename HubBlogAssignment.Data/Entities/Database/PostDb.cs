using HubBlogAssignment.Data.Entities;
using System;
using System.Collections.Generic;

namespace HubBlogAssignment.Shared
{
    public class PostDb: BaseEntity
    {
        public PostDb()
        {
            Categories = new HashSet<Category>();
            Comments = new HashSet<CommentDb>();
            Votes = new HashSet<Vote>();
        }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public ICollection<Vote> Votes { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<CommentDb> Comments { get; set; }
    }
}
