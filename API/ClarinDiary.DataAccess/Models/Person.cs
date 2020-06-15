using System;
using System.Collections.Generic;

namespace ClarinDiary.DataAccess.Models
{
    public partial class Person
    {
        public Person()
        {
            PostComment = new HashSet<PostComment>();
            PostIdAuthorNavigation = new HashSet<Post>();
            PostIdPublisherNavigation = new HashSet<Post>();
        }

        public Guid Id { get; set; }
        public string Identification { get; set; }
        public string FullName { get; set; }
        public Guid? IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<PostComment> PostComment { get; set; }
        public virtual ICollection<Post> PostIdAuthorNavigation { get; set; }
        public virtual ICollection<Post> PostIdPublisherNavigation { get; set; }
    }
}
