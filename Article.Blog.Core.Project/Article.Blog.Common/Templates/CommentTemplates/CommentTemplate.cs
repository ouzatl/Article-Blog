﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Article.Blog.Common.Templates.CommentTemplates
{
    public class CommentTemplate
    {
        [Key]
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}