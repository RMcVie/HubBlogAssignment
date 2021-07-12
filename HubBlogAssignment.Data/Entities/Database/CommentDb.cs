using HubBlogAssignment.Data.Entities;
using System;
using System.Collections.Generic;

namespace HubBlogAssignment.Shared
{
    public class CommentDb : BaseEntity
    {
        public string Content { get; set; }
        public User User { get; set; }
        public PostDb Post { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
