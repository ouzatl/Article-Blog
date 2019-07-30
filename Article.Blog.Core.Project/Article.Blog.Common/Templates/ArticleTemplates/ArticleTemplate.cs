using System;
using System.ComponentModel.DataAnnotations;

namespace Article.Blog.Common.Templates.ArticleTemplates
{
    public class ArticleTemplate
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}