using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.Entities
{
    public class Vote : BaseEntity
    {
        public User User { get; set; }
        public PostDb Post { get; set; }
        public CommentDb Comment { get; set; }
    }
}
