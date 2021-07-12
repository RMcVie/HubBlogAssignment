using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubBlogAssignment.Shared
{
    public class CommentReadDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public string User { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
    }
}
