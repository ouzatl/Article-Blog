using Article.Blog.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Article.Blog.Data
{
    public class ArticleBlogContext : DbContext
    {
        public ArticleBlogContext(DbContextOptions<ArticleBlogContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Article.Blog.Data.Models.Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}