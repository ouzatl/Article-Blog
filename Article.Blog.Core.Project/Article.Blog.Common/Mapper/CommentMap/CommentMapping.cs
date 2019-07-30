using Article.Blog.Common.Templates.CommentTemplates;
using Article.Blog.Data.Models;
using AutoMapper;

namespace Article.Blog.Common.Mapper.CommentMap
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {
            CreateMap<Comment, CommentTemplate>().ReverseMap();
        }
    }
}