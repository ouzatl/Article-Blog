using Article.Blog.Data;
using Article.Blog.Data.Models;

namespace Article.Blog.Repository.Repositories.CommentRepository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ArticleBlogContext dbContext) : base(dbContext)
        {
        }
    }
}