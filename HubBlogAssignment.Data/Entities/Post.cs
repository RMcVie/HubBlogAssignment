using System.Collections.Generic;
using HubBlogAssignment.Data.Entities.Database;

namespace HubBlogAssignment.Data.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Categories = new List<Category>();
        }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public int VotesCount { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
