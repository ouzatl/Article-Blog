using Article.Blog.Common.Templates.ArticleTemplates;
using AutoMapper;

namespace Article.Blog.Common.Mapper.ArticleMap
{
    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            CreateMap<Article.Blog.Data.Models.Article, ArticleTemplate>().ReverseMap();
        }
    }
}