using HubBlogAssignment.Data.Entities;
using System;
using System.Collections.Generic;

namespace HubBlogAssignment.Shared
{
    public class Post: BaseEntity
    {
        public Post()
        {
            Categories = new List<Category>();
            Comments = new List<Comment>();
        }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
