using Article.Blog.Data.Models;
using System.Collections.Generic;

namespace Article.Blog.Repository.Repositories.CommentRepository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        List<Comment> GetCommentByArticleId(int id);
    }
}