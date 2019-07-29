using Article.Blog.Data;
using Article.Blog.Data.Models;

namespace Article.Blog.Repository.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ArticleBlogContext dbContext) : base(dbContext)
        {
        }
    }
}