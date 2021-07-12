﻿using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public User User { get; set; }
        public int VotesCount { get; set; }
    }
}
