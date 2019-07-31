using System.Collections.Generic;

namespace Article.Blog.Repository.Repositories.ArticleRepository
{
    public interface IArticleRepository : IBaseRepository<Article.Blog.Data.Models.Article>
    {
        List<Article.Blog.Data.Models.Article> Search(string searchKey);
    }
}