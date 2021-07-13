using System.Collections.Generic;

namespace HubBlogAssignment.Data.Entities.Database
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
