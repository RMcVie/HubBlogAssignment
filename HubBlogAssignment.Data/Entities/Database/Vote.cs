namespace HubBlogAssignment.Data.Entities.Database
{
    public class Vote : BaseEntity
    {
        public User User { get; set; }
        public PostDb Post { get; set; }
        public CommentDb Comment { get; set; }
    }
}
