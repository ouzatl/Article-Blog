using Article.Blog.Common.NLog;
using Article.Blog.Data;
using Article.Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Article.Blog.Repository.Repositories.ArticleRepository
{
    public class ArticleRepository : BaseRepository<Article.Blog.Data.Models.Article>, IArticleRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILog _logger;

        public ArticleRepository(ArticleBlogContext dbContext, ILog logger) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Data.Models.Article> Search(string searchKey)
        {
            try
            {
            }
            catch (System.Exception ex)
            {

                _logger.Error($"GetCommentByArticleId : {ex}");
            }

            return null;
        }
    }
}