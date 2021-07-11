﻿using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.Entities
{
    public class User: BaseEntity
    {
        public User()
        {
            Comments = new List<Comment>();
            Posts = new List<Post>();
        }

        public string DisplayName { get; set; }
        public Guid AADObjectId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}