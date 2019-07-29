using Article.Blog.Data;

namespace Article.Blog.Repository.Repositories.ArticleRepository
{
    public class ArticleRepository : BaseRepository<Article.Blog.Data.Models.Article>, IArticleRepository
    {
        public ArticleRepository(ArticleBlogContext dbContext) : base(dbContext)
        {
        }
    }
}