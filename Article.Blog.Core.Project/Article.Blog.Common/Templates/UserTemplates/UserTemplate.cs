using System;

namespace Article.Blog.Common.Templates.UserTemplates
{
    public class UserTemplate
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime ModifiedOn { get; set; }
    }
}