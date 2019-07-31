using System.Collections.Generic;
using System.Linq;
using Article.Blog.Common.NLog;
using Article.Blog.Data;
using Article.Blog.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Article.Blog.Repository.Repositories.CommentRepository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILog _logger;

        public CommentRepository(ArticleBlogContext dbContext, ILog logger) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Comment> GetCommentByArticleId(int articleId)
        {
            try
            {
                var commentListByArticleId = _dbContext.Set<Comment>().Where(x => x.ArticleId == articleId && x.IsActive == true).ToList();

                if (commentListByArticleId != null)
                    return commentListByArticleId;
            }
            catch (System.Exception ex)
            {
                _logger.Error($"GetCommentByArticleId : {ex}");
            }

            return null;
        }
    }
}