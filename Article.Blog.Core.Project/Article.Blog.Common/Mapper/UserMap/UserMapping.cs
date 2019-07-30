using Article.Blog.Common.Templates.UserTemplates;
using Article.Blog.Data.Models;
using AutoMapper;

namespace Article.Blog.Common.Mapper.UserMap
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserTemplate>().ReverseMap();
        }
    }
}