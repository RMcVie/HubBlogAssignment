using HubBlogAssignment.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Data.Entities
{
    public class Category: BaseEntity
    {
        public Category()
        {
            Posts = new List<PostDb>();
        }

        public string Name { get; set; }
        public ICollection<PostDb> Posts { get; set; }
    }
}
